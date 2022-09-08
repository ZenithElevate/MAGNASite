/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    internal enum ArrayFilterFlags
    {
        ARRAY_FILTER_USE_VALUE, // ARRAY_FILTER_USE_KEY is used with array_filter() to pass each key as the first argument to the given callback function.
        ARRAY_FILTER_USE_KEY, // ARRAY_FILTER_USE_KEY is used with array_filter() to pass each key as the first argument to the given callback function.
        ARRAY_FILTER_USE_BOTH, // ARRAY_FILTER_USE_BOTH is used with array_filter() to pass both value and key to the given callback function.
        COUNT_NORMAL, // 
        COUNT_RECURSIVE, // 
        EXTR_OVERWRITE, // 
        EXTR_SKIP, // 
        EXTR_PREFIX_SAME, // 
        EXTR_PREFIX_ALL, // 
        EXTR_PREFIX_INVALID, // 
        EXTR_PREFIX_IF_EXISTS, // 
        EXTR_IF_EXISTS, // 
        EXTR_REFS, // 
    }
}