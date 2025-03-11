


namespace michele.natale.Tests.GF2s;

using Numerics;
using static Randoms.RandomHelper;

public class UnitTest
{
  public static void Start()
  {
    TestAuthor();
    TestConstructor();
    TestOperations();


    Console.WriteLine($"{nameof(UnitTest)}: FINISH\n");
    Console.WriteLine();

  }

  #region Author
  private static void TestAuthor()
  {
    var author = GF2.AuthorInfo;
    //Console.WriteLine(author);
  }
  #endregion Author

  #region Constructors

  private static void TestConstructor()
  {
    TestOrder();
    TestOrderIdp();
    TestOrderIdpValue();
    TestGF2Init();
  }

  private static void TestOrder()
  {
    //Up to 31 is possible, but a few are enough for the test.

    var cnt = GF2.MAX_ORDER_EXP - 23;
    for (var i = GF2.MIN_ORDER_EXP; i < cnt; i++)
    {
      var order = 1ul << i;
      var gf = new GF2(order);
    }
  }

  private static void TestOrderIdp()
  {
    //Up to 31 is possible, but a few are enough for the test.

    var cnt = GF2.MAX_ORDER_EXP - 15;
    for (var i = GF2.MIN_ORDER_EXP + 6; i < cnt; i++)
    {
      var order = 1ul << i;
      var idp = GF2.ToIDPs[GF2.ToExponent(order)];
      var gf = new GF2(order, idp);
    }
  }

  private static void TestOrderIdpValue()
  {
    //Up to 31 is possible, but a few are enough for the test.

    var cnt = GF2.MAX_ORDER_EXP - 11;
    for (var i = GF2.MIN_ORDER_EXP + 12; i < cnt; i++)
    {
      var order = 1ul << i;
      var value = RngInt(order);
      var idp = GF2.ToIDPs[GF2.ToExponent(order)];
      var gf = new GF2(order, idp, value);
    }
  }

  private static void TestGF2Init()
  {
    //Up to 31 is possible, but a few are enough for the test.

    var cnt = GF2.MAX_ORDER_EXP - 11;
    for (var i = GF2.MIN_ORDER_EXP + 12; i < cnt; i++)
    {
      var order = 1ul << i;
      var value = RngInt(order);
      var idp = GF2.ToIDPs[GF2.ToExponent(order)];
      var gf_1 = new GF2(order, idp, value);

      var gf_2 = new GF2(gf_1);
      if (gf_1 != gf_2) throw new Exception();
    }
  }

  #endregion Constructors

  #region Operations
  private static void TestOperations()
  {
    var rounds = 1;
    TestAdditions(rounds);
    TestSubtractions(rounds);
    TestMultiplications(rounds);
    TestDivisions(rounds);
    TestModulos(rounds);
    TestPows(rounds);
    TestSqrts(rounds);
    TestInverses(rounds);
  }

  private static void TestAdditions(int rounds)
  {

    for (var i = 0; i < rounds; i++)
    {
      //Large numbers (exp > 20) sometimes 
      //take a very long time to calculate.
      var max_exp = GF2.ToIDPs.Keys.Max() - 12;

      var exp = RngInt(1, max_exp + 1);

      var order = 1ul << exp;
      var idp = GF2.ToIDPs[exp];

      var value1 = RngInt(order);
      var gf2x_1 = new GF2(order, idp, value1);

      var value2 = RngInt(order);
      var gf2x_2 = new GF2(order, idp, value2);

      var gf2x_3 = gf2x_1 + gf2x_2;
      var gf2x_4 = gf2x_3 - gf2x_2;
      if (gf2x_1 != gf2x_4) throw new Exception();

      var gf2x_5 = gf2x_2 + gf2x_1;
      var gf2x_6 = gf2x_5 - gf2x_1;
      if (gf2x_2 != gf2x_6) throw new Exception();

    }
  }

  private static void TestSubtractions(int rounds)
  {
    for (var i = 0; i < rounds; i++)
    {
      //Large numbers (exp > 20) sometimes 
      //take a very long time to calculate.
      var max_exp = GF2.ToIDPs.Keys.Max() - 12;

      var exp = RngInt(1, max_exp + 1);
      var order = 1ul << exp;
      var idp = GF2.ToIDPs[exp];

      var value1 = RngInt(order);
      var gf2x_1 = new GF2(order, idp, value1);

      var value2 = RngInt(order);
      var gf2x_2 = new GF2(order, idp, value2);

      var gf2x_3 = gf2x_1 - gf2x_2;
      var gf2x_4 = gf2x_3 + gf2x_2;
      if (gf2x_1 != gf2x_4) throw new Exception();

      var gf2x_5 = gf2x_2 - gf2x_1;
      var gf2x_6 = gf2x_5 + gf2x_1;
      if (gf2x_2 != gf2x_6) throw new Exception();
    }
  }

  private static void TestMultiplications(int rounds)
  {
    for (var i = 0; i < rounds; i++)
    {
      //Large numbers (exp > 20) sometimes 
      //take a very long time to calculate.
      var max_exp = GF2.ToIDPs.Keys.Max() - 12;

      var exp = RngInt(1, max_exp + 1);
      var order = 1ul << exp;
      var idp = GF2.ToIDPs[exp];

      var value1 = RngInt(1ul, order);
      var gf2x_1 = new GF2(order, idp, value1);

      var value2 = RngInt(1ul, order);
      var gf2x_2 = new GF2(order, idp, value2);

      var gf2x_3 = gf2x_1 * gf2x_2;
      var gf2x_4 = gf2x_3 / gf2x_2;
      if (gf2x_1 != gf2x_4) throw new Exception();

      var gf2x_5 = gf2x_2 * gf2x_1;
      var gf2x_6 = gf2x_5 / gf2x_1;
      if (gf2x_2 != gf2x_6) throw new Exception();
    }
  }

  private static void TestDivisions(int rounds)
  {
    for (var i = 0; i < rounds; i++)
    {
      //Large numbers (exp > 20) sometimes 
      //take a very long time to calculate.
      var max_exp = GF2.ToIDPs.Keys.Max() - 12;

      var exp = RngInt(1, max_exp + 1);
      var order = 1ul << exp;
      var idp = GF2.ToIDPs[exp];

      var value1 = RngInt(1ul, order);
      var gf2x_1 = new GF2(order, idp, value1);

      var value2 = RngInt(1ul, order);
      var gf2x_2 = new GF2(order, idp, value2);

      var gf2x_3 = gf2x_1 / gf2x_2;
      var gf2x_4 = gf2x_3 * gf2x_2;
      if (gf2x_1 != gf2x_4) throw new Exception();

      var gf2x_5 = gf2x_2 / gf2x_1;
      var gf2x_6 = gf2x_5 * gf2x_1;
      if (gf2x_2 != gf2x_6) throw new Exception();
    }
  }

  private static void TestModulos(int rounds)
  {
    //No modulo calculation
  }

  private static void TestPows(int rounds)
  {
    for (var i = 0; i < rounds; i++)
    {
      //Large numbers (exp > 20) sometimes 
      //take a very long time to calculate.
      var max_exp = GF2.ToIDPs.Keys.Max() - 12;

      var exp = RngInt(1, max_exp + 1);
      var order = 1ul << exp;
      var idp = GF2.ToIDPs[exp];

      var value1 = RngInt(1ul, order);
      var gf2x_1 = new GF2(order, idp, value1);

      var value2 = RngInt(1ul, order);
      var gf2x_2 = new GF2(order, idp, value2);

      var gf2x_3 = gf2x_1 / gf2x_2;
      var gf2x_4 = gf2x_3 * gf2x_2;
      if (gf2x_1 != gf2x_4) throw new Exception();

      var gf2x_5 = gf2x_2 / gf2x_1;
      var gf2x_6 = gf2x_5 * gf2x_1;
      if (gf2x_2 != gf2x_6) throw new Exception();
    }
  }

  private static void TestSqrts(int rounds)
  {
    //No sqrt-calculation
  }

  private static void TestInverses(int rounds)
  {
    for (var i = 0; i < rounds; i++)
    {
      //Large numbers (exp > 20) sometimes 
      //take a very long time to calculate.
      var max_exp = GF2.ToIDPs.Keys.Max() - 12;

      var exp = RngInt(1, max_exp + 1);
      var order = 1ul << exp;
      var idp = GF2.ToIDPs[exp];

      var value1 = RngInt(1ul, order);
      var gf2x_1 = new GF2(order, idp, value1);

      var value2 = RngInt(1ul, order);
      var gf2x_2 = new GF2(order, idp, value2);

      var gf2x_3 = gf2x_1 / gf2x_2;
      var gf2x_4 = gf2x_1 * gf2x_2.InvMul; 

      var gf2x_5 = gf2x_2 / gf2x_1;
      var gf2x_6 = gf2x_2 * gf2x_1.InvMul; 

      var gf2x_7 = gf2x_1 - gf2x_2;
      var gf2x_8 = gf2x_1 + gf2x_2.InvAdd; 

      var gf2x_9 = gf2x_2 - gf2x_1;
      var gf2x_10 = gf2x_2 + gf2x_1.InvAdd; 
    }
  }

  #endregion Operations

}
