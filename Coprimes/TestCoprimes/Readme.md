# Mathematic Coprimes

Shows in a simple way how to generate generic coprimes for any relevant data type.

      var i32 = cpr.NextRangeCoprime<int>();

      var i32s = cpr.RngRangeCoprime<int>(128);

      cpr.NextRangeCoprime(i32s);

<p><br>Special DataTypes such as Int128 or BigInteger are also possible</p>

>Here for example for Int128

      var i128 = cpr.NextRangeCoprime<Int128>();

      var i128s = cpr.RngRangeCoprime<Int128>(128);

      cpr.NextRangeCoprime(i128s);


>Here for example for BigInteger

      var bits = ToPowerTwo(RandomHolder.NextInt32(8, 1025));
      
      var bi = CoprimesRandom.NextRangeCoprimeBigInteger(bits);

      var bis = CoprimesRandom.RngRangeCoprimeBigInteger(128, bits);

      CoprimesRandom.NextRangeCoprimeBigInteger(bis, bits);


and many others ...

