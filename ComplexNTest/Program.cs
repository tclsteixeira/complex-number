using System;
using ComplexN;
using System.Globalization;

namespace ComplexNTest
{

    public class MainClass
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("ComplexN library tests");
            Console.WriteLine();

            CultureInfo invCult = CultureInfo.InvariantCulture;

            ComplexNumber z1 = new ComplexNumber(1.2, -4.1);
            ComplexNumber z2 = new ComplexNumber(3.4, 2.3);

            Console.WriteLine("{0} = {1} or {2}", "z1", z1.ToString(ComplexNumber.Frmt_abi, invCult), z1.ToString(invCult));
            Console.WriteLine("{0} = {1} or {2}", "z2" , z2.ToString(ComplexNumber.Frmt_abi, invCult), z2.ToString(invCult));

            Console.WriteLine("{0} phase = {1}", "z1", z1.Phase);
            Console.WriteLine("{0} phase = {1}", "z2", z2.Phase);

            Console.WriteLine("{0} conjugate = {1}", "z1", z1.Conjugate());
            Console.WriteLine("{0} conjugate = {1}", "z2", z2.Conjugate());

            Console.WriteLine("{0} + {1} = {2}", "z1", "z2", (z1+z2).ToString(invCult));
            Console.WriteLine("{0} - {1} = {2}", "z1", "z2", (z1-z2).ToString(invCult));
            Console.WriteLine("{0} * {1} = {2}", "z1", "z2", (z1*z2).ToString(invCult));
            Console.WriteLine("{0} / {1} = {2}", "z1", "z2", (z1/z2).ToString(invCult));

            Console.WriteLine("{0}^{1} = {2}", "z1", "z2", (z1.Power(z2)).ToString(invCult));

            Console.WriteLine("sine -> ComplexNumber.Sin({0}) = {1}", "z1", (ComplexNumber.Sin(z1).ToString(invCult)));
            Console.WriteLine("hyperbolic sine -> ComplexNumber.Sinh({0}) = {1}", "z1", (ComplexNumber.Sinh(z1).ToString(invCult)));
            Console.WriteLine("sine arch -> ComplexNumber.Asin({0}) = {1}", "z1", (ComplexNumber.Asin(z1).ToString(invCult)));
            Console.WriteLine("hyperbolic sine arch -> ComplexNumber.Asinh({0}) = {1}", "z1", (ComplexNumber.Asinh(z1).ToString(invCult)));
            Console.WriteLine("tangent -> ComplexNumber.Tan({0}) = {1}", "z1", (ComplexNumber.Tan(z1).ToString(invCult)));
            Console.WriteLine("hyperbolic tangent -> ComplexNumber.Tanh({0}) = {1}", "z1", (ComplexNumber.Tanh(z1).ToString(invCult)));
            Console.WriteLine("tangent arch -> ComplexNumber.Atan({0}) = {1}", "z1", (ComplexNumber.Atan(z1).ToString(invCult)));
            Console.WriteLine("hyperbolic tangent arch -> ComplexNumber.Atanh({0}) = {1}", "z1", (ComplexNumber.Atanh(z1).ToString(invCult)));

            Console.WriteLine("Exponential function (e^z1) -> ComplexNumber.Exp({0}) = {1}", "z1", (ComplexNumber.Exp(z1).ToString(invCult)));
            Console.WriteLine("Natural (base e) logarithm -> ComplexNumber.Log({0}) or ln(z1) = {1}", "z1", (ComplexNumber.Log(z1).ToString(invCult)));
            Console.WriteLine("Logarithm in base 2 -> ComplexNumber.Log2({0}) = {1}", "z1", (ComplexNumber.Log2(z1).ToString(invCult)));
            Console.WriteLine("Logarithm in base 10 -> ComplexNumber.Log10({0}) = {1}", "z1", (ComplexNumber.Log10(z1).ToString(invCult)));

            Console.WriteLine("Argument (phase of a complex number in radians) -> ComplexNumber.Arg({0}) = {1}", "z1", (ComplexNumber.Arg(z1).ToString(invCult)));
            Console.WriteLine("Factorial function -> ComplexNumber.Fact({0}) = {1}", "z1", (ComplexNumber.Fact(z1).ToString(invCult)));
            Console.WriteLine("Fibonacci number function -> ComplexNumber.Fib({0}) = {1}", "z1", (ComplexNumber.Fib(z1).ToString(invCult)));

            Console.WriteLine("Ceilling function -> ComplexNumber.Ceiling({0}) = {1}", "z1", (ComplexNumber.Ceiling(z1).ToString(invCult)));
            Console.WriteLine("Floor function -> ComplexNumber.Floor({0}) = {1}", "z1", (ComplexNumber.Floor(z1).ToString(invCult)));

            Console.WriteLine("Complex zero -> ComplexNumber.Zero = {0}", (ComplexNumber.Zero.ToString(invCult)));
            Console.WriteLine("Complex one -> ComplexNumber.One = {0}", (ComplexNumber.One.ToString(invCult)));
            Console.WriteLine("Complex imaginary one -> ComplexNumber.ImaginaryOne = {0}", (ComplexNumber.ImaginaryOne.ToString(invCult)));

            Console.WriteLine("Parse complex number string -> ComplexNumber.Parse({0}) = {1}", "\"(-3.45; -5.23)\"", (ComplexNumber.Parse("(-3.45; -5.23)", invCult).ToString(invCult)));

            Console.WriteLine("\nMany more functions are available ...\n");

        }

    }

}
