# Galois Field or Finite Field

Is a very simple [Galois Field](https://en.wikipedia.org/wiki/Finite_field), or [Finite Field](https://en.wikipedia.org/wiki/Finite_field) calculator with the base 2 (Generator, Character), which has two main operations, [addition and multiplication](https://en.wikipedia.org/wiki/Finite_field_arithmetic). All other operators (subtraction, division, inverse, etc.) are operations that follow very specific mathematical rules and are therefore legitimate. 

[Exp and log lists](https://en.wikipedia.org/wiki/Finite_field_arithmetic#Generator_based_tables) are created during initialization so that the calculations can be performed very quickly. The [polynomial calculations](https://en.wikipedia.org/wiki/Polynomial) normally used are no longer necessary for the calculations contained here. To make a Field expansion, the “Generator = 2” can be exponentiated with any [natural number](https://en.wikipedia.org/wiki/Natural_number) (e.g. Order = 2^8). For the joint calculations, they must simply always have the same “Order”.

The value is always a [natural number](https://en.wikipedia.org/wiki/Natural_number) (system of 10), which is adjusted to the GF2x system at the time of calculation.

The following [parameters](https://en.wikipedia.org/wiki/Finite_field_arithmetic) are required to create a Galois Field.
- Generator (fixed = 2),  
- Exponent,
- Order,
- Irreducible Polynomial,
- Exp-Log-List,
- Value

There are numerous answers to the question of where Galois Fields are used. The fact is, however, that Galois Fields are primarily used in mathematics ([number theory](https://en.wikipedia.org/wiki/Number_theory) etc.) as well as in computer science ([cryptography](https://en.wikipedia.org/wiki/Cryptography) and [coding theory](https://en.wikipedia.org/wiki/Coding_theory) etc.).  

And here is a bit of code:
```
private static void Test()
{
  var exp = 8;
  var order = 1ul << exp;
  var idp = GF2.ToIDPs[exp];

  var value1 = RngInt(order);
  var gf2x_1 = new GF2(order, idp, value1);

  var value2 = RngInt(order);
  var gf2x_2 = new GF2(order, idp, value2);

  var add = gf2x_1 + gf2x_2;
  var sub = gf2x_1 - gf2x_2;
  var mul = gf2x_1 * gf2x_2;
  var div = gf2x_1 / gf2x_2;

  ...
}
```
