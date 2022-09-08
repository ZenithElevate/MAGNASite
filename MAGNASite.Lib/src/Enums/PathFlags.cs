/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum PathFlags
    {
        XDIFF_PATCH_NORMAL = 1,     // default mode, normal patch
        XDIFF_PATCH_REVERSE = 2,    // reversed patch
        XDIFF_PATCH_IGNORESPACE = 4 // Starting from version 1.5.0, you can also use binary OR to enable
    }
}
