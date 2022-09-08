/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum FileGlobFlags
    {
        GLOB_MARK = 1,      // Adds a slash (a backslash on Windows) to each directory returned
        GLOB_NOSORT = 2,    // Return files as they appear in the directory (no sorting). When this flag is not used, the pathnames are sorted alphabetically
        GLOB_NOCHECK = 4,   // Return the search pattern if no files matching it were found
        GLOB_NOESCAPE = 8,  // Backslashes do not quote metacharacters
        GLOB_BRACE = 16,    // Expands {a, b, c} to match 'a', 'b', or 'c'
        GLOB_ONLYDIR = 32,  // Return only directory entries which match the pattern
        GLOB_ERR = 64,      // Stop on read errors(like unreadable directories), by default errors are ignored.
    }
}
