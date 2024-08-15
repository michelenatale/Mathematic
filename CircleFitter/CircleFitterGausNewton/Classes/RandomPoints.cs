 

namespace michele.natale.CyrcleFitters;


public class RandomPoints
{
  public const float DMAX = 0.12F;
  public const float DMIN = 0.05F;
  public static readonly Random Rand = new();

  /// <summary>
  /// Returns a list of random points
  /// </summary>
  /// <param name="min">desired min</param>
  /// <param name="max">desired max</param>
  /// <param name="size">desired size</param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  public static List<PointF> RngXYData(
    int size, int min, int max)
  {
    if (min < 0 || min >= max)
      throw new ArgumentException($"{min} / {max} ?");

    var d = max - min;
    List<PointF> result = [];
    for (var i = 0; i < size; i++)
    {
      result.Add(new PointF(
          Convert.ToSingle(min + d * Rand.NextDouble()),
          Convert.ToSingle(min + d * Rand.NextDouble())));
    }
    return result;
  }

  /// <summary>
  /// returns a list of random points, where the 
  /// points approximate a random circle.
  /// </summary>
  /// <param name="size">desired size</param>
  /// <param name="radius">desired radius</param>
  /// <param name="client_size">desired ClientSize</param>
  /// <returns></returns>
  public static List<PointF> RngCirclePoints(
    int size, float radius, Size client_size)
  {
    double carc;
    var angleoffset = 360.0f / size;
    var result = new List<PointF>(size);
    for (int i = 0; i < size; i++)
    {
      carc = i * angleoffset * Math.PI / 180.0f;
      result.Add(new PointF(
          Convert.ToSingle(radius * Math.Sin(carc)),
          Convert.ToSingle(radius * Math.Cos(carc))));
    }

    var cnt = Rand.Next(1, result.Count);
    var scale = Enumerable.Range(0, result.Count)
                .OrderBy(x => Rand.Next()).Take(cnt).ToList();

    for (int i = 0; i < cnt; i++)
    {
      var point = result[scale[i]];
      result[scale[i]] = new PointF(
          Convert.ToSingle(point.X + Distortion(DMIN, DMAX) * point.X),
          Convert.ToSingle(point.Y + Distortion(DMIN, DMAX) * point.Y));
    }

    var cmax = Math.Min(client_size.Width, client_size.Height);
    var center = RngCenter(radius, (cmax - radius) / 2.0f);

    return result.Offset(center);
  }

  /// <summary>
  /// Returns a random point.
  /// </summary>
  /// <param name="min">desired min</param>
  /// <param name="max">desired max</param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  public static PointF RngCenter(float min, float max)
  {
    if (min < 0 || min >= max)
      throw new ArgumentException($"{min} / {max} ?");

    var d = max - min;
    var x = Convert.ToSingle(min + Rand.NextDouble() * d);
    var y = Convert.ToSingle(min + Rand.NextDouble() * d);
    return new PointF(x, y);
  }

  /// <summary>
  /// Returns a random value between a min and max.
  /// </summary>
  /// <param name="min">desired min</param>
  /// <param name="max">desired max</param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  public static float Distortion(float min, float max)
  {
    if (min < 0 || min >= max)
      throw new ArgumentException($"{min} / {max} ?");

    var d = max - min;
    var sign = (Rand.Next() & 1) == 0 ? 1 : -1;
    return Convert.ToSingle(min + Rand.NextDouble() * d) * sign;
  }
}

