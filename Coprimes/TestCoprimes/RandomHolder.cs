
using System.Security.Cryptography;

namespace TestCoprimes;


public class RandomHolder
{
  public readonly static RandomNumberGenerator Rand =
    RandomNumberGenerator.Create();


  public static byte[] ToBytes(int size)
  {
    var bytes = new byte[size];
    ToBytes(bytes);
    return bytes;
  }

  public static byte[] ToBytes(int size, bool with_zeros = true)
  {
    var result = new byte[size];
    ToBytes(result, with_zeros);
    return result;
  }

  public static void ToBytes(Span<byte> bytes, bool with_zeros = true)
  {
    if (with_zeros)
    {
      Rand.GetBytes(bytes);
      return;
    }
    Rand.GetNonZeroBytes(bytes);
  }

  public static byte[] ToBytes(int size, byte max)
  {
    return ToBytes(size, 0, max);
  }

  public static byte[] ToBytes(int size, byte min, byte max)
  {
    var result = new byte[size];
    ToBytes(result, min, max);
    return result;
  }

  public static void ToBytes(Span<byte> bytes, byte max)
  {
    ToBytes(bytes, 0, max);
  }

  public static void ToBytes(Span<byte> bytes, byte min, byte max)
  {
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(min, max);

    var d = (byte)(max - min);
    var length = bytes.Length;
    var tmp = new byte[length];
    Rand.GetNonZeroBytes(tmp);
    for (int i = 0; i < length; i++)
    {
      var b = (byte)(tmp[i] % d);
      bytes[i] = (byte)(min + b);
    }
  }

  public static int NextInt32()
  {
    var bytes = new byte[4];
    Rand.GetNonZeroBytes(bytes);
    return Math.Abs(BitConverter.ToInt32(bytes, 0));
  }

  public static int NextInt32(int max)
  {
    return NextInt32(0, max);
  }

  public static int NextInt32(int min, int max)
  {
    if (min < 0) min = Math.Abs(min);
    if (max < 0) max = Math.Abs(max);
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(min, max);

    var d = max - min;
    var bytes = new byte[4];
    Rand.GetNonZeroBytes(bytes);
    var tmp = Math.Abs(BitConverter.ToInt32(bytes, 0));
    return min + tmp % d;
  }

  public static int[] ToInt32(int size, int min, int max)
  {
    var result = new int[size];
    ToInt32(result, min, max);
    return result;
  }

  public static void ToInt32(Span<int> ints, int min, int max)
  {
    if (min < 0) min = Math.Abs(min);
    if (max < 0) max = Math.Abs(max);
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(min, max);

    var d = max - min;
    var length = ints.Length;
    var bytes = new byte[length * 4];
    Rand.GetNonZeroBytes(bytes);
    for (int i = 0; i < length; i++)
    {
      var tmp = BitConverter.ToInt32(bytes, 4 * i) % d;
      if (tmp < 0) tmp = Math.Abs(tmp);
      ints[i] = min + tmp;
    }
  }

  public static long NextInt64()
  {
    var bytes = new byte[8];
    Rand.GetNonZeroBytes(bytes);
    return Math.Abs(BitConverter.ToInt64(bytes, 0));
  }

  public static long NextInt64(long max)
  {
    return NextInt64(0, max);
  }

  public static long NextInt64(long min, long max)
  {
    if (min < 0) min = Math.Abs(min);
    if (max < 0) max = Math.Abs(max);
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(min, max);

    var d = max - min;
    var bytes = new byte[8];
    Rand.GetNonZeroBytes(bytes);
    var tmp = BitConverter.ToInt64(bytes) % d;
    if (tmp < 0) tmp = Math.Abs(tmp);
    return min + tmp;
  }

  public static long[] ToInt64(int size, long min, long max)
  {
    var result = new long[size];
    ToInt64(result, min, max);
    return result;
  }

  public static void ToInt64(Span<long> ints, long min, long max)
  {
    if (min < 0) min = Math.Abs(min);
    if (max < 0) max = Math.Abs(max);
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(min, max);

    var d = max - min;
    var length = ints.Length;
    var bytes = new byte[length * 8];
    Rand.GetNonZeroBytes(bytes);
    for (int i = 0; i < length; i++)
    {
      var tmp = BitConverter.ToInt64(bytes, 8 * i) % d;
      if (tmp < 0) tmp = Math.Abs(tmp);
      ints[i] = min + tmp;
    }
  }

  public static ulong NextUInt64()
  {
    var bytes = new byte[8];
    Rand.GetNonZeroBytes(bytes);
    return BitConverter.ToUInt64(bytes, 0);
  }

  public static ulong NextUInt64(ulong max)
  {
    return NextUInt64(0, max);
  }

  public static ulong NextUInt64(ulong min, ulong max)
  {
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(min, max);

    var d = max - min;
    return min + NextUInt64() % d;
  }

  public static ulong[] ToUInt64(int size, ulong min, ulong max)
  {
    var result = new ulong[size];
    ToUInt64(result, min, max);
    return result;
  }

  public static void ToUInt64(Span<ulong> uls, ulong min, ulong max)
  {
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(min, max);

    var d = max - min;
    var length = uls.Length;
    var bytes = new byte[length * 8];
    Rand.GetNonZeroBytes(bytes);
    for (int i = 0; i < length; i++)
    {
      var ul = BitConverter.ToUInt64(bytes, 8 * i) % d;
      uls[i] = min + ul;
    }
  }
}
