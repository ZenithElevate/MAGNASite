/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum FileMatchFlags
    {
        FNM_NOESCAPE,   // Disable backslash escaping.
        FNM_PATHNAME,   // Slash in string only matches slash in the given pattern.
        FNM_PERIOD,     // Leading period in string must be exactly matched by period in the given pattern.
        FNM_CASEFOLD    // Caseless match. Part of the GNU extension. 
    }
}
