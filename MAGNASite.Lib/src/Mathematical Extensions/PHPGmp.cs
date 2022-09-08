/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */

using Math.Gmp.Native;

namespace MAGNASite.Lib
{

    internal class PHPGMP
    {
        public static PHPGMP gmp_abs(PHPGMP num)
        {
            return null;
        } //Absolute value

        public static PHPGMP gmp_abs(long num)
        {
            return null;
        } //Absolute value

        public static PHPGMP gmp_abs(string num)
        {
            return null;
        } //Absolute value

        public static void gmp_add() { } //Add numbers
        public static void gmp_and() { } //Bitwise AND
        public static void gmp_binomial() { } //Calculates binomial coefficient
        public static void gmp_clrbit() { } //Clear bit
        public static void gmp_cmp() { } //Compare numbers
        public static void gmp_com() { } //Calculates one's complement
        public static void gmp_div_q() { } //Divide numbers
        public static void gmp_div_qr() { } //Divide numbers and get quotient and remainder
        public static void gmp_div_r() { } //Remainder of the division of numbers
        public static void gmp_div() { } //Alias of gmp_div_q
        public static void gmp_divexact() { } //Exact division of numbers
        public static void gmp_export() { } //Export to a binary string
        public static void gmp_fact() { } //Factorial
        public static void gmp_gcd() { } //Calculate GCD
        public static void gmp_gcdext() { } //Calculate GCD and multipliers
        public static void gmp_hamdist() { } //Hamming distance
        public static void gmp_import() { } //Import from a binary string
        public static void gmp_init() { } //Create GMP number
        public static void gmp_intval() { } //Convert GMP number to integer
        public static void gmp_invert() { } //Inverse by modulo
        public static void gmp_jacobi() { } //Jacobi symbol
        public static void gmp_kronecker() { } //Kronecker symbol
        public static void gmp_lcm() { } //Calculate LCM
        public static void gmp_legendre() { } //Legendre symbol
        public static void gmp_mod() { } //Modulo operation
        public static void gmp_mul() { } //Multiply numbers
        public static void gmp_neg() { } //Negate number
        public static void gmp_nextprime() { } //Find next prime number
        public static void gmp_or() { } //Bitwise OR
        public static void gmp_perfect_power() { } //Perfect power check
        public static void gmp_perfect_square() { } //Perfect square check
        public static void gmp_popcount() { } //Population count
        public static void gmp_pow() { } //Raise number into power
        public static void gmp_powm() { } //Raise number into power with modulo
        public static void gmp_prob_prime() { } //Check if number is "probably prime"
        public static void gmp_random_bits() { } //Random number
        public static void gmp_random_range() { } //Random number
        public static void gmp_random_seed() { } //Sets the RNG seed
        public static void gmp_random() { } //Random number
        public static void gmp_root() { } //Take the integer part of nth root
        public static void gmp_rootrem() { } //Take the integer part and remainder of nth root
        public static void gmp_scan0() { } //Scan for 0
        public static void gmp_scan1() { } //Scan for 1
        public static void gmp_setbit() { } //Set bit
        public static void gmp_sign() { } //Sign of number
        public static void gmp_sqrt() { } //Calculate square root
        public static void gmp_sqrtrem() { } //Square root with remainder
        public static void gmp_strval() { } //Convert GMP number to string
        public static void gmp_sub() { } //Subtract numbers
        public static void gmp_testbit() { } //Tests if a bit is set
        public static void gmp_xor() { } //Bitwise XOR
    }
}
