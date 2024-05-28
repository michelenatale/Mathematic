

using System.Numerics;
using System.Runtime.CompilerServices;

namespace TestCoprimes;


public class CoprimesRandom
{

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T[] RngRangeCoprime<T>(int size) where T : INumber<T>
  {
    if(typeof(T) == typeof(BigInteger))
     throw new NotImplementedException();

    var result = new T[size]; 
    this.NextCoprimesValue(result);
    return result;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static BigInteger[] RngRangeCoprimeBigInteger(int size, int bits) 
  {
    var result = new BigInteger[size];
      NextCoprimesValueBigInteger(result, bits);
    return result;
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T NextRangeCoprime<T>() where T : INumber<T>
  {
    if(typeof(T) == typeof(BigInteger))
      throw new NotImplementedException();
    return this.NextCoprimesValue<T>();
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void NextRangeCoprime<T>(T[] destination) where T : INumber<T>
  {
    this.NextCoprimesValue(destination);
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private unsafe T NextCoprimesValue<T>() where T : INumber<T>
  {
    var coprimes = CoprimesSource.ToCoprimes256();
    TypeCode typecode = Type.GetTypeCode(typeof(T)); 

    var rbyte = new byte[sizeof(T)];
    var cplengthg1 = coprimes.Length - 2;
    RandomHolder.ToBytes(rbyte, (byte)coprimes.Length);
    switch (typecode)
    {
      case TypeCode.SByte:
        return T.CreateTruncating((coprimes[(rbyte[0] % cplengthg1) + 2]) & sbyte.MaxValue);
      case TypeCode.Byte:
        return T.CreateTruncating(coprimes[(rbyte[0] % cplengthg1) + 2]);
      case TypeCode.Int16:
        var rshort = new ushort[1];
        rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
        rbyte[1] = coprimes[rbyte[1]];
        Buffer.BlockCopy(rbyte, 0, rshort, 0, rbyte.Length);
        return T.CreateTruncating(rshort[0] & short.MaxValue);
      case TypeCode.UInt16:
        var rushort = new ushort[1];
        rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
        rbyte[1] = coprimes[rbyte[1]];
        Buffer.BlockCopy(rbyte, 0, rushort, 0, rbyte.Length);
        return T.CreateTruncating(rushort[0]);
      case TypeCode.Int32:
        var rint32 = new uint[1];
        rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
        for (var i = 1; i < rbyte.Length; i++)
          rbyte[i] = coprimes[rbyte[i]];
        Buffer.BlockCopy(rbyte, 0, rint32, 0, rbyte.Length);
        return T.CreateTruncating(rint32[0] & int.MaxValue);
      case TypeCode.UInt32:
        var ruint32 = new uint[1];
        rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
        for (var i = 1; i < rbyte.Length; i++)
          rbyte[i] = coprimes[rbyte[i]];
        Buffer.BlockCopy(rbyte, 0, ruint32, 0, rbyte.Length);
        return T.CreateTruncating(ruint32[0]);
      case TypeCode.Int64:
        var rint64 = new ulong[1];
        rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
        for (var i = 1; i < rbyte.Length; i++)
          rbyte[i] = coprimes[rbyte[i]];
        Buffer.BlockCopy(rbyte, 0, rint64, 0, rbyte.Length);
        return T.CreateTruncating(rint64[0] & long.MaxValue);
      case TypeCode.UInt64:
        var ruint64 = new ulong[1];
        rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
        for (var i = 1; i < rbyte.Length; i++)
          rbyte[i] = coprimes[rbyte[i]];
        Buffer.BlockCopy(rbyte, 0, ruint64, 0, rbyte.Length);
        return T.CreateTruncating(ruint64[0]);
      default:
        if (typeof(T) == typeof(Int128))
        {
          Span<UInt128> rint128;
          rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
          for (var i = 1; i < rbyte.Length; i++)
            rbyte[i] = coprimes[rbyte[i]];
          fixed (byte* ptr = rbyte) rint128 = new Span<UInt128>(ptr, 1);
          return T.CreateTruncating(rint128[0] & (UInt128)Int128.MaxValue);
        }
        else if (typeof(T) == typeof(UInt128))
        {
          Span<UInt128> ruint128;
          rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
          for (var i = 1; i < rbyte.Length; i++)
            rbyte[i] = coprimes[rbyte[i]];
          fixed (byte* ptr = rbyte) ruint128 = new Span<UInt128>(ptr, 1);
          return T.CreateTruncating(ruint128[0]);
        }
        else break;
    }
    throw new TypeLoadException();
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private unsafe void NextCoprimesValue<T>(T[] destination) where T : INumber<T>
  {
    if (typeof(T) == typeof(BigInteger))
      throw new NotImplementedException();

    var coprimes = CoprimesSource.ToCoprimes256();
    var typecode = Type.GetTypeCode(typeof(T));

    var tsize = sizeof(T);
    var cplengthg1 = coprimes.Length - 2;
    var rbyte = new byte[tsize * destination.Length];
    RandomHolder.ToBytes(rbyte, (byte)coprimes.Length);

    for (var i = 0; i < rbyte.Length; i++)
      if (i % tsize == 0) rbyte[i] = coprimes[(rbyte[i] % cplengthg1) + 2];
      else rbyte[i] = coprimes[rbyte[i]];

    switch (typecode)
    {
      case TypeCode.SByte:
        for (var i = 0; i < destination.Length; i++)
          destination[i] = T.CreateTruncating(rbyte[i] & sbyte.MaxValue);
        return;
      case TypeCode.Byte:
        for (var i = 0; i < destination.Length; i++)
          destination[i] = T.CreateTruncating(rbyte[i]);
        return;
      case TypeCode.Int16:
        var rshort = new ushort[1];
        for (var i = 0; i < destination.Length; i++)
        {
          Buffer.BlockCopy(rbyte, i * tsize, rshort, 0, tsize);
          destination[i] = T.CreateTruncating(rshort[0] & short.MaxValue);
        }
        return;
      case TypeCode.UInt16:
        var rushort = new ushort[1];
        for (var i = 0; i < destination.Length; i++)
        {
          Buffer.BlockCopy(rbyte, i * tsize, rushort, 0, tsize);
          destination[i] = T.CreateTruncating(rushort[0]);
        }
        return;
      case TypeCode.Int32:
        var rint32 = new uint[1];
        for (var i = 0; i < destination.Length; i++)
        {
          Buffer.BlockCopy(rbyte, i * tsize, rint32, 0, tsize);
          destination[i] = T.CreateTruncating(rint32[0] & int.MaxValue);
        }
        return;
      case TypeCode.UInt32:
        var ruint32 = new uint[1];
        for (var i = 0; i < destination.Length; i++)
        {
          Buffer.BlockCopy(rbyte, i * tsize, ruint32, 0, tsize);
          destination[i] = T.CreateTruncating(ruint32[0]);
        }
        return;
      case TypeCode.Int64:
        var rint64 = new ulong[1];
        for (var i = 0; i < destination.Length; i++)
        {
          Buffer.BlockCopy(rbyte, i * tsize, rint64, 0, tsize);
          destination[i] = T.CreateTruncating(rint64[0] & long.MaxValue);
        }
        return;
      case TypeCode.UInt64:
        var ruint64 = new ulong[1];
        for (var i = 0; i < destination.Length; i++)
        {
          Buffer.BlockCopy(rbyte, i * tsize, ruint64, 0, tsize);
          destination[i] = T.CreateTruncating(ruint64[0]);
        }
        return;
      default:
        if (typeof(T) == typeof(Int128))
        {
          Span<UInt128> rint128;
          fixed (byte* ptr = rbyte) rint128 = new Span<UInt128>(ptr, destination.Length);
          for (var i = 0; i < destination.Length; i++)
            destination[i] = T.CreateTruncating(rint128[i] & (UInt128)Int128.MaxValue);
          return;
        }
        else if (typeof(T) == typeof(UInt128))
        {
          Span<UInt128> ruint128;
          fixed (byte* ptr = rbyte) ruint128 = new Span<UInt128>(ptr, destination.Length);
          for (var i = 0; i < destination.Length; i++)
            destination[i] = T.CreateTruncating(ruint128[i] & (UInt128)Int128.MaxValue);
          return;
        }
        else break;
    }
    throw new TypeLoadException();
  }



  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void NextRangeCoprimeBigInteger(
    BigInteger[] destination, int bits)
  {
    NextCoprimesValueBigInteger(destination, bits);
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static BigInteger NextRangeCoprimeBigInteger(int bits)
  {
    if (!int.IsPow2(bits))
      throw new NotImplementedException();

    var coprimes = CoprimesSource.ToCoprimes256();

    var rbyte = new byte[bits / 8];
    var cplengthg1 = coprimes.Length - 2;
    RandomHolder.ToBytes(rbyte, (byte)coprimes.Length);

    rbyte[0] = coprimes[(rbyte[0] % cplengthg1) + 2];
    for (var i = 1; i < rbyte.Length; i++)
      rbyte[i] = coprimes[rbyte[i]];
    return new BigInteger(rbyte, true, false);
  }

  private static void NextCoprimesValueBigInteger(BigInteger[] destination, int bits)
  {
    if (!int.IsPow2(bits))
      throw new NotImplementedException();

    var coprimes = CoprimesSource.ToCoprimes256();

    var tsize = bits / 8;
    var cplengthg1 = coprimes.Length - 2;
    var rbyte = new byte[tsize * destination.Length];
    RandomHolder.ToBytes(rbyte, (byte)coprimes.Length);

    for (var i = 0; i < rbyte.Length; i++)
      if (i % tsize == 0) rbyte[i] = coprimes[(rbyte[i] % cplengthg1) + 2];
      else rbyte[i] = coprimes[rbyte[i]];

    for (var i = 0; i < destination.Length; i++)
    {
      var b = rbyte.Skip(i * tsize).Take(tsize).ToArray();
      destination[i] = new BigInteger(b, true, false);
    }

  }
}
