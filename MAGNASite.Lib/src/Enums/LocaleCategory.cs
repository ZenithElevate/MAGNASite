/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */

namespace MAGNASite.Lib
{
    internal partial class PHPStrings
    {
        internal enum LocaleCategory
        {
            LC_ALL,         // for all of the below
            LC_COLLATE,     // for string comparison, see strcoll()
            LC_CTYPE,       // for character classification and conversion, for example strtoupper()
            LC_MONETARY,    // for localeconv()
            LC_NUMERIC,     // for decimal separator(See also localeconv())
            LC_TIME,        // for date and time formatting with strftime()
            LC_MESSAGES     // for system responses(available if PHP was compiled with libintl)
        }
    }
}
