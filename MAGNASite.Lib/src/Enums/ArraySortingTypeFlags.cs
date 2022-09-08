/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    internal enum ArraySortingTypeFlags //used by various sort functions
    {
        SORT_REGULAR, // SORT_REGULAR is used to compare items normally. 
        SORT_NUMERIC, // SORT_NUMERIC is used to compare items numerically. 
        SORT_STRING, // SORT_STRING is used to compare items as strings. 
        SORT_LOCALE_STRING, // SORT_LOCALE_STRING is used to compare items as strings, based on the current locale. 
        SORT_NATURAL, // SORT_NATURAL is used to compare items as strings using "natural ordering" like natsort(). 
        SORT_FLAG_CASE, // SORT_FLAG_CASE can be combined(bitwise OR) with SORT_STRING or SORT_NATURAL to sort strings case-insensitively.
    }
}