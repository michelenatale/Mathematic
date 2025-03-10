
using System.Numerics;

namespace michele.natale.Numerics;

partial struct GF2
{

  /// <summary>
  /// C-Tor 
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="order">Desired order</param>
  public GF2(ulong order)
  {
    if (!IsMentionedAuthor)
      Author_Info();

    var exp = ToExponent(order);

    var idp = ToIDPs[exp];
    AssertGF2(order, idp);

    this.IDP = idp;
    this.Order = order;
    this.Exponent = exp;

    this.Exp = new ulong[this.Order];
    this.Log = new ulong[this.Order];

    this.Init();
    this.Value = 0;
  }

  /// <summary>  /// <summary>
  /// C-Tor 
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="order">Desired order</param>
  /// <param name="idp">Desired irreducible Polynomial</param>
  public GF2(ulong order, ulong idp)
  {
    AssertGF2(order, idp);

    this.IDP = idp;
    this.Order = order;
    this.Exponent = ToExponent(order);

    this.Exp = new ulong[this.Order];
    this.Log = new ulong[this.Order];

    this.Init();
    this.Value = 0;
  }

  /// <summary>
  /// C-Tor 
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="order">Desired order</param>
  /// <param name="idp">Desired irreducible polynomial</param>
  /// <param name="value">Desired value</param>
  public GF2(ulong order, ulong idp, ulong value)
    : this(order, idp) => this.Value = ExtMod(value, order);

  /// <summary>
  /// C-Tor 
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="value">Desired GF2-Value</param>
  public GF2(GF2 value) : this(value.Order, value.IDP, value.Value) {}

  private void Init()
  {
    if (!IsMentionedAuthor)
      Author_Info();

    var value = 0x01ul;
    for (uint i = 0; i < this.Order; i++)
    {
      this.Exp[i] = value;
      value <<= 1;
      if (value >= this.Order)
      {
        value ^= this.IDP;
        value &= this.Order - 1;
      }
      if (i < this.Order - 1)
        this.Log[this.Exp[i]] = i;
    }
  }


  /// <summary>
  /// Calculates the exponent from the variable order.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="order"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static int ToExponent(ulong order)
  {
    if (!ulong.IsPow2(order))
      throw new ArgumentOutOfRangeException(nameof(order),
        $"{nameof(order)} is not a power of two");

    var result = 1;
    while (1ul << result++ != order) ;
    result--;

    return result;
  }

  private static void AssertGF2(ulong order, ulong idp)
  {
    if (!ulong.IsPow2(order) || order < 2)
      throw new ArgumentOutOfRangeException(nameof(order),
        $"{nameof(order)} is not a power of two or order < 2");

    if (order > 2 && order >= idp)
      throw new ArgumentOutOfRangeException(nameof(idp),
        $"{nameof(idp)} is not a valid irreducible polynomial!");

    //// There are several possibilities for irreducible 
    //// polynomials. If the one from GF2 is to be used, 
    //// then decomment here.

    //var exp = ToExponent(order);
    //var expidp = ToIDPs[exp];
    //if(idp != expidp) throw new ArgumentException(
    //  $"{nameof(idp)} is not a valid irreducible polynomial!",
    //    nameof(idp));
  }

  /// <summary>
  /// Calculates the extended modulo calculation, which always results in a positive number.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <typeparam name="T">Desired Type</typeparam>
  /// <param name="value">Desired Value</param>
  /// <param name="order">Desired Order</param>
  /// <returns>New Modulo Value</returns>
  public static T ExtMod<T>(T value, T order)
    where T : INumber<T> => ((value % order) + order) % order;

}
