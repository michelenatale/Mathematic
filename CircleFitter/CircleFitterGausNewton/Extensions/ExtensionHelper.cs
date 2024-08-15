

using System.Numerics;

namespace michele.natale.CyrcleFitters;


public static class ExtensionHelper
{

  #region Matrix Jagged Single

  public static float[][] Copy(this float[][] m)
  {
    var result = new float[m.Length][];
    for (var i = 0; i < result.Length; i++)
      result = [.. m]; // Copy
    return result;
  }

  public static float[][] ToMTrans(this float[][] m)
  {
    // Transponieren
    var r = m.Length;
    var c = m[0].Length;

    float[] t;
    var result = new float[c][];
    for (var i = 0; i < c; i++)
    {
      t = new float[r];
      for (var j = 0; j < r; j++)
        t[j] = m[j][i];
      result[i] = t;
    }
    return result;
  }

  public static float[][] ToMTrans(this List<float[]> mlist)
  {
    return mlist.ToArray().ToMTrans();
  }

  public static float[][] ToMMult(this float[][] m1, float[][] m2)
  {
    var rm1 = m1.Length;
    var rm2 = m2.Length;
    var cm1 = m1[0].Length;
    var cm2 = m2[0].Length;

    if (cm1 != rm2)
      throw new ArgumentException($"MMULT: {nameof(m1)} / {nameof(m2)} ?");

    var result = NewJaggedMatrix(rm1, cm2, false);
    for (var i = 0; i < rm1; i++)
      for (var j = 0; j < cm2; j++)
        for (var k = 0; k < cm1; k++)
          result[i][j] += m1[i][k] * m2[k][j];
    return result;
  }

  public static float[] ToMMult(this float[][] m1, float[] m2)
  {
    var rm1 = m1.Length;
    var rm2 = m2.Length;
    var cm1 = m1[0].Length;
    if (cm1 != rm2)
      throw new ArgumentException($"MMULT: {nameof(m1)} / {nameof(m2)} ?");

    var result = new float[rm1];
    for (var i = 0; i < rm1; i++)
      for (var j = 0; j < cm1; j++)
        result[i] += m1[i][j] * m2[j];
    return result;
  }

  public static float[][] NewJaggedMatrix(this float[][] m1, bool _identity = true)
  {
    var cm1 = m1[0].Length;
    if (m1.Length != cm1)
      _identity = false;

    var result = new float[m1.Length + 1][];
    for (var i = 0; i < result.Length; i++)
    {
      result[i] = new float[cm1];
      if (_identity)
        result[i][i] = 1.0F;
    }
    return result;
  }

  public static Matrix4x4 ToMatrix4(this float[][] m1)
  {
    if (m1.Length < 3 && m1[0].Length < 3)
      throw new ArgumentOutOfRangeException($"ToMatrix4: {nameof(m1)}.Length ?");
    var result = Matrix4x4.Identity;
    {
      result.M11 = m1[0][0]; result.M12 = m1[0][1]; result.M13 = m1[0][2];
      result.M21 = m1[1][0]; result.M22 = m1[1][1]; result.M23 = m1[1][2];
      result.M31 = m1[2][0]; result.M32 = m1[2][1]; result.M33 = m1[2][2];
    }
    return result;
  }

  public static Matrix4x4 ToMatrix4(this float[] m1)
  {
    var result = Matrix4x4.Identity;
    result.M11 = m1[0]; result.M21 = m1[1]; result.M31 = m1[2];
    return result;
  }

  public static float[][] NewJaggedMatrix(int row, int col, bool _identity = true)
  {
    var result = new float[row][];
    if (row != col)
      _identity = false;

    for (var i = 0; i < result.Length; i++)
    {
      result[i] = new float[col];
      if (_identity)
        result[i][i] = 1.0F;
    }
    return result;
  }

  #endregion Matrix Jagged Single

  #region Matrix
  public static Matrix4x4 Copy(this Matrix4x4 m4)
  {
    var result = new Matrix4x4();
    { 
      result.M11 = m4.M11; result.M12 = m4.M12; result.M13 = m4.M13; result.M14 = m4.M14;
      result.M21 = m4.M21; result.M22 = m4.M22; result.M23 = m4.M23; result.M24 = m4.M24;
      result.M31 = m4.M31; result.M32 = m4.M32; result.M33 = m4.M33; result.M34 = m4.M34;
      result.M41 = m4.M41; result.M42 = m4.M42; result.M43 = m4.M43; result.M44 = m4.M44;
    }
    return result;
  }
  public static Matrix4x4 MNegation(this Matrix4x4 m4)
  {
    var result = new Matrix4x4();
    { 
      result.M11 = -m4.M11; result.M12 = -m4.M12; result.M13 = -m4.M13; result.M14 = -m4.M14;
      result.M21 = -m4.M21; result.M22 = -m4.M22; result.M23 = -m4.M23; result.M24 = -m4.M24;
      result.M31 = -m4.M31; result.M32 = -m4.M32; result.M33 = -m4.M33; result.M34 = -m4.M34;
      result.M41 = -m4.M41; result.M42 = -m4.M42; result.M43 = -m4.M43; result.M44 = -m4.M44;
    }
    return result;
  }
  #endregion Matrix

  #region Vector
  public static Vector3 ToPreXyr(this List<PointF> p2dfs)
  {
    // Provisorisches Zentrum und Startradius 0.1
    var xmin = p2dfs.Select(p => p.X).Min();
    var xmax = p2dfs.Select(p => p.X).Max();

    var ymin = p2dfs.Select(p => p.Y).Min();
    var ymax = p2dfs.Select(p => p.Y).Max();

    return new Vector3(Math.Abs(xmax + xmin) / 2f, Math.Abs(ymax + ymin) / 2f, 0.1F);
  }
  #endregion Vector

  #region Methodes
  public static float XX(this float value)
  {
    return value * value;
  }

  public static float SqrtF(this float value)
  {
    return Convert.ToSingle(Math.Sqrt(value));
  }
  #endregion Methodes

  #region Center & Offset

  public static List<PointF> Offset(this List<PointF> p2dfs, PointF _offset)
  {
    return p2dfs.Select(p2df => Offset(p2df, _offset)).ToList();
  }

  public static PointF Offset(this PointF p2df, PointF _offset)
  {
    return new PointF() { X = p2df.X + _offset.X, Y = p2df.Y + _offset.Y };
  }

  public static PointF ToPreCenter(this List<PointF> p2dfs)
  {
    var xmin = p2dfs.Select(p => p.X).Min();
    var xmax = p2dfs.Select(p => p.X).Max();

    var ymin = p2dfs.Select(p => p.Y).Min();
    var ymax = p2dfs.Select(p => p.Y).Max();
    return new PointF()
    {
      X = Math.Abs(xmax + xmin) / 2f,
      Y = Math.Abs(ymax + ymin) / 2f,
    };
  }

  #endregion Center & Offset

}
