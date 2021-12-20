using System;


namespace ComplexN.Common
{

    /// <summary>
    /// Implements gamma function.
    /// </summary>
    internal static class Gammafunc
    {
        /*************************************************************************
        Gamma function

        Input parameters:
            X   -   argument

        Domain:
            0 < X < 171.6
            -170 < X < 0, X is not an integer.

        Relative error:
         arithmetic   domain     # trials      peak         rms
            IEEE    -170,-33      20000       2.3e-15     3.3e-16
            IEEE     -33,  33     20000       9.4e-16     2.2e-16
            IEEE      33, 171.6   20000       2.3e-15     3.2e-16

        Cephes Math Library Release 2.8:  June, 2000
        Original copyright 1984, 1987, 1989, 1992, 2000 by Stephen L. Moshier
        Translated to AlgoPascal by Bochkanov Sergey (2005, 2006, 2007).
        *************************************************************************/
        /// <summary>
        /// Computes the Gamma function for a given real value.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks>
        /// Gamma function is an extension of the factorial function for real numbers. In x=0 and negative integer values gamma function has poles.
        /// There are infinitely many continuous extensions of the factorial to non-integers: 
        /// infinitely many curves can be drawn through any set of isolated points. 
        /// The gamma function is the most useful solution in practice, being analytic (except at the non-positive integers), 
        /// and it can be characterized in several ways. 
        /// </remarks>
        internal static double Gammafunction(double x)
        {
            double result = 0;
            double p = 0;
            double pp = 0;
            double q = 0;
            double qq = 0;
            double z = 0;
            int i = 0;
            double sgngam = 0;

            sgngam = 1;
            q = Math.Abs(x);
            if ((double)(q) > (double)(33.0))
            {
                if ((double)(x) < (double)(0.0))
                {
                    p = (int)Math.Floor(q);
                    i = (int)Math.Round(p);
                    if (i % 2 == 0)
                    {
                        sgngam = -1;
                    }
                    z = q - p;
                    if ((double)(z) > (double)(0.5))
                    {
                        p = p + 1;
                        z = q - p;
                    }
                    z = q * Math.Sin(Math.PI * z);
                    z = Math.Abs(z);
                    z = Math.PI / (z * gammastirf(q));
                }
                else
                {
                    z = gammastirf(x);
                }
                result = sgngam * z;
                return result;
            }
            z = 1;
            while ((double)(x) >= (double)(3))
            {
                x = x - 1;
                z = z * x;
            }
            while ((double)(x) < (double)(0))
            {
                if ((double)(x) > (double)(-0.000000001))
                {
                    result = z / ((1 + 0.5772156649015329 * x) * x);
                    return result;
                }
                z = z / x;
                x = x + 1;
            }
            while ((double)(x) < (double)(2))
            {
                if ((double)(x) < (double)(0.000000001))
                {
                    result = z / ((1 + 0.5772156649015329 * x) * x);
                    return result;
                }
                z = z / x;
                x = x + 1.0;
            }
            if ((double)(x) == (double)(2))
            {
                result = z;
                return result;
            }
            x = x - 2.0;
            pp = 1.60119522476751861407E-4;
            pp = 1.19135147006586384913E-3 + x * pp;
            pp = 1.04213797561761569935E-2 + x * pp;
            pp = 4.76367800457137231464E-2 + x * pp;
            pp = 2.07448227648435975150E-1 + x * pp;
            pp = 4.94214826801497100753E-1 + x * pp;
            pp = 9.99999999999999996796E-1 + x * pp;
            qq = -2.31581873324120129819E-5;
            qq = 5.39605580493303397842E-4 + x * qq;
            qq = -4.45641913851797240494E-3 + x * qq;
            qq = 1.18139785222060435552E-2 + x * qq;
            qq = 3.58236398605498653373E-2 + x * qq;
            qq = -2.34591795718243348568E-1 + x * qq;
            qq = 7.14304917030273074085E-2 + x * qq;
            qq = 1.00000000000000000320 + x * qq;
            result = z * pp / qq;
            return result;
        }


        /// <summary>
        /// Computes the gamma function using Stirling's formula.
        /// </summary>
        /// <param name="x">The source value to compute his gamma function result.</param>
        /// <returns>The gamma function result.</returns>
        /// <remarks>
        /// Stirling's formula gives a good estimate of , also for non-integers, it does not provide the exact value. Extensions of his formula that correct the error were given by Stirling himself and by Jacques Philippe Marie Binet.
        /// Stirling's approximation is asymptotically equal to the factorial function for large values of x.
        /// </remarks>
        private static double gammastirf(double x)
        {
            double result = 0;
            double y = 0;
            double w = 0;
            double v = 0;
            double stir = 0;

            w = 1 / x;
            stir = 7.87311395793093628397E-4;
            stir = -2.29549961613378126380E-4 + w * stir;
            stir = -2.68132617805781232825E-3 + w * stir;
            stir = 3.47222221605458667310E-3 + w * stir;
            stir = 8.33333333333482257126E-2 + w * stir;
            w = 1 + w * stir;
            y = Math.Exp(x);
            if ((double)(x) > (double)(143.01608))
            {
                v = Math.Pow(x, 0.5 * x - 0.25);
                y = v * (v / y);
            }
            else
            {
                y = Math.Pow(x, x - 0.5) / y;
            }
            result = 2.50662827463100050242 * y * w;
            return result;
        }


    }

}
