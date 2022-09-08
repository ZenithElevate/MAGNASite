/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Runtime.InteropServices;

namespace MAGNASite.Lib
{
    internal class PHPXattr : ErrorAwareBaseClass
    {
        public static bool FILE_USE_INCLUDE_PATH { get; set; }

        public static void xattr_get() { } // Get an extended attribute
        public static void xattr_list() { } // Get a list of extended attributes
        public static void xattr_remove() { } // Remove an extended attribute
        public static void xattr_set() { } // Set an extended attribute

        /// <summary>
        /// Check if filesystem supports extended attributes
        /// </summary>
        /// <param name="filename">The path of the tested file.</param>
        /// <param name="flags"></param>
        /// <returns>This function returns true if filesystem supports extended attributes, false if it doesn't and null if it can't be determined (for example wrong path or lack of permissions to file).</returns>
        public static bool xattr_supported(string filename, int flags = 0)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true; // TODO: DeviceIoControl(h, FSCTL_FILESYSTEM_GET_STATISTICS, NULL, 0, buffer, sizeof(buffer), &dw, NULL); fs = (FILESYSTEM_STATISTICS *)buffer; fs->FileSystemType == FILESYSTEM_STATISTICS_TYPE_NTFS;
            }
            else
            {
                return false; // TODO
            }
        }
    }
}
