
namespace michele.natale.Numerics;

partial struct GF2
{

  private readonly ulong MValue;

  /// <summary>
  /// Smallest permitted exponent for the order calculation in a GF2.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public const int MIN_ORDER_EXP = 1;

  /// <summary>
  /// Largest permitted exponent for the order calculation in a GF2.
  /// <para>Note: Actually it would be 63, but for performance reasons it is recommended to stay at 31.</para>
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public const int MAX_ORDER_EXP = 31;

  /// <summary>
  /// Current GF2 value that represents a decimal number.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public ulong Value
  {
    get => this.MValue;
    init => this.MValue = ExtMod(value, this.Order);
  }

  /// <summary>
  /// The generator for GF2, which is always 2.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public const ulong Generator = 0x2ul; //Basic

  /// <summary>
  /// The current Exp-List for GF2.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public readonly ulong[] Exp { get; init; } = [];

  /// <summary>
  /// The current Log-List for GF2.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public readonly ulong[] Log = [];

  /// <summary>
  /// The current irreducible polynomial of this GF2.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// <para>Sample see here <see href="https://www.wolframalpha.com/input?i=gf%28256%29">wolframalpha</see></para>
  /// </summary>
  public readonly ulong IDP = 0ul;

  /// <summary>
  /// The current order (2^x) of this GF2.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public readonly ulong Order = 0ul;

  /// <summary>
  /// The current Exponent of this GF2.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public readonly int Exponent = -1;
}
