/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum FileReadFlags
    {
        FILE_USE_INCLUDE_PATH = 1, // Search for the file in the include_path. 
        FILE_IGNORE_NEW_LINES = 2, // Omit newline at the end of each array element
        FILE_SKIP_EMPTY_LINES = 4  //Skip empty lines
    }
}
