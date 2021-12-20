// <copyright file="ComplexExtensions.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
//
// Copyright (c) 2009-2010 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace ComplexN
{
    using System;
    using System.Collections.Generic;
	//using ArisMathLib.Common;
    //using ArisMathLib.SpecialFunc;
    using System.Runtime.CompilerServices;

#if !PORTABLE
    using System.Runtime;
    using ComplexN.Common;
#endif


    /// <summary>
    /// Extension methods for the Complex type provided by System.Numerics
    /// </summary>
    public static class ComplexExtensions
    {


        private static class Constants
        {
            /// <summary>The number sqrt(1/2) = 1/sqrt(2) = sqrt(2)/2</summary>
            public const double Sqrt1Over2 = 0.70710678118654752440084436210484903928483593768845d;

            /// <summary>The number pi*2</summary>
            public const double Pi2 = 6.2831853071795864769252867665590057683943387987502d;

            /// <summary>The number log[e](10)</summary>
            public const double Ln10 = 2.3025850929940456840179914546843642076011014886288d;

        }


        /// <summary>
        /// Numerically stable hypotenuse of a right angle triangle, i.e. <code>(a,b) -> sqrt(a^2 + b^2)</code>
        /// </summary>
        /// <param name="a">The length of side a of the triangle.</param>
        /// <param name="b">The length of side b of the triangle.</param>
        /// <returns>Returns <code>sqrt(a<sup>2</sup> + b<sup>2</sup>)</code> without underflow/overflow.</returns>
        private static double Hypotenuse(double a, double b)
        {
            if (Math.Abs(a) > Math.Abs(b))
            {
                double r = b / a;
                return Math.Abs(a) * Math.Sqrt(1 + (r * r));
            }

            if (b != 0.0)
            {
                // NOTE (ruegg): not "!b.AlmostZero()" to avoid convergence issues (e.g. in SVD algorithm)
                double r = a / b;
                return Math.Abs(b) * Math.Sqrt(1 + (r * r));
            }

            return 0d;
        }

        /// <summary>
        /// Gets the squared magnitude of the <c>Complex</c> number.
        /// </summary>
        /// <param name="complex">The <see cref="ComplexNumber"/> number to perfom this operation on.</param>
        /// <returns>The squared magnitude of the <c>Complex</c> number.</returns>
        public static double MagnitudeSquared(this ComplexNumber complex)
        {
            return (complex.Re * complex.Re) + (complex.Im * complex.Im);
        }

        /// <summary>
        /// Gets the unity of this complex (same argument, but on the unit circle; exp(I*arg))
        /// </summary>
        /// <returns>The unity of this <c>Complex</c>.</returns>
        public static ComplexNumber Sign(this ComplexNumber complex)
        {
            if (double.IsPositiveInfinity(complex.Re) && double.IsPositiveInfinity(complex.Im))
            {
                return new ComplexNumber(Constants.Sqrt1Over2, Constants.Sqrt1Over2);
            }

            if (double.IsPositiveInfinity(complex.Re) && double.IsNegativeInfinity(complex.Im))
            {
                return new ComplexNumber(Constants.Sqrt1Over2, -Constants.Sqrt1Over2);
            }

            if (double.IsNegativeInfinity(complex.Re) && double.IsPositiveInfinity(complex.Im))
            {
                return new ComplexNumber(-Constants.Sqrt1Over2, -Constants.Sqrt1Over2);
            }

            if (double.IsNegativeInfinity(complex.Re) && double.IsNegativeInfinity(complex.Im))
            {
                return new ComplexNumber(-Constants.Sqrt1Over2, Constants.Sqrt1Over2);
            }

            // don't replace this with "Magnitude"!
            var mod = Hypotenuse(complex.Re, complex.Im);
            if (mod == 0.0d)
            {
                return ComplexNumber.Zero;
            }

            return new ComplexNumber(complex.Re / mod, complex.Im / mod);
        }

        /// <summary>
        /// Gets the conjugate of the <c>Complex</c> number.
        /// </summary>
        /// <param name="complex">The <see cref="ComplexNumber"/> number to perfom this operation on.</param>
        /// <remarks>
        /// The semantic of <i>setting the conjugate</i> is such that
        /// <code>
        /// // a, b of type Complex32
        /// a.Conjugate = b;
        /// </code>
        /// is equivalent to
        /// <code>
        /// // a, b of type Complex32
        /// a = b.Conjugate
        /// </code>
        /// </remarks>
        /// <returns>The conjugate of the <see cref="Complex"/> number.</returns>
        //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ComplexNumber Conjugate(this ComplexNumber complex)
        {
            return ComplexNumber.Conjugate(complex);
        }

        /// <summary>
        /// Returns the multiplicative inverse of a complex number.
        /// </summary>
        //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ComplexNumber Reciprocal(this ComplexNumber complex)
        {
            return ComplexNumber.Reciprocal(complex);
        }

        /// <summary>
        /// Exponential of this <c>Complex</c> (exp(x), E^x).
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns>
        /// The exponential of this complex number.
        /// </returns>
        //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ComplexNumber Exp(this ComplexNumber complex)
        {
            return ComplexNumber.Exp(complex);
        }

        /// <summary>
        /// Natural Logarithm of this <c>Complex</c> (Base E).
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns>
        /// The natural logarithm of this complex number.
        /// </returns>
        //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ComplexNumber Ln(this ComplexNumber complex)
        {
            return ComplexNumber.Ln(complex);  // Compliant with Mathematica
            //return ComplexNumber.Log(complex);
        }

        /// <summary>
        /// Common Logarithm of this <c>Complex</c> (Base 10).
        /// </summary>
        /// <returns>The common logarithm of this complex number.</returns>
        //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ComplexNumber Log10(this ComplexNumber complex)
        {
            return ComplexNumber.Log10(complex);
        }

        /// <summary>
        /// Logarithm of this <c>Complex</c> with custom base.
        /// </summary>
        /// <returns>The logarithm of this complex number.</returns>
        //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static ComplexNumber Log(this ComplexNumber complex, double baseValue)
        {
            return Ln(complex) / Math.Log(baseValue); // ComplexNumber.LogBaseN(complex, baseValue);
        }

        /// <summary>
        /// Raise this <c>Complex</c> to the given value.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <param name="exponent">
        /// The exponent.
        /// </param>
        /// <returns>
        /// The complex number raised to the given exponent.
        /// </returns>
        public static ComplexNumber Power(this ComplexNumber complex, ComplexNumber exponent)
        {
            if (complex.IsZero())
            {
                if (exponent.IsZero())
                {
                    return ComplexNumber.One;
                }

                if (exponent.Re > 0d)
                {
                    return ComplexNumber.Zero;
                }

                if (exponent.Re < 0d)
                {
                    return exponent.Im == 0d
                        ? new ComplexNumber(double.PositiveInfinity, 0d)
                        : new ComplexNumber(double.PositiveInfinity, double.PositiveInfinity);
                }

                return new ComplexNumber(double.NaN, double.NaN);
            }

            return ComplexNumber.Pow(complex, exponent);
        }

        /// <summary>
        /// Raise this <c>Complex</c> to the inverse of the given value.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <param name="rootExponent">
        /// The root exponent.
        /// </param>
        /// <returns>
        /// The complex raised to the inverse of the given exponent.
        /// </returns>
        public static ComplexNumber Root(this ComplexNumber complex, ComplexNumber rootExponent)
        {
            return ComplexNumber.Pow(complex, 1.0d / rootExponent);
        }

        ///// <summary>
        ///// The Square (power 2) of this <c>Complex</c>
        ///// </summary>
        ///// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        ///// <returns>
        ///// The square of this complex number.
        ///// </returns>
        //public static ComplexNumber Square(this ComplexNumber complex)
        //{
        //    if (complex.IsReal())
        //    {
        //        return new ComplexNumber(complex.Re * complex.Re, 0.0);
        //    }

        //    return new ComplexNumber((complex.Re * complex.Re) - (complex.Im * complex.Im), 2 * complex.Re * complex.Im);
        //}


        /// <summary>
        /// Computes the square of a complex number.
        /// </summary>
        /// <param name="z">The argument.</param>
        /// <returns>The value of z<sup>2</sup>.</returns>
        /// <remarks>
        /// <para>Unlike <see cref="MoreMath.Sqr(double)"/>, there is slightly more to this method than shorthand for z * z.
        /// In terms of real and imaginary parts z = x + i y, the product z * z = (x * x - y * y) + i(x * y + x * y),
        /// which not only requires 6 flops to evaluate, but also computes the real part as an expression that can easily overflow
        /// or suffer from significant cancelation error. By instead computing z<sup>2</sup> via as (x - y) * (x + y) + i 2 * x * y,
        /// this method not only requires fewer flops but is also less subject to overflow and cancelation error. You should
        /// therefore generally favor the computation of z<sup>2</sup> using this method over its computation as z * z.</para> 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexNumber Square(this ComplexNumber z)
        {
            // This form has one less flop than z * z, and, more importantly, evaluates
            // (x - y) * (x + y) instead of x * x - y * y; the latter would be more
            // subject to cancelation errors and overflow or underflow 
            return (new ComplexNumber((z.Re - z.Im) * (z.Re + z.Im), 2.0 * z.Re * z.Im));
        }


        /// <summary>
        /// The Square Root (power 1/2) of this <c>Complex</c>
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns>
        /// The square root of this complex number.
        /// </returns>
        public static ComplexNumber SquareRoot(this ComplexNumber complex)
        {
            // Note: the following code should be equivalent to Complex.Sqrt(complex),
            // but it turns out that is implemented poorly in System.Numerics,
            // hence we provide our own implementation here. Do not replace.

            if (complex.IsRealNonNegative())
            {
                return new ComplexNumber(Math.Sqrt(complex.Re), 0.0);
            }

            ComplexNumber result;

            var absReal = Math.Abs(complex.Re);
            var absImag = Math.Abs(complex.Im);
            double w;
            if (absReal >= absImag)
            {
                var ratio = complex.Im / complex.Re;
                w = Math.Sqrt(absReal) * Math.Sqrt(0.5 * (1.0 + Math.Sqrt(1.0 + (ratio * ratio))));
            }
            else
            {
                var ratio = complex.Re / complex.Im;
                w = Math.Sqrt(absImag) * Math.Sqrt(0.5 * (Math.Abs(ratio) + Math.Sqrt(1.0 + (ratio * ratio))));
            }

            if (complex.Re >= 0.0)
            {
                result = new ComplexNumber(w, complex.Im / (2.0 * w));
            }
            else if (complex.Im >= 0.0)
            {
                result = new ComplexNumber(absImag / (2.0 * w), w);
            }
            else
            {
                result = new ComplexNumber(absImag / (2.0 * w), -w);
            }

            return result;
        }

        /// <summary>
        /// Evaluate all square roots of this <c>Complex</c>.
        /// </summary>
        public static Tuple<ComplexNumber, ComplexNumber> SquareRoots(this ComplexNumber complex)
        {
            var principal = SquareRoot(complex);
            return new Tuple<ComplexNumber, ComplexNumber>(principal, -principal);
        }

        /// <summary>
        /// Evaluate all cubic roots of this <c>Complex</c>.
        /// </summary>
        public static Tuple<ComplexNumber, ComplexNumber, ComplexNumber> CubicRoots(this ComplexNumber complex)
        {
            var r = Math.Pow(complex.Magnitude, 1d/3d);
            var theta = complex.Phase/3;
            const double shift = Constants.Pi2/3;
            return new Tuple<ComplexNumber, ComplexNumber, ComplexNumber>(
                ComplexNumber.FromPolarCoordinates(r, theta),
                ComplexNumber.FromPolarCoordinates(r, theta + shift),
                ComplexNumber.FromPolarCoordinates(r, theta - shift));
        }

        /// <summary>
        /// Gets a value indicating whether the <c>Complex32</c> is zero.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns><c>true</c> if this instance is zero; otherwise, <c>false</c>.</returns>
        public static bool IsZero(this ComplexNumber complex)
        {
            return complex.Re == 0.0 && complex.Im == 0.0;
        }

        /// <summary>
        /// Gets a value indicating whether the <c>Complex32</c> is one.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns><c>true</c> if this instance is one; otherwise, <c>false</c>.</returns>
        public static bool IsOne(this ComplexNumber complex)
        {
            return complex.Re == 1.0 && complex.Im == 0.0;
        }

        /// <summary>
        /// Gets a value indicating whether the <c>Complex32</c> is the imaginary unit.
        /// </summary>
        /// <returns><c>true</c> if this instance is ImaginaryOne; otherwise, <c>false</c>.</returns>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        public static bool IsImaginaryOne(this ComplexNumber complex)
        {
            return complex.Re == 0.0 && complex.Im == 1.0;
        }

        /// <summary>
        /// Gets a value indicating whether the provided <c>Complex32</c>evaluates
        /// to a value that is not a number.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns>
        /// <c>true</c> if this instance is <c>NaN</c>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static bool IsNaN(this ComplexNumber complex)
        {
            return double.IsNaN(complex.Re) || double.IsNaN(complex.Im);
        }

        /// <summary>
        /// Gets a value indicating whether the provided <c>Complex32</c> evaluates to an
        /// infinite value.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns>
        ///     <c>true</c> if this instance is infinite; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// True if it either evaluates to a complex infinity
        /// or to a directed infinity.
        /// </remarks>
        public static bool IsInfinity(this ComplexNumber complex)
        {
            return double.IsInfinity(complex.Re) || double.IsInfinity(complex.Im);
        }

        /// <summary>
        /// Gets a value indicating whether the provided <c>Complex32</c> is real.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns><c>true</c> if this instance is a real number; otherwise, <c>false</c>.</returns>
        public static bool IsReal(this ComplexNumber complex)
        {
            return complex.Im == 0.0;
        }

        /// <summary>
        /// Gets a value indicating whether the provided <c>Complex32</c> is real and not negative, that is &gt;= 0.
        /// </summary>
        /// <param name="complex">The <see cref="Complex"/> number to perfom this operation on.</param>
        /// <returns>
        ///     <c>true</c> if this instance is real nonnegative number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRealNonNegative(this ComplexNumber complex)
        {
            return complex.Im == 0.0f && (complex.Re >= 0);
        }

        /// <summary>
        /// Returns a Norm of a value of this type, which is appropriate for measuring how
        /// close this value is to zero.
        /// </summary>
        public static double Norm(this ComplexNumber complex)
        {
            return complex.MagnitudeSquared();
        }

        ///// <summary>
        ///// Returns a Norm of a value of this type, which is appropriate for measuring how
        ///// close this value is to zero.
        ///// </summary>
        //public static double Norm(this Complex32 complex)
        //{
        //    return complex.MagnitudeSquared;
        //}

        /// <summary>
        /// Returns a Norm of the difference of two values of this type, which is
        /// appropriate for measuring how close together these two values are.
        /// </summary>
        public static double NormOfDifference(this ComplexNumber complex, ComplexNumber otherValue)
        {
            return (complex - otherValue).MagnitudeSquared();
        }

        ///// <summary>
        ///// Returns a Norm of the difference of two values of this type, which is
        ///// appropriate for measuring how close together these two values are.
        ///// </summary>
        //public static double NormOfDifference(this Complex32 complex, Complex32 otherValue)
        //{
        //    return (complex - otherValue).MagnitudeSquared;
        //}

        /// <summary>
        /// Creates a complex number based on a string. The string can be in the
        /// following formats (without the quotes): 'n', 'ni', 'n +/- ni',
        /// 'ni +/- n', 'n,n', 'n,ni,' '(n,n)', or '(n,ni)', where n is a double.
        /// </summary>
        /// <returns>
        /// A complex number containing the value specified by the given string.
        /// </returns>
        /// <param name="value">
        /// The string to parse.
        /// </param>
        public static ComplexNumber ToComplex(this string value)
        {
            return value.ToComplex(null);
        }

        /// <summary>
        /// Creates a complex number based on a string. The string can be in the
        /// following formats (without the quotes): 'n', 'ni', 'n +/- ni',
        /// 'ni +/- n', 'n,n', 'n,ni,' '(n,n)', or '(n,ni)', where n is a double.
        /// </summary>
        /// <returns>
        /// A complex number containing the value specified by the given string.
        /// </returns>
        /// <param name="value">
        /// the string to parse.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> that supplies culture-specific
        /// formatting information.
        /// </param>
        public static ComplexNumber ToComplex(this string value, IFormatProvider formatProvider)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            value = value.Trim();
            if (value.Length == 0)
            {
                throw new FormatException();
            }

            // strip out parens
            if (value.StartsWith("(", StringComparison.Ordinal))
            {
                if (!value.EndsWith(")", StringComparison.Ordinal))
                {
                    throw new FormatException();
                }

                value = value.Substring(1, value.Length - 2).Trim();
            }

            // keywords
            var numberFormatInfo = formatProvider.GetNumberFormatInfo();
            var textInfo = formatProvider.GetTextInfo();
            var keywords =
                new[]
                {
                    textInfo.ListSeparator, numberFormatInfo.NaNSymbol,
                    numberFormatInfo.NegativeInfinitySymbol, numberFormatInfo.PositiveInfinitySymbol,
                    "+", "-", "i", "j"
                };

            // lexing
            var tokens = new LinkedList<string>();
            GlobalizationHelper.Tokenize(tokens.AddFirst(value), keywords, 0);
            var token = tokens.First;

            // parse the left part
            bool isLeftPartImaginary;
            var leftPart = ParsePart(ref token, out isLeftPartImaginary, formatProvider);
            if (token == null)
            {
                return isLeftPartImaginary ? new ComplexNumber(0, leftPart) : new ComplexNumber(leftPart, 0);
            }

            // parse the right part
            if (token.Value == textInfo.ListSeparator)
            {
                // format: real,imag
                token = token.Next;

                if (isLeftPartImaginary)
                {
                    // left must not contain 'i', right doesn't matter.
                    throw new FormatException();
                }

                bool isRightPartImaginary;
                var rightPart = ParsePart(ref token, out isRightPartImaginary, formatProvider);

                return new ComplexNumber(leftPart, rightPart);
            }
            else
            {
                // format: real + imag
                bool isRightPartImaginary;
                var rightPart = ParsePart(ref token, out isRightPartImaginary, formatProvider);

                if (!(isLeftPartImaginary ^ isRightPartImaginary))
                {
                    // either left or right part must contain 'i', but not both.
                    throw new FormatException();
                }

                return isLeftPartImaginary ? new ComplexNumber(rightPart, leftPart) : new ComplexNumber(leftPart, rightPart);
            }
        }

        /// <summary>
        /// Parse a part (real or complex) from a complex number.
        /// </summary>
        /// <param name="token">Start Token.</param>
        /// <param name="imaginary">Is set to <c>true</c> if the part identified itself as being imaginary.</param>
        /// <param name="format">
        /// An <see cref="IFormatProvider"/> that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns>Resulting part as double.</returns>
        /// <exception cref="FormatException"/>
        private static double ParsePart(ref LinkedListNode<string> token, out bool imaginary, IFormatProvider format)
        {
            imaginary = false;
            if (token == null)
            {
                throw new FormatException();
            }

            // handle prefix modifiers
            if (token.Value == "+")
            {
                token = token.Next;

                if (token == null)
                {
                    throw new FormatException();
                }
            }

            var negative = false;
            if (token.Value == "-")
            {
                negative = true;
                token = token.Next;

                if (token == null)
                {
                    throw new FormatException();
                }
            }

            // handle prefix imaginary symbol
            if (String.Compare(token.Value, "i", StringComparison.OrdinalIgnoreCase) == 0
                || String.Compare(token.Value, "j", StringComparison.OrdinalIgnoreCase) == 0)
            {
                imaginary = true;
                token = token.Next;

                if (token == null)
                {
                    return negative ? -1 : 1;
                }
            }

#if PORTABLE
            var value = GlobalizationHelper.ParseDouble(ref token);
#else
            var value = GlobalizationHelper.ParseDouble(ref token, format.GetCultureInfo());
#endif

            // handle suffix imaginary symbol
            if (token != null && (String.Compare(token.Value, "i", StringComparison.OrdinalIgnoreCase) == 0
                                  || String.Compare(token.Value, "j", StringComparison.OrdinalIgnoreCase) == 0))
            {
                if (imaginary)
                {
                    // only one time allowed: either prefix or suffix, or neither.
                    throw new FormatException();
                }

                imaginary = true;
                token = token.Next;
            }

            return negative ? -value : value;
        }

        /// <summary>
        /// Converts the string representation of a complex number to a double-precision complex number equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A string containing a complex number to convert.
        /// </param>
        /// <param name="result">
        /// The parsed value.
        /// </param>
        /// <returns>
        /// If the conversion succeeds, the result will contain a complex number equivalent to value.
        /// Otherwise the result will contain Complex.Zero.  This parameter is passed uninitialized.
        /// </returns>
        public static bool TryToComplex(this string value, out ComplexNumber result)
        {
            return value.TryToComplex(null, out result);
        }

        /// <summary>
        /// Converts the string representation of a complex number to double-precision complex number equivalent.
        /// A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">
        /// A string containing a complex number to convert.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information about value.
        /// </param>
        /// <param name="result">
        /// The parsed value.
        /// </param>
        /// <returns>
        /// If the conversion succeeds, the result will contain a complex number equivalent to value.
        /// Otherwise the result will contain complex32.Zero.  This parameter is passed uninitialized
        /// </returns>
        public static bool TryToComplex(this string value, IFormatProvider formatProvider, out ComplexNumber result)
        {
            bool ret;
            try
            {
                result = value.ToComplex(formatProvider);
                ret = true;
            }
            catch (ArgumentNullException)
            {
                result = ComplexNumber.Zero;
                ret = false;
            }
            catch (FormatException)
            {
                result = ComplexNumber.Zero;
                ret = false;
            }

            return ret;
        }

        ///// <summary>
        ///// Creates a <c>Complex32</c> number based on a string. The string can be in the
        ///// following formats (without the quotes): 'n', 'ni', 'n +/- ni',
        ///// 'ni +/- n', 'n,n', 'n,ni,' '(n,n)', or '(n,ni)', where n is a double.
        ///// </summary>
        ///// <returns>
        ///// A complex number containing the value specified by the given string.
        ///// </returns>
        ///// <param name="value">
        ///// the string to parse.
        ///// </param>
        //public static Complex32 ToComplex32(this string value)
        //{
        //    return Complex32.Parse(value);
        //}

        ///// <summary>
        ///// Creates a <c>Complex32</c> number based on a string. The string can be in the
        ///// following formats (without the quotes): 'n', 'ni', 'n +/- ni',
        ///// 'ni +/- n', 'n,n', 'n,ni,' '(n,n)', or '(n,ni)', where n is a double.
        ///// </summary>
        ///// <returns>
        ///// A complex number containing the value specified by the given string.
        ///// </returns>
        ///// <param name="value">
        ///// the string to parse.
        ///// </param>
        ///// <param name="formatProvider">
        ///// An <see cref="IFormatProvider"/> that supplies culture-specific
        ///// formatting information.
        ///// </param>
        //public static Complex32 ToComplex32(this string value, IFormatProvider formatProvider)
        //{
        //    return Complex32.Parse(value, formatProvider);
        //}

        ///// <summary>
        ///// Converts the string representation of a complex number to a single-precision complex number equivalent.
        ///// A return value indicates whether the conversion succeeded or failed.
        ///// </summary>
        ///// <param name="value">
        ///// A string containing a complex number to convert.
        ///// </param>
        ///// <param name="result">
        ///// The parsed value.
        ///// </param>
        ///// <returns>
        ///// If the conversion succeeds, the result will contain a complex number equivalent to value.
        ///// Otherwise the result will contain complex32.Zero.  This parameter is passed uninitialized.
        ///// </returns>
        //public static bool TryToComplex32(this string value, out Complex32 result)
        //{
        //    return Complex32.TryParse(value, out result);
        //}

        ///// <summary>
        ///// Converts the string representation of a complex number to single-precision complex number equivalent.
        ///// A return value indicates whether the conversion succeeded or failed.
        ///// </summary>
        ///// <param name="value">
        ///// A string containing a complex number to convert.
        ///// </param>
        ///// <param name="formatProvider">
        ///// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information about value.
        ///// </param>
        ///// <param name="result">
        ///// The parsed value.
        ///// </param>
        ///// <returns>
        ///// If the conversion succeeds, the result will contain a complex number equivalent to value.
        ///// Otherwise the result will contain Complex.Zero.  This parameter is passed uninitialized.
        ///// </returns>
        //public static bool TryToComplex32(this string value, IFormatProvider formatProvider, out Complex32 result)
        //{
        //    return Complex32.TryParse(value, formatProvider, out result);
        //}
    }
}