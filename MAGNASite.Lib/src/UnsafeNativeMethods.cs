/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text;

namespace MAGNASite.Lib
{
    internal class UnsafeNativeMethods
    {
        /// <summary>
        /// Retrieves the number of milliseconds that have elapsed since the system was started.
        /// </summary>
        /// <returns>The number of milliseconds.</returns>
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.U8)]
        public static extern ulong GetTickCount64();

        /// <summary>
        /// Gets the current unbiased interrupt-time count, in units of 100 nanoseconds. The unbiased interrupt-time count does not include time the system spends in sleep or hibernation.
        /// </summary>
        /// <param name="UnbiasedTime"></param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails because it is called with a null parameter, the return value is zero.</returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool QueryUnbiasedInterruptTime(ref ulong UnbiasedTime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpDirectoryName">A directory on the disk.</param>
        /// <param name="lpFreeBytesAvailable">A pointer to a variable that receives the total number of free bytes on a disk that are available to the user who is associated with the calling thread. This parameter can be NULL.</param>
        /// <param name="lpTotalNumberOfBytes">A pointer to a variable that receives the total number of bytes on a disk that are available to the user who is associated with the calling thread. This parameter can be NULL.</param>
        /// <param name="lpTotalNumberOfFreeBytes">A pointer to a variable that receives the total number of free bytes on a disk. This parameter can be NULL.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        /// <summary>
        /// Establishes a hard link between an existing file and a new file. This function is only supported on the NTFS file system, and only for files, not directories.
        /// </summary>
        /// <param name="lpFileName">The name of the new file.</param>
        /// <param name="lpExistingFileName">The name of the existing file.</param>
        /// <param name="lpSecurityAttributes">Reserved; must be NULL.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

        public struct _FILETIME
        {
            public CLong dwLowDateTime;    // The low-order part of the file time.
            public CLong dwHighDateTime;   // The high-order part of the file time.
        };

        /// <summary>
        /// Contains information that the GetFileInformationByHandle function retrieves.
        /// </summary>
        public struct _BY_HANDLE_FILE_INFORMATION
        {
            public CLong dwFileAttributes;     // The file attributes. For possible values and their descriptions, see File Attribute Constants.
            public _FILETIME ftCreationTime;   // A FILETIME structure that specifies when a file or directory is created. If the underlying file system does not support creation time, this member is zero (0).
            public _FILETIME ftLastAccessTime; // A FILETIME structure. For a file, the structure specifies the last time that a file is read from or written to. For a directory, the structure specifies when the directory is created. For both files and directories, the specified date is correct, but the time of day is always set to midnight. If the underlying file system does not support the last access time, this member is zero (0).
            public _FILETIME ftLastWriteTime;  // A FILETIME structure. For a file, the structure specifies the last time that a file is written to. For a directory, the structure specifies when the directory is created. If the underlying file system does not support the last write time, this member is zero (0).
            public CLong dwVolumeSerialNumber; // The serial number of the volume that contains a file.
            public CLong nFileSizeHigh;        // The high-order part of the file size.
            public CLong nFileSizeLow;         // The low-order part of the file size.
            public CLong nNumberOfLinks;       // The number of links to this file. For the FAT file system this member is always 1. For the NTFS file system, it can be more than 1.
            public CLong nFileIndexHigh;       // The high-order part of a unique identifier that is associated with a file. For more information, see nFileIndexLow.
            public CLong nFileIndexLow;        // The low-order part of a unique identifier that is associated with a file.
        };

        /// <summary>
        /// Retrieves file information for the specified file.
        /// </summary>
        /// <param name="hFile">A handle to the file that contains the information to be retrieved.</param>
        /// <param name="lpFileInformation">A pointer to a BY_HANDLE_FILE_INFORMATION structure that receives the file information.</param>
        /// <returns>If the function succeeds, the return value is nonzero and file information data is contained in the buffer pointed to by the lpFileInformation parameter.<br />
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetFileInformationByHandle(SafeFileHandle hFile, out _BY_HANDLE_FILE_INFORMATION lpFileInformation);

        /// <summary>
        /// Contains the security descriptor for an object and specifies whether the handle retrieved by specifying this structure is inheritable.
        /// </summary>
        public struct _SECURITY_ATTRIBUTES
        {
            CLong nLength;                  // The size, in bytes, of this structure. Set this value to the size of the SECURITY_ATTRIBUTES structure.
            IntPtr lpSecurityDescriptor;    // A pointer to a SECURITY_DESCRIPTOR structure that controls access to the object.
            bool bInheritHandle;            // A Boolean value that specifies whether the returned handle is inherited when a new process is created. If this member is TRUE, the new process inherits the handle.
        }

        public const uint FILE_ATTRIBUTE_ARCHIVE = 32;        // (0x20) The file should be archived.Applications use this attribute to mark files for backup or removal.
        public const uint FILE_ATTRIBUTE_ENCRYPTED = 16384;   // (0x4000) The file or directory is encrypted.For a file, this means that all data in the file is encrypted.For a directory, this means that encryption is the default for newly created files and subdirectories.For more information, see File Encryption.
        public const uint FILE_ATTRIBUTE_HIDDEN = 2;          // (0x2) The file is hidden.Do not include it in an ordinary directory listing.
        public const uint FILE_ATTRIBUTE_NORMAL = 128;        // (0x80) The file does not have other attributes set.This attribute is valid only if used alone.
        public const uint FILE_ATTRIBUTE_OFFLINE = 4096;      // (0x1000) The data of a file is not immediately available.This attribute indicates that file data is physically moved to offline storage.This attribute is used by Remote Storage, the hierarchical storage management software.Applications should not arbitrarily change this attribute.
        public const uint FILE_ATTRIBUTE_READONLY = 1;        // (0x1) The file is read only.Applications can read the file, but cannot write to or delete it.
        public const uint FILE_ATTRIBUTE_SYSTEM = 4;          // (0x4) The file is part of or used exclusively by an operating system.
        public const uint FILE_ATTRIBUTE_TEMPORARY = 256;     // (0x100) The file is being used for temporary storage.

        public const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;     // The file is being opened or created for a backup or restore operation.The system ensures that the calling process overrides file security checks when the process has SE_BACKUP_NAME and SE_RESTORE_NAME privileges.For more information, see Changing Privileges in a Token.
        public const uint FILE_FLAG_DELETE_ON_CLOSE = 0x04000000;      // The file is to be deleted immediately after all of its handles are closed, which includes the specified handle and any other open or duplicated handles.
        public const uint FILE_FLAG_NO_BUFFERING = 0x20000000;         // The file or device is being opened with no system caching for data reads and writes. This flag does not affect hard disk caching or memory mapped files.
        public const uint FILE_FLAG_OPEN_NO_RECALL = 0x00100000;       // The file data is requested, but it should continue to be located in remote storage. It should not be transported back to local storage.This flag is for use by remote storage systems.
        public const uint FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000;   // Normal reparse point processing will not occur; CreateFile will attempt to open the reparse point.When a file is opened, a file handle is returned, whether or not the filter that controls the reparse point is operational.
        public const uint FILE_FLAG_OVERLAPPED = 0x40000000;           // The file or device is being opened or created for asynchronous I/O.
        public const uint FILE_FLAG_POSIX_SEMANTICS = 0x01000000;      // Access will occur according to POSIX rules.This includes allowing multiple files with names, differing only in case, for file systems that support that naming. Use care when using this option, because files created with this flag may not be accessible by applications that are written for MS-DOS or 16-bit Windows.
        public const uint FILE_FLAG_RANDOM_ACCESS = 0x10000000;        // Access is intended to be random. The system can use this as a hint to optimize file caching.
        public const uint FILE_FLAG_SESSION_AWARE = 0x00800000;        // The file or device is being opened with session awareness.If this flag is not specified, then per-session devices (such as a device using RemoteFX USB Redirection) cannot be opened by processes running in session 0. This flag has no effect for callers not in session 0. This flag is supported only on server editions of Windows.
        public const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000;      // Access is intended to be sequential from beginning to end.The system can use this as a hint to optimize file caching.
        public const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;      // Write operations will not go through any intermediate cache, they will go directly to disk.

        public const uint GENERIC_ALL = 28;
        public const uint GENERIC_EXECUTE = 29;
        public const uint GENERIC_WRITE = 30;
        public const uint GENERIC_READ = 31;

        public const uint FILE_SHARE_DELETE = 0x00000004;  // Enables subsequent open operations on a file or device to request delete access.
        public const uint FILE_SHARE_READ = 0x00000001;    // Enables subsequent open operations on a file or device to request read access.
        public const uint FILE_SHARE_WRITE = 0x00000002;   // Enables subsequent open operations on a file or device to request write access.

        public const uint CREATE_ALWAYS = 2;       // Creates a new file, always.
        public const uint CREATE_NEW = 1;          // Creates a new file, only if it does not already exist.
        public const uint OPEN_ALWAYS = 4;         // Opens a file, always.
        public const uint OPEN_EXISTING = 3;       // Opens a file or device, only if it exists.
        public const uint TRUNCATE_EXISTING = 5;   // Opens a file and truncates it so that its size is zero bytes, only if it exists.

        /// <summary>
        /// Creates or opens a file or I/O device.
        /// </summary>
        /// <param name="lpFileName">The name of the file or device to be created or opened. You may use either forward slashes (/) or backslashes (\) in this name.</param>
        /// <param name="dwDesiredAccess">The requested access to the file or device, which can be summarized as read, write, both or 0 to indicate neither).</param>
        /// <param name="dwShareMode">The requested sharing mode of the file or device, which can be read, write, both, delete, all of these, or none (refer to the following table).</param>
        /// <param name="lpSecurityAttributes">A pointer to a SECURITY_ATTRIBUTES structure that contains two separate but related data members: an optional security descriptor, and a Boolean value that determines whether the returned handle can be inherited by child processes.</param>
        /// <param name="dwCreationDisposition">An action to take on a file or device that exists or does not exist.</param>
        /// <param name="dwFlagsAndAttributes">The file or device attributes and flags, FILE_ATTRIBUTE_NORMAL being the most common default value for files.</param>
        /// <param name="hTemplateFile">A valid handle to a template file with the GENERIC_READ access right.</param>
        /// <returns>If the function succeeds, the return value is an open handle to the specified file, device, named pipe, or mail slot.<br />
        /// If the function fails, the return value is INVALID_HANDLE_VALUE.To get extended error information, call GetLastError.</returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFileA(
            [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A valid handle to an open object.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CloseHandle(SafeHandle hObject);

        public enum LCTYPE : uint
        {
            LOCALE_NOUSEROVERRIDE = 0x80000000,   // do not use user overrides
            LOCALE_RETURN_NUMBER = 0x20000000,   // return number instead of string

            // Modifier for genitive names
            LOCALE_RETURN_GENITIVE_NAMES = 0x10000000,   //Flag to return the Genitive forms of month names

            //
            //  The following LCTypes are mutually exclusive in that they may NOT
            //  be used in combination with each other.
            //

            //
            // These are the various forms of the name of the locale:
            //
            LOCALE_SLOCALIZEDDISPLAYNAME = 0x00000002,   // localized name of locale, eg "German (Germany)" in UI language
            LOCALE_SENGLISHDISPLAYNAME = 0x00000072,   // Display name (language + country usually) in English, eg "German (Germany)"
            LOCALE_SNATIVEDISPLAYNAME = 0x00000073,   // Display name in native locale language, eg "Deutsch (Deutschland)

            LOCALE_SLOCALIZEDLANGUAGENAME = 0x0000006f,   // Language Display Name for a language, eg "German" in UI language
            LOCALE_SENGLISHLANGUAGENAME = 0x00001001,   // English name of language, eg "German"
            LOCALE_SNATIVELANGUAGENAME = 0x00000004,   // native name of language, eg "Deutsch"

            LOCALE_SLOCALIZEDCOUNTRYNAME = 0x00000006,   // localized name of country, eg "Germany" in UI language
            LOCALE_SENGLISHCOUNTRYNAME = 0x00001002,   // English name of country, eg "Germany"
            LOCALE_SNATIVECOUNTRYNAME = 0x00000008,   // native name of country, eg "Deutschland"

            // Additional LCTYPEs
            LOCALE_SABBREVLANGNAME = 0x00000003,   // abbreviated language name

            LOCALE_ICOUNTRY = 0x00000005,   // country code
            LOCALE_SABBREVCTRYNAME = 0x00000007,   // abbreviated country name
            LOCALE_IGEOID = 0x0000005B,   // geographical location id

            LOCALE_IDEFAULTLANGUAGE = 0x00000009,   // default language id
            LOCALE_IDEFAULTCOUNTRY = 0x0000000A,   // default country code
            LOCALE_IDEFAULTCODEPAGE = 0x0000000B,   // default oem code page
            LOCALE_IDEFAULTANSICODEPAGE = 0x00001004,   // default ansi code page
            LOCALE_IDEFAULTMACCODEPAGE = 0x00001011,   // default mac code page

            LOCALE_SLIST = 0x0000000C,   // list item separator
            LOCALE_IMEASURE = 0x0000000D,   // 0 = metric, 1 = US

            LOCALE_SDECIMAL = 0x0000000E,   // decimal separator
            LOCALE_STHOUSAND = 0x0000000F,   // thousand separator
            LOCALE_SGROUPING = 0x00000010,   // digit grouping
            LOCALE_IDIGITS = 0x00000011,   // number of fractional digits
            LOCALE_ILZERO = 0x00000012,   // leading zeros for decimal
            LOCALE_INEGNUMBER = 0x00001010,   // negative number mode
            LOCALE_SNATIVEDIGITS = 0x00000013,   // native digits for 0-9

            LOCALE_SCURRENCY = 0x00000014,   // local monetary symbol
            LOCALE_SINTLSYMBOL = 0x00000015,   // uintl monetary symbol
            LOCALE_SMONDECIMALSEP = 0x00000016,   // monetary decimal separator
            LOCALE_SMONTHOUSANDSEP = 0x00000017,   // monetary thousand separator
            LOCALE_SMONGROUPING = 0x00000018,   // monetary grouping
            LOCALE_ICURRDIGITS = 0x00000019,   // # local monetary digits
            LOCALE_IINTLCURRDIGITS = 0x0000001A,   // # uintl monetary digits
            LOCALE_ICURRENCY = 0x0000001B,   // positive currency mode
            LOCALE_INEGCURR = 0x0000001C,   // negative currency mode

            LOCALE_SDATE = 0x0000001D,   // date separator (derived from LOCALE_SSHORTDATE, use that instead)
            LOCALE_STIME = 0x0000001E,   // time separator (derived from LOCALE_STIMEFORMAT, use that instead)
            LOCALE_SSHORTDATE = 0x0000001F,   // short date format string
            LOCALE_SLONGDATE = 0x00000020,   // long date format string
            LOCALE_STIMEFORMAT = 0x00001003,   // time format string
            LOCALE_IDATE = 0x00000021,   // short date format ordering (derived from LOCALE_SSHORTDATE, use that instead)
            LOCALE_ILDATE = 0x00000022,   // long date format ordering (derived from LOCALE_SLONGDATE, use that instead)
            LOCALE_ITIME = 0x00000023,   // time format specifier (derived from LOCALE_STIMEFORMAT, use that instead)
            LOCALE_ITIMEMARKPOSN = 0x00001005,   // time marker position (derived from LOCALE_STIMEFORMAT, use that instead)
            LOCALE_ICENTURY = 0x00000024,   // century format specifier (short date, LOCALE_SSHORTDATE is preferred)
            LOCALE_ITLZERO = 0x00000025,   // leading zeros in time field (derived from LOCALE_STIMEFORMAT, use that instead)
            LOCALE_IDAYLZERO = 0x00000026,   // leading zeros in day field (short date, LOCALE_SSHORTDATE is preferred)
            LOCALE_IMONLZERO = 0x00000027,   // leading zeros in month field (short date, LOCALE_SSHORTDATE is preferred)
            LOCALE_S1159 = 0x00000028,   // AM designator
            LOCALE_S2359 = 0x00000029,   // PM designator

            LOCALE_ICALENDARTYPE = 0x00001009,   // type of calendar specifier
            LOCALE_IOPTIONALCALENDAR = 0x0000100B,   // additional calendar types specifier
            LOCALE_IFIRSTDAYOFWEEK = 0x0000100C,   // first day of week specifier
            LOCALE_IFIRSTWEEKOFYEAR = 0x0000100D,   // first week of year specifier

            LOCALE_SDAYNAME1 = 0x0000002A,   // long name for Monday
            LOCALE_SDAYNAME2 = 0x0000002B,   // long name for Tuesday
            LOCALE_SDAYNAME3 = 0x0000002C,   // long name for Wednesday
            LOCALE_SDAYNAME4 = 0x0000002D,   // long name for Thursday
            LOCALE_SDAYNAME5 = 0x0000002E,   // long name for Friday
            LOCALE_SDAYNAME6 = 0x0000002F,   // long name for Saturday
            LOCALE_SDAYNAME7 = 0x00000030,   // long name for Sunday
            LOCALE_SABBREVDAYNAME1 = 0x00000031,   // abbreviated name for Monday
            LOCALE_SABBREVDAYNAME2 = 0x00000032,   // abbreviated name for Tuesday
            LOCALE_SABBREVDAYNAME3 = 0x00000033,   // abbreviated name for Wednesday
            LOCALE_SABBREVDAYNAME4 = 0x00000034,   // abbreviated name for Thursday
            LOCALE_SABBREVDAYNAME5 = 0x00000035,   // abbreviated name for Friday
            LOCALE_SABBREVDAYNAME6 = 0x00000036,   // abbreviated name for Saturday
            LOCALE_SABBREVDAYNAME7 = 0x00000037,   // abbreviated name for Sunday
            LOCALE_SMONTHNAME1 = 0x00000038,   // long name for January
            LOCALE_SMONTHNAME2 = 0x00000039,   // long name for February
            LOCALE_SMONTHNAME3 = 0x0000003A,   // long name for March
            LOCALE_SMONTHNAME4 = 0x0000003B,   // long name for April
            LOCALE_SMONTHNAME5 = 0x0000003C,   // long name for May
            LOCALE_SMONTHNAME6 = 0x0000003D,   // long name for June
            LOCALE_SMONTHNAME7 = 0x0000003E,   // long name for July
            LOCALE_SMONTHNAME8 = 0x0000003F,   // long name for August
            LOCALE_SMONTHNAME9 = 0x00000040,   // long name for September
            LOCALE_SMONTHNAME10 = 0x00000041,   // long name for October
            LOCALE_SMONTHNAME11 = 0x00000042,   // long name for November
            LOCALE_SMONTHNAME12 = 0x00000043,   // long name for December
            LOCALE_SMONTHNAME13 = 0x0000100E,   // long name for 13th month (if exists)
            LOCALE_SABBREVMONTHNAME1 = 0x00000044,   // abbreviated name for January
            LOCALE_SABBREVMONTHNAME2 = 0x00000045,   // abbreviated name for February
            LOCALE_SABBREVMONTHNAME3 = 0x00000046,   // abbreviated name for March
            LOCALE_SABBREVMONTHNAME4 = 0x00000047,   // abbreviated name for April
            LOCALE_SABBREVMONTHNAME5 = 0x00000048,   // abbreviated name for May
            LOCALE_SABBREVMONTHNAME6 = 0x00000049,   // abbreviated name for June
            LOCALE_SABBREVMONTHNAME7 = 0x0000004A,   // abbreviated name for July
            LOCALE_SABBREVMONTHNAME8 = 0x0000004B,   // abbreviated name for August
            LOCALE_SABBREVMONTHNAME9 = 0x0000004C,   // abbreviated name for September
            LOCALE_SABBREVMONTHNAME10 = 0x0000004D,   // abbreviated name for October
            LOCALE_SABBREVMONTHNAME11 = 0x0000004E,   // abbreviated name for November
            LOCALE_SABBREVMONTHNAME12 = 0x0000004F,   // abbreviated name for December
            LOCALE_SABBREVMONTHNAME13 = 0x0000100F,   // abbreviated name for 13th month (if exists)

            LOCALE_SPOSITIVESIGN = 0x00000050,   // positive sign
            LOCALE_SNEGATIVESIGN = 0x00000051,   // negative sign
            LOCALE_IPOSSIGNPOSN = 0x00000052,   // positive sign position (derived from INEGCURR)
            LOCALE_INEGSIGNPOSN = 0x00000053,   // negative sign position (derived from INEGCURR)
            LOCALE_IPOSSYMPRECEDES = 0x00000054,   // mon sym precedes pos amt (derived from ICURRENCY)
            LOCALE_IPOSSEPBYSPACE = 0x00000055,   // mon sym sep by space from pos amt (derived from ICURRENCY)
            LOCALE_INEGSYMPRECEDES = 0x00000056,   // mon sym precedes neg amt (derived from INEGCURR)
            LOCALE_INEGSEPBYSPACE = 0x00000057,   // mon sym sep by space from neg amt (derived from INEGCURR)

            LOCALE_FONTSIGNATURE = 0x00000058,   // font signature
            LOCALE_SISO639LANGNAME = 0x00000059,   // ISO abbreviated language name
            LOCALE_SISO3166CTRYNAME = 0x0000005A,   // ISO abbreviated country name

            LOCALE_IDEFAULTEBCDICCODEPAGE = 0x00001012,   // default ebcdic code page
            LOCALE_IPAPERSIZE = 0x0000100A,   // 1 = letter, 5 = legal, 8 = a3, 9 = a4
            LOCALE_SENGCURRNAME = 0x00001007,   // english name of currency
            LOCALE_SNATIVECURRNAME = 0x00001008,   // native name of currency
            LOCALE_SYEARMONTH = 0x00001006,   // year month format string
            LOCALE_SSORTNAME = 0x00001013,   // sort name
            LOCALE_IDIGITSUBSTITUTION = 0x00001014,   // 0 = context, 1 = none, 2 = national

            LOCALE_SNAME = 0x0000005c,   // locale name (with sort info) (ie: de-DE_phoneb)
            LOCALE_SDURATION = 0x0000005d,   // time duration format
            LOCALE_SKEYBOARDSTOINSTALL = 0x0000005e,   // (windows only) keyboards to install
            LOCALE_SSHORTESTDAYNAME1 = 0x00000060,   // Shortest day name for Monday
            LOCALE_SSHORTESTDAYNAME2 = 0x00000061,   // Shortest day name for Tuesday
            LOCALE_SSHORTESTDAYNAME3 = 0x00000062,   // Shortest day name for Wednesday
            LOCALE_SSHORTESTDAYNAME4 = 0x00000063,   // Shortest day name for Thursday
            LOCALE_SSHORTESTDAYNAME5 = 0x00000064,   // Shortest day name for Friday
            LOCALE_SSHORTESTDAYNAME6 = 0x00000065,   // Shortest day name for Saturday
            LOCALE_SSHORTESTDAYNAME7 = 0x00000066,   // Shortest day name for Sunday
            LOCALE_SISO639LANGNAME2 = 0x00000067,   // 3 character ISO abbreviated language name
            LOCALE_SISO3166CTRYNAME2 = 0x00000068,   // 3 character ISO country name
            LOCALE_SNAN = 0x00000069,   // Not a Number
            LOCALE_SPOSINFINITY = 0x0000006a,   // + Infinity
            LOCALE_SNEGINFINITY = 0x0000006b,   // - Infinity
            LOCALE_SSCRIPTS = 0x0000006c,   // Typical scripts in the locale
            LOCALE_SPARENT = 0x0000006d,   // Fallback name for resources
            LOCALE_SCONSOLEFALLBACKNAME = 0x0000006e,   // Fallback name for within the console
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpLocaleName"></param>
        /// <param name="LCType"></param>
        /// <param name="lpLCData"></param>
        /// <param name="cchData"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetLocaleInfoEx(string lpLocaleName, LCTYPE LCType, StringBuilder lpLCData, int cchData);
    }
}
