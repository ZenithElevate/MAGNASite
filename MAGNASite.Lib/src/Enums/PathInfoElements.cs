/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum PathInfoElements
    {
        PATHINFO_ALL = 1,
        PATHINFO_DIRNAME = 2,
        PATHINFO_BASENAME = 4,
        PATHINFO_EXTENSION = 8,
        PATHINFO_FILENAME = 16
    }
}
