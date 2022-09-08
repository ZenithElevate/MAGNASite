/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    /// <summary>
    /// 100 times the number of ticks in a time unit: divide by 100 to get the actual value.
    /// </summary>
    internal enum HRTimeUnits
    {
        SECOND = 1000000000,
        MILLISECOND = 1000000,
        MICROSECOND = 1000,
        NANOSECOND = 1,
    }
}
