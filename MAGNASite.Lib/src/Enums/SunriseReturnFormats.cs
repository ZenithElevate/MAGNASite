/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    internal enum SunriseReturnFormats
    {
        SUNFUNCS_RET_STRING,    // returns the result as string 16:46
        SUNFUNCS_RET_DOUBLE,    // returns the result as float 16.78243132
        SUNFUNCS_RET_TIMESTAMP  // returns the result as int (timestamp) 1095034606
    }
}