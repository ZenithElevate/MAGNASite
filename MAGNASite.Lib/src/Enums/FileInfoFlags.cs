/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum FileInfoFlags
    {
        FILEINFO_NONE,              // No special handling. 
        FILEINFO_SYMLINK,           // Follow symlinks. 
        FILEINFO_MIME_TYPE,         // Return the mime type. 
        FILEINFO_MIME_ENCODING,     // Return the mime encoding of the file. 
        FILEINFO_MIME,              // Return the mime type and mime encoding as defined by RFC 2045. 
        FILEINFO_COMPRESS,          // Decompress compressed files. Disabled due to thread safety issues. 
        FILEINFO_DEVICES,           // Look at the contents of blocks or character special devices. 
        FILEINFO_CONTINUE,          // Return all matches, not just the first. 
        FILEINFO_PRESERVE_ATIME,    // If possible preserve the original access time. 
        FILEINFO_RAW,               // Don't translate unprintable characters to a \ooo octal representation. 
        FILEINFO_EXTENSION          // Returns the file extension appropriate for the MIME type detected in the file. For types that commonly have multiple file extensions, such as JPEG images, then the return value is multiple extensions separated by a forward slash e.g.: "jpeg/jpg/jpe/jfif". For unknown types not available in the magic.mime database, then return value is "???". Available since PHP 7.2.0.     }
    }
}
