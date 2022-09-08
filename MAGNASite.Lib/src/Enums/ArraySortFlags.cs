/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    internal enum ArraySortFlags
    {
        SORT_REGULAR = 0,       // compare items normally (don't change types)
        SORT_NUMERIC = 1,       // compare items numerically
        SORT_STRING2,           // compare items as strings
        SORT_LOCALE_STRING = 5, // compare items as strings, based on the current locale.It uses the locale, which can be changed using setlocale()
        SORT_NATURAL = 6,       // compare items as strings using "natural ordering" like natsort()
        SORT_FLAG_CASE = 8      // can be combined(bitwise OR) with SORT_STRING or SORT_NATURAL to sort strings case-insensitively
    }
}