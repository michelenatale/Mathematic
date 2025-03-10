
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace michele.natale.Randoms;

public class RandomHelper
{

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T RngInt<T>()
    where T : INumber<T>
  {
    var rand = Random.Shared;
    var sz = Unsafe.SizeOf<T>();

    var bytes = new byte[sz];
    rand.NextBytes(bytes);

    return Unsafe.ReadUnaligned<T>(
      ref MemoryMarshal.GetReference(bytes.AsSpan()));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T[] RngInt<T>(int size)
    where T : INumber<T> =>
      [.. Enumerable.Range(0, size).Select(_ => RngInt<T>())];

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T RngInt<T>(T max)
    where T : INumber<T>, INumberBase<T> =>
      RngInt(T.Zero, max);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T RngInt<T>(T min, T max)
    where T : INumber<T>, INumberBase<T>
  {
    var result = new T[1];
    RngInts(result, min, max);
    return result.First();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T[] RngInts<T>(int size, T min, T max)
    where T : INumber<T>, INumberBase<T>
  {
    var result = new T[size];
    RngInts(result, min, max);
    return result;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void RngInts<T>(T[] ints, T min, T max)
    where T : INumber<T>, INumberBase<T>
  {
    if (IsNullOrEmpty(ints)) return;
    ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(min, max);

    var d = max - min;
    var length = ints.Length;
    var rand = Random.Shared;
    var type_bits = Unsafe.SizeOf<T>();
    var bytes = new byte[type_bits * length];
    rand.NextBytes(bytes);
    //DataTypes Int128 and UInt128 are not yet recognized as primitives. 
    if (typeof(T).IsPrimitive || typeof(T) == typeof(Int128) || typeof(T) == typeof(UInt128))
      for (int i = 0; i < length; i++)
      {
        var tmp = Unsafe.ReadUnaligned<T>(ref bytes[i * type_bits]);
        ints[i] = T.Abs(tmp); ints[i] %= d; ints[i] += min;
      }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsNullOrEmpty<T>(T[] ints)
    where T : INumber<T>, INumberBase<T>
  {
    if (ints is null) return true;
    return ints.Length == 0;
  }
}
