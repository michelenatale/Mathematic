
namespace michele.natale.Numerics;

partial struct GF2
{
  /// <summary>
  /// Makes a copy of the current GF2.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public GF2 Copy =>
    new(this.Order, this.IDP, this.Value);

}
