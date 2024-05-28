# Mathematic Coprimes

Shows in a simple way how to generate generic coprimes for any relevant data type.

      var i32 = cpr.NextRangeCoprime<int>();
      if (!IsCoprimeRange(i32))
        throw new ArgumentException(null);

      var i32s = cpr.RngRangeCoprime<int>(128);
      if (!IsCoprimeRange(i32s))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(i32s);
      if (!IsCoprimeRange(i32s))
        throw new ArgumentException(null);

Special DataTypes such as Int128 or BigInteger are also possible

Here for example for Int128

      var i128 = cpr.NextRangeCoprime<Int128>();
      if (!IsCoprimeRange(i128))
        throw new ArgumentException(null);

      var i128s = cpr.RngRangeCoprime<Int128>(128);
      if (!IsCoprimeRange(i128s))
        throw new ArgumentException(null);

      cpr.NextRangeCoprime(i128s);
      if (!IsCoprimeRange(i128s))
        throw new ArgumentException(null);

Here for example for BigInteger

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

