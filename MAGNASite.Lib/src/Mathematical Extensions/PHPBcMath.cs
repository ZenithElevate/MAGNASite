/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */

namespace MAGNASite.Lib
{

    internal class PHPBcMath
    {
        /// <summary>
        /// Add two arbitrary precision numbers
        /// </summary>
        /// <param name="num1">The left operand, as a string.</param>
        /// <param name="num2">The right operand, as a string.</param>
        /// <param name="scale">This optional parameter is used to set the number of digits after the decimal place in the result. If omitted, it will default to the scale set globally with the bcscale() function, or fallback to 0 if this has not been set.</param>
        /// <returns>The sum of the two operands, as a string.</returns>
        public static string bcadd(string num1, string num2, int? scale = null) 
        {
            return num1; // TODO
        }

        public static void bccomp() { } //Compare two arbitrary precision numbers
        public static void bcdiv() { } //Divide two arbitrary precision numbers
        public static void bcmod() { } //Get modulus of an arbitrary precision number
        public static void bcmul() { } //Multiply two arbitrary precision numbers
        public static void bcpow() { } //Raise an arbitrary precision number to another
        public static void bcpowmod() { } //Raise an arbitrary precision number to another, reduced by a specified modulus
        public static void bcscale() { } //Set or get default scale parameter for all bc math functions
        public static void bcsqrt() { } //Get the square root of an arbitrary precision number
        public static void bcsub() { } //Subtract one arbitrary precision number from another
    }
}
