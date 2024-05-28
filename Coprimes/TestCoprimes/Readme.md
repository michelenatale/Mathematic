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
