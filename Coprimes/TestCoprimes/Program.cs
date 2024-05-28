

using TestCoprimes;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace CoPrimesTests;


public class Program
{
  public static void Main()
  {
    var rounds = 1000;
    TestRangeCoprimes(rounds);


    Console.WriteLine();
    Console.WriteLine("FINISH");
    Console.ReadLine();
  }

  private static void TestRangeCoprimes(int round)
  {

    Console.Write($"{nameof(TestRangeCoprimes)}: ");

    var sw = new Stopwatch();

    for (var i = 0; i < round; i++)
    {

      var cpr = new CoprimesRandom();

      sw.Start();

      var b = cpr.NextRangeCoprime<byte>();
      if (!IsCoprimeRange(b))
        throw new ArgumentException(null);

      var bytes = cpr.RngRangeCoprime<byte>(128);
      if (!IsCoprimeRange(bytes))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(bytes);
      if (!IsCoprimeRange(bytes))
        throw new ArgumentException(null);


      var sb = cpr.NextRangeCoprime<sbyte>();
      if (!IsCoprimeRange(sb))
        throw new ArgumentException(null);

      var sbytes = cpr.RngRangeCoprime<sbyte>(128);
      if (!IsCoprimeRange(sbytes))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(sbytes);
      if (!IsCoprimeRange(sbytes))
        throw new ArgumentException(null);


      var s = cpr.NextRangeCoprime<short>();
      if (!IsCoprimeRange(s))
        throw new ArgumentException(null);

      var sht = cpr.RngRangeCoprime<short>(128);
      if (!IsCoprimeRange(sht))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(sht);
      if (!IsCoprimeRange(sht))
        throw new ArgumentException(null);


      var us = cpr.NextRangeCoprime<ushort>();
      if (!IsCoprimeRange(us))
        throw new ArgumentException(null);

      var usht = cpr.RngRangeCoprime<ushort>(128);
      if (!IsCoprimeRange(usht))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(usht);
      if (!IsCoprimeRange(usht))
        throw new ArgumentException(null);


      var i32 = cpr.NextRangeCoprime<int>();
      if (!IsCoprimeRange(i32))
        throw new ArgumentException(null);

      var i32s = cpr.RngRangeCoprime<int>(128);
      if (!IsCoprimeRange(i32s))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(i32s);
      if (!IsCoprimeRange(i32s))
        throw new ArgumentException(null);


      var ui32 = cpr.NextRangeCoprime<uint>();
      if (!IsCoprimeRange(ui32))
        throw new ArgumentException(null);

      var ui32s = cpr.RngRangeCoprime<uint>(128);
      if (!IsCoprimeRange(ui32s))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(ui32s);
      if (!IsCoprimeRange(ui32s))
        throw new ArgumentException(null);


      var lng = cpr.NextRangeCoprime<long>();
      if (!IsCoprimeRange(lng))
        throw new ArgumentException(null);

      var lngs = cpr.RngRangeCoprime<long>(128);
      if (!IsCoprimeRange(lngs))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(lngs);
      if (!IsCoprimeRange(lngs))
        throw new ArgumentException(null);


      var ulng = cpr.NextRangeCoprime<ulong>();
      if (!IsCoprimeRange(ulng))
        throw new ArgumentException(null);

      var ulngs = cpr.RngRangeCoprime<ulong>(128);
      if (!IsCoprimeRange(ulngs))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(ulngs);
      if (!IsCoprimeRange(ulngs))
        throw new ArgumentException(null);


      var i128 = cpr.NextRangeCoprime<Int128>();
      if (!IsCoprimeRange(i128))
        throw new ArgumentException(null);

      var i128s = cpr.RngRangeCoprime<Int128>(128);
      if (!IsCoprimeRange(i128s))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(i128s);
      if (!IsCoprimeRange(i128s))
        throw new ArgumentException(null);


      var ui128 = cpr.NextRangeCoprime<UInt128>();
      if (!IsCoprimeRange(ui128))
        throw new ArgumentException(null);

      var ui128s = cpr.RngRangeCoprime<UInt128>(128);
      if (!IsCoprimeRange(ui128s))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(ui128s);
      if (!IsCoprimeRange(ui128s))
        throw new ArgumentException(null);


      //BigInteger
      var bits = ToPowerTwo(RandomHolder.NextInt32(8, 1025));
      
      var bi = CoprimesRandom.NextRangeCoprimeBigInteger(bits);
      if (!IsCoprimeRangeBigInteger(bi) || !IsLessEqualsBits(bi, bits))
        throw new ArgumentException(null);

      var bis = CoprimesRandom.RngRangeCoprimeBigInteger(128, bits);
      if (!IsCoprimeRangeBigInteger(bis) || !IsLessEqualsBits(bis, bits))
        throw new ArgumentException(null);

      CoprimesRandom.NextRangeCoprimeBigInteger(bis, bits);
      if (!IsCoprimeRangeBigInteger(bis) || !IsLessEqualsBits(bis, bits))
        throw new ArgumentException(null);

      sw.Stop();

      if (i % 100 == 0)
        Console.Write(".");
    }


    Console.Write($" all TypeChecks; r = {round:N0}; t = {sw.ElapsedMilliseconds}ms; td = {sw.ElapsedMilliseconds / round}ms");
    Console.WriteLine(); Console.WriteLine();

    Console.WriteLine();
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private unsafe static bool IsCoprimeRange<T>(T[] destination) where T : INumber<T>
  {
    BigInteger vmax = BigInteger.One << ToRangeNumber<T>();
    for (var i = 0; i < destination.Length; i++)
      if (BigInteger.GreatestCommonDivisor(vmax, UInt128.CreateTruncating(destination[i])) != 1)
        return false;
    return true;
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private unsafe static bool IsCoprimeRange<T>(T value) where T : INumber<T>
  {
    BigInteger vmax = BigInteger.One << ToRangeNumber<T>();
    return BigInteger.GreatestCommonDivisor(vmax, UInt128.CreateTruncating(value)) == 1;
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private unsafe static int ToRangeNumber<T>() where T : INumber<T>
  {
    var tsize = sizeof(T);

    if (typeof(T) == typeof(Int128)) return tsize * 8 - 1;
    else if (typeof(T) == typeof(UInt128)) return tsize * 8;
    else if (typeof(T) == typeof(BigInteger)) return tsize * 8;

    TypeCode typecode = Type.GetTypeCode(typeof(T));
    return typecode switch
    {
      TypeCode.Byte => tsize * 8,
      TypeCode.SByte => tsize * 8 - 1,
      TypeCode.Int16 => tsize * 8 - 1,
      TypeCode.UInt16 => tsize * 8,
      TypeCode.Int32 => tsize * 8 - 1,
      TypeCode.UInt32 => tsize * 8,
      TypeCode.Int64 => tsize * 8 - 1,
      TypeCode.UInt64 => tsize * 8,
      _ => throw new TypeAccessException(),
    };
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsCoprimeRangeBigInteger(BigInteger[] numbers)
  {

    for (var i = 0; i < numbers.Length; i++)
    {
      var bits = numbers[i].IsPowerOfTwo
        ? (int)BigInteger.Log2(numbers[i])
        : (int)BigInteger.Log2(numbers[i]) + 1;
      if (bits < 8) bits = 8;
      while (!int.IsPow2(bits)) bits++;
      var vmax = BigInteger.One << bits;
      if (BigInteger.GreatestCommonDivisor(vmax, numbers[i]) != 1)
        return false;
    }
    return true;
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsCoprimeRangeBigInteger(BigInteger value)
  {
    var bits = value.IsPowerOfTwo
        ? (int)BigInteger.Log2(value)
        : (int)BigInteger.Log2(value) + 1;
    if (bits < 8) bits = 8;
    while (!int.IsPow2(bits)) bits++;

    BigInteger vmax = BigInteger.One << bits;
    return BigInteger.GreatestCommonDivisor(vmax, value) == 1;
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsCoprimeRangeBigInteger(BigInteger value, int bits)
  {
    if (!int.IsPow2(bits)) throw new NotImplementedException();

    BigInteger vmax = BigInteger.One << bits;
    return BigInteger.GreatestCommonDivisor(vmax, value) == 1;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int ToPowerTwo(BigInteger value)
  {
    var bits = value.IsPowerOfTwo
        ? (int)BigInteger.Log2(value)
        : (int)BigInteger.Log2(value) + 1;
    if (bits < 8) bits = 8;
    return ToPowerTwo(bits);
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int ToPowerTwo(int bits)
  {
    while (!int.IsPow2(bits)) bits++;
    return bits;
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsLessEqualsBits(BigInteger[] numbers, int bits)
  {
    for (int i = 0; i < numbers.Length; i++)
      if (!IsLessEqualsBits(numbers[i], bits)) return false;
    return true;
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsEqualsBits(BigInteger number, int bits)
  {
    return ToPowerTwo(number) == bits;
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsLessEqualsBits(BigInteger number, int bits)
  {
    return ToPowerTwo(number) <= bits;
  }


  //[MethodImpl(MethodImplOptions.AggressiveInlining)]
  //private static bool IsGreaterEqualsBits(BigInteger number, int bits)
  //{
  //  return ToPowerTwo(number) >= bits;
  //}
}


