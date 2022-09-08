/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    internal enum IniScannerModes
    {
        INI_SCANNER_NORMAL, // ?
        INI_SCANNER_RAW,    // Option values will not be parsed.
        INI_SCANNER_TYPED   // In this mode boolean, null and integer types are preserved when possible. String values "true", "on" and "yes" are converted to true. "false", "off", "no" and "none" are considered false. "null" is converted to null in typed mode. Also, all numeric strings are converted to integer type if it is possible.
    }
}
