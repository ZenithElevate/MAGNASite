/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Runtime.InteropServices;
using MAGNASite.Lib.Enums;

namespace MAGNASite.Lib
{
    /// <summary>
    /// Directories
    /// </summary>
    internal class PHPDirectories : ErrorAwareBaseClass
    {
        public static bool FILE_USE_INCLUDE_PATH { get; set; }

        /// <summary>
        /// Change directory
        /// </summary>
        /// <param name="directory">The new current directory.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool chdir(string directory) 
        {
            if (Directory.Exists(directory))
            {
                Directory.SetCurrentDirectory(directory);
            }
            return true;
        }

        /// <summary>
        /// Change the root directory
        /// </summary>
        /// <param name="directory">The path to change the root directory to.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool chroot(string directory) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return false; // TODO
            }
            else
            {
                return false; // TODO
            }
        }

        /// <summary>
        /// Close directory handle
        /// </summary>
        /// <param name="dir_handle">The directory handle resource previously opened with opendir(). If the directory handle is not specified, the last link opened by opendir() is assumed.</param>
        public static void closedir(FileStream dir_handle = null)
        {
            dir_handle.Close();
        }

        public static void dir() {} //Return an instance of the Directory class

        /// <summary>
        /// Gets the current working directory
        /// </summary>
        /// <returns>Returns the current working directory on success, or false on failure.</returns>
        public static string getcwd()
        {
            return Directory.GetCurrentDirectory();
        }

        public static void opendir() {} //Open directory handle
        public static void readdir() {} //Read entry from directory handle
        public static void rewinddir() {} //Rewind directory handle

        /// <summary>
        /// List files and directories inside the specified path
        /// </summary>
        /// <param name="directory">The directory that will be scanned.</param>
        /// <param name="sorting_order">The sort order</param>
        /// <param name="context">For a description of the context parameter, refer to the streams section of the manual.</param>
        public static string[] scandir(string directory, int sorting_order = (int)DirScanOrders.SCANDIR_SORT_ASCENDING, FileStream context = null)
        {
            switch ((DirScanOrders)sorting_order)
            {
                case DirScanOrders.SCANDIR_SORT_DESCENDING:
                    return Directory.GetFileSystemEntries(directory).OrderByDescending(e => e).ToArray();
                case DirScanOrders.SCANDIR_SORT_NONE:
                    return Directory.GetFileSystemEntries(directory);
                //case DirScanOrders.SCANDIR_SORT_ASCENDING:
                default:
                    return Directory.GetFileSystemEntries(directory).OrderBy(e => e).ToArray();
            }
        }
    }
}
