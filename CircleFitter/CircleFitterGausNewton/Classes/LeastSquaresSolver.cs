

using System.Numerics;

namespace michele.natale.CyrcleFitters;

public class LeastSquaresSolver
{

  private bool CalcFinish = false;
  private readonly List<PointF> Points2D = null!;
  private readonly List<float[]> ResultList = [];

  private const int MAX_ITER = 15; // 1000 
  private const double D_MKQ2 = 1 / 1_000_000.0;

  /// <summary>
  /// C-Tor
  /// </summary>
  /// <param name="p2ds"></param>
  public LeastSquaresSolver(List<PointF> p2ds) =>
    this.Points2D = p2ds;

  /// <summary>
  /// Returns a vector.
  /// </summary>
  public Vector4 ResultXYRLSQ2
  {
    get
    {
      var last = this.ResultList.Last();
      return new Vector4(last[0], last[1], last[2], last[3]);
    }
  }

  /// <summary>
  /// Start the LeastSquaresSolver
  /// </summary>
  public void StartLSS()
  {
    // Define starting vector (provisional)
    var center = this.Points2D.ToPreCenter();
    // Write provisional center (for starting vector)
    // into the result list
    this.ResultList.Add([center.X, center.Y, 0.1f, 0.0f, 0.0f]);

    for (int i = 0; i <= MAX_ITER; i++)
    {
      var startvector = this.ToStartVector();
      var fpmkq2 = this.ToFyxr_Pr_MKQ2(startvector);
      var a = this.ToA(startvector, fpmkq2.Item2);
      var atxa = ToATxA(a);
      var invatxa = this.ToMInvATxA(atxa);
      var atxfyxr = ToATxFyxr(a, fpmkq2.Item1);
      var uvw = ToUVW(invatxa, atxfyxr);
      this.SetNewStartVector(fpmkq2.Item3, uvw);
      this.SetDeltaMKQ2();
      if (this.CalcFinish) break;
    }
  }

  private Vector3 ToStartVector()
  {
    // Assemble values for the starting vector
    // vector from the result list.
    var result = new Vector3();
    var last = this.ResultList.Last();
    result.X = last[0];
    result.Y = last[1];
    result.Z = last[2];
    return result;
  }

  private (List<float>, List<float>, List<float>) ToFyxr_Pr_MKQ2(Vector3 xyr)
  {
    // Calculate the values of the least squares
    // This requires the measurement list with the points (a,b)
    // and the provisional start vector (x,y,r)
    // The result is 3 parameters 
    // - sqrt((a-xi)^2 + (b-yi)^2)
    // - sqrt((a-xi)^2 + (b-yi)^2) - r >>> (residuals)
    // - (sqrt((a-xi)^2 + (b-yi)^2) - r)^2 
    // The sum of the last parameter 
    // Sum((sqrt((a-xi)^2 + (b-yi)^2) - r)^2)
    // results in the “sum of least squares”


    // Werte der kleinsten Quadrate ausrechnen
    // Dazu wird die Messliste mit den Punkten (a,b)
    // gebraucht, sowie den provisorischen Startvektor (x,y,r)
    // Das Ergebnis sind 3 Parameter 
    // - sqrt((a-xi)^2 + (b-yi)^2)
    // - sqrt((a-xi)^2 + (b-yi)^2) - r       >>> (Residuen)
    // - (sqrt((a-xi)^2 + (b-yi)^2) - r)^2 
    // Die Summe des letzten Parameter 
    // Sum((sqrt((a-xi)^2 + (b-yi)^2) - r)^2)
    // ergibt die "Summe der kleinsten Quadrate"
    float xq, yq, xysqrt;
    var fyxrpr = new List<float>();
    var fyxr = new List<float>();
    var mkq2 = new List<float>();

    for (var i = 0; i < this.Points2D.Count; i++)
    {
      xq = (xyr.X - this.Points2D[i].X).XX();
      yq = (xyr.Y - this.Points2D[i].Y).XX();
      xysqrt = (xq + yq).SqrtF();
      fyxrpr.Add(xysqrt); // Fyxr
      fyxr.Add(xysqrt - xyr.Z); // FyxrPr
      mkq2.Add(fyxr[^1].XX()); // MKQ2
    }
    return (fyxr, fyxrpr, mkq2);
  }

  private List<float[]> ToA(Vector3 xyr, List<float> fyxrpr)
  {
    // Using partial derivation to compile the
    // A-list (pre-matrix)

    // Mittels partiellen Ableitung die
    // A-Liste zusammenstellen (Vormatrix)
    float x, y, r;
    var result = new List<float[]>();

    for (int i = 0; i < fyxrpr.Count; i++)
    {
      x = (this.Points2D[i].X - xyr.X) / -fyxrpr[i];
      y = (this.Points2D[i].Y - xyr.Y) / -fyxrpr[i];
      r = -1.0f;
      result.Add([x, y, r]);
    }
    return result;
  }

  private static Matrix4x4 ToATxA(List<float[]> a)
  {
    //Transpose the A-list and multiply with the original 
    // multiply according to the rules of matrix multiplication
    // Result is a 4x4 matrix (ATxA)

    // A-Liste Transponieren und mit der Originalen 
    // multiplizieren nach den Regeln der Matrixmultiplikation
    // Ergebnis ist eine 4x4-Matrix (ATxA)
    var at = a.ToMTrans();
    var ata = at.ToMMult(a.ToArray());
    return ata.ToMatrix4();
  }

  private Matrix4x4 ToMInvATxA(Matrix4x4 atxa)
  {
    // Invert (inverse) and negate the ATxA matrix

    // Die ATxA-Matrix invertieren (Inverse) und negieren

    if (Matrix4x4.Invert(atxa, out var t))
      return Matrix4x4.Negate(t); 
    throw new Exception($"Exception: {nameof(ToMInvATxA)}.Invert");
  }

  private static Matrix4x4 ToATxFyxr(
    List<float[]> a, List<float> fyxr)
  {
    // Multiply transposed A list with parameter 2 (ToFyxrPr)

    // Transponierte A-Liste mit Parameter 2 (ToFyxrPr) multiplizieren
    var at = a.ToMTrans();
    var tmp = at.ToMMult(fyxr.ToArray());
    return tmp.ToMatrix4();
  }

  private static Matrix4x4 ToUVW(
    Matrix4x4 invatxa, Matrix4x4 atxfyxr)
  {
    // InvATxA multiplied by ATxFyxr then results in the
    // UVW matrix. This matrix contains all the values to
    // subsequently calculate the final result for the
    // new start value.

    // InvATxA multipliziert mit ATxFyxr ergibt dann die
    // UVW-Matrix. Diese Matrix beinhaltet alle Werte um
    // nachher das Schlussergebnis auszurechnen für den
    // neuen Startwert.
    return invatxa * atxfyxr;
  }

  private void SetNewStartVector(
    List<float> mkq2, Matrix4x4 uvw)
  {
    // The new start vector is immediately written 
    // to the result list

    // Der neue Startvektor wird gleich in die 
    // Resultliste geschrieben
    var last = this.ResultList[^1];

    // Sum of the least squares
    // Summe der kleinsten Quadrate
    last[3] = mkq2.Sum();

    // Column addition of the last start vector with the 
    // UVW matrix, results in the new start values

    // Spaltenaddition des letzten Startvektor
    // mit der UVW-Matrix, ergibt die  neuen Startwerte
    this.ResultList.Add(
    [
        uvw.M11 + last[0],
        uvw.M21 + last[1],
        uvw.M31 + last[2],
        0f, 0f
    ]);
  }

  private void SetDeltaMKQ2()
  {
    //Calculate the delta of the sum of the squared distances.
    //This value will hopefully always move towards 0, and can
    //therefore only be calculated when at least 2 values of
    //the sum of the squared distances are available.

    //This is used to decide whether the calculation should be aborted.

    //Das Delta der Summe der quadrierten Abstände ausrechnen.
    //Dieser Wert wird hoffentlich immer Richtung 0 wandern,
    //und kann daher erst ausgerechnet werden, wenn min. 2 Werte
    //der Summe der quadrierten Abstände vorliegen.

    //Daraus wird entschieden, ob die Kalkulation abgebrochen wird.

    if (this.ResultList.Count > 2)
    {
      int idx = this.ResultList.Count - 3;
      float s1 = this.ResultList[idx][3];
      float s2 = this.ResultList[idx + 1][3];
      this.ResultList[idx + 1][4] = s1 - s2;
      if (D_MKQ2 > Math.Abs(this.ResultList[idx + 1][4]))
      {
        this.CalcFinish = true;
        this.ResultList.RemoveAt(this.ResultList.Count - 1);
      }
    }
  }
}

