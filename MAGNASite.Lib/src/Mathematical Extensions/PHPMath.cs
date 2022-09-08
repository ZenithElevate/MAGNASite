/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */

using MAGNASite.Lib.src.Enums;

namespace MAGNASite.Lib
{

    internal class PHPMath
    {
        public const double M_PI = 3.14159265358979323846; 	    // Pi
        public const double M_E = 2.7182818284590452354;        // e
        public const double M_LOG2E = 1.4426950408889634074;    // log_2 e
        public const double M_LOG10E = 0.43429448190325182765;  // log_10 e
        public const double M_LN2 = 0.69314718055994530942;     // log_e 2 	 
        public const double M_LN10 = 2.30258509299404568402;    // log_e 10 	 
        public const double M_PI_2 = 1.57079632679489661923;    // pi/2 	 
        public const double M_PI_4 = 0.78539816339744830962;    // pi/4 	 
        public const double M_1_PI = 0.31830988618379067154;    // 1/pi
        public const double M_2_PI = 0.63661977236758134308;    // 2/pi
        public const double M_SQRTPI = 1.77245385090551602729;  // sqrt(pi)
        public const double M_2_SQRTPI = 1.12837916709551257390;// 2/sqrt(pi)
        public const double M_SQRT2 = 1.41421356237309504880;   // sqrt(2)
        public const double M_SQRT3 = 1.73205080756887729352;   // sqrt(3)
        public const double M_SQRT1_2 = 0.70710678118654752440; // 1/sqrt(2)
        public const double M_LNPI = 1.14472988584940017414;    // log_e(pi)
        public const double M_EULER = 0.57721566490153286061;   // Euler constant
        //public const double NAN         = NAN; 	              // (as a double)    Not A Number
        //public const double INF         = INF; 	              // (as a double)    The infinite

        public const int MT_RAND_MT19937 = 0;   // Uses the fixed, correct, Mersenne Twister implementation, available as of PHP 7.1.0.
        public const int MT_RAND_PHP = 0;       // Uses an incorrect Mersenne Twister implementation which was used as the default up till PHP 7.1.0. This mode is available for backward compatibility. 

        static Random Rng = new Random();

        /// <summary>
        /// Absolute value
        /// </summary>
        /// <param name="num">The numeric value to process.</param>
        /// <returns>The absolute value of num. If the argument num is of type double, the return type is also double, otherwise it is int (as double usually has a bigger value range than int).</returns>
        public static int abs(int num)
        {
            return Math.Abs(num);
        }

        /// <summary>
        /// Absolute value
        /// </summary>
        /// <param name="num">The numeric value to process.</param>
        /// <returns>The absolute value of num. If the argument num is of type double, the return type is also double, otherwise it is int (as double usually has a bigger value range than int).</returns>
        public static double abs(double num)
        {
            return Math.Abs(num);
        }

        /// <summary>
        /// Arc cosine
        /// </summary>
        /// <param name="num">The numeric value to process.</param>
        /// <returns>The arc cosine of num in radians.</returns>
        public static double acos(double num)
        {
            return Math.Acos(num);
        }

        /// <summary>
        /// Inverse hyperbolic cosine
        /// </summary>
        /// <param name="num">The numeric value to process.</param>
        /// <returns>The inverse hyperbolic cosine of num.</returns>
        public static double acosh(double num)
        {
            return Math.Acosh(num);
        }

        /// <summary>
        /// Arc sine
        /// </summary>
        /// <param name="num">The numeric value to process.</param>
        /// <returns>The arc sine of num in radians.</returns>
        public static double asin(double num)
        {
            return Math.Asin(num);
        }

        /// <summary>
        /// Inverse hyperbolic sine
        /// </summary>
        /// <param name="num">The numeric value to process.</param>
        /// <returns>The inverse hyperbolic sine of num.</returns>
        public static double asinh(double num)
        {
            return Math.Asinh(num);
        }

        /// <summary>
        /// Arc tangent of two variables
        /// </summary>
        /// <param name="y">Dividend parameter.</param>
        /// <param name="x">Divisor parameter.</param>
        /// <returns>The arc tangent of y/x in radians.</returns>
        public static double atan2(double y, double x)
        {
            return Math.Atan2(y, x);
        }

        /// <summary>
        /// Arc tangent
        /// </summary>
        /// <param name="num">The numeric value to process.</param>
        /// <returns>The arc tangent of num in radians.</returns>
        public static double atan(double num)
        {
            return Math.Atan(num);
        }

        /// <summary>
        /// Inverse hyperbolic tangent
        /// </summary>
        /// <param name="num">The numeric value to process.</param>
        /// <returns>Inverse hyperbolic tangent of num</returns>
        public static double atanh(double num)
        {
            return Math.Atanh(num);
        }

        /// <summary>
        /// Convert a number between arbitrary bases
        /// </summary>
        /// <param name="num">The number to convert. Any invalid characters in num are silently ignored. As of PHP 7.4.0 supplying any invalid characters is deprecated.</param>
        /// <param name="from_base">The base num is in.</param>
        /// <param name="to_base">The base to convert num to.</param>
        /// <returns> num converted to base to_base.</returns>
        public static string base_convert(string num, int from_base, int to_base)
        {
            return num; // TODO
        }

        /// <summary>
        /// Binary to decimal
        /// </summary>
        /// <param name="binary_string">The binary string to convert. Any invalid characters in binary_string are silently ignored. As of PHP 7.4.0 supplying any invalid characters is deprecated.</param>
        /// <returns>The decimal value of binary_string.</returns>
        public static long bindec(string binary_string)
        {
            return Convert.ToInt64(binary_string);
        }

        /// <summary>
        /// Round fractions up
        /// </summary>
        /// <param name="num">The value to round.</param>
        /// <returns>num rounded up to the next highest integer. The return value of ceil() is still of type float as the value range of float is usually bigger than that of int.</returns>
        public static double ceil(double num)
        {
            return Math.Ceiling(num);
        }

        /// <summary>
        /// Cosine
        /// </summary>
        /// <param name="num">An angle in radians.</param>
        /// <returns>The cosine of num.</returns>
        public static double cos(double num)
        {
            return Math.Cos(num);
        }

        /// <summary>
        /// Hyperbolic cosine
        /// </summary>
        /// <param name="num">The argument to process.</param>
        /// <returns>The hyperbolic cosine of num.</returns>
        public static double cosh(double num)
        {
            return Math.Cosh(num);
        }

        /// <summary>
        /// Decimal to binary
        /// </summary>
        /// <param name="num">Decimal value to convert.</param>
        /// <returns>Binary string representation of num.</returns>
        public static string decbin(long num)
        {
            return Convert.ToString(num, 2);
        }

        /// <summary>
        /// Decimal to hexadecimal
        /// </summary>
        /// <param name="num">Decimal value to convert.</param>
        /// <returns>Hexadecimal string representation of num.</returns>
        public static string dechex(long num)
        {
            return Convert.ToString(num, 16);
        }

        /// <summary>
        /// Decimal to octal
        /// </summary>
        /// <param name="num">Decimal value to convert.</param>
        /// <returns>Octal string representation of num.</returns>
        public static string decoct(long num)
        {
            return Convert.ToString(num, 8);
        }

        /// <summary>
        /// Converts the number in degrees to the radian equivalent
        /// </summary>
        /// <param name="num">Angular value in degrees.</param>
        /// <returns>The radian equivalent of num.</returns>
        public static double deg2rad(double num)
        {
            return M_PI * num / 180;
        }

        /// <summary>
        /// Calculates the exponent of e
        /// </summary>
        /// <param name="num">The power to raise e to.</param>
        /// <returns>'e' raised to the power of num.</returns>
        public static double exp(double num)
        {
            return Math.Pow(M_E, num);
        } 

        public static void expm1() { } // Returns exp(number) - 1, computed in a way that is accurate even when the value of number is close to zero

        /// <summary>
        /// Returns exp(number) - 1, computed in a way that is accurate even when the value of number is close to zero
        /// </summary>
        /// <param name="num1">The dividend (numerator).</param>
        /// <param name="num2">The divisor.</param>
        /// <returns>The floating point result of num1/num2.</returns>
        public static double expm1(double num1, double num2)
        {
            return num1 / num2;
        }

        /// <summary>
        /// Round fractions down
        /// </summary>
        /// <param name="num">The numeric value to round.</param>
        /// <returns>num rounded to the next lowest integer. The return value of floor() is still of type float because the value range of float is usually bigger than that of int. This function returns false in case of an error (e.g. passing an array).</returns>
        public static double floor(double num)
        {
            return Math.Floor(num);
        }

        /// <summary>
        /// Returns the doubleing point remainder(modulo) of the division of the arguments
        /// </summary>
        /// <param name="num1">The dividend.</param>
        /// <param name="num2">The divisor.</param>
        /// <returns>The floating point remainder of num1/num2.</returns>
        public static double fmod(double num1, double num2)
        {
            return num1 % num2;
        }

        /// <summary>
        /// Show largest possible random value
        /// </summary>
        /// <returns>The largest possible random value returned by rand()</returns>
        public static int getrandmax()
        {
            return short.MaxValue;
        }

        /// <summary>
        /// Hexadecimal to decimal
        /// </summary>
        /// <param name="hex_string">The hexadecimal string to convert.</param>
        /// <returns>The decimal representation of hex_string.</returns>
        public static long hexdec(string hex_string)
        {
            return Convert.ToInt64(hex_string);
        }

        /// <summary>
        /// Calculate the length of the hypotenuse of a right-angle triangle
        /// </summary>
        /// <param name="x">Length of first side.</param>
        /// <param name="y">Length of second side.</param>
        /// <returns>Calculated length of the hypotenuse.</returns>
        public static double hypot(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// Integer division
        /// </summary>
        /// <param name="num1">Number to be divided.</param>
        /// <param name="num2">Number which divides the num1.</param>
        /// <returns></returns>
        public static long intdiv(long num1, long num2)
        {
            return num1 / num2;
        }

        /// <summary>
        /// Finds whether a value is a legal finite number
        /// </summary>
        /// <param name="num">The value to check.</param>
        /// <returns>true if num is a legal finite number within the allowed range for a PHP float on this platform, else false.</returns>
        public static bool is_finite(double num)
        {
            return !double.IsNaN(num) && !double.IsInfinity(num);
        }

        /// <summary>
        /// Finds whether a value is infinite
        /// </summary>
        /// <param name="num">The value to check.</param>
        /// <returns>true if num is infinite, else false.</returns>
        public static bool is_infinite(double num)
        {
            return double.IsInfinity(num);
        }

        /// <summary>
        /// Finds whether a value is not a number
        /// </summary>
        /// <param name="num">The value to check.</param>
        /// <returns>Returns true if num is 'not a number', else false.</returns>
        public static bool is_nan(double num)
        {
            return double.IsNaN(num);
        }

        /// <summary>
        /// Combined linear congruential generator
        /// </summary>
        /// <returns>A pseudo random float value between 0.0 and 1.0, inclusive.</returns>
        public static double lcg_value()
        {
            return Rng.NextDouble();
        }

        /// <summary>
        /// Base-10 logarithm
        /// </summary>
        /// <param name="num">The argument to process.</param>
        /// <returns>The base-10 logarithm of num.</returns>
        public static double log10(double num)
        {
            return Math.Log10(num);
        }

        /// <summary>
        /// Returns log(1 + number), computed in a way that is accurate even when the value of number is close to zero
        /// </summary>
        /// <param name="num">The argument to process.</param>
        /// <returns>log(1 + num)</returns>
        public static double log1p(double num) 
        {
            return Math.Log(1 + num);
        }

        /// <summary>
        /// Natural logarithm
        /// </summary>
        /// <param name="num">The argument to process.</param>
        /// <returns>The logarithm of num to base, if given, or the natural logarithm.</returns>
        public static double log(double num)
        {
            return Math.Log(num);
        }

        /// <summary>
        /// Find highest value
        /// </summary>
        /// <param name="values">Any comparable values.</param>
        /// <returns>max() returns the parameter value considered "highest" according to standard comparisons. If multiple values of different types evaluate as equal (e.g. 0 and 'abc') the first provided to the function will be returned.</returns>
        /// <exception cref="Exception"></exception>
        public static long max(params long[] values) 
        {
            if (values == null)
            {
                throw new Exception("Warning:  max() expects at least 1 parameter, 0 given in "); // TODO
            }

            return values.Max();
        }

        /// <summary>
        /// Find highest value
        /// </summary>
        /// <param name="values">Any comparable values.</param>
        /// <returns>max() returns the parameter value considered "highest" according to standard comparisons. If multiple values of different types evaluate as equal (e.g. 0 and 'abc') the first provided to the function will be returned.</returns>
        /// <exception cref="Exception"></exception>
        public static long max(IEnumerable<long> values)
        {
            if (values == null)
            {
                throw new Exception("Warning:  max() expects at least 1 parameter, 0 given in "); // TODO
            }

            return values.Max();
        } // Find highest value

        /// <summary>
        /// Find highest value
        /// </summary>
        /// <param name="values">Any comparable values.</param>
        /// <returns>max() returns the parameter value considered "highest" according to standard comparisons. If multiple values of different types evaluate as equal (e.g. 0 and 'abc') the first provided to the function will be returned.</returns>
        /// <exception cref="Exception"></exception>
        public static double max(params double[] values) 
        {
            if (values == null)
            {
                throw new Exception("Warning:  max() expects at least 1 parameter, 0 given in "); // TODO
            }

            return values.Max();
        } // Find highest value

        /// <summary>
        /// Find highest value
        /// </summary>
        /// <param name="values">Any comparable values.</param>
        /// <returns>max() returns the parameter value considered "highest" according to standard comparisons. If multiple values of different types evaluate as equal (e.g. 0 and 'abc') the first provided to the function will be returned.</returns>
        /// <exception cref="Exception"></exception>
        public static double max(IEnumerable<double> values) 
        {
            if (values == null)
            {
                throw new Exception("Warning:  max() expects at least 1 parameter, 0 given in "); // TODO
            }

            return values.Max();
        } // Find highest value

        /// <summary>
        /// Find lowest value
        /// </summary>
        /// <param name="values">Any comparable values.</param>
        /// <returns>min() returns the parameter value considered "lowest" according to standard comparisons. If multiple values of different types evaluate as equal (e.g. 0 and 'abc') the first provided to the function will be returned.</returns>
        public static long min(params long[] values)
        {
            if (values == null)
            {
                throw new Exception("Warning:  min() expects at least 1 parameter, 0 given in "); // TODO
            }

            return values.Min();
        }

        /// <summary>
        /// Find lowest value
        /// </summary>
        /// <param name="values">Any comparable values.</param>
        /// <returns>min() returns the parameter value considered "lowest" according to standard comparisons. If multiple values of different types evaluate as equal (e.g. 0 and 'abc') the first provided to the function will be returned.</returns>
        public static long min(IEnumerable<long> values)
        {
            if (values == null)
            {
                throw new Exception("Warning:  min() expects at least 1 parameter, 0 given in "); // TODO
            }

            return values.Min();
        }

        /// <summary>
        /// Find lowest value
        /// </summary>
        /// <param name="values">Any comparable values.</param>
        /// <returns>min() returns the parameter value considered "lowest" according to standard comparisons. If multiple values of different types evaluate as equal (e.g. 0 and 'abc') the first provided to the function will be returned.</returns>
        public static double min(params double[] values)
        {
            if (values == null)
            {
                throw new Exception("Warning:  min() expects at least 1 parameter, 0 given in "); // TODO
            }

            return values.Min();
        }

        /// <summary>
        /// Find lowest value
        /// </summary>
        /// <param name="values">Any comparable values.</param>
        /// <returns>min() returns the parameter value considered "lowest" according to standard comparisons. If multiple values of different types evaluate as equal (e.g. 0 and 'abc') the first provided to the function will be returned.</returns>
        public static double min(IEnumerable<double> values)
        {
            if (values == null)
            {
                throw new Exception("Warning:  min() expects at least 1 parameter, 0 given in "); // TODO
            }

            return values.Min();
        }

        /// <summary>
        /// Show largest possible random value
        /// </summary>
        /// <returns>Returns the maximum random value returned by a call to mt_rand() without arguments, which is the maximum value that can be used for its max parameter without the result being scaled up (and therefore less random).</returns>
        public static long mt_getrandmax()
        {
            return int.MaxValue; // 2147483647
        }

        /// <summary>
        /// Generate a random value via the Mersenne Twister Random Number Generator
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long mt_rand(long min = 0, long max = int.MaxValue) 
        {
            return (long)Rng.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Seeds the Mersenne Twister Random Number Generator
        /// </summary>
        /// <param name="seed">An arbitrary int seed value.</param>
        /// <param name="mode">Use one of the following constants to specify the implementation of the algorithm to use.</param>
        public static void mt_srand(int seed = 0, int mode = MT_RAND_MT19937)
        {
            Rng = new Random(seed);
        }

        /// <summary>
        /// Octal to decimal
        /// </summary>
        /// <param name="octal_string">The octal string to convert. Any invalid characters in octal_string are silently ignored. As of PHP 7.4.0 supplying any invalid characters is deprecated.</param>
        /// <returns>The decimal representation of octal_string.</returns>
        public static long octdec(string octal_string)
        {
            return Convert.ToInt64(octal_string);
        }

        /// <summary>
        /// Get value of pi
        /// </summary>
        /// <returns>The value of pi as float.</returns>
        public static double pi()
        {
            return M_PI;
        }

        /// <summary>
        /// Exponential expression
        /// </summary>
        /// <param name="num">The base to use.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns>num raised to the power of exponent. If both arguments are non-negative integers and the result can be represented as an integer, the result will be returned with int type, otherwise it will be returned as a float.</returns>
        public static long pow(long num, long exponent)
        {
            return (long)Math.Pow(num, exponent);
        }

        /// <summary>
        /// Exponential expression
        /// </summary>
        /// <param name="num">The base to use.</param>
        /// <param name="exponent">The exponent.</param>
        /// <returns>num raised to the power of exponent. If both arguments are non-negative integers and the result can be represented as an integer, the result will be returned with int type, otherwise it will be returned as a float.</returns>
        public static double pow(double num, double exponent)
        {
            return Math.Pow(num, exponent);
        }

        /// <summary>
        /// Converts the radian number to the equivalent number in degrees
        /// </summary>
        /// <param name="num">A radian value.</param>
        /// <returns>The equivalent of num in degrees.</returns>
        public static double rad2deg(double num)
        {
            return num * 180 / M_PI;
        }

        /// <summary>
        /// Generate a random integer
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long rand(long min = 0, long max = int.MaxValue)
        {
            return (long)Rng.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Rounds a double
        /// </summary>
        /// <param name="num">The value to round.</param>
        /// <param name="precision">The optional number of decimal digits to round to.</param>
        /// <param name="mode">Use one of the following constants to specify the mode in which rounding occurs.</param>
        /// <returns>The value rounded to the given precision as a float.</returns>
        public static double round(double num, int precision = 0, int mode = (int)RoundModes.PHP_ROUND_HALF_UP)
        {
            return Math.Round(num, precision, (MidpointRounding)mode);
        }

        /// <summary>
        /// Sine
        /// </summary>
        /// <param name="num">A value in radians.</param>
        /// <returns>The sine of num.</returns>
        public static double sin(double num)
        {
            return Math.Sin(num);
        }

        /// <summary>
        /// Hyperbolic sine
        /// </summary>
        /// <param name="num">The argument to process.</param>
        /// <returns>The hyperbolic sine of num.</returns>
        public static double sinh(double num)
        {
            return Math.Sinh(num);
        }

        /// <summary>
        /// Square root
        /// </summary>
        /// <param name="num">The argument to process.</param>
        /// <returns>The square root of num or the special value NAN for negative numbers.</returns>
        public static double sqrt(double num)
        {
            return Math.Sqrt(num);
        }

        /// <summary>
        /// Seed the random number generator
        /// </summary>
        /// <param name="seed">An arbitrary int seed value.</param>
        /// <param name="mode">???</param>
        public static void srand(int seed = 0, int mode = MT_RAND_MT19937) => mt_srand(seed, mode);

        /// <summary>
        /// Tangent
        /// </summary>
        /// <param name="num">The argument to process in radians.</param>
        /// <returns>The tangent of num.</returns>
        public static double tan(double num)
        {
            return Math.Tan(num);
        }

        /// <summary>
        /// Hyperbolic tangent
        /// </summary>
        /// <param name="num">The argument to process.</param>
        /// <returns>The hyperbolic tangent of num.</returns>
        public static double tanh(double num)
        {
            return Math.Tanh(num);
        }
    }
}
