using System;
using System.Text;
//using ArisMathLib.Common;
using System.Runtime.CompilerServices;
using ComplexN.Common;

namespace ComplexN
{


    /// <summary>
    /// Represents a complex number.
    /// </summary>
    public struct ComplexNumber : IEquatable<ComplexNumber>, IConvertible, IFormattable
    {


        #region MathUtils class implementation


        /// <summary>
        /// Math utililty functions used by complex functions.
        /// </summary>
        private static class MathUtils
        {

            /// <summary>The number pi/2</summary>
            private const double PiOver2 = 1.5707963267948966192313216916397514420985846996876d;

            /// <summary>The number pi/4</summary>
            private const double PiOver4 = 0.78539816339744830961566084581987572104929234984378d;

            /// <summary>The number pi</summary>
            private const double Pi = 3.1415926535897932384626433832795028841971693993751d;

            /// <summary>The number (pi)/180 - factor to convert from Degree (deg) to Radians (rad).</summary>
            private const double Degree = 0.017453292519943295769236907684886127134428718885417d;

            /// <summary>The number (pi)/200 - factor to convert from NewGrad (grad) to Radians (rad).</summary>
            private const double Grad = 0.015707963267948966192313216916397514420985846996876d;

            private const double C_PI_DIV_180 = Degree; // Convert from degree to radians //0.017453292519943295769236907684886127134428718885417d;
            private const double C_180_DIV_PI = 57.295779513082320876798154814105d;

            private const double C_PI_DIV_200 = Grad;    //0.015707963267948966192313216916397514420985846996876d;
            private const double C_200_DIV_PI = 63.66197723675813430755350534900574481378385829618257949906693d;    // 63.661977236758134307553505349006;

            private const double C_PI_DIV_2 = PiOver2;   // 1.5707963267948966192313216916397514420985846996875529d;
            private const double C_PI_DIV_4 = PiOver4;   // 0.78539816339744830961566084581987572104929234984378d; 0.7853981633974483096156608458198757210492923498437764d;
            private const double C_PI = Pi;  // 3.1415926535897932384626433832795028841971693993751d; 3.1415926535897932384626433832795028841971693993751058;


            /// <summary>
            /// Computes the sign of a specified number.
            /// </summary>
            /// <param name="x">The source argument number.</param>
            /// <returns>Returns '0' if the given number is zero, '-1' if is negative, '1' if is positive, double.NaN if is indetermined.</returns>
            internal static double Sign(double x)
            {
                double FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (x == 0)
                            FResult = (double)0.0;
                        else if (x > 0)
                            FResult = (double)1.0;
                        else if (x < 0)
                            FResult = (double)-1.0;
                        else if (double.IsNegativeInfinity(x))
                            FResult = (double)-1.0;
                        else if (double.IsPositiveInfinity(x))
                            FResult = (double)1.0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Inverse sine function.
            /// </summary>
            /// <param name="x">Angle in radians.</param>
            /// <returns>Function result.</returns>
            /// <remarks>ArcSin = Math.Asin for x in domain [-1..1].</remarks>
            internal static object ArcSin(double x)
            {
                object FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (x == -1)
                            FResult = -C_PI_DIV_2;
                        else if (x == 0)
                            FResult = (double)0.0;
                        else if (x == 1)
                            FResult = C_PI_DIV_2;
                        else if ((x > -1) && (x < 1))
                            FResult = Math.Asin(x);
                        else
                        {
                            //x domain gives a complex number result.
                            ComplexNumber z = new ComplexNumber(x, 0);
                            FResult = ComplexNumber.Asin(z);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Inverse cosine function.
            /// </summary>
            /// <param name="x">Angle in radians.</param>
            /// <returns>Function result.</returns>
            /// <remarks>Arccos = Math.Acos for x in domain [-1..1].</remarks>
            internal static object ArcCos(double x)
            {
                object FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (x == -1)
                            FResult = C_PI;
                        else if (x == 0)
                            FResult = C_PI_DIV_2;
                        else if (x == 1)
                            FResult = (double)0.0;
                        else if ((x > -1) && (x < 1))
                            FResult = Math.Acos(x);
                        else
                        {
                            //x domain gives a complex number result.
                            ComplexNumber z = new ComplexNumber(x, 0);
                            FResult = ComplexNumber.Acos(z);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Arc Tangent function.
            /// </summary>
            /// <param name="x">Value of the trignometric function (Real number).</param>
            /// <returns>Angle in radians.</returns>
            internal static double ArcTan(double x)
            {
                double FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (x == -1)
                            FResult = -C_PI_DIV_4;

                        else if (x == 0)
                            FResult = (double)0.0;

                        else if (x == 1)
                            FResult = C_PI_DIV_4;

                        else if (double.IsPositiveInfinity(x))
                            FResult = C_PI_DIV_2;

                        else if (double.IsNegativeInfinity(x))
                            FResult = -C_PI_DIV_2;

                        else
                        {
                            FResult = Math.Atan(x);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Arc CoTangent function.
            /// </summary>
            /// <param name="x">Value of the trignometric function (Real number).</param>
            /// <returns>Angle in radians.</returns>
            internal static double ArcCot(double x)
            {
                double FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (double.IsPositiveInfinity(x))
                            FResult = (double)0.0;

                        else if (double.IsNegativeInfinity(x))
                            FResult = (double)0.0;

                        else if (x == 0)
                            FResult = C_PI_DIV_2;

                        else if (x == -1)
                            FResult = -C_PI_DIV_4;

                        else if (x == 1)
                            FResult = C_PI_DIV_4;

                        else
                            FResult = Math.Atan(1.0 / x);
                        //FResult = (double)1.0 / Cot(x);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Inverse secant function.
            /// </summary>
            /// <param name="x">Angle in radians.</param>
            /// <returns>Returns the inverse secant calculation if succeeded, NaN otherwise.</returns>
            /// <remarks>Arcsec = ArcCos(1/x) for x<=-1 or x>=1 </remarks>
            internal static object ArcSec(double x)
            {
                object FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (double.IsPositiveInfinity(x))
                            FResult = C_PI_DIV_2;

                        else if (double.IsNegativeInfinity(x))
                            FResult = C_PI_DIV_2;

                        else if ((x < -1) || (x > 1))     //Valid domain
                            FResult = Math.Acos(1 / x);

                        else if (x == -1)
                            FResult = C_PI;

                        else if (x == 1)
                            FResult = (double)0.0;

                        else if (x == 0)
                            FResult = double.PositiveInfinity;
                        else
                        {
                            //  -1 < x < 1
                            //Result is a complex number.
                            ComplexNumber z = new ComplexNumber(x, 0);
                            FResult = ComplexNumber.Asec(z);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Inverse hiperbolic secant function.
            /// </summary>
            /// <param name="x">Angle in radians.</param>
            /// <returns>Function result.</returns>
            /// <remarks>Arcsech = ln(1/x + sqrt(1/x + 1)*sqrt(1/x - 1))</remarks>
            internal static object ArcSech(double x)
            {
                object FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (x == 0)
                            FResult = double.PositiveInfinity;
                        else if (x == 1)
                            FResult = (double)0.0;
                        //else if (x == -1)
                        //    FResult = new ComplexNumber(0, C_PI);
                        else if ((x > 0) && (x < 1))
                            FResult = Math.Log(1.0 / x + Math.Sqrt(1.0 / x + 1) * Math.Sqrt(1.0 / x - 1));
                        else
                        {
                            //X domain in (-inf..0) or (1..+inf) --> Complex number
                            ComplexNumber z = new ComplexNumber(x, 0);
                            FResult = ComplexNumber.Asech(z);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Inverse cosecant function.
            /// </summary>
            /// <param name="x">The function argument.</param>
            /// <returns>Returns the inverse cosecant calculation if succeeded, NaN otherwise.</returns>
            /// <remarks>ArcCosec = ArcSin(1/x) for x domain = (-inf..-1] or [1..+inf).</remarks>
            internal static object ArcCosec(double x)
            {
                object FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (double.IsPositiveInfinity(x))
                            FResult = (double)0.0;    // +0

                        else if (double.IsNegativeInfinity(x))
                            FResult = (double)0.0;    // -0

                        else if (x == -1)
                            FResult = -C_PI_DIV_2;

                        else if (x == 0)
                            FResult = double.PositiveInfinity;

                        else if (x == 1)
                            FResult = C_PI_DIV_2;

                        else if ((x < -1) || (x > 1))     //Valid domain for real numbers result.
                            FResult = Math.Asin(1 / x);

                        else
                        {
                            ComplexNumber z = new ComplexNumber(x, 0);
                            FResult = ComplexNumber.Acosec(z);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Inverse hiperbolic cosecant function.
            /// </summary>
            /// <param name="x">Angle in radians.</param>
            /// <returns>Function result.</returns>
            /// <remarks>ArcCosech = ln(1/x + sqrt(1/x^2 + 1))</remarks>
            internal static double ArcCosech(double x)
            {
                double FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (double.IsInfinity(x))
                            FResult = 0;
                        else if (x == 0)
                            FResult = double.PositiveInfinity;
                        else
                            FResult = Math.Log(1.0 / x + Math.Sqrt(1.0 / Math.Pow(x, 2) + 1));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Factorial function.
            /// </summary>
            /// <param name="x">Function argument.</param>
            /// <returns>Function result.</returns>
            internal static BigInteger Fact_BigInt(int x)
            {
                BigInteger temp = new BigInteger(1);
                try
                {
                    if (x > 0)
                    {
                        for (int i = 1; i <= x; i++)
                            temp *= i;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return temp;
            }


            /// <summary>
            /// Factorial function.
            /// </summary>
            /// <param name="x">Function argument.</param>
            /// <returns>Function result.</returns>
            internal static double Fact_Double(double x)
            {
                double FResult = double.NaN;
                BigInteger temp = null;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (double.IsPositiveInfinity(x))
                            FResult = double.PositiveInfinity;

                        else if (double.IsNegativeInfinity(x))
                            FResult = double.NaN;

                        else if (MathUtils.IsIntegerNumber(x))
                        {
                            if (x >= 0)
                            {
                                temp = Fact_BigInt((int)Math.Floor(x));
                                FResult = double.Parse(temp.ToString());
                            }
                            else
                            {
                                FResult = double.PositiveInfinity;
                            }
                        }
                        else
                        {
                            FResult = Gammafunc.Gammafunction(1 + x);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Checks if a given number belongs to the Integer numbers set.
            /// </summary>
            /// <param name="n">The source number to check.</param>
            /// <returns>True if the number belongs to the Integers numbers set, i.e. his decimal part is zero, false otherwise.</returns>
            internal static bool IsIntegerNumber(double n)
            {
                bool FResult = false;
                try
                {
                    FResult = Math.Floor(n) == n;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }

            /// <summary>
            /// Computes the natural logarithm of a specified number.
            /// </summary>
            /// <param name="x">The source argument number.</param>
            /// <returns>Returns the natural logarithm of a specified number if succeeded, double.NaN otherwise.</returns>
            internal static object Ln(double x)
            {
                object FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (double.IsInfinity(x))
                            FResult = double.PositiveInfinity;

                        else if (x == 0)
                            FResult = double.NegativeInfinity;

                        else if (x < 0)
                        {
                            //x in domain for complex numbers.
                            FResult = ComplexNumber.Ln(x);
                        }
                        else
                        {
                            // x > 0
                            FResult = Math.Log(x);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Computes the square root of a specified number.
            /// </summary>
            /// <param name="x">The source input number.</param>
            /// <returns>Returns the square root of a specified number if succeeded, double.NaN otherwise.</returns>
            internal static object Sqrt(double x)
            {
                object FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (double.IsPositiveInfinity(x))
                            FResult = double.PositiveInfinity;

                        else if (x == 0)
                            FResult = (double)0.0;

                        else if ((x < 0) || (double.IsNegativeInfinity(x)))
                        {
                            //Complex numbers domain.
                            FResult = ComplexNumber.Sqrt(new ComplexNumber(x, 0));
                        }
                        else
                        {
                            //Real numbers domain.
                            FResult = Math.Sqrt(x);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


            /// <summary>
            /// Computes the base 10 logarithm of a given number.
            /// </summary>
            /// <param name="x">The source argument number.</param>
            /// <returns>Returns the base 10 logarithm of a given number if succeeded, double.NaN otherwise.</returns>
            internal static object Log10(double x)
            {
                object FResult = double.NaN;
                try
                {
                    if (!double.IsNaN(x))
                    {
                        if (double.IsInfinity(x))
                            FResult = double.PositiveInfinity;

                        else if (x == 0)
                            FResult = double.NegativeInfinity;

                        else if (x < 0)
                        {
                            FResult = ComplexNumber.Log10(x);
                        }
                        else
                        {
                            FResult = Math.Log10(x);
                        }

                        //if (arg1 < 0)
                        //    FResult = ComplexNumber.Log10(arg1);
                        //else
                        //    FResult = Math.Log10(arg1);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return FResult;
            }


        }   //  class MathUtils ...


        #endregion MathUtils class implementation



        public const char C_ImSym_i = 'i';
        public const char C_ImSym_j = 'j';
        public const char C_PolarAngleSym_A = 'A';

        private double mRe;
        private double mIm;
        private double mMagnitude;
        private double mPhase;

        //private string mPolarFrmt = "Pol";
        private static char mImUnitSymbol = 'i';
        private static char mPolarAngleSymbol = 'A';


        // Special print formats
        public const string Frmt_abi = "I";     // a+bi format
        public const string Frmt_abj = "J";     // a+bj format
        public const string Frmt_rAo_Rad = "AR";   // rAO in radians format
        public const string Frmt_rAo_Deg = "AD";   // rAO in degrees format
        public const string Frmt_rAo_Gra = "AG";   // rAO in grads format

        //private int mFormatPrecision = -1;

        /// <summary>The number sqrt(1/2) = 1/sqrt(2) = sqrt(2)/2</summary>
        internal const double Sqrt1Over2 = 0.70710678118654752440084436210484903928483593768845d;

        /// <summary>The number pi*2</summary>
        internal const double Pi2 = 6.2831853071795864769252867665590057683943387987502d;

        /// <summary>The number log[e](10)</summary>
        internal const double Ln10 = 2.3025850929940456840179914546843642076011014886288d;

        /// <summary>The number log[e](2)</summary>
        internal const double Ln2 = 0.69314718055994530941723212145817656807550013436026d;


        //Some constants to speed up angle calculations.
        private const double C_PIdiv2 = 1.5707963267948966192313216916397514420985846996875529;
        private const double C_180divPI = 57.29577951308232087679815481410517033240547246656432154916024;
        private const double C_PIdiv180 = 0.017453292519943295769236907684886127134428718885417254560971;
        private const double C_PIdiv200 = 0.015707963267948966192313216916397514420985846996875529104874;
        private const double C_200divPI = 63.66197723675813430755350534900574481378385829618257949906693;
        private static double C_PI = 3.1415926535897932384626433832795028841971693993751058;

        public const string mSquareRootSymbol = "√";

        //Used for the LnGamma computations.
        public static double ACCURACY = 1E-10; //1e-10;

        /// <summary>
        /// Gets the square root of negative one.
        /// </summary>
        public static readonly ComplexNumber I = new ComplexNumber(0.0, 1.0);


       
        #region Constructor


        public ComplexNumber(double re, double im)
        {
            this.mRe = re;
            this.mIm = im;

            this.mMagnitude = Abs(re, im);
            this.mPhase = ComputeArg(re, im);
        }


        #endregion Constructor


        
        #region properties


        ///// <summary>
        ///// Gets or sets the
        ///// </summary>
        //public int FormatPrecision
        //{
        //    get { return this.mFormatPrecision; }
        //    set { this.mFormatPrecision = value; }
        //}


        /// <summary>
        /// Gets the phase (in radians) of a complex number.
        /// </summary>
        public double Phase
        {
            get { return this.mPhase; }
        }


        /// <summary>
        /// Gets the magnitude (or absolute value) of a complex number.
        /// </summary>
        public double Magnitude
        {
            get { return this.mMagnitude; }
        }


        /// <summary>
        /// Gets the real part of the complex number.
        /// </summary>
        public double Re
        {
            get { return this.mRe; }
            set // Needed for serialization.
            {
                if (this.mRe != value)
                {
                    this.mRe = value;
                    this.mMagnitude = Abs(this);
                    this.mPhase = Math.Atan2(this.mIm, this.mRe);
                }
            }
        }


        /// <summary>
        /// Gets the real part of the complex number.
        /// </summary>
        public double Real
        {
            get { return this.Re; }
            set // Needed for serialization.
            {
                this.Re = value;
                //if (this.mRe != value)
                //{
                //    this.mRe = value;
                //    this.mMagnitude = Abs(this);
                //    this.mPhase = Math.Atan2(this.mIm, this.mRe);
                //}
            }
        }


        /// <summary>
        /// Gets the imaginary part of the complex number.
        /// </summary>
        public double Im
        {
            get { return this.mIm; }
            set
            {
                if (this.mIm != value)
                {
                    this.mIm = value;
                    this.mMagnitude = Abs(this);
                    this.mPhase = Math.Atan2(this.mIm, this.mRe);
                }
            }
        }


        /// <summary>
        /// Gets the imaginary part of the complex number.
        /// </summary>
        public double Imaginary
        {
            get { return this.Im; }
            set
            {
                this.Im = value;
            }
        }


        /// <summary>
        /// Returns a new Complex instance with a real number equal to one and an imaginary number equal to zero.
        /// </summary>
        public static ComplexNumber One
        {
            get { return new ComplexNumber(1, 0); }
        }


        /// <summary>
        /// Returns a new Complex instance with a real number equal to zero and an imaginary number equal to one.
        /// </summary>
        public static ComplexNumber ImaginaryOne
        {
            get { return new ComplexNumber(0, 1); }
        }


        /// <summary>
        /// Returns a new ComplexNumber instance with a real number equal to zero and an imaginary number equal to zero.
        /// </summary>
        public static ComplexNumber Zero
        {
            get { return new ComplexNumber(0, 0); }
        }


        #endregion properties




        #region Operators


        ///// <summary>
        ///// Defines an implicit conversion of an object instance to a complex number.
        ///// </summary>
        ///// <param name="value">The value to convert to a complex number.</param>
        ///// <returns>An object that contains the real and imaginary part.</returns>
        //public static explicit operator ComplexNumber(object value)
        //{
        //    return ComplexNumber.Parse(value.ToString());
        //}


        ///// <summary>
        ///// Defines an explicit conversion of a double-precision floating-point number to a complex number.
        ///// </summary>
        ///// <param name="value">The value to convert to a complex number.</param>
        ///// <returns>An object that contains the value of the value parameter as its real part and zero as its imaginary part.</returns>
        //public static explicit operator ComplexNumber(double value)
        //{
        //    return new ComplexNumber(value, 0);
        //}


        /// <summary>
        /// Defines an implicit conversion of a double-precision floating-point number to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>An object that contains the value of the value parameter as its real part and zero as its imaginary part.</returns>
        public static implicit operator ComplexNumber(double value)
        {
            return new ComplexNumber(value, 0);
        }


        /// <summary>
        /// Defines an implicit conversion of a 32-bit signed integer to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>An object that contains the value of the value parameter as its real part and zero as its imaginary part.</returns>
        public static implicit operator ComplexNumber(int value)
        {
            return new ComplexNumber(value, 0);
        }


        /// <summary>
        /// Defines an implicit conversion of a 64-bit signed integer to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>An object that contains the value of the value parameter as its real part and zero as its imaginary part.</returns>
        public static implicit operator ComplexNumber(long value)
        {
            return new ComplexNumber(value, 0);
        }


        /// <summary>
        /// Defines an implicit conversion of an unsigned byte to a complex number.
        /// </summary>
        /// <param name="value">The value to convert to a complex number.</param>
        /// <returns>An object that contains the value of the value parameter as its real part and zero as its imaginary part.</returns>
        public static implicit operator ComplexNumber(byte value)
        {
            return new ComplexNumber(value, 0);
        }


        /// <summary>
        /// Tests the equality of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>True if the two complex numbers are equal, otherwise false.</returns>
        public static bool operator ==(ComplexNumber z1, ComplexNumber z2)
        {
            return z1.Equals(z2);
        }


        /// <summary>
        /// Tests the inequality of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>False if the two complex numbers are equal, otherwise true.</returns>
        public static bool operator !=(ComplexNumber z1, ComplexNumber z2)
        {
            return !z1.Equals(z2);
        }


        /// <summary>
        /// Lesser operator for comparison of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>Returns the boolean result of the comparison of magnitudes of the two complex numbers.</returns>
        public static bool operator <(ComplexNumber z1, ComplexNumber z2)
        {
            bool FResult;
            try
            {
                FResult = z1.Magnitude < z2.Magnitude;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Greater operator for comparison of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>Returns the boolean result of the comparison of magnitudes of the two complex numbers.</returns>
        public static bool operator >(ComplexNumber z1, ComplexNumber z2)
        {
            bool FResult;
            try
            {
                FResult = z1.Magnitude > z2.Magnitude;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Less or equal to operator for comparison of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>Returns the boolean result of the comparison less or equal to of magnitudes of the two complex numbers.</returns>
        public static bool operator <=(ComplexNumber z1, ComplexNumber z2)
        {
            bool FResult;
            try
            {
                FResult = (z1.Magnitude < z2.Magnitude) || (z1 == z2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Greater or equal to operator for comparison of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>Returns the boolean result of the comparison greater or equal to of magnitudes of the two complex numbers.</returns>
        public static bool operator >=(ComplexNumber z1, ComplexNumber z2)
        {
            bool FResult;
            try
            {
                FResult = (z1.Magnitude > z2.Magnitude) || (z1 == z2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Addition operator for the sum of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>The result of the sum of the two complex numbers.</returns>
        public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(z1.Re + z2.Re, z1.Im + z2.Im);                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }





        /// <summary>
        ///  The minus operator for the subtraction of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>The result of the subtraction of the two complex numbers.</returns>
        public static ComplexNumber operator -(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(z1.Re - z2.Re, z1.Im - z2.Im);                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Fixs the product result of two complex numbers when NaN.
        /// </summary>
        /// <returns>The product result fixed.</returns>
        /// <param name="result">The product result.</param>
        /// <param name="z1">The first term.</param>
        /// <param name="z2">The second term.</param>
        private static ComplexNumber FixNaNProductResult(
                                            ComplexNumber result, 
                                            ComplexNumber z1, ComplexNumber z2)
        {
            // we assume that 0*inf = 0 
            // (people say result should be undefined, but this gives more 
            // consistent and expected commonsense results)
            if (double.IsNaN(result.Re))
                if (((z1.Re == 0) || (z2.Re == 0)) || ((z1.Im == 0) || (z2.Im == 0)))
                    result.Re = 0;

            if (double.IsNaN(result.Im))
                if (((z1.Re == 0) || (z2.Re == 0)) || ((z1.Im == 0) || (z2.Im == 0)))
                    result.Im = 0;

            return result;
        }


        /// <summary>
        /// Fixs the product result of two complex numbers when NaN.
        /// </summary>
        /// <returns>The product result fixed.</returns>
        /// <param name="result">The product result.</param>
        /// <param name="d">The first term.</param>
        /// <param name="z">The second term.</param>
        private static ComplexNumber FixNaNProductResult(
                                            ComplexNumber result,
                                            double d, ComplexNumber z)
        {
            // we assume that 0*inf = 0 
            // (people say result should be undefined, but this gives more 
            // consistent and expected commonsense results)
            if (double.IsNaN(result.Re))
                if ((d == 0) || (z.Re == 0))
                    result.Re = 0;

            if (double.IsNaN(result.Im))
                if ((d == 0) || (z.Im == 0))
                    result.Im = 0;

            return result;
        }


        /// <summary>
        ///  The multiply operator for the multiplication of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>The result of the multiplication of the two complex numbers.</returns>
        public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber FResult;
            try
            {
                //(ac - bd) + (ad + bc)i
                FResult = new ComplexNumber(z1.Re * z2.Re - z1.Im * z2.Im, z1.Re * z2.Im + z1.Im * z2.Re);

                // we assume that 0*inf = 0 
                // (people say result should be undefined, but this gives more 
                // consistent and expected commonsense results)
                FResult = FixNaNProductResult(FResult, z1, z2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        ///  The multiply operator for the multiplication of a real number by a complex number.
        /// </summary>
        /// <param name="x">The real value to multiply.</param>
        /// <param name="z">The complex number to multiply.</param>
        /// <returns>The result of the multiplication of a real number by a complex number.</returns>
        public static ComplexNumber operator *(double x, ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(x * z.Re, x * z.Im);
                // we assume that 0*inf = 0 
                // (people say result should be undefined, but this gives more 
                // consistent and expected commonsense results)
                FResult = FixNaNProductResult(FResult, x, z);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        ///  The multiply operator for the multiplication of a complex numbers by a real number.
        /// </summary>        
        /// <param name="z">The complex number to multiply.</param>
        /// <param name="x">The real value to multiply.</param>
        /// <returns>The result of the multiplication of a complex numbers by a real number.</returns>
        public static ComplexNumber operator *(ComplexNumber z, double x)
        {
            ComplexNumber FResult;
            try
            {
                FResult = x * z;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        ///  The division operator for the division of two complex numbers.
        /// </summary>
        /// <param name="z1">The first complex number.</param>
        /// <param name="z2">The second complex number.</param>
        /// <returns>The result of the division of the two complex numbers.</returns>
        public static ComplexNumber operator /(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber FResult;
            try
            {
                //((ac + bd) / (c2 + d2)) + ((bc - ad) / (c2 + d2))i
                double den = Math.Pow(z2.Re, 2) + Math.Pow(z2.Im, 2);
                FResult = new ComplexNumber((z1.Re * z2.Re + z1.Im * z2.Im) / den, (z1.Im * z2.Re - z1.Re * z2.Im) / den);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// The division operator for the division of a real number by a complex number.
        /// </summary>
        /// <param name="x">The real number.</param>
        /// <param name="z">The complex number.</param>
        /// <returns>The result of the division of a real number by a complex number.</returns>
        public static ComplexNumber operator /(double x, ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                ComplexNumber conj = ComplexNumber.Conjugate(z);
                FResult = (x * conj) / (z * conj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// The division operator for the division of a complex number by a real number.
        /// </summary>        
        /// <param name="z">The complex number.</param>
        /// /// <param name="x">The real number.</param>
        /// <returns>The result of the division of a complex number by a real number.</returns>
        public static ComplexNumber operator /(ComplexNumber z, double x)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(z.Re / x, z.Im / x);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// The UnaryNegation method defines the operation of the unary negation (additive inverse) operator for complex numbers.
        /// </summary>
        /// <param name="z">The source complex number.</param>        
        /// <returns>Returns the result of the Real and Imaginary components of the value parameter multiplied by -1.</returns>
        public static ComplexNumber operator -(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(-1 * z.Re, -1 * z.Im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        #endregion Operators




        #region Methods


        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            else if (obj.GetType().Equals(this.GetType()))
            {
                ComplexNumber z = (ComplexNumber)obj;
                return (this.Re.Equals(z.Re) && this.Im.Equals(z.Im));
            }
            else
                return false;
        }


        public override int GetHashCode()
        {
            return this.Re.GetHashCode() ^ this.Im.GetHashCode();
        }




        #region IEquatable<ComplexNumber> implementation


        public bool Equals(ComplexNumber other)
        {
            return this.Re.Equals(other.Re) && this.Im.Equals(other.Im);
        }


        #endregion IEquatable<ComplexNumber> implementation



        #region IFormattable Members implementation

        /// <summary>
        /// A string representation of this complex number.
        /// </summary>
        /// <returns>
        /// The string representation of this complex number.
        /// </returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// A string representation of this complex number.
        /// </summary>
        /// <returns>
        /// The string representation of this complex number formatted as specified by the
        /// format string.
        /// </returns>
        /// <param name="format">
        /// A format specification.
        /// </param>
        public string ToString(string format)
        {
            return ToString(format, null);
        }

        /// <summary>
        /// A string representation of this complex number.
        /// </summary>
        /// <returns>
        /// The string representation of this complex number formatted as specified by the
        /// format provider.
        /// </returns>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        public string ToString(IFormatProvider formatProvider)
        {
            return ToString(null, formatProvider);
        }

        /// <summary>
        /// A string representation of this complex number.
        /// </summary>
        /// <returns>
        /// The string representation of this complex number formatted as specified by the
        /// format string and format provider.
        /// </returns>
        /// <exception cref="FormatException">
        /// if the n, is not a number.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// if s, is <see langword="null"/>.
        /// </exception>
        /// <param name="format">
        /// A format specification.
        /// Valid specific formats are: "I|<num dec>", "J|<num dec>", "AR|<num dec>", "AD|<num dec>", "AG|<num dec>".
        ///                             Furthermore all other standard numeric formats are also valid.
        /// </param>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            string FResult = string.Empty;
            const char sepChar = '|';
            const string sepParts = ", ";   // Fortran uses comma instead of semicolon  //"; ";

            if (!string.IsNullOrWhiteSpace(format))
            {
                // Check for special complex number formats
                string[] frmParts = format.Split(sepChar);
                string part1 = string.Empty; string part2 = string.Empty;

                if (frmParts.Length > 0)
                {
                    int numDec;
                    part1 = frmParts[0].ToUpper();

                    if (frmParts.Length > 1)
                        part2 = frmParts[1];

                    if (!int.TryParse(part2, out numDec))
                        numDec = -1;

                    if (part1 == Frmt_abi)              // Cartesian form: a+bi
                    {
                        FResult = this.ExpressionString(formatProvider, numDec, ComplexNumber.C_ImSym_i);
                    }
                    else if (format == Frmt_abj)        // Cartesian form: a+bj
                    {
                        FResult = this.ExpressionString(formatProvider, numDec, ComplexNumber.C_ImSym_j);
                    }
                    else if (format == Frmt_rAo_Rad)    // Polar form: rAO with angle in radians
                    {
                        FResult = this.ToPolarString(formatProvider, numDec);
                    }
                    else if (format == Frmt_rAo_Deg)    // Polar form: rAO with angle in degrees
                    {
                        FResult = this.ToPolarStringDegrees(formatProvider, numDec);
                    }
                    else if (format == Frmt_rAo_Gra)    // Polar form: rAO with angle in grads
                    {
                        FResult = this.ToPolarStringGrads(formatProvider, numDec);
                    }
                }
            }

            // default format "(re; im)"
            if (string.IsNullOrEmpty(FResult))
            {
                var ret = new StringBuilder();
                ret.Append("(").Append(this.mRe.ToString(format, formatProvider)).Append(sepParts).Append(this.mIm.ToString(format, formatProvider)).Append(")");
                FResult = ret.ToString();
            }

            return FResult;
        }

        #endregion



        //#region IPrecisionSupport<Complex> implementation

        ///// <summary>
        ///// Returns a Norm of a value of this type, which is appropriate for measuring how
        ///// close this value is to zero.
        ///// </summary>
        ///// <returns>
        ///// A norm of this value.
        ///// </returns>
        //double IPrecisionSupport<ComplexNumber>.Norm()
        //{
        //    return this.MagnitudeSquared();
        //}

        ///// <summary>
        ///// Returns a Norm of the difference of two values of this type, which is
        ///// appropriate for measuring how close together these two values are.
        ///// </summary>
        ///// <param name="otherValue">
        ///// The value to compare with.
        ///// </param>
        ///// <returns>
        ///// A norm of the difference between this and the other value.
        ///// </returns>
        //double IPrecisionSupport<ComplexNumber>.NormOfDifference(ComplexNumber otherValue)
        //{
        //    return (this - otherValue).MagnitudeSquared();
        //}


        //#endregion IPrecisionSupport<Complex> implementation




        /// <summary>
        /// Converts a given object instance to a complex number type.
        /// </summary>
        /// <param name="arg">The object argument to convert.</param>
        /// <returns>The complex number if succeeded.</returns>
        /// <remarks>The object argument must represent a complex number or any real valid number.</remarks>
        public static ComplexNumber Cast(object arg)
        {
            ComplexNumber FResult;
            try
            {
				if (arg == null)
					throw new ArgumentNullException( string.Format("argument '{0}' is undefined", "arg") );// CommonTextService.MsgArgumentUndefined, "arg"));
                    
                    //throw new ArgumentNullException(string.Format(CommonTextService.MsgArgumentUndefined, "arg"));
                else
                {
                    if (arg is ComplexNumber)
                        FResult = (ComplexNumber)arg;
                    else
                    {
                        if (arg is double dbl)
                            FResult = dbl;
                        else if (arg is float flt)
                            FResult = flt;
                        //else if (arg is decimal dec)
                            //FResult = dec;
                        else if (arg is Int32 i32)
                            FResult = i32;
                        else if (arg is Int64 i64)
                            FResult = i64;
                        else if (arg is Int16 i16)
                            FResult = i16;
                        else if (arg is byte by)
                            FResult = by;
                        else if (arg is UInt32 ui32)
                            FResult = ui32;
                        else if (arg is UInt64 ui64)
                            FResult = ui64;
                        else if (arg is UInt16 ui16)
                            FResult = ui16;

                        else
                            FResult = (ComplexNumber)arg;
                    }
                }
            }
            catch (InvalidCastException)
            {
				throw new InvalidCastException($"argument '{nameof(arg)}={arg}' cannot be casted to a complex number type.");// CommonTextService.MsgInvalidComplexNumberParameterType);
                
                //throw new InvalidCastException(CommonTextService.MsgInvalidComplexNumberParameterType);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Computes the conjugate of a complex number and returns the result.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the conjugate of a complex number.</returns>
        /// <remarks>The conjugate of a complex number inverts the sign of the imaginary component; that is, it applies unary negation to the imaginary component. If a + bi is a complex number, its conjugate is a - bi.</remarks>
        public static ComplexNumber Conjugate(ComplexNumber z)
        {
            try
            {
                return new ComplexNumber(z.Re, -1 * z.Im);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Returns the multiplicative inverse of a complex number.
        /// </summary>
        /// <returns>The reciprocal of <paramref name="value" />.</returns>
        /// <param name="value">A complex number.</param>
        public static ComplexNumber Reciprocal(ComplexNumber value)
        {
            if (value.IsZero())
            {
                return Zero;
            }

            return 1.0d / value;
        }


        /// <summary>
        /// Creates a complex number from a point's polar coordinates.
        /// </summary>
        /// <param name="magnitude">The magnitude, which is the distance from the origin (the intersection of the x-axis and the y-axis) to the number.</param>
        /// <param name="phase">The phase, which is the angle from the line to the horizontal axis, measured in radians.</param>
        /// <returns>Returns a complex number.</returns>
        /// <remarks>
        /// The FromPolarCoordinates method instantiates a complex number based on its polar coordinates.
        /// Because there are multiple representations of a point on a complex plane, the return value of the FromPolarCoordinates method is normalized. 
        /// The magnitude is normalized to a positive number, and the phase is normalized to a value in the range of -PI to PI. As a result, 
        /// the values of the Phase and Magnitude properties of the resulting complex number may not equal the original values of the magnitude and phase parameters.
        /// </remarks>
        public static ComplexNumber FromPolarCoordinates(double magnitude, double phase)
        {
            ComplexNumber FResult;
            try
            {
                double r = magnitude;  //Math.Abs(magnitude);
                double nPhase = phase;
                
                //Normalize phase to fit in the range -PI<= phase <=PI
                if (Math.Abs(phase) > 2 * Math.PI)
                {
                    nPhase = phase % (2 * Math.PI);
                }

                if (nPhase > Math.PI)
                    nPhase -= 2* Math.PI;

                else if (nPhase < -Math.PI)
                    nPhase += 2 * Math.PI;
                
                //Calc cartesian coords in the complex plane.
                FResult = new ComplexNumber(r*Math.Cos(nPhase), r*Math.Sin(nPhase));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Creates a complex number from a point's polar coordinates with angle in degrees.
        /// </summary>
        /// <param name="magnitude">The magnitude, which is the distance from the origin (the intersection of the x-axis and the y-axis) to the number.</param>
        /// <param name="degrees">Is the angle from the line to the horizontal axis, measured in degrees.</param>
        /// <returns>Returns the complex number instance.</returns>
        /// <remarks>The angle will be normalized to the range [-PI..PI].</remarks>
        public static ComplexNumber FromPolarCoordinatesInDegrees(double magnitude, double degrees)
        {
            ComplexNumber FResult;
            try
            {
                double radians = DegreesToRadians(degrees);     //Get angle in radians.
                FResult = FromPolarCoordinates(magnitude, radians);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Gets the absolute value (or magnitude) of a complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <param name="re">The real part of the complex number.</param>
        /// <param name="im">The imaginary part of the complex number.</param>
        /// <returns>Returns the absolute value (or magnitude).</returns>
        private static double Abs(double re, double im)
        {
            double FResult = double.NaN;
            try
            {
                if (re == 0)
                    FResult = im;
                else if (im == 0)
                    FResult = re;
                else if (re > im)
                    FResult = re * Math.Sqrt(1 + ((im * im) / (re * re)));
                else
                    FResult = im * Math.Sqrt(1 + ((re * re) / (im * im)));

                FResult = Math.Abs(FResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Gets the absolute value (or magnitude) of a complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the absolute value (or magnitude).</returns>
        public static double Abs(ComplexNumber z)
        {
            double FResult = double.NaN;
            try
            {
                FResult = Abs(z.Re, z.Im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Cheks if the complex number is a true Real number (with no imaginary part).
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns true if the complex number is a true Real number, false otherwise.</returns>
        public static bool IsTrueReal(ComplexNumber z)
        {
            bool FResult = false;
            try
            {
                FResult = (z.Im == 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Rounds the complex number parts.
        /// </summary>
        /// <param name="z">The complex number to round.</param>
        /// <returns>Returns a complex number with the rounded complex number parts.</returns>
        /// <remarks>The imaginary part is rounded to the next int only if decimal part is greator than 0.5.</remarks>
        public static ComplexNumber Round(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                double re = z.Re;
                double im = z.Im;
                //double truncIm = MathUtils.Truncate(im);

                //if ((im - truncIm) > 0.5)
                //    im++;
                //else
                //    im = truncIm;

                re = Math.Round(re);
                im = Math.Round(im);

                FResult = new ComplexNumber(re, im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Rounds the complex number parts to a given number of decimal places.
        /// </summary>
        /// <param name="z">The complex number to round.</param>
        /// <param name="numDec">The number of decimal places.</param>
        /// <returns>Returns a complex number with the rounded complex number parts.</returns>
        public static ComplexNumber Round(ComplexNumber z, int numDec)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(Math.Round(z.Re, numDec), Math.Round(z.Im, numDec));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


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
        public static ComplexNumber Square(ComplexNumber z)
        {
            // This form has one less flop than z * z, and, more importantly, evaluates
            // (x - y) * (x + y) instead of x * x - y * y; the latter would be more
            // subject to cancelation errors and overflow or underflow 
            return (new ComplexNumber((z.Re - z.Im) * (z.Re + z.Im), 2.0 * z.Re * z.Im));
        }


        /// <summary>
        /// Calcs the square root of a complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the square root of the complex number.</returns>
        public static ComplexNumber Sqrt(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            double im = double.NaN;
            try
            {
                re = z.Re;
                im = z.Im;

                if ((im == 0) && ((re >= 0) || double.IsPositiveInfinity(re)))
                {
                    //Real number domain.
                    FResult = new ComplexNumber((double)MathUtils.Sqrt(re), 0);
                }
                else
                {
                    //if (z.Equals(ComplexNumber.ImaginaryOne))
                    //    FResult = ComplexNumber.ImaginaryOne;
                    if ((re == 0) && ((im == 1) || (im == -1)))
                    {
                        ComplexNumber z1 = new ComplexNumber(1, im);
                        FResult = z1 / (double)MathUtils.Sqrt(2);
                    }
                    else
                    {
                        //Complex.FromPolarCoordinates(Math.Sqrt(value.Magnitude), value.Phase/2.0)
                        FResult = ComplexNumber.FromPolarCoordinates(Math.Sqrt(z.Magnitude), z.Phase / 2.0);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns e raised to the power specified by a complex number.
        /// </summary>
        /// <param name="z">A complex number that specifies a power.</param>
        /// <returns>The number e raised to the power value.</returns>
        public static ComplexNumber Exp(ComplexNumber power)
        {
            ComplexNumber FResult;
            try
            {
                double m = Math.Exp(power.Re);
                FResult = (new ComplexNumber(m * Math.Cos(power.Im), m * Math.Sin(power.Im)));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Computes the argument (angle in radians) of a complex number.
        /// </summary>
        /// <param name="re">The complex number real part.</param>
        /// <param name="im">The complex number imaginary part.</param>
        /// <returns>The argument (phase in radians) of a complex number.</returns>
        private static double ComputeArg(double re, double im)
        {
            double FResult = double.NaN;
            try
            {
                FResult = Math.Atan2(im, re);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Gets the phase of a complex number in radians.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Gets the phase of a complex number in the range -PI..PI.</returns>
        /// <remarks>
        /// The phase of a complex number is the angle between the line joining it to the origin and the real axis of the complex plane.</para>
        /// The phase of complex numbers in the upper complex plane lies between 0 and &#x3C0;. The phase of complex numbers
        /// in the lower complex plane lies between 0 and -&#x3C0;. The phase of a real number is zero.</para>
        /// </remarks>
        public static double Arg(ComplexNumber z)
        {
            double FResult = double.NaN;
            try
            {
                FResult = z.Phase;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Gets the complex number with the smallest real part integral value greater than or equal to the input real part number.</returns>
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns a complex number with the smallest real part integral value greater than or equal to the input real part number.</returns>
        public static ComplexNumber Ceiling(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(Math.Ceiling(z.Re), z.Im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Gets the complex number with a real part that is the largest integer less than or equal to the real part of the input complex number.</returns>
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns a complex number with a real part that is the largest integer less than or equal to the real part of the input complex number.</returns>
        public static ComplexNumber Floor(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(Math.Floor(z.Re), z.Im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns a specified complex number raised to a power specified by a double-precision floating-point number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <param name="power">A double-precision floating-point number that specifies a power.</param>
        /// <returns></returns>
        public static ComplexNumber Pow(ComplexNumber z, double power)
        {
            ComplexNumber FResult;
            try
            {
                double m = Math.Pow(ComplexNumber.Abs(z), power);
                double t = ComplexNumber.Arg(z) * power;
                FResult = new ComplexNumber(m * Math.Cos(t), m * Math.Sin(t));

                // Maybe faster.
                // TODO: Check accuracy.
                //if (value.IsZero())
                //{
                //    if (power == 0d)
                //    {
                //        return One;
                //    }

                //    return power > 0d
                //        ? Zero
                //        : new Complex(double.PositiveInfinity, 0.0);
                //}

                //return Exp(power * Log(value));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Raises a real number to an arbitrary complex power.
        /// </summary>
        /// <param name="z1">The real base, which must be non-negative.</param>
        /// <param name="z">The complex exponent.</param>
        /// <returns>The value of x<sup>z</sup>.</returns>
        public static ComplexNumber Pow(double x, ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                if (x < 0.0) throw new ArgumentOutOfRangeException("x");

                if (z.IsZero()) // (z.Equals(ComplexNumber.Zero))
                    return ComplexNumber.One; //new ComplexNumber(1.0, 0);

                if (x == 0.0) 
                    return ComplexNumber.Zero;

                double m = Math.Pow(x, z.Re);
                double t = Math.Log(x) * z.Im;
                FResult = new ComplexNumber(m * Math.Cos(t), m * Math.Sin(t));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Raises a complex number to an arbitrary complex power.
        /// </summary>
        /// <param name="z1">A complex number to be raised to a power.</param>
        /// <param name="z2">A complex number that specifies a power.</param>
        /// <returns>The value of z1<sup>z2</sup>.</returns>
        public static ComplexNumber Pow(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber FResult;
            try
            {
                double r = ComplexNumber.Abs(z1);

                if (ComplexNumber.Abs(z2) < ACCURACY)
                { // s=0?
                    FResult = ComplexNumber.One;
                }
                else if (r < ACCURACY)
                {  // z=0?
                    FResult = ComplexNumber.Zero; // w=0
                }
                else
                {
                    double phi = ComplexNumber.Arg(z1);
                    double phase = z2.Re * phi + z2.Im * Math.Log(r);
                    double[] w = new double[2];
                    w[0] = Math.Pow(r, z2.Re) * Math.Exp(-z2.Im * phi);
                    w[1] = w[0];

                    w[0] *= Math.Cos(phase);
                    w[1] *= Math.Sin(phase);

                    FResult = new ComplexNumber(w[0], w[1]);
                }

                // Maybe faster.
                // TODO: Check accuracy.
                //if (value.IsZero())
                //{
                //    if (power.IsZero())
                //    {
                //        return One;
                //    }

                //    if (power.Real > 0.0)
                //    {
                //        return Zero;
                //    }

                //    if (power.Real < 0)
                //    {
                //        if (power.Imaginary == 0.0)
                //        {
                //            return new Complex(double.PositiveInfinity, 0.0);
                //        }

                //        return new Complex(double.PositiveInfinity, double.PositiveInfinity);
                //    }

                //    return double.NaN;
                //}

                //return Exp(power * Log(value));



                //Wrong???
                //FResult = ComplexNumber.Exp(z1 * z2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the natural (base e) logarithm of a specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the natural (base e) logarithm of a specified complex number (compliant with Mathematica)</returns>
        public static ComplexNumber Ln(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                if ((z.Im == 0) && ( (re >= 0) || double.IsInfinity(re) ) )
                {                    
                    FResult = new ComplexNumber((double)MathUtils.Ln(re), 0);       
                }
                else
                    FResult = new ComplexNumber(0.5 * Math.Log(z.MagnitudeSquared()), z.Phase);
                    //FResult = new ComplexNumber(Math.Log(z.Magnitude), z.Phase);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the natural (base e) logarithm of a specified complex number (same as 'Ln' except that doesn´t check for infinity values).
        /// </summary>
        /// <returns>The natural (base e) logarithm of <paramref name="value" />.</returns>
        /// <param name="value">A complex number.</param>
        public static ComplexNumber Log(ComplexNumber value)
        {
            if (value.IsRealNonNegative())
            {
                return new ComplexNumber(Math.Log(value.Re), 0.0);
            }

            return new ComplexNumber(0.5 * Math.Log(value.MagnitudeSquared()), value.Phase);
        }


        /// <summary>
        /// Returns the logarithm of complex number <paramref name="z"/> in base <paramref name="baseValue"/>.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <param name="baseValue">The base of the logarithm.</param>
        /// <returns>Returns the logarithm of a specified complex number in a specified base.</returns>
        public static ComplexNumber LogBaseN(ComplexNumber z, ComplexNumber baseValue)
        {
            ComplexNumber FResult;
            try
            {
                FResult = ComplexNumber.Ln(z) / ComplexNumber.Ln(baseValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the logarithm of a specified complex number in a specified Real base.
        /// </summary>
        /// <returns>The logarithm of <paramref name="value" /> in base <paramref name="baseValue" />.</returns>
        /// <param name="value">A complex number.</param>
        /// <param name="baseValue">The Real base of the logarithm.</param>
        public static ComplexNumber Log(ComplexNumber value, double baseValue)
        {
            return Log(value) / Math.Log(baseValue);
        }


        /// <summary>
        /// Returns the logarithm of a specified complex number in base 10.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the logarithm of a specified complex number in base 10.</returns>
        public static ComplexNumber Log10(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                if ((z.Im == 0) && ((re >= 0) || double.IsInfinity(re)))
                { 
                    FResult = new ComplexNumber((double)MathUtils.Log10(re), 0);
                }
                else 
                {
                    FResult = Log(z) / Ln10;

                    //FResult = ComplexNumber.LogBaseN(z, 10);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the logarithm of a specified complex number in base 2.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the logarithm of a specified complex number in base 2.</returns>
        public static ComplexNumber Log2(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                FResult = Log(z) / Ln2;   // ComplexNumber.LogBaseN(z, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Computes the Lucas number of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the Lucas number of the specified complex number if succeeded, NaN+NaNi otherwise.</returns>
        public static ComplexNumber Lucas(ComplexNumber z)
        {
            ComplexNumber FResult = new ComplexNumber(double.NaN, double.NaN);
            //double num = double.NaN;
            //double factor = double.NaN;
            try
            {
                //Perfect results.

                ComplexNumber coszPI = ComplexNumber.Cos(z * Math.PI);
                double sqrt5 = Math.Sqrt(5);
                double minusOnePlusSqrt5 = (-1 + Math.Sqrt(5));
                double onePlusSqrt5 = (1 + Math.Sqrt(5));
                FResult = (coszPI * ComplexNumber.Pow(minusOnePlusSqrt5, z) + ComplexNumber.Pow(onePlusSqrt5, z))
                            / ComplexNumber.Pow(2, z);

                //num = 0.5 * (1 + C_Sqrt5);
                //factor = Math.Cosh(z.Im * Math.PI);
                //FResult = ComplexNumber.Pow(num, z) - ComplexNumber.Pow(num, -z) * factor;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Computes the factorial of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the factorial of the specified complex number if succeeded, NaN+NaNi otherwise.</returns>
        public static ComplexNumber Fact(ComplexNumber z)
        {
            ComplexNumber FResult = new ComplexNumber(double.NaN, double.NaN);
            ComplexNumber arg;
            try
            {
                double re = z.Re;
                double im = z.Im;
                arg = z;

                if ((im == 0) && MathUtils.IsIntegerNumber(re))
                {
                    FResult = MathUtils.Fact_Double(re);
                }
                else
                {
                    //Use gamma function instead.
                    if (!double.IsNaN(re))
                    {
                        if (!double.IsInfinity(re))
                            arg = new ComplexNumber(re + 1, im);
                    }

                    FResult = ComplexNumber.Gamma(arg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Computes the Fibonacci number of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the Fibonacci number of the specified complex number if succeeded, NaN+NaNi otherwise.</returns>
        public static ComplexNumber Fib(ComplexNumber z)
        {
            ComplexNumber FResult = new ComplexNumber(double.NaN, double.NaN);
            ComplexNumber L1 = new ComplexNumber(double.NaN, double.NaN);
            ComplexNumber L2 = new ComplexNumber(double.NaN, double.NaN);
            try
            {
                
                //double num = 0.5 * (1 + C_Sqrt5);
                //double factor = Math.Cosh(z.Im * Math.PI);

                //FResult = (ComplexNumber.Pow(new ComplexNumber(num, 0), z) + ComplexNumber.Pow(new ComplexNumber(num, 0), -z) * factor) / C_Sqrt5;

                //Perfect results.
                L1 = ComplexNumber.Lucas(new ComplexNumber(z.Re - 1, z.Im));
                L2 = ComplexNumber.Lucas(new ComplexNumber(z.Re + 1, z.Im));
                FResult = 0.2 * (L1 + L2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Computes the natural logarithm of the sine of a complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the natural logarithm of the sine of a complex number.</returns>
        public static ComplexNumber LnSin(ComplexNumber z)
        {
            ComplexNumber FResult = ComplexNumber.Zero;
            double re = double.NaN;
            double im = double.NaN;
            try
            {
                if (Math.Abs(z.Im) <= 709)
                {
                    re = Math.Sin(z.Re) * Math.Cosh(z.Im);
                    im = Math.Cos(z.Re) * Math.Sinh(z.Im);
                    FResult = ComplexNumber.Ln(new ComplexNumber(re, im));
                }
                else
                {
                    // approximately cosh y = sinh y = e^|y| / 2:
                    // ln |sin z| = ln |y| - ln 2 for z = x + iy
                    re = Math.Abs(z.Im) - Math.Log(2);

                    // arg |sin z| = arctan( sgn y cot x ) for z = x + iy:
                    if (z.Im < 0)
                    {
                        im = Math.Atan(-1 / Math.Tan(z.Re));
                    }
                    else
                    {
                        im = Math.Atan(1 / Math.Tan(z.Re));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


       


    //    /** Returns the natural logarithm of the sine of a complex number <i>z</i>.
    //*  @param z a complex number in the array representation
    //*  @return lnSin <i>z</i>
    //*/

    //    public static double[] lnSin(double[] z)
    //    {
    //        double[] result = new double[2];

    //        if (Math.abs(z[1]) <= 709)
    //        {
    //            result[0] = Math.sin(z[0]) * Math.cosh(z[1]);  // since JDK 1.5:
    //            //result[0] = Math.sin(z[0]) * ( Math.exp(z[1]) + Math.exp(-z[1]) ) / 2;
    //            result[1] = Math.cos(z[0]) * Math.sinh(z[1]); // since JDK 1.5
    //            //result[1] = Math.cos(z[0]) * ( Math.exp(z[1]) - Math.exp(-z[1]) ) / 2;
    //            result = ln(result);
    //        }
    //        else
    //        { // approximately cosh y = sinh y = e^|y| / 2:
    //            // ln |sin z| = ln |y| - ln 2 for z = x + iy
    //            result[0] = Math.abs(z[1]) - Math.log(2);
    //            // arg |sin z| = arctan( sgn y cot x ) for z = x + iy:
    //            if (z[1] < 0)
    //            {
    //                result[1] = Math.atan(-1 / Math.tan(z[0]));
    //            }
    //            else
    //            {
    //                result[1] = Math.atan(1 / Math.tan(z[0]));
    //            }
    //        }
    //        return result;
    //    }


        // Euler-Mascheroni constant &#947; = 0.577215664901532860605.
        private static double C_gamma = 0.577215664901532860605;
        

        /// <summary>
        /// Computes the natural logarithm of the Euler gamma function of a complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the natural logarithm of the Euler gamma function of a complex number.</returns>
        /// <remarks>
        /// For Re(z) > 0, it is computed according to the method of Lanczos.
        /// Otherwise, the following formula is applied, which holds for any z but converges more slowly.
        /// 
        /// For negative Re(z) (&lt; 0), the accuracy is 9 to 10 decimal places.
        /// TODO: For double type, 14 to 15 decimal places accuracy is required (better solution needed)). 
        /// </remarks>         
        public static ComplexNumber LnGamma(ComplexNumber z) 
        {
            try 
	        {
                //nao funciona  re < 0 (falta parte imaginária)

                if (z.Re < 0)
                {
                    // modified part
                    if (z.Im == 0.0)
                    {
                        double re = z.Re;
                        // Is an int negative?
                        if (re == Math.Truncate(re))
                        {
                            // +infinity
                            return new ComplexNumber(double.PositiveInfinity, 0); 
                        }
                    }
                    //--------------

                    //----------------------------------------------------------
                    //The folowing commented formulas do no apply for Re(z) < 0.
                    //  w = ComplexNumber.gamma(z);
                    //  w = ComplexNumber.Ln(w);
                    //----------------------------------------------------------

                    //Extracted from boost. *******
                    ComplexNumber result = 0;
                    int sresult = 1;

                    if (z.Re < 0)
                    {
                        //-----------------------------------------------------------------------
                        // This algorythm gives hi precision real part for any complex number with re < 0, but
                        // wrong imaginary part.
                        //-----------------------------------------------------------------------

                        //if(Math.floor(z) == z)
                        //   return policies::raise_pole_error<T>(function, "Evaluation of tgamma at a negative integer %1%.", z, pol);

                        ComplexNumber z2 = z;

                        ComplexNumber t = z2 * ComplexNumber.Sin(Math.PI * z2); //detail::sinpx(z);

                        z2 = -z2;

                        if (t.Re < 0)
                        {
                            t = -t;
                        }
                        else
                        {
                            sresult = -sresult;
                        }

                        result = Math.Log(Math.PI) - ComplexNumber.LnGamma(z2) - ComplexNumber.Ln(t); //LnGamma(z, pol, l, 0) - ComplexNumber.Ln(t);
                        //return result;
                    }

                    double rePrec = result.Re;  //Hi precision real part.
                    //**********

                    ////TODO: needs some revision, not very precise when (im != 0) and (re < 0).

                    //Remove comments if boost do not work well.
                    // For Re(z) < 0
                    ComplexNumber w;
                    w = ComplexNumber.Ln(z) + (C_gamma * z);
                    w = -1.0 * w;

                    //Must be double to avoid overflow.
                    double nMax = (Math.Abs(z.Re) / ACCURACY);

                    if (nMax > 10000.0) nMax = 10000.0;

                    int intnMax = (int)nMax;

                    ComplexNumber z_n;
                    for (int n = 1; n <= intnMax; n++)
                    {
                        z_n = (1.0 / n) * z;
                        w = w + z_n;
                        w = w - ComplexNumber.Ln(ComplexNumber.One + z_n);
                    }

                    //Get result with accurate real part, but not so accurate imaginary part.
                    w = new ComplexNumber(rePrec, w.Im);

                    ////Get more accurate real part.
                    //if (z.Im == 0)
                    //{
                    //    double sign = 0;
                    //    double rePrecise = gammafunc.lngamma(z.Re, ref sign);
                    //    w = new ComplexNumber(rePrecise, w.Im);
                    //}

                    return w;
                }

                ComplexNumber x = new ComplexNumber(z.Re, z.Im);
                double[] c = { 76.18009173, -86.50532033, 24.01409822, -1.231739516, 0.00120858003, -5.36382e-6 };

                bool reflec;
                double xre = x.Re;
                double xIm = x.Im;

                if (x.Re >= 1.0) 
                {
                    // For Re(z) in [1..+infinit[
                    reflec = false;
                    xre = x.Re - 1.0;                    
                }
                else 
                {
                    // For Re(z) in [0..1[
                    reflec = true;
                    xre = 1.0 - x.Re;

                    //if (z.Re >= 0)
                        xIm = -xIm;
                }

                x = new ComplexNumber(xre, xIm);

                ComplexNumber xh  = new ComplexNumber(x.Re + 0.5, x.Im);
                ComplexNumber xgh = new ComplexNumber(x.Re + 5.5, x.Im);
                ComplexNumber s = ComplexNumber.One;
                ComplexNumber anum = new ComplexNumber(x.Re, x.Im);

                for (int i = 0; i < c.Length; ++i) 
                {
                     anum = anum + ComplexNumber.One;
                     s = s + (c[i] / anum);
                }

                s = 2.506628275 * s;

                //g = xh * Math.log(xgh) + Math.log(s) - xgh;
                ComplexNumber g = xh * ComplexNumber.Ln(xgh);
                g = g + ComplexNumber.Ln(s);
                g = g - xgh;

                if (reflec) 
                {
                    //result = Math.log(xx * Math.PI) - g - Math.log( Math.sin(xx * Math.PI) );
                    ComplexNumber PIxX = Math.PI * x;
                    ComplexNumber result = ComplexNumber.Ln(PIxX);
                    result = result - g;
                    result = result - ComplexNumber.LnSin(PIxX);
                    return result;
                } 
                else 
                {
                    return g;
                }
	        }
	        catch (Exception ex)
	        {
		        throw ex;
	        }
        }


        /// <summary>
        /// Returns the sine of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the sine of the specified complex number.</returns>
        public static ComplexNumber Sin(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(Math.Sin(z.Re) * Math.Cosh(z.Im), Math.Cos(z.Re) * Math.Sinh(z.Im));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the hyperbolic sine of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the hyperbolic sine of the specified complex number.</returns>
        public static ComplexNumber Sinh(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                // (Sinh(a) * Cos(b), Cosh(a) * Sin(b))
                FResult = new ComplexNumber(Math.Sinh(z.Re) * Math.Cos(z.Im), Math.Cosh(z.Re) * Math.Sin(z.Im));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the arc of the hyperbolic sine of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc of the hyperbolic sine of the specified complex number.</returns>
        public static ComplexNumber Asinh(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                // Needs accurate results.
                // arcsinh(z) = log(z + sqrt(z^2+1))
                FResult = ComplexNumber.Ln(z + ComplexNumber.Sqrt(z * z + 1));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the cosine of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the cosine of the specified complex number.</returns>
        public static ComplexNumber Cos(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                FResult = new ComplexNumber(Math.Cos(z.Re) * Math.Cosh(z.Im), -(Math.Sin(z.Re) * Math.Sinh(z.Im)));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the hyperbolic cosine of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the hyperbolic cosine of the specified complex number.</returns>
        public static ComplexNumber Cosh(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                //(Math.Cosh(a) * Math.Cos(b), Math.Sinh(a) * Math.Sin(b))
                FResult = new ComplexNumber(Math.Cosh(z.Re) * Math.Cos(z.Im), Math.Sinh(z.Re) * Math.Sin(z.Im));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the arc of the hyperbolic cosine of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc of the hyperbolic cosine of the specified complex number.</returns>
        public static ComplexNumber Acosh(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
               //
               // We use the relation acosh(z) = +-i acos(z)
               // Choosing the sign of multiplier to give real(acosh(z)) >= 0
               // as well as compatibility with C99.
               //
                FResult = ComplexNumber.Acos(z);

                if (z.Im == 0)
                {
                    if (z.Re >= -1)
                    {
                        if (z.Re > 1)
                            FResult = -ComplexNumber.ImaginaryOne * FResult;
                        else if (!double.IsNaN(FResult.Im) && !Signbit(FResult.Im))
                            FResult = ComplexNumber.ImaginaryOne * FResult;
                        else
                            FResult = -ComplexNumber.ImaginaryOne * FResult;
                    }
                    else
                        FResult = ComplexNumber.ImaginaryOne * FResult;
                }
                else
                {
                    if (z.Im < 0)
                        FResult = -ComplexNumber.ImaginaryOne * FResult;
                    else
                        FResult = ComplexNumber.ImaginaryOne * FResult;
                }


               //std::complex<T> result = boost::math::acos(z);
               //if(!(boost::math::isnan)(result.imag()) && signbit(result.imag()))
               //   return detail::mult_i(result);
               //return detail::mult_minus_i(result);


               // // Needs accurate results.
               // // log(z-sqrt{z^2-1}).
               // FResult =  ComplexNumber.Ln(z - ComplexNumber.Sqrt(z*z - 1));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the tangent of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the tangent of the specified complex number.</returns>
        public static ComplexNumber Tan(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                FResult = ComplexNumber.Sin(z) / ComplexNumber.Cos(z);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the hyperbolic tangent of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the hyperbolic tangent of the specified complex number.</returns>
        public static ComplexNumber Tanh(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                // Sinh(value) / Cosh(value)
                FResult = ComplexNumber.Sinh(z) / ComplexNumber.Cosh(z);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        ///// <summary>
        ///// Returns the arc of the hyperbolic tangent of the specified complex number.
        ///// </summary>
        ///// <param name="z">The source complex number.</param>
        ///// <returns>Returns the arc of the hyperbolic tangent of the specified complex number.</returns>
        //public static ComplexNumber Atanh(ComplexNumber z)
        //{
        //    ComplexNumber FResult;
        //    try
        //    {
        //        // 1/2 * (ln(1+z)-ln(1-z))
        //        FResult = (ComplexNumber.Ln(1 + z) - ComplexNumber.Ln(1 - z)) / 2.0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return FResult;
        //}


        //std::sqrt((std::numeric_limits<double>::max)()) / t;
        private static double safe_max(double t)
        {
           return Math.Sqrt(1.79769e+308) / t;
        }

        
        private static double safe_min(double t)
        {
            //std::sqrt((std::numeric_limits<double>::min)()) * t;
            return Math.Sqrt(2.2250738585072014e-308) * t;           
        }


        private static bool Signbit(double t)
        {
            if (t < 0) return true;
            else return false;

            //// Convert the double to an 8-byte array. 
            //byte[] bytes = BitConverter.GetBytes(t);

            //// Get the sign bit (byte 7, bit 7).
            ////(-) = 1, (+) = 0.
            //int res = (bytes[7] & 0x80) == 0x80 ? 1 : 0;

            //if (res == 1)
            //    return true;
            //else
            //    return false;

            ////result += String.Format("Sign: {0}\n", 
            ////                  (bytes[7] & 0x80) == 0x80 ? "1 (-)" : "0 (+)");
        }


        private static double ChangeSign(double t)
        {
            return -1 * t;
        }


        //private static byte Changebit(byte B, int n)
        //{
        //    try 
        //    {	        
        //        if ((n < 0) || (n > 7))
        //            throw new ArgumentOutOfRangeException("The position (zero based) of the bit to change must be between '0' and '7'.");

        //        byte[] bytes = new byte[1] {B};
        //        BitArray bits = new BitArray(bytes);
        //        bits.Set(n, !bits[n]);

        //        byte[] bytes2 = new byte[1];
        //        bits.CopyTo(bytes2, 0);

        //        return bytes2[0];

        //        //byte mask = 1 << n;  // if n is 3, mask results in 00001000
        //        //bytevalue = bytevalue or mask
        //    }
        //    catch (Exception ex)
        //    {		
        //        throw ex;
        //    }
        //}


        ///// <summary>
        ///// Changes the sign bit of a double number.
        ///// </summary>
        ///// <param name="t">The value to change his sign bit.</param>
        //private static void ChangeSignbit(double t)
        //{
        //    // Convert the double to an 8-byte array. 
        //    byte[] bytes = BitConverter.GetBytes(t);

        //    bytes[0] = Changebit(bytes[0], 0);
        //}

        
        /// <summary>
        /// Returns the arc of the hyperbolic tangent of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc of the hyperbolic tangent of the specified complex number.</returns>
        /// <remarks>
        /// References:
        ///
        /// Eric W. Weisstein. "Inverse Hyperbolic Tangent." 
        /// From MathWorld--A Wolfram Web Resource. 
        /// http://mathworld.wolfram.com/InverseHyperbolicTangent.html
        ///
        /// Also: The Wolfram Functions Site,
        /// http://functions.wolfram.com/ElementaryFunctions/ArcTanh/
        ///
        /// Also "Abramowitz and Stegun. Handbook of Mathematical Functions."
        /// at : http://jove.prohosting.com/~skripty/toc.htm
        ///
        /// Based on: http://www.boost.org/doc/libs/1_51_0/boost/math/complex/atanh.hpp
        /// </remarks>
        public static ComplexNumber Atanh(ComplexNumber z)
        {
            //#ifndef BOOST_MATH_COMPLEX_ATANH_INCLUDED
            //#define BOOST_MATH_COMPLEX_ATANH_INCLUDED

            //#ifndef BOOST_MATH_COMPLEX_DETAILS_INCLUDED
            //#  include <boost/math/complex/details.hpp>
            //#endif
            //#ifndef BOOST_MATH_LOG1P_INCLUDED
            //#  include <boost/math/special_functions/log1p.hpp>
            //#endif
            //#include <boost/assert.hpp>

            //#ifdef BOOST_NO_STDC_NAMESPACE
            //namespace std{ using ::sqrt; using ::fabs; using ::acos; using ::asin; using ::atan; using ::atan2; }
            //#endif

            const double pi = Math.PI;
            const double half_pi = pi / 2;
            const double one = 1.0D;
            const double two = 2.0D;
            const double four = 4.0D;
            const double zero = 0;
            const double a_crossover = 0.3D;

            //#ifdef BOOST_MSVC
            //#pragma warning(push)
            //#pragma warning(disable:4127)
            //#endif

            double x = Math.Abs(z.Re);
            double y = Math.Abs(z.Im);

            double real, imag;  // our results

            double safe_upper = safe_max(two);
            double safe_lower = safe_min(2);
            
           //
           // Begin by handling the special cases specified in C99:
           //
           if(double.IsNaN(x))
           {
              if (double.IsNaN(y))
                return new ComplexNumber(x, x);
              else if (double.IsInfinity(y))
                  return new ComplexNumber(0, Signbit(z.Im) ? -half_pi : half_pi ); //std::complex<T>(0, ((boost::math::signbit)(z.imag()) ? -half_pi : half_pi));
              else
                 return new ComplexNumber(x, x);     // std::complex<T>(x, x);
           }
           else if(double.IsNaN(y))
           {
              if(x == 0)
                 return new ComplexNumber(x, y);    // std::complex<T>(x, y);
              if(double.IsInfinity(x))      //  (boost::math::isinf)(x))
                 return new ComplexNumber(0, y);    // std::complex<T>(0, y);
              else
                 return new ComplexNumber(y, y);    // std::complex<T>(y, y);
           }
           else if((x > safe_lower) && (x < safe_upper) && (y > safe_lower) && (y < safe_upper))
           {
              double xx = x*x;
              double yy = y*y;
              double x2 = x * two;

              ///
              // The real part is given by:
              // 
              // real(atanh(z)) == log((1 + x^2 + y^2 + 2x) / (1 + x^2 + y^2 - 2x))
              // 
              // However, when x is either large (x > 1/E) or very small
              // (x < E) then this effectively simplifies
              // to log(1), leading to wildly inaccurate results.  
              // By dividing the above (top and bottom) by (1 + x^2 + y^2) we get:
              //
              // real(atanh(z)) == log((1 + (2x / (1 + x^2 + y^2))) / (1 - (-2x / (1 + x^2 + y^2))))
              //
              // which is much more sensitive to the value of x, when x is not near 1
              // (remember we can compute log(1+x) for small x very accurately).
              //
              // The cross-over from one method to the other has to be determined
              // experimentally, the value used below appears correct to within a 
              // factor of 2 (and there are larger errors from other parts
              // of the input domain anyway).
              //
              double alpha = two*x / (one + xx + yy);

              if(alpha < a_crossover)
              {
                 real = Math.Log(1+alpha) - Math.Log(1 + ChangeSign(alpha));  //boost::math::log1p(alpha) - boost::math::log1p((boost::math::changesign)(alpha));
              }
              else
              {
                 double xm1 = x - one;
                 real = Math.Log(1+(x2 + xx + yy)) - Math.Log(xm1*xm1 + yy);    // boost::math::log1p(x2 + xx + yy) - std::log(xm1*xm1 + yy);
              }

              real /= four;

              if (Signbit(z.Re))   //  ((boost::math::signbit)(z.real()))
                 real = ChangeSign(real);   //(boost::math::changesign)(real);

              imag = Math.Atan2((y * two), (one - xx - yy));   //std::atan2((y * two), (one - xx - yy));
              imag /= two;

              if(z.Im < 0)
                 imag = ChangeSign(imag); //(boost::math::changesign)(imag);
           }
           else
           {
              //
              // This section handles exception cases that would normally cause
              // underflow or overflow in the main formulas.
              //
              // Begin by working out the real part, we need to approximate
              //    alpha = 2x / (1 + x^2 + y^2)
              // without either overflow or underflow in the squared terms.
              //
              double alpha = 0;

              if(x >= safe_upper)
              {
                 if (double.IsInfinity(x) || double.IsInfinity(y))  //((boost::math::isinf)(x) || (boost::math::isinf)(y))
                 {
                    alpha = 0;
                 }
                 else if(y >= safe_upper)
                 {
                    // Big x and y: divide alpha through by x*y:
                    alpha = (two/y) / (x/y + y/x);
                 }
                 else if(y > one)
                 {
                    // Big x: divide through by x:
                    alpha = two / (x + y*y/x);
                 }
                 else
                 {
                    // Big x small y, as above but neglect y^2/x:
                    alpha = two/x;
                 }
              }
              else if(y >= safe_upper)
              {
                 if(x > one)
                 {
                    // Big y, medium x, divide through by y:
                    alpha = (two*x/y) / (y + x*x/y);
                 }
                 else
                 {
                    // Small x and y, whatever alpha is, it's too small to calculate:
                    alpha = 0;
                 }
              }
              else
              {
                 // one or both of x and y are small, calculate divisor carefully:
                 double div = one;

                 if(x > safe_lower)
                    div += x*x;
                 if(y > safe_lower)
                    div += y*y;

                 alpha = two * x / div;
              }

              if (alpha < a_crossover)
              {
                 real = Math.Log(1+alpha) - Math.Log(1+ChangeSign(alpha)); //  boost::math::log1p(alpha) - boost::math::log1p((boost::math::changesign)(alpha));
              }
              else
              {
                 // We can only get here as a result of small y and medium sized x,
                 // we can simply neglect the y^2 terms:
                 System.Diagnostics.Debug.Assert(x >= safe_lower);
                 System.Diagnostics.Debug.Assert(x <= safe_upper);
                 //BOOST_ASSERT(x >= safe_lower);
                 //BOOST_ASSERT(x <= safe_upper);
                 ////BOOST_ASSERT(y <= safe_lower);
                 double xm1 = x - one;
                 real = Math.Log(1 + two*x + x*x) - Math.Log(xm1*xm1);  //  std::log(1 + two*x + x*x) - std::log(xm1*xm1);
              }
      
              real /= four;

              if (Signbit(z.Re))     //((boost::math::signbit)(z.real()))
                 real = ChangeSign(real); //(boost::math::changesign)(real);

              //
              // Now handle imaginary part, this is much easier,
              // if x or y are large, then the formula:
              //    atan2(2y, 1 - x^2 - y^2)
              // evaluates to +-(PI - theta) where theta is negligible compared to PI.
              //
              if((x >= safe_upper) || (y >= safe_upper))
              {
                 imag = pi;
              }
              else if(x <= safe_lower)
              {
                 //
                 // If both x and y are small then atan(2y),
                 // otherwise just x^2 is negligible in the divisor:
                 //
                 if(y <= safe_lower)
                    imag = Math.Atan2(two*y, one);    // std::atan2(two*y, one);
                 else
                 {
                    if((y == zero) && (x == zero))
                       imag = 0;
                    else
                       imag = Math.Atan2(two*y, one - y*y);     // std::atan2(two*y, one - y*y);
                 }
              }
              else
              {
                 //
                 // y^2 is negligible:
                 //
                 if((y == zero) && (x == one))
                    imag = 0;
                 else
                    imag = Math.Atan2(two*y, 1 - x*x);    // std::atan2(two*y, 1 - x*x);
              }

              imag /= two;

              if (Signbit(z.Im))     //((boost::math::signbit)(z.imag()))
                 imag = ChangeSign(imag);   //(boost::math::changesign)(imag);
              else if ((z.Im == 0) && (z.Re > 1))
                  imag = ChangeSign(imag);
            }

           return new ComplexNumber(real, imag);  //std::complex<T>(real, imag);

            //#ifdef BOOST_MSVC
            //#pragma warning(pop)
            //#endif
        }


        /// <summary>
        /// Returns the cotangent of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the cotangent of the specified complex number.</returns>
        public static ComplexNumber Cot(ComplexNumber z)
        {
            ComplexNumber FResult;
            double twoa = 2.0 * z.Re;
            double twob = 2.0 * z.Im;
            try
            {
                // COT( z ) is equivalent to COS(z) / SIN(z).
                // To get better accuracy it is not implemented that way. 
                // With the notation z = "a+bj" the used formulas are ...
                double re = Math.Sin(twoa) / (Math.Cosh(twob) - Math.Cos(twoa));
                double im = (-Math.Sinh(twob)) / (Math.Cosh(twob) - Math.Cos(twoa));

                FResult = new ComplexNumber(re, im);

                // Cos(value) / Sin(value)
                //FResult = ComplexNumber.Cos(z) / ComplexNumber.Sin(z);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the hyperbolic cotangent of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the hyperbolic cotangent of the specified complex number.</returns>
        public static ComplexNumber Coth(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                // 1 / tanh(value)
                FResult = 1.0 / ComplexNumber.Tanh(z);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the arc cotangent of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc cotangent of the specified complex number.</returns>
        public static ComplexNumber Acot(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                if (z.Im == 0)
                {
                    double dblResult = MathUtils.ArcCot(re);
                    FResult = new ComplexNumber(dblResult, 0);
                }
                else
                {
                    // ATan(1/value)
                    FResult = ComplexNumber.Atan(1.0 / z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the arc of the hyperbolic cotangent of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc of the hyperbolic cotangent of the specified complex number.</returns>
        public static ComplexNumber Acoth(ComplexNumber z)
        {
            ComplexNumber FResult;
            try
            {
                if ((z.Re == 0) && (z.Im == 0))
                {
                    FResult = new ComplexNumber(0, C_PIdiv2);
                }
                else if (double.IsPositiveInfinity(z.Re) && (z.Im == 0))
                {
                    FResult = new ComplexNumber(0.0, 0.0);
                }
                else if (double.IsNegativeInfinity(z.Re) && (z.Im == 0))
                {
                    FResult = new ComplexNumber(0.0, 0.0);
                }
                else if ((z.Re == 1) && (z.Im == 0))
                {
                    FResult = new ComplexNumber(double.PositiveInfinity, 0);
                }
                else if ((z.Re == -1) && (z.Im == 0))
                {
                    FResult = new ComplexNumber(double.NegativeInfinity, 0);
                }
                else
                {
                    // ATanh(1/value)
                    FResult = ComplexNumber.Atanh(1.0 / z);
                }    
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the secant of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the secant of the specified complex number.</returns>
        public static ComplexNumber Sec(ComplexNumber z)
        {
            ComplexNumber FResult;
            double a = z.Re;
            double b = z.Im;
            double twoa = 2.0 * a;
            double twob = 2.0 * b;
            try
            {
                // SEC( z ) is equivalent to (1 / COS( z )).
                // To get better accuracy it is not implemented that way. 
                // With the notation z="a+bj" the used formulas are ...
                double re = (2.0 * Math.Cos(a) * Math.Cosh(b)) / (Math.Cosh(twob) + Math.Cos(twoa));
                double im = (2.0 * Math.Sin(a) * Math.Sinh(b)) / (Math.Cosh(twob) + Math.Cos(twoa));

                FResult = new ComplexNumber(re, im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the arc secant of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc secant of the specified complex number.</returns>
        public static ComplexNumber Asec(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                if ((z.Im == 0) && ((re == -1) || (re == 0) || (re == 1) || (re < -1) || (re > 1) || (double.IsInfinity(re)) ) )
                {
                    double dblResult = (double)MathUtils.ArcSec(re);
                    FResult = new ComplexNumber(dblResult, 0);
                }
                else
                    FResult = ComplexNumber.Acos(1.0 / z);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Computes the sign of a specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the sign of a specified complex number.</returns>
        public static ComplexNumber Sign(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            double im = double.NaN;
            try
            {
                re = z.Re;
                im = z.Im;

                if (im == 0)
                {
                    if (re == 0)
                        FResult = new ComplexNumber(0, 0);
                    else
                        FResult = new ComplexNumber((double)MathUtils.Sign(re), 0);
                }
                else if (double.IsNegativeInfinity(im))
                    FResult = -ComplexNumber.ImaginaryOne;

                else if (double.IsPositiveInfinity(im))
                    FResult = ComplexNumber.ImaginaryOne;

                else
                {
                    //Perfect results.
                    FResult = z / z.Magnitude;// (Math.Sqrt(5));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the hyperbolic secant of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the hyperbolic secant of the specified complex number.</returns>
        public static ComplexNumber Sech(ComplexNumber z)
        {
            ComplexNumber FResult;
            //double b = z.Im;
            //double twoa = 2 * z.Re;
            //double twob = 2 * b;            
            try
            {
                //Perfect results.
                FResult = 1.0 / ComplexNumber.Cos(z * ComplexNumber.ImaginaryOne);


                //Not perfect results.
                //// SECH( z ) is equivalent to (1 / COSH( z )).
                //// To get better accuracy it is not implemented that way. 
                //// With the notation z="a+bj" the used formulas are ...
                //double re = (2 * Math.Cosh(twoa) * Math.Cos(b)) / (Math.Cosh(twoa) + Math.Cos(twob));
                //double im = (-2 * Math.Sinh(twoa) * Math.Sin(b)) / (Math.Cosh(twoa) + Math.Cos(twob));

                //FResult = new ComplexNumber(re, im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the arc of the hyperbolic secant of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc of the hyperbolic secant of the specified complex number.</returns>
        /// <remarks>asech = acosh(1.0/z).</remarks>
        public static ComplexNumber Asech(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                //Handle special cases for domain that return Real number result.
                if ((z.Im == 0) && ((re == 0) || (re == 1) || (re == -1) || ((re > 0) && (re < 1))))
                {
                    if (re == -1)
                        FResult = new ComplexNumber(0, C_PI);
                    else
                    {
                        double dblResult = (double)MathUtils.ArcSech(re);
                        FResult = new ComplexNumber(dblResult, 0.0);
                    }
                }
                else
                    FResult = ComplexNumber.Acosh(1.0 / z);   
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the cosecant of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the cosecant of the specified complex number.</returns>
        public static ComplexNumber Cosec(ComplexNumber z)
        {
            ComplexNumber FResult;
            double a = z.Re;
            double b = z.Im;
            double twoa = 2.0 * a;
            double twob = 2.0 * b;
            try
            {
                // COSEC( z ) is equivalent to (1 / SIN( z )).
                // To get better accuracy it is not implemented that way. 
                // With the notation z="a+bj" the used formulas are ...
                double re = (2.0 * Math.Sin(a) * Math.Cosh(b)) / (Math.Cosh(twob) - Math.Cos(twoa));
                double im = (-2.0 * Math.Cos(a) * Math.Sinh(b)) / (Math.Cosh(twob) - Math.Cos(twoa));

                FResult = new ComplexNumber(re, im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the arc of the cosecant of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc of the cosecant of the specified complex number.</returns>
        public static ComplexNumber Acosec(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                if ((z.Im == 0) && ((re == -1) || (re == 0) || (re == 1) || (re < -1) || (re > 1) || (double.IsInfinity(re))))
                {
                    double dblResult = (double)MathUtils.ArcCosec(re);
                    FResult = new ComplexNumber(dblResult, 0);
                }
                else
                {
                    // ArcCosec( z ) = ArcSin(1/z).
                    FResult = ComplexNumber.Asin(1.0 / z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the hyperbolic cosecant of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the hyperbolic cosecant of the specified complex number.</returns>
        public static ComplexNumber Cosech(ComplexNumber z)
        {
            ComplexNumber FResult;
            double a = z.Re;
            double b = z.Im;
            double twoa = 2.0 * a;
            double twob = 2.0 * b;
            try
            {
                // COSEC( z ) is equivalent to (1 / SINH( z )).
                // To get better accuracy it is not implemented that way. 
                // With the notation z="a+bj" the used formulas are ...
                double re = (2.0 * Math.Sinh(twoa) * Math.Cos(b)) / (Math.Cosh(twoa) - Math.Cos(twob));
                double im = (-2.0 * Math.Cosh(twoa) * Math.Sin(b)) / (Math.Cosh(twoa) - Math.Cos(twob));

                FResult = new ComplexNumber(re, im);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the arc of the hyperbolic cosecant of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the arc of the hyperbolic cosecant of the specified complex number.</returns>
        public static ComplexNumber Acosech(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                if (z.Im == 0) //&& ((re == 0) || (double.IsInfinity(re))))
                {
                    FResult = new ComplexNumber(MathUtils.ArcCosech(re), 0);
                }
                else
                {
                    //Ln(Sqrt(1+1/(z^2)+1/z))
                    FResult = ComplexNumber.Ln(ComplexNumber.Sqrt(1 + 1 / (z * z)) + 1 / z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the angle that is the arc sine of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the angle that is the arc sine of the specified complex number.</returns>
        public static ComplexNumber Asin(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                //Handle special cases.
                if ((z.Im == 0) && ((re == -1) || (re == 0) || (re == 1) || ((re > -1) && (re < 1)) || (double.IsInfinity(re)) ))
                {
                    if (double.IsPositiveInfinity(re))
                        FResult = new ComplexNumber(0, double.NegativeInfinity);
                    else if (double.IsNegativeInfinity(re))
                        FResult = new ComplexNumber(0, double.PositiveInfinity);
                    else
                    {
                        double dblResult = (double)MathUtils.ArcSin(re);
                        FResult = new ComplexNumber(dblResult, 0);
                    }
                }
                else
                {
                    FResult = -ImaginaryOne * Ln(ImaginaryOne * z + Sqrt(One - z * z));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the angle that is the arc cosine of the specified complex number.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the angle that is the arc cosine of the specified complex number.</returns>
        public static ComplexNumber Acos(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                if ((z.Im == 0) && ((re == -1) || (re == 0) || (re == 1) || ((re > -1) && (re < 1)) || (double.IsInfinity(re))))
                {
                    if (double.IsPositiveInfinity(re))
                        FResult = new ComplexNumber(0, double.PositiveInfinity);
                    else if (double.IsNegativeInfinity(re))
                        FResult = new ComplexNumber(0, double.NegativeInfinity);
                    else
                    {
                        double dblResult = (double)MathUtils.ArcCos(re);
                        FResult = new ComplexNumber(dblResult, 0);
                    }
                }
                else
                {
                    //(-ImaginaryOne) * Log(value + ImaginaryOne*Sqrt(One - value * value)))
                    FResult = (-ImaginaryOne) * (Ln(z + ImaginaryOne * Sqrt(One - z * z)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Returns the angle that is the arc tangent of the specified complex number.
        /// </summary>
        /// <returns>Returns the angle that is the arc tangent of the specified complex number.</returns>
        public static ComplexNumber Atan(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            try
            {
                re = z.Re;

                if (z.Im == 0)
                {
                    double dblResult = MathUtils.ArcTan(re);
                    FResult = new ComplexNumber(dblResult, 0);
                }
                else
                    FResult = (ImaginaryOne / new ComplexNumber(2.0, 0.0)) * (Ln(One - ImaginaryOne * z) - Ln(One + ImaginaryOne * z));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        // Used by gamma2 function
        private static int g = 7;
        private static double[] p = {0.99999999999980993, 676.5203681218851, -1259.1392167224028,
                    771.32342877765313, -176.61502916214059, 12.507343278686905,
                    -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7};


        // NOT TESTED YET
        /// <summary>
        /// Simple Gamma function (test).
        /// </summary>
        /// <returns>The gamma2.</returns>
        /// <param name="z">The z coordinate.</param>
        public static ComplexNumber Gamma2(ComplexNumber z)
        {

            // Reflection formula
            if (z.Real < 0.5)
            {
                return Math.PI / (ComplexNumber.Sin(Math.PI * z) * Gamma2(1 - z));
            }
            else
            {
                z -= 1;
                ComplexNumber x = p[0];
                for (var i = 1; i < g + 2; i++)
                {
                    x += p[i] / (z + i);
                }
                ComplexNumber t = z + g + 0.5;
                return ComplexNumber.Sqrt(2 * Math.PI) * (ComplexNumber.Pow(t, z + 0.5)) * ComplexNumber.Exp(-t) * x;
            }
        }


        /// <summary>
        /// Computes the gamma function for a given complex numbers.
        /// </summary>
        /// <param name="z">The source complex number.</param>
        /// <returns>Returns the gamma function for a given complex numbers.</returns>
        public static ComplexNumber Gamma(ComplexNumber z)
        {
            ComplexNumber FResult;
            double re = double.NaN;
            double im = double.NaN;
            try
            {
                re = z.Re;
                im = z.Im;

                if ((im == 0) && (double.IsInfinity(re) || MathUtils.IsIntegerNumber(re) ) )
                {
                    double dblFact = MathUtils.Fact_Double(re - 1);
                    FResult = new ComplexNumber(dblFact, 0);
                }
                else
                { 
                    FResult = GammaImpl(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        //// Coefficients used by the GNU Scientific Library
        //private static double g = 7;
        //private static double[] p = new double[] {0.99999999999980993, 676.5203681218851, -1259.1392167224028,
                                                    //771.32342877765313, -176.61502916214059, 12.507343278686905,
                                                    //-0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7};

        /// <summary>
        /// Computes the gamma function for a complex number.
        /// </summary>
        /// <param name="z">The source complex number argument.</param>
        /// <returns>The gamma function result.</returns>
        private static ComplexNumber GammaImpl(ComplexNumber z)
        {
            // Coefficients used by the GNU Scientific Library
            //double g = 7;
            //double[] p = new double[] {0.99999999999980993, 676.5203681218851, -1259.1392167224028,
            //                           771.32342877765313, -176.61502916214059, 12.507343278686905,  
            //                           -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7};
            try
            {
                //if ( z.Re < 0 ) 
                //{ 
                //    // Weierstrass form:
                //    ComplexNumber w = ComplexNumber.One / (z * ComplexNumber.Pow(Math.E, (C_gamma * z)));
                //    int nMax = (int) ( 1E-6 / ACCURACY );
                
                //    ComplexNumber z_n;

                //    for ( int n = 1; n <= nMax; n++ ) 
                //    {
                //        z_n = (1.0/n) * z;
                //        w = w * (ComplexNumber.One + z_n);
                //        w = w / (ComplexNumber.Pow(Math.E, z_n));
                //    }

                //    w = 1.0 / w;
                //    return w;
                //}


                //Handle special cases.
                if (double.IsInfinity(z.Im))
                    return new ComplexNumber(0, 0);

                else if (double.IsPositiveInfinity(z.Re))
                    return new ComplexNumber(double.PositiveInfinity, 0);

                else if (double.IsNegativeInfinity(z.Re))
                    return new ComplexNumber(double.NaN, double.NaN);




                ComplexNumber x = new ComplexNumber(z.Re, z.Im);
                double[] c = { 76.18009173, -86.50532033, 24.01409822, -1.231739516, 0.00120858003, -5.36382e-6 };

                bool reflec;
                double xre = x.Re;
                double xIm = x.Im;

                if ( x.Re >= 1.0 ) 
                {
                    reflec = false;
                    xre = x.Re - 1.0;
                } 
                else 
                {
                    reflec = true;
                    xre = 1.0 - x.Re;
                    xIm = -xIm;
                }

                x = new ComplexNumber(xre, xIm);

                ComplexNumber xh = new ComplexNumber(x.Re + 0.5, x.Im);
                ComplexNumber xgh = new ComplexNumber(x.Re + 5.5, x.Im);
                ComplexNumber s = ComplexNumber.One;
                ComplexNumber anum = new ComplexNumber(x.Re, x.Im);

                for (int i = 0; i < c.Length; ++i)
                {
                    anum = anum + ComplexNumber.One;
                    s = s + (c[i] / anum);
                }

                s = 2.506628275 * s;

                //g = Math.pow(xgh, xh) * s / Math.exp(xgh);
                ComplexNumber g = ComplexNumber.Pow(xgh, xh) * s;
                g = g / ComplexNumber.Pow(Math.E, xgh);

                if (reflec)
                {
                    //result =  PI x / (g * sin(PI x));
                    if ( Math.Abs(x.Im) > 709 ) 
                    {  
                        // sin( 710 i ) = Infinity !! 
                        return ComplexNumber.Zero;
                    }

                    ComplexNumber PIxX = Math.PI * x;
                    ComplexNumber result = PIxX;
                    result = result / (g * ComplexNumber.Sin(PIxX));
                    return result;
                }
                else 
                {
                    return g;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



            ////ComplexNumber FResult;
            //try 
            //{	        
            //    if (z.Re < 0.5)
            //        return Math.PI / (Sin(Math.PI * z) * gamma(1-z));
            //    else
            //    {
            //        z -= 1;
            //        //double x = p[0];
            //        ComplexNumber px = new ComplexNumber(p[0], 0);

            //        for (int i = 1; i < (g+3); i++)
            //        {
            //            px += p[i]/(z+i);
            //        }

            //        //for i in range(1, g+2)
            //        //    x += p[i]/(z+i);

            //        ComplexNumber t = z + g + 0.5;
            //        return Math.Sqrt(2*Math.PI) * Pow(t, z+0.5) * Exp(-t) * px;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

////Python sample
//def gamma(z):
//    z = complex(z)
//    # Reflection formula
//    if z.real < 0.5:
//        return pi / (sin(pi*z)*gamma(1-z))
//    else:
//        z -= 1
//        x = p[0]
//        for i in range(1, g+2):
//            x += p[i]/(z+i)
//        t = z + g + 0.5
//        return sqrt(2*pi) * t**(z+0.5) * exp(-t) * x


        /// <summary>
        /// Returns a complex number expression string with his numeric parts in a given culture format and rounded to a given number of decimal places.
        /// </summary>
        /// <param name="formatProvider">The format provider to apply to the numbers.</param>
        /// <param name="numDec">The number of decimal places to round.</param>
        /// <returns>Returns a complex number expression string.</returns>
        public string ExpressionString(IFormatProvider formatProvider, int numDec, char imSymbol)
        {
            string FResult = string.Empty;
            string imSign = string.Empty;
            string imStr = string.Empty;
            double im = this.Im;
            try
            {
                if (im == 1)
                    imStr = imSymbol.ToString();
                else if (im == -1)
                {
                    imStr = string.Concat("-", imSymbol.ToString());
                }
                else if (im != 0)
                {
                    if (numDec < 0)
                    {
                        imStr = im.ToString(formatProvider);
                    }
                    else
                    {
                        imStr = Math.Round(im, numDec).ToString(formatProvider);
                    }

                    imStr = string.Concat(imStr, imSymbol);
                }

                if (this.Re != 0)
                {
                    if (numDec < 0)
                        FResult = this.Re.ToString(formatProvider);
                    else
                        FResult = Math.Round(this.Re, numDec).ToString(formatProvider);

                    if (im != 0)
                    {
                        //Has real and imaginary part.
                        imSign = (im < 0) ? "" : "+";
                        FResult = string.Concat(FResult, imSign, imStr);
                    }
                }
                else
                {
                    //Zero real?
                    if (im == 0)
                        FResult = "0";
                    else
                    {
                        //Only imaginary part.
                        //if (im < 0)
                        //{
                        //    FResult = imStr;
                        //}
                        //else
                        //    FResult = imStr;

                        FResult = imStr;

                        //if (im == 1)
                        //    FResult = string.Format("{0}(-1)", mSquareRootSymbol);
                        //else if (im == -1)
                        //    FResult = string.Format("-{0}(-1)", mSquareRootSymbol);
                        //else
                        //    FResult = imStr;
                    }    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        ///// <summary>
        ///// Converts the value of the current complex number to its equivalent string representation in Cartesian form.
        ///// </summary>
        ///// <returns>The string representation of the current instance in Cartesian form.</returns>
        //public override string ToString()
        //{
        //    return this.ExpressionString(System.Globalization.CultureInfo.CurrentCulture, -1, mImUnitSymbol);           
        //    //return string.Concat("(", this.Re.ToString(), "; ", this.Im.ToString(), ")");
        //}


        /// <summary>
        /// Converts the value of the current complex number to its equivalent string representation in polar form with the angle in radians.
        /// </summary>
        /// <param name="provider">The format provider.</param>
        /// <param name="numDec">The number of decimal places precision.</param>
        /// <returns>The string representation of the current instance in polar form with the angle in radians.</returns>
        public string ToPolarString(IFormatProvider provider, int numDec)
        {
            try
            {
                if (numDec < 0) //No rounding at all.
                    return string.Concat(this.Magnitude.ToString(provider), mPolarAngleSymbol.ToString(), (this.Phase).ToString(provider));
                else
                    return string.Concat(Math.Round(this.Magnitude, numDec).ToString(provider), mPolarAngleSymbol.ToString(), Math.Round((this.Phase), numDec).ToString(provider));
                //return string.Concat("(", this.Re.ToString(), "; ", this.Im.ToString(), ")");
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }


        /// <summary>
        /// Converts the value of the current complex number to its equivalent string representation in polar form with the angle in degrees.
        /// </summary>
        /// <param name="provider">The format provider.</param>
        /// <param name="numDec">The number of decimal places precision.</param>
        /// <returns>The string representation of the current instance in polar form with the angle in degrees.</returns>
        public string ToPolarStringDegrees(IFormatProvider provider, int numDec)
        {
            try
            {
                if (numDec < 0) //No rounding at all.
                    return string.Concat(this.Magnitude.ToString(provider), mPolarAngleSymbol.ToString(), RadiansToDegrees(this.Phase).ToString(provider));
                else
                    return string.Concat(Math.Round(this.Magnitude, numDec).ToString(provider), mPolarAngleSymbol.ToString(), Math.Round(RadiansToDegrees(this.Phase), numDec).ToString(provider));
                //return string.Concat("(", this.Re.ToString(), "; ", this.Im.ToString(), ")");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Converts the value of the current complex number to its equivalent string representation in polar form with the angle in grades.
        /// </summary>
        /// <param name="provider">The format provider.</param>
        /// <param name="numDec">The number of decimal places precision.</param>
        /// <returns>The string representation of the current instance in polar form with the angle in grads.</returns>
        public string ToPolarStringGrads(IFormatProvider provider, int numDec)
        {
            try
            {
                if (numDec < 0) //No rounding at all.
                    return string.Concat(this.Magnitude.ToString(provider), mPolarAngleSymbol.ToString(), RadiansToGrads(this.Phase).ToString(provider));
                else
                    return string.Concat(Math.Round(this.Magnitude, numDec).ToString(provider), mPolarAngleSymbol.ToString(), Math.Round(RadiansToGrads(this.Phase), numDec).ToString(provider));
                //return string.Concat("(", this.Re.ToString(), "; ", this.Im.ToString(), ")");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        ///// <summary>
        ///// Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified culture-specific formatting information.
        ///// </summary>
        ///// <param name="provider">An object that supplies culture-specific formatting information.</param>
        ///// <returns>The string representation of the current instance in Cartesian form, as specified by provider.</returns>
        ///// <remarks>If provider is null, the returned string is formatted using the NumberFormatInfo object of the current culture.</remarks>
        //public string ToString(IFormatProvider provider)
        //{
        //    try
        //    {
        //        if (provider == null)
        //            return this.ToString();
        //        else
        //            return this.ExpressionString(provider, -1, mImUnitSymbol);

        //            //return string.Concat("(", this.Re.ToString(provider), "; ", this.Im.ToString(provider), ")");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        ///// <summary>
        ///// Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified format for its real and imaginary parts.
        ///// </summary>
        ///// <param name="format">A standard or custom numeric format string.</param>
        ///// <returns>The string representation of the current instance in Cartesian form.</returns>
        ///// <remarks>
        ///// If format is equal to String.Empty or is null, the real and imaginary parts of the complex number are formatted with the general format specifier ("G"). 
        ///// If format is any non valid value, the method throws a FormatException.
        ///// </remarks>
        //public string ToString(string format)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(format))
        //            return this.ToString();
        //        else
        //        {
        //            return string.Concat("(", this.Re.ToString(format), "; ", this.Im.ToString(format), ")");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        ///// <summary>
        ///// Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified format and 
        ///// culture-specific format information for its real and imaginary parts.
        ///// </summary>
        ///// <param name="format">A standard or custom numeric format string.</param>
        ///// <param name="provider">An object that supplies culture-specific formatting information.</param>
        ///// <returns>The string representation of the current instance in Cartesian form, as specified by format and provider.</returns>
        ///// <remarks>
        ///// The format parameter can be any valid standard numeric format specifier, or any combination of custom numeric format specifiers. 
        ///// If format is equal to String.Empty or is null, the real and imaginary parts of the complex number are formatted with the general format specifier ("G"). 
        ///// If format is any other value, the method throws a FormatException.
        ///// </remarks>
        //public string ToString(string format, IFormatProvider provider)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(format))
        //        {
        //            if (provider == null)
        //                return this.ToString();
        //            else
        //                return this.ToString(provider);
        //        }
        //        else
        //        {
        //            if (provider == null)
        //                return this.ToString(format);
        //            else
        //                return string.Concat("(", this.Re.ToString(format, provider), "; ", this.Im.ToString(format, provider), ")");
        //        }                    
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        /// <summary>
        /// Converts the current instance to his string representation based on a 
        /// given culture format and rounded to a given number of decimal places.
        /// </summary>
        /// <param name="provider">The culture format.</param>
        /// <param name="numDec">The number of decimal places to round his parts.</param>
        /// <returns>Returns the string representation of the current instance.</returns>
        public string ToString(IFormatProvider provider, int numDec)
        {
            string FResult = string.Empty;
            try
            {
                FResult = this.ExpressionString(provider, numDec, mImUnitSymbol);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Parses a string representing a complex number in the current culture and float number style.
        /// </summary>
        /// <param name="s">The source string to parse.</param>
        /// <returns>The complex number instance.</returns>
        public static ComplexNumber Parse(string s)
        {
            ComplexNumber FResult;
            try
            {                
                if (!TryParse(s, out FResult))
                {
                    throw new FormatException(string.Format("The argument string '{0}' has an invalid format for a complex number representation in the current culture.", s));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Parses a string representing a complex number using a given format provider and float number style.
        /// </summary>
        /// <param name="s">The source string to parse.</param>
        /// <param name="provider">The format provider to apply.</param>
        /// <returns>The complex number instance.</returns>
        public static ComplexNumber Parse(string s, IFormatProvider provider)
        {
            ComplexNumber FResult;
            try
            {
                if (!TryParse(s, System.Globalization.NumberStyles.Float, provider, out FResult))
                {
                    throw new FormatException(string.Format("The argument string '{0}' has an invalid format for a complex number representation in the given format provider.", s));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Parses a string representing a complex number in a given number style in the current culture.
        /// </summary>
        /// <param name="s">The source string to parse.</param>
        /// <param name="style">The number style to apply.</param>
        /// <returns>The complex number instance.</returns>
        public static ComplexNumber Parse(string s, System.Globalization.NumberStyles style)
        {
            ComplexNumber FResult;
            try
            {
                if (!TryParse(s, style, System.Globalization.CultureInfo.CurrentCulture, out FResult))
                {
                    throw new FormatException(string.Format("The argument string '{0}' has an invalid format for a complex number representation in the given number style for the current culture.", s));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Parses a string representing a complex number in a given number style and using a given format provider.
        /// </summary>
        /// <param name="s">The source string to parse.</param>
        /// <param name="style">The number style to apply.</param>
        /// <param name="provider">The format provider to apply.</param>
        /// <returns>The complex number instance.</returns>
        public static ComplexNumber Parse(string s, System.Globalization.NumberStyles style, IFormatProvider provider)
        {
            ComplexNumber FResult;
            try
            {                
                if (!TryParse(s, style, provider, out FResult))
                {
                    throw new FormatException(string.Format("The argument string '{0}' has an invalid format for a complex number representation in the given number style and format provider.", s));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Tries to parse a string representing a complex number in the current culture format and in float number style.
        /// </summary>
        /// <param name="s">The source string to parse.</param>
        /// <param name="cn">Returns the resulting complex number value.</param>
        /// <returns>Returns true if succeeded, false otherwise.</returns>
        public static bool TryParse(string s, out ComplexNumber cn)
        {
            bool FResult = false;
            try
            {
                FResult = TryParse(s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.CurrentCulture, out cn);                 
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Tries to parse a string representing a complex number in the current culture format and in float number style.
        /// </summary>
        /// <param name="s">The source string to parse.</param>
        /// <param name="style">The number style to support.</param>
        /// <param name="provider">The format provider to apply.</param>
        /// <param name="cn">Returns the resulting complex number value.</param>
        /// <returns>Returns true if succeeded, false otherwise.</returns>
        public static bool TryParse(string s, System.Globalization.NumberStyles style, IFormatProvider provider, out ComplexNumber cn)
        {
            bool FResult = false;
            cn = new ComplexNumber();
            try
            {                
                string[] parts = s.Split(new char[] {'(', ';', ')'}, StringSplitOptions.RemoveEmptyEntries);

                if (parts.GetLength(0) > 0)
                {
                    double re;

                    if (double.TryParse(parts[0], style, provider, out re))
                    {
                        if (parts.GetLength(0) > 1)
                        {
                            double im;

                            if (double.TryParse(parts[1], style, provider, out im))
                            {
                                cn = new ComplexNumber(re, im);
                                FResult = true;
                            }
                        }
                        else
                        { 
                            // As only real part
                            cn = new ComplexNumber(re, 0);
                            FResult = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        


        /// <summary>
        /// Converts an angle from degrees to radians.
        /// </summary>
        /// <param name="degrees">The source angle in degrees.</param>
        /// <returns>Returns an angle in radians if succeeded, double.NaN otherwise.</returns>
        private static double DegreesToRadians(double degrees)
        {
            double FResult = double.NaN;
            try
            {
                FResult = degrees * C_PIdiv180;// MathUtils.DegreesToRadians(degrees);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Converts an angle from radians to degrees.
        /// </summary>
        /// <param name="radians">The source angle in radians.</param>
        /// <returns>Returns an angle in degrees if succeeded, double.NaN otherwise.</returns>
        private static double RadiansToDegrees(double radians)
        {
            double FResult = double.NaN;
            try
            {
                FResult = radians * C_180divPI;// MathUtils.DegreesToRadians(degrees);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Converts an angle from grads to radians.
        /// </summary>
        /// <param name="grads">The source angle in grads.</param>
        /// <returns>Returns an angle in radians if succeeded, double.NaN otherwise.</returns>
        private static double GradsToRadians(double grads)
        {
            double FResult = double.NaN;
            try
            {
                FResult = grads * C_PIdiv200;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }


        /// <summary>
        /// Converts an angle from radians to grads.
        /// </summary>
        /// <param name="radians">The source angle in radians.</param>
        /// <returns>Returns an angle in grads if succeeded, double.NaN otherwise.</returns>
        private static double RadiansToGrads(double radians)
        {
            double FResult = double.NaN;
            try
            {
                FResult = radians * C_200divPI;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return FResult;
        }



        #endregion Methods




        #region IConvertible implementation


        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            if ((this.mRe != 0.0) || (this.mIm != 0.0))
                return true;
            else
                return false;
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return System.Convert.ToByte(this.mRe);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return System.Convert.ToDecimal(this.mRe);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return this.mRe;
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return System.Convert.ToInt16(this.mRe);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return System.Convert.ToInt32(this.mRe);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return System.Convert.ToInt64(this.mRe);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return System.Convert.ToSByte(this.mRe);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return System.Convert.ToSingle(this.mRe);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this.mRe, conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return System.Convert.ToUInt16(this.mRe);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return System.Convert.ToUInt32(this.mRe);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return System.Convert.ToUInt64(this.mRe);
        }

        #endregion IConvertible implementation


    }


}

