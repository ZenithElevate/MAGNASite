/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum FileWriteFlags
    {
        FILE_USE_INCLUDE_PATH = 1,  // Search for filename in the include directory. See include_path for more information.
        FILE_APPEND = 2,            // If file filename already exists, append the data to the file instead of overwriting it.
        LOCK_EX = 4                 // Acquire an exclusive lock on the file while proceeding to the writing. In other words, a flock() call happens between the fopen() call and the fwrite() call. This is not identical to an fopen() call with mode "x". 
    }
}
