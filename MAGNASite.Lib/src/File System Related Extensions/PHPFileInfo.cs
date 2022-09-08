/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using MAGNASite.Lib.Enums;

namespace MAGNASite.Lib
{
    /// <summary>
    /// File information
    /// </summary>
    internal class PHPFileInfo : ErrorAwareBaseClass
    {
        public static bool FILE_USE_INCLUDE_PATH { get; set; }

        public static void finfo_buffer() {} // Return information about a string buffer
        public static void finfo_close() {} // Close finfo instance

        /// <summary>
        /// Return information about a file
        /// </summary>
        /// <param name="finfo">An finfo instance, returned by finfo_open().</param>
        /// <param name="filename">Name of a file to be checked.</param>
        /// <param name="flags">One or disjunction of more Fileinfo constants.</param>
        /// <param name="context">For a description of contexts, refer to Stream Functions.</param>
        /// <returns>Returns a textual description of the contents of the filename argument, or false if an error occurred.</returns>
        public static string finfo_file(PHPFInfo finfo, string filename, int flags = (int)FileInfoFlags.FILEINFO_NONE, FileStream context = null)
        {
            return string.Empty;
        }

        /// <summary>
        /// Create a new finfo instance
        /// </summary>
        /// <param name="flags">One or disjunction of more Fileinfo constants.</param>
        /// <param name="magic_database">Name of a magic database file, usually something like /path/to/magic.mime.</param>
        /// <returns>(Procedural style only) Returns an finfo instance on success, or false on failure.</returns>
        public static PHPFInfo finfo_open(int flags = (int)FileInfoFlags.FILEINFO_NONE, string magic_database = null)
        {
            // "P:\Program Files\Apache Software Foundation\Apache24\conf\magic"
            return null; // TODO
        }

        public static void finfo_set_flags() {} // Set libmagic configuration options
        public static void mime_content_type() {} // Detect MIME Content-type for a file
    }

    internal class PHPFInfo : ErrorAwareBaseClass
    {
        //public static void buffer() => PHPFileInfo.finfo_buffer(); // Return information about a string buffer
        //public static string file() => PHPFileInfo.finfo_file(); // Return information about a file
        //public static void set_flags() => PHPFileInfo.finfo_set_flags(); // Set libmagic configuration options
    }
}
