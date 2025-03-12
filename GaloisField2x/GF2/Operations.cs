

namespace michele.natale.Numerics;

partial struct GF2
{

  /// <summary>
  /// Returns the multiplicative inverse of the current value. 
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public GF2 InvMul =>
    this.Value != 0 ? new GF2(this.Order, this.IDP, this.Exp[this.Order - this.Log[this.Value] - 1])
    : throw new ArgumentOutOfRangeException(
        nameof(this.InvMul), $"{nameof(this.InvMul)}(0) is undefined!");

  /// <summary>
  /// Returns the additive inverse of the current value. 
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  public GF2 InvAdd => new(this.Order, this.IDP, this.Value);

  //public static explicit operator ulong(GF2 value) => value.Value;
  //public static explicit operator GF2(ulong value) => new(value);

  /// <summary>
  /// Calculates the Galois addition.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="value">Desired GF2-Value</param>
  /// <returns>New GF2-Value</returns>
  public ulong Addition(ulong value) =>
    ExtMod(this.Value ^ value, this.Order);

  /// <summary>
  /// Calculates the Galois subtraction.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="value">Desired GF2-Value</param>
  /// <returns>new GF2-Value</returns>
  public ulong Subtract(ulong value) =>
    ExtMod(this.Value ^ value, this.Order);

  /// <summary>
  /// Calculates the Galois multiplication.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="value">Desired GF2-Value</param>
  /// <returns>new GF2-Value</returns>
  public ulong Multiply(ulong value)
  {
    ulong order = this.Order, result = 0;

    value = ExtMod(value, order);
    if (this.Value != 0 && value != 0)
    {
      var tmp = (this.Log[this.Value] + this.Log[value]) % (order - 1);
      result = this.Exp[tmp];
    }

    return ExtMod(result, order);
  }

  /// <summary>
  /// Calculates the Galois division.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="value">Desired GF2-Value</param>
  /// <returns>new GF2-Value</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public ulong Divide(ulong value)
  {
    value = ExtMod(value, this.Order);

    if (value == 0)
      throw new ArgumentNullException(nameof(value));

    ulong order = this.Order, result = 0;
    if (this.Value != 0)
    {
      var tmp = (order + this.Log[this.Value] - this.Log[value] - 1) % (order - 1);
      result = this.Exp[tmp];
    }

    return ExtMod(result, order); 
  }


  /// <summary>
  /// Calculates the Galois addition.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF2-Value</param>
  /// <param name="right">Desired GF2-Value</param>
  /// <returns>Addition of two GF2 values.</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static GF2 operator +(GF2 left, GF2 right) =>
    left.Order == right.Order ? new(left.Order, left.IDP, left.Value ^ right.Value)
    : throw new ArgumentOutOfRangeException(nameof(left),
     $"'+'-Operation has failed!");

  /// <summary>
  /// Calculates the Galois Subtraction.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF2-Value</param>
  /// <param name="right">Desired GF2-Value</param>
  /// <returns>Subtraction of two GF2-Values</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static GF2 operator -(GF2 left, GF2 right) =>
    left.Order == right.Order ? new(left.Order, left.IDP, left.Value ^ right.Value)
    : throw new ArgumentOutOfRangeException(nameof(left),
     $"'-'-Operation has failed!");

  /// <summary>
  /// Calculates the Galois Multiplication.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF-Value</param>
  /// <param name="right">Desired GF-Value</param>
  /// <returns>Multiplication of two GF2-Values</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static GF2 operator *(GF2 left, GF2 right)
  {
    if (left.Order != right.Order)
      throw new ArgumentOutOfRangeException(nameof(left),
        $"'*'-Operation has failed!");

    ulong order = left.Order, result = 0;
    if (left.Value != 0 && right.Value != 0)
    {
      var tmp = (left.Log[left.Value] + right.Log[right.Value]) % (order - 1);
      result = left.Exp[tmp];
    }
    return new GF2(order, left.IDP, result);
  }

  /// <summary>
  /// Calculates the Galois Multiplication.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF-Value</param>
  /// <param name="right">Desired GF-Value</param>
  /// <returns>Division of two GF-Values</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <exception cref="ArgumentNullException"></exception>
  public static GF2 operator /(GF2 left, GF2 right)
  {
    if (left.Order != right.Order)
      throw new ArgumentOutOfRangeException(nameof(left),
        $"'/'-Operation has failed!");

    if (right.Value == 0)
      throw new ArgumentNullException(nameof(right));

    ulong order = left.Order, result = 0;
    if (left.Value != 0)
    {
      var tmp = (order + left.Log[left.Value] - right.Log[right.Value] - 1) % (order - 1);
      result = left.Exp[tmp];
    }
    return new GF2(order, left.IDP, result);
  }

  /// <summary>
  /// Increments to the next GF2 value.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="value">Desired GF2-Value</param>
  /// <returns></returns>
  public static GF2 operator ++(GF2 value) =>
    //new(value.Order, value.IDP, value.Value ^ 1ul);
    new(value.Order, value.IDP, value.Value + 1);

  /// <summary>
  /// Decrements to the previous GF2 value.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="value">Desired GF2-Value</param>
  /// <returns></returns>
  public static GF2 operator --(GF2 value) =>
    //new (value.Order, value.IDP, value.Value ^ 1ul);
    new(value.Order, value.IDP, value.Value - 1);

  /// <summary>
  /// Raises a GF2 value to the power of a specified value.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="value">Desired GF2-Value</param>
  /// <param name="exp">Desired Exponent</param>
  /// <returns>Raising the value to the power of the exponent.</returns>
  public static GF2 Pow(GF2 value, int exp)
  {
    var result = new GF2(value.Order, value.IDP, 1);
    for (var i = 0; i < exp; i++)
      result *= value;
    return result;
  }

  /// <summary>
  /// Compares two GF2-values for equality.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF2-Value</param>
  /// <param name="right">Desired GF2-Value</param>
  /// <returns>True if Equals, ortherwise false.</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static bool operator ==(GF2 left, GF2 right) =>
    left.Order == right.Order ? left.Value == right.Value
    : throw new ArgumentOutOfRangeException(nameof(left),
      $"'=='-Operation has failed!");

  /// <summary>
  /// Compares two GF-values for inequality.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF2-Value</param>
  /// <param name="right">Desired GF2-Value</param>
  /// <returns>True if not Equals, ortherwise false.</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static bool operator !=(GF2 left, GF2 right) =>
    left.Order == right.Order ? left.Value != right.Value
    : throw new ArgumentOutOfRangeException(nameof(left),
      $"'!='-Operation has failed!");

  /// <summary>
  /// Compares the current GF2 value with another one.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="obj">Desired Value</param>
  /// <returns>True if Equals, ortherwise false.</returns>
  public override bool Equals(object? obj)
  {
    if (obj is null) return false;
    if (obj is not GF2 gf) return false;
    return this.Order == gf.Order && this.Value == gf.Value;
  }

  /// <summary>
  /// Compare the left GF2 value to see if it is smaller than the right one.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF2-Value</param>
  /// <param name="right">Desired GF2-Value</param>
  /// <returns>True if the left GF2 value is smaller, otherwise false.</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static bool operator <(GF2 left, GF2 right) =>
    left.Order == right.Order ? left.Value < right.Value
    : throw new ArgumentOutOfRangeException(nameof(left),
      $"'!='-Operation has failed!");

  /// <summary>
  /// Compare the left GF2 value to see if it is greater than the right one.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF2-Value</param>
  /// <param name="right">Desired GF2-Value</param>
  /// <returns>True if the left GF2 value is greater, otherwise false.</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static bool operator >(GF2 left, GF2 right) =>
    left.Order == right.Order ? left.Value > right.Value
    : throw new ArgumentOutOfRangeException(nameof(left),
      $"'!='-Operation has failed!");

  /// <summary>
  /// Compares the left GF2 value to see whether it is less than or equal to the right one.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF2-Value</param>
  /// <param name="right">Desired GF2-Value</param>
  /// <returns>True if the left GF2 value is less than or equal to, otherwise false.</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static bool operator <=(GF2 left, GF2 right) =>
    left.Order == right.Order ? left.Value <= right.Value
    : throw new ArgumentOutOfRangeException(nameof(left),
      $"'!='-Operation has failed!");

  /// <summary>
  /// Compares the left GF2 value to see whether it is greater than or equal to the right one.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <param name="left">Desired GF2-Value</param>
  /// <param name="right">Desired GF2-Value</param>
  /// <returns>True if the left GF2 value is greater than or equal to, otherwise false.</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static bool operator >=(GF2 left, GF2 right) =>
    left.Order == right.Order ? left.Value >= right.Value
    : throw new ArgumentOutOfRangeException(nameof(left),
      $"'!='-Operation has failed!");

  /// <summary>
  /// Converts the value of this instance into its equivalent string representation.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <returns>
  /// The string representation of the value of this instance, 
  /// consisting of a sequence of digits ranging from 0 to 9, 
  /// without a sign or leading zeroes.
  /// </returns>
  public override string ToString() => this.Value.ToString();

  /// <summary>
  /// Calculates a hash code from the current combination of fields.
  /// <para>Updated by <see href="https://github.com/michelenatale">© Michele Natale 2025</see></para>  
  /// </summary>
  /// <returns>Return the HashCode</returns>
  public override int GetHashCode() =>
    HashCode.Combine(this.Value, this.Order, this.IDP);


}
