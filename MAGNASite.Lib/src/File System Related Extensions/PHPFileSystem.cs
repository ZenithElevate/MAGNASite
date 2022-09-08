/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using MAGNASite.Lib.Enums;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static MAGNASite.Lib.UnsafeNativeMethods;

namespace MAGNASite.Lib
{
    /// <summary>
    /// File system
    /// </summary>
    internal class PHPFileSystem : ErrorAwareBaseClass
    {
        static Dictionary<string, FileInfo> StatCacheFile = new Dictionary<string, FileInfo>();
        static Dictionary<string, DirectoryInfo> StatCacheDir = new Dictionary<string, DirectoryInfo>();
        static Dictionary<string, FileStream> OpenTempFiles = new Dictionary<string, FileStream>();

        public static bool FILE_USE_INCLUDE_PATH { get; set; }

        private static FileInfo GetCachedFileInfo(string filename)
        {
            FileInfo fi;
            if (StatCacheFile.ContainsKey(filename))
            {
                fi = StatCacheFile[filename];
            }
            else
            {
                fi = new FileInfo(filename);
                StatCacheFile.Add(filename, fi);
            }

            return fi;
        }

        private static DirectoryInfo GetCachedDirInfo(string filename)
        {
            DirectoryInfo di;
            if (StatCacheDir.ContainsKey(filename))
            {
                di = StatCacheDir[filename];
            }
            else
            {
                di = new DirectoryInfo(filename);
                StatCacheDir.Add(filename, di);
            }

            return di;
        }

        /// <summary>
        /// Returns trailing name component of path.
        /// </summary>
        /// <remarks>
        /// Note: basename() operates naively on the input string, and is not aware of the actual filesystem, or path components such as "..".<br/>
        /// Caution: basename() is locale aware, so for it to see the correct basename with multibyte character paths, the matching locale must be set using the setlocale() function.If path contains characters which are invalid for the current locale, the behavior of basename() is undefined.
        /// </remarks>
        /// <param name="path"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string basename(string path, string suffix = "")
        {
            return string.IsNullOrEmpty(suffix) ?
                Path.GetFileName(path) :
                path.EndsWith(suffix, StringComparison.CurrentCultureIgnoreCase) ?
                    Path.GetFileName(path).TrimEnd(suffix.ToArray()) :
                    Path.GetFileName(path);
        }

        /// <summary>
        /// Changes file group
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <param name="group">A group name.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool chgrp(string filename, string group)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true; // TODO
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Changes file group
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <param name="group">A group number.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool chgrp(string filename, int group)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true; // TODO
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Changes file mode
        /// </summary>
        /// <remarks>
        /// Note: The current user is the user under which PHP runs.It is probably not the same user you use for normal shell or FTP access.The mode can be changed only by user who owns the file on most systems.<br />
        /// Note: This function will not work on remote files as the file to be examined must be accessible via the server's filesystem.
        /// </remarks>
        /// <param name="filename">Path to the file.</param>
        /// <param name="permissions">
        /// Note that permissions is not automatically assumed to be an octal value, so to ensure the expected operation, you need to prefix permissions with a zero (0). Strings such as "g+w" will not work properly.<br />
        /// The permissions parameter consists of three octal number components specifying access restrictions for the owner, the user group in which the owner is in, and to everybody else in this order.One component can be computed by adding up the needed permissions for that target user base. Number 1 means that you grant execute rights, number 2 means that you make the file writeable, number 4 means that you make the file readable.Add up these numbers to specify needed rights.You can also read more about modes on Unix systems with 'man 1 chmod' and 'man 2 chmod'. 
        /// </param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool chmod(string filename, int permissions)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true; // TODO
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Changes file owner.
        /// </summary>
        /// <remarks>Note: This function will not work on remote files as the file to be examined must be accessible via the server's filesystem.<br />
        /// Note: On Windows, this function fails silently when applied on a regular file.</remarks>
        /// <param name="filename">Path to the file.</param>
        /// <param name="user">A user name or number.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool chown(string filename, string user)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true; // TODO
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Changes file owner
        /// </summary>
        /// <remarks>Note: This function will not work on remote files as the file to be examined must be accessible via the server's filesystem.<br />
        /// Note: On Windows, this function fails silently when applied on a regular file.</remarks>
        /// <param name="filename">Path to the file.</param>
        /// <param name="user">A user name or number.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool chown(string filename, int user)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true; // TODO
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Clears file status cache
        /// </summary>
        /// <remarks> Note: This function caches information about specific filenames, so you only need to call clearstatcache() if you are performing multiple operations on the same filename and require the information about that particular file to not be cached.</remarks>
        /// <param name="clear_realpath_cache">Whether to also clear the realpath cache.</param>
        /// <param name="filename">Clear the realpath cache for a specific filename only; only used if clear_realpath_cache is true.</param>
        public void clearstatcache(bool clear_realpath_cache = false, string filename = "")
        {
            if (string.IsNullOrEmpty(filename))
            {
                StatCacheDir.Clear();
                StatCacheFile.Clear();
            }
            else
            {
                StatCacheDir.Remove(filename);
                StatCacheFile.Remove(filename);
            }
        }

        /// <summary>
        /// Copies file
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool copy(string from, string to, FileStream context = null)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to)) return true;

            try
            {
                File.Copy(from, to);
                return true;
            }
            catch (Exception)
            {
                //AddError(Resources.rsErrorFileCopyFailed); // TODO
                return false;
            }
        }

        /// <summary>
        /// See unlink or unset
        /// </summary>
        [Obsolete("There is no delete keyword or function in the PHP language. If you arrived at this page seeking to delete a file, try unlink(). To delete a variable from the local scope, check out unset().")]
        public static void delete()
        {
            throw new NotImplementedException("There is no delete keyword or function in the PHP language.");
        }

        /// <summary>
        /// Returns a parent directory's path
        /// </summary>
        /// <param name="path">A path. On Windows, both slash (/) and backslash (\) are used as directory separator character. In other environments, it is the forward slash (/).</param>
        /// <param name="levels">The number of parent directories to go up. This must be an integer greater than 0.</param>
        /// <returns> Returns the path of a parent directory. If there are no slashes in path, a dot ('.') is returned, indicating the current directory. Otherwise, the returned string is path with any trailing /component removed.<br />
        /// Caution: Be careful when using this function in a loop that can reach the top-level directory as this can result in an infinite loop.</returns>
        public static string dirname(string path, int levels = 1)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;

            return levels == 1 ? Path.GetDirectoryName(path) :
                dirname(Directory.GetParent(path).Name, levels - 1);
        }

        /// <summary>
        /// Returns available space on filesystem or disk partition
        /// </summary>
        /// <param name="directory">A directory of the filesystem or disk partition.</param>
        /// <returns>Returns the number of available bytes as a float or false on failure.</returns>
        public static float disk_free_space(string directory)
        {
            if (string.IsNullOrEmpty(directory)) return 0L; // TODO

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                UnsafeNativeMethods.GetDiskFreeSpaceEx(directory, out ulong lpFreeBytesAvailable, out _, out _);
                return lpFreeBytesAvailable;
            }
            else return new DriveInfo(directory).AvailableFreeSpace;
        }

        /// <summary>
        /// Returns the total size of a filesystem or disk partition
        /// </summary>
        /// <param name="directory">A directory of the filesystem or disk partition.</param>
        /// <returns>Returns the total number of bytes as a float or false on failure.</returns>
        public static float disk_total_space(string directory)
        {
            if (string.IsNullOrEmpty(directory)) return 0L; // TODO

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                UnsafeNativeMethods.GetDiskFreeSpaceEx(directory, out _, out ulong lpTotalNumberOfBytes, out _);
                return lpTotalNumberOfBytes;
            }
            else return new DriveInfo(directory).TotalSize;
        }

        /// <summary>
        /// Alias of disk_free_space
        /// </summary>
        /// <param name="directory">A directory of the filesystem or disk partition.</param>
        /// <returns>Returns the number of available bytes as a float or false on failure.</returns>
        public static float diskfreespace(string directory) => disk_free_space(directory);

        /// <summary>
        /// Closes an open file pointer
        /// </summary>
        /// <param name="stream">The file pointer must be valid, and must point to a file successfully opened by fopen() or fsockopen().</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool fclose(FileStream stream)
        {
            if (stream == null) return true;

            stream.Close();

            return true;
        }

        /// <summary>
        /// Synchronizes data(but not meta-data) to the file
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static bool fdatasync(FileStream stream)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return fsync(stream);
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Tests for end-of-file on a file pointer
        /// </summary>
        /// <param name="stream">The file pointer must be valid, and must point to a file successfully opened by fopen() or fsockopen() (and not yet closed by fclose()).</param>
        /// <returns>Returns true if the file pointer is at EOF or an error occurs (including socket timeout); otherwise returns false.</returns>
        public static bool feof(FileStream stream)
        {
            return stream.CanSeek ?
                stream.Position == stream.Length :
                false;
        }

        public static bool fflush(FileStream stream)
        {
            try
            {
                var t = stream.FlushAsync();
                _ = Task.WhenAny(t);
                return true;
            }
            catch (Exception)
            {
                //AddError(); // TODO
                return false;
            }
        } // Flushes the output to a file

        /// <summary>
        /// Gets character from file pointer
        /// </summary>
        /// <param name="stream">The file pointer must be valid, and must point to a file successfully opened by fopen() or fsockopen() (and not yet closed by fclose()).</param>
        /// <returns>Returns a string containing a single character read from the file pointed to by stream.Returns false on EOF.<br />
        /// Warning: This function may return Boolean false, but may also return a non-Boolean value which evaluates to false. Please read the section on Booleans for more information. Use the === operator for testing the return value of this function.</returns>
        public string fgetc(FileStream stream)
        {
            try
            {
                return Encoding.Default.GetString(new byte[] { (byte)stream.ReadByte() });
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets line from file pointer and parse for CSV fields
        /// </summary>
        /// <param name="stream">A valid file pointer to a file successfully opened by fopen(), popen(), or fsockopen().</param>
        /// <param name="length">Must be greater than the longest line(in characters) to be found in the CSV file(allowing for trailing line-end characters). Otherwise the line is split in chunks of length characters, unless the split would occur inside an enclosure.<br />
        /// Omitting this parameter (or setting it to 0, or null in PHP 8.0.0 or later) the maximum line length is not limited, which is slightly slower.</param>
        /// <param name="separator">The optional separator parameter sets the field separator (one single-byte character only).</param>
        /// <param name="enclosure">The optional enclosure parameter sets the field enclosure character (one single-byte character only).</param>
        /// <param name="escape">The optional escape parameter sets the escape character(at most one single-byte character). An empty string ("") disables the proprietary escape mechanism.<br />
        /// Note: Usually an enclosure character is escaped inside a field by doubling it; however, the escape character can be used as an alternative.So for the default parameter values "" and \" have the same meaning. Other than allowing to escape the enclosure character the escape character has no special meaning; it isn't even meant to escape itself.</param>
        /// <returns> Returns an indexed array containing the fields read on success, or false on failure.<br />
        /// Note: A blank line in a CSV file will be returned as an array comprising a single null field, and will not be treated as an error.<br />
        /// Note: If PHP is not properly recognizing the line endings when reading files either on or created by a Macintosh computer, enabling the auto_detect_line_endings run-time configuration option may help resolve the problem.</returns>
        public static IEnumerable<string> fgetcsv(FileStream stream, int? length = null, string separator = ",", string enclosure = "\"", string escape = @"\")
        {
            throw new NotImplementedException(""); // TODO
        }

        /// <summary>
        /// Gets line from file pointer
        /// </summary>
        /// <remarks>Note: If PHP is not properly recognizing the line endings when reading files either on or created by a Macintosh computer, enabling the auto_detect_line_endings run-time configuration option may help resolve the problem.<br />
        /// Note: People used to the 'C' semantics of fgets() should note the difference in how EOF is returned.
        /// </remarks>
        /// <param name="stream">The file pointer must be valid, and must point to a file successfully opened by fopen() or fsockopen() (and not yet closed by fclose()).</param>
        /// <param name="length">Reading ends when length, // 1 bytes have been read, or a newline (which is included in the return value), or an EOF (whichever comes first). If no length is specified, it will keep reading from the stream until it reaches the end of the line. </param>
        /// <returns>Returns a string of up to length, // 1 bytes read from the file pointed to by stream. If there is no more data to read in the file pointer, then false is returned.</returns>
        public static string fgets(FileStream stream, int? length = null)
        {
            var remaining = (int)stream.Length; // (int)stream.Position + 1;
            byte[] buffer = new byte[remaining];
            var t = stream.ReadAsync(buffer, 0, Math.Min(length ?? remaining, remaining));
            _ = Task.WhenAny(t);
            return Encoding.Default.GetString(buffer).
                Replace("\0", string.Empty);
        }

        /// <summary>
        /// Gets line from file pointer and strip HTML tags
        /// </summary>
        /// <param name="handle">The file pointer must be valid, and must point to a file successfully opened by fopen() or fsockopen() (and not yet closed by fclose()).</param>
        /// <param name="length">Length of the data to be retrieved.</param>
        /// <param name="allowable_tags">You can use the optional third parameter to specify tags which should not be stripped. See strip_tags() for details regarding allowable_tags.</param>
        /// <returns>Returns a string of up to length, // 1 bytes read from the file pointed to by handle, with all HTML and PHP code stripped. </returns>
        [Obsolete("This function has been DEPRECATED as of PHP 7.3.0, and REMOVED as of PHP 8.0.0. Relying on this function is highly discouraged.")]
        public static string fgetss(FileStream handle, int? length = null, string allowable_tags = null)
        {
            return fgets(handle, length).
                Replace("<?php", string.Empty). // TODO
                Replace("?>", string.Empty); // TODO
        }

        /// <summary>
        /// Checks whether a file or directory exists
        /// </summary>
        /// <remarks>Note: The results of this function are cached. See clearstatcache() for more details.</remarks>
        /// <param name="filename">Path to the file or directory.<br />
        /// On windows, use //computername/share/filename or \\computername\share\filename to check files on network shares.</param>
        /// <returns>Returns true if the file or directory specified by filename exists; false otherwise.<br />
        /// Note: This function will return false for symlinks pointing to non-existing files.</returns>
        public static bool file_exists(string filename)
        {
            if (StatCacheFile.ContainsKey(filename))
            {
                return StatCacheFile[filename].Exists;
            }

            if (StatCacheDir.ContainsKey(filename))
            {
                return StatCacheDir[filename].Exists;
            }

            if (File.Exists(filename))
            {
                FileInfo fi = GetCachedFileInfo(filename);
                return fi.Exists;
            }

            if (Directory.Exists(filename))
            {
                DirectoryInfo di = GetCachedDirInfo(filename);
                return di.Exists;
            }

            return false;
        }

        /// <summary>
        /// Reads entire file into a string
        /// </summary>
        /// <remarks>Note: If you're opening a URI with special characters, such as spaces, you need to encode the URI with urlencode().</remarks>
        /// <param name="filename">Name of the file to read.</param>
        /// <param name="use_include_path">Note: The FILE_USE_INCLUDE_PATH constant can be used to trigger include path search.This is not possible if strict typing is enabled, since FILE_USE_INCLUDE_PATH is an int. Use true instead.</param>
        /// <param name="context">A valid context resource created with stream_context_create(). If you don't need to use a custom context, you can skip this parameter by null.</param>
        /// <param name="offset">The offset where the reading starts on the original stream.Negative offsets count from the end of the stream.<br />
        /// Seeking (offset) is not supported with remote files.Attempting to seek on non-local files may work with small offsets, but this is unpredictable because it works on the buffered stream.</param>
        /// <param name="length">Maximum length of data read. The default is to read until end of file is reached. Note that this parameter is applied to the stream processed by the filters.</param>
        /// <returns> The function returns the read data or false on failure.<br />
        /// Warning: This function may return Boolean false, but may also return a non-Boolean value which evaluates to false. Please read the section on Booleans for more information.Use the === operator for testing the return value of this function.</returns>
        public static string file_get_contents(string filename, bool use_include_path = false, FileStream context = null, int offset = 0, int? length = null)
        {
            var url = new Uri(filename);
            switch (url.Scheme)
            {
                case "file":
                    filename = File.Exists(filename) ?
                        filename :
                        use_include_path ?
                            Path.Combine(PHPIniFile.include_path, filename) :
                            filename;

                    FileInfo fi = GetCachedFileInfo(filename);
                    var l = length == null ? (int)(fi.Length) - offset : length ?? 0;
                    byte[] buffer = new byte[l];
                    using (var fs = context ?? new FileStream(filename, FileMode.Open))
                    {
                        fs.Seek(offset, SeekOrigin.Begin);
                        var cts = new CancellationTokenSource(PHPIniFile.max_execution_time * 1000);
                        var t = fs.ReadAsync(buffer, 0, l, cts.Token);
                        try
                        {
                            _ = Task.WhenAny(t);
                            return Encoding.Default.GetString(buffer);
                        }
                        catch
                        {
                            return string.Empty;
                        }
                    }
                case "http":
                case "https":
                    return string.Empty; // TODO
                case "ftp":
                case "ftps":
                case "sftp":
                    return string.Empty; // TODO
                default:
                    return string.Empty; // TODO
            }
        }

        /// <summary>
        /// Write data to a file
        /// </summary>
        /// <param name="filename">Path to the file where to write the data.</param>
        /// <param name="data">The data to write. Can be either a string, an array or a stream resource.</param>
        /// <param name="flags">The value of flags can be any combination of the following flags, joined with the binary OR (|) operator.</param>
        /// <param name="context">A valid context resource created with stream_context_create().</param>
        /// <returns>This function returns the number of bytes that were written to the file, or false on failure.</returns>
        public static int file_put_contents(string filename, object data, int flags = 0, FileStream context = null)
        {
            var url = new Uri(filename);
            switch (url.Scheme)
            {
                case "file":
                    filename = File.Exists(filename) ?
                        filename :
                        (flags | (int)FileWriteFlags.FILE_USE_INCLUDE_PATH) == flags ?
                            Path.Combine(PHPIniFile.include_path, filename) :
                            filename;

                    using (var fs = context ?? new FileStream(filename, FileMode.OpenOrCreate))
                    {
                        var cts = new CancellationTokenSource(PHPIniFile.max_execution_time * 1000);
                        var buffer = Encoding.Default.GetBytes((string)data);
                        var t = fs.WriteAsync(buffer, cts.Token);
                        try
                        {
                            _ = Task.WhenAny(t.AsTask());
                            return buffer.Length;
                        }
                        catch
                        {
                            return 0;
                        }
                    }
                case "http":
                case "https":
                    return 0; // TODO
                case "ftp":
                case "ftps":
                case "sftp":
                    return 0; // TODO
                default:
                    return 0; // TODO
            }
        }

        /// <summary>
        /// Reads entire file into an array
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <param name="flags">The optional parameter flags can be one, or more, of the following constants:</param>
        /// <param name="context"></param>
        public static string[] file(string filename, int flags = 0, FileStream context = null)
        {
            var url = new Uri(filename);
            switch (url.Scheme)
            {
                case "file":
                    filename = File.Exists(filename) ?
                        filename :
                        (flags | (int)FileReadFlags.FILE_USE_INCLUDE_PATH) == flags ?
                            Path.Combine(PHPIniFile.include_path, filename) :
                            filename;

                    using (var fs = context ?? new FileStream(filename, FileMode.Open))
                    {
                        var cts = new CancellationTokenSource(PHPIniFile.max_execution_time * 1000);
                        var t = File.ReadAllLinesAsync(filename);
                        try
                        {
                            _ = Task.WhenAny(t);
                            return t.Result;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                case "http":
                case "https":
                    return null; // TODO
                case "ftp":
                case "ftps":
                case "sftp":
                    return null; // TODO
                default:
                    return null; // TODO
            }
        }

        /// <summary>
        /// Gets last access time of file
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns the time the file was last accessed, or false on failure. The time is returned as a Unix timestamp.</returns>
        public static long fileatime(string filename)
        {
            if (File.Exists(filename))
            {
                return GetCachedFileInfo(filename).LastAccessTime.Ticks;
            }
            else if (Directory.Exists(filename))
            {
                return GetCachedDirInfo(filename).LastAccessTime.Ticks;
            }

            return 0L; // TODO
        }

        /// <summary>
        /// Gets inode change time of file
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns the time the file was last changed, or false on failure. The time is returned as a Unix timestamp.</returns>
        public static long filectime(string filename)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return fileatime(filename);
            }
            else
            {
                return 0L; // TODO
            }
        }

        /// <summary>
        /// Gets file group
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns the group ID of the file, or false if an error occurs. The group ID is returned in numerical format, use posix_getgrgid() to resolve it to a group name. Upon failure, false is returned.</returns>
        public static int filegroup(string filename)
        {
            if (File.Exists(filename))
            {
                return 0;
            }
            else if (Directory.Exists(filename))
            {
                return 0;
            }

            return 0; // TODO
        }

        public void fileinode() { } // Gets file inode

        /// <summary>
        /// Gets file modification time
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns the time the file was last changed, or false on failure. The time is returned as a Unix timestamp.</returns>
        public static long filemtime(string filename)
        {
            if (File.Exists(filename))
            {
                return GetCachedFileInfo(filename).LastWriteTime.Ticks;
            }
            else if (Directory.Exists(filename))
            {
                return GetCachedDirInfo(filename).LastWriteTime.Ticks;
            }

            return 0L; // TODO
        }

        /// <summary>
        /// Gets file owner
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns the user ID of the owner of the file, or false on failure. The user ID is returned in numerical format, use posix_getpwuid() to resolve it to a username.</returns>
        public static int fileowner(string filename)
        {
            if (File.Exists(filename))
            {
                return 0;
            }
            else if (Directory.Exists(filename))
            {
                return 0;
            }

            return 0; // TODO
        }

        /// <summary>
        /// Gets file permissions
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns> Returns the file's permissions as a numeric mode. Lower bits of this mode are the same as the permissions expected by chmod(), however on most platforms the return value will also include information on the type of file given as filename. The examples below demonstrate how to test the return value for specific permissions and file types on POSIX systems, including Linux and macOS.<br />
        /// For local files, the specific return value is that of the st_mode member of the structure returned by the C library's stat() function. Exactly which bits are set can vary from platform to platform, and looking up your specific platform's documentation is recommended if parsing the non-permission bits of the return value is required.<br />
        /// Returns false on failure.</returns>
        public static int fileperms(string filename)
        {
            return 0; // TODO
        }

        /// <summary>
        /// Gets file size
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns> Returns the size of the file in bytes, or false (and generates an error of level E_WARNING) in case of an error.<br />
        /// Note: Because PHP's integer type is signed and many platforms use 32bit integers, some filesystem functions may return unexpected results for files which are larger than 2GB. </returns>
        public static long filesize(string filename)
        {
            return new FileInfo(filename).Length;
        }

        /// <summary>
        /// Gets file type
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns> Returns the type of the file. Possible values are fifo, char, dir, block, link, file, socket and unknown.<br />
        /// Returns false if an error occurs.filetype() will also produce an E_NOTICE message if the stat call fails or if the file type is unknown.</returns>
        public static string filetype(string filename)
        {
            if (File.Exists(filename))
            {
                if(new FileInfo(filename).ResolveLinkTarget(false) != null)
                {
                    return "link";
                }

                return "file";
            }
            if (Directory.Exists(filename))
            {
                return "dir";
            }

            return "unknown"; // TODO: fifo, char, block
        }

        /// <summary>
        /// Portable advisory file locking
        /// </summary>
        /// <param name="stream">A file system pointer resource that is typically created using fopen().</param>
        /// <param name="operation">operation is one of the following:<br />
        /// LOCK_SH to acquire a shared lock (reader).<br />
        /// LOCK_EX to acquire an exclusive lock (writer).<br />
        /// LOCK_UN to release a lock (shared or exclusive).<br />
        /// It is also possible to add LOCK_NB as a bitmask to one of the above operations, if flock() should not block during the locking attempt.</param>
        /// <param name="would_block">The optional third argument is set to 1 if the lock would block (EWOULDBLOCK errno condition).</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool flock(FileStream stream, int operation, int? would_block = null)
        {
            return true; // TODO
        }

        /// <summary>
        /// Match filename against a pattern
        /// </summary>
        /// <param name="pattern">The shell wildcard pattern.</param>
        /// <param name="filename">The tested string. This function is especially useful for filenames, but may also be used on regular strings.<br />
        /// The average user may be used to shell patterns or at least in their simplest form to '?' and '*' wildcards so using fnmatch() instead of preg_match() for frontend search expression input may be way more convenient for non-programming users.</param>
        /// <param name="flags">The value of flags can be any combination of the following flags, joined with the binary OR (|) operator.</param>
        /// <returns>Returns true if there is a match, false otherwise.</returns>
        public static bool fnmatch(string pattern, string filename, int flags = 0)
        {
            Regex mask = new Regex(
                '^' +
                pattern
                    .Replace(".", "[.]")
                    .Replace("*", ".*")
                    .Replace("?", ".")
                + '$',
                (flags | (int)FileMatchFlags.FNM_CASEFOLD) == flags ? RegexOptions.IgnoreCase : RegexOptions.None);
            return mask.IsMatch(filename);
        }

        /// <summary>
        /// Opens file or URL
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <param name="mode">The mode parameter specifies the type of access you require to the stream. It may be any of the following:</param>
        /// <param name="use_include_path">The optional third use_include_path parameter can be set to '1' or true if you want to search for the file in the include_path, too.</param>
        /// <param name="context">A context stream resource.</param>
        /// <returns>Returns a file pointer resource on success, or false on failure.</returns>
        public static FileStream fopen(string filename, string mode, bool use_include_path = false, FileStream context = null)
        {
            var url = new Uri(filename);
            switch(url.Scheme)
            {
                default:
                    break;
            }

            return null; // TODO
        }
        public void fpassthru() { } // Output all remaining data on a file pointer

        /// <summary>
        /// Format line as CSV and write to file pointer
        /// </summary>
        /// <param name="stream">The file pointer must be valid, and must point to a file successfully opened by fopen() or fsockopen() (and not yet closed by fclose()).</param>
        /// <param name="fields">An array of strings.</param>
        /// <param name="separator">The optional separator parameter sets the field delimiter (one single-byte character only).</param>
        /// <param name="enclosure">The optional enclosure parameter sets the field enclosure (one single-byte character only).</param>
        /// <param name="escape">The optional escape parameter sets the escape character (at most one single-byte character). An empty string ("") disables the proprietary escape mechanism.</param>
        /// <param name="eol">The optional eol parameter sets a custom End of Line sequence.</param>
        /// <returns>Returns the length of the written string or false on failure.</returns>
        public static int fputcsv(FileStream stream, string[] fields, string separator = ",", string enclosure = "\"", string escape = "\\", string eol = "\n")
        {
            var line = string.Join(separator, fields.Select(f => $"{enclosure}{f}{enclosure}"));
            var buffer = Encoding.Default.GetBytes(line);
            var result = buffer.Length;
            var t = stream.WriteAsync(buffer);
            _ = Task.WhenAny(t.AsTask());
            buffer = Encoding.Default.GetBytes(eol);
            result += buffer.Length;
            t = stream.WriteAsync(buffer);
            _ = Task.WhenAny(t.AsTask());
            return result;
        }

        /// <summary>
        /// Alias of fwrite
        /// </summary>
        /// <param name="stream">A file system pointer resource that is typically created using fopen().</param>
        /// <param name="data">The string that is to be written.</param>
        /// <param name="length">If length is an int, writing will stop after length bytes have been written or the end of data is reached, whichever comes first.</param>
        /// <returns>Returns the number of bytes written, or false on failure.</returns>
        public static long fputs(FileStream stream, string data, int? length = null) => fwrite(stream, data, length);

        /// <summary>
        /// Binary-safe file read
        /// </summary>
        /// <param name="stream">A file system pointer resource that is typically created using fopen().</param>
        /// <param name="length">Up to length number of bytes read.</param>
        /// <returns>Returns the read string or false on failure.</returns>
        public static string fread(FileStream stream, int length)
        {
            var buffer = new byte[length];
            var cts = new CancellationTokenSource(PHPIniFile.max_execution_time * 1000);
            var t = stream.ReadAsync(buffer, 0, length, cts.Token);
            try
            {
                _ = Task.WhenAny(t);
                return Encoding.Default.GetString(buffer);
            }
            catch
            {
                return string.Empty;
            }
        }

        public void fscanf() { } // Parses input from a file according to a format

        /// <summary>
        /// Seeks on a file pointer
        /// </summary>
        /// <param name="stream">A file system pointer resource that is typically created using fopen().</param>
        /// <param name="offset">The offset.<br />
        /// To move to a position before the end-of-file, you need to pass a negative value in offset and set whence to SEEK_END.</param>
        /// <param name="whence">Whence.</param>
        /// <returns>Upon success, returns 0; otherwise, returns -1.</returns>
        public static int fseek(FileStream stream, int offset, int whence = (int)FileSeekWhence.SEEK_SET)
        {
            if (stream.CanSeek)
            {
                stream.Seek(offset,
                    whence == (int)FileSeekWhence.SEEK_SET ?
                        SeekOrigin.Begin : 
                        whence == (int)FileSeekWhence.SEEK_CUR ? SeekOrigin.Current :
                            SeekOrigin.End
                );

                return 0;
            }

            return -1;
        }

        /// <summary>
        /// Gets information about a file using an open file pointer
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Dictionary<string, string> fstat(FileStream stream)
        {
            var filename = stream.Name;
            return stat(filename); // TODO
        }

        /// <summary>
        /// Synchronizes changes to the file(including meta-data)
        /// </summary>
        /// <param name="stream">The file pointer must be valid, and must point to a file successfully opened by fopen() or fsockopen() (and not yet closed by fclose()).</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool fsync(FileStream stream)
        {
            try
            {
                var t = stream.FlushAsync();
                _ = Task.WhenAny(t);
                return true;
            }
            catch (Exception)
            {
                //AddError(); // TODO
                return false;
            }
        }

        /// <summary>
        /// Returns the current position of the file read/write pointer
        /// </summary>
        /// <param name="stream">The file pointer must be valid, and must point to a file successfully opened by fopen() or popen(). ftell() gives undefined results for append-only streams (opened with "a" flag).</param>
        /// <returns> Returns the position of the file pointer referenced by stream as an integer; i.e., its offset into the file stream.<br />
        /// If an error occurs, returns false. </returns>
        public static long ftell(FileStream stream)
        {
            return stream.Position;
        }

        /// <summary>
        /// Truncates a file to a given length
        /// </summary>
        /// <param name="stream">The file pointer.</param>
        /// <param name="size">The size to truncate to.<br />    
        /// Note: If size is larger than the file then the file is extended with null bytes.<br />
        /// If size is smaller than the file then the file is truncated to that size.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool ftruncate(FileStream stream, long size)
        {
            if (stream.CanSeek && stream.CanWrite)
            {
                try
                {
                    stream.SetLength(size);
                    return true;
                }
                catch (Exception)
                {
                    //AddError(); // TODO
                }
            }
            return false;
        }

        /// <summary>
        /// Binary-safe file write
        /// </summary>
        /// <param name="stream">A file system pointer resource that is typically created using fopen().</param>
        /// <param name="data">The string that is to be written.</param>
        /// <param name="length">If length is an int, writing will stop after length bytes have been written or the end of data is reached, whichever comes first.</param>
        /// <returns></returns>
        public static long fwrite(FileStream stream, string data, int? length = null)
        {
            try
            {
                var cts = new CancellationTokenSource(PHPIniFile.max_execution_time * 1000);
                var buffer = length == null?
                    Encoding.Default.GetBytes(data) :
                    Encoding.Default.GetBytes(data.ToCharArray(), 0, length ?? 0);
                var t = stream.WriteAsync(buffer, cts.Token);
                _ = Task.WhenAny(t.AsTask());
                return buffer.Length;
            }
            catch (Exception)
            {
                //AddError(); // TODO
                return 0;
            }
        }

        /// <summary>
        /// Find pathnames matching a pattern
        /// </summary>
        /// <param name="pattern">The pattern. No tilde expansion or parameter substitution is done.</param>
        /// <param name="flags">Valid flags:</param>
        /// <returns>Returns an array containing the matched files/directories, an empty array if no file matched or false on error.</returns>
        public static string[] glob(string pattern, int flags = 0)
        {
            return Directory.GetDirectories(pattern).ToList().
                Concat(Directory.GetFiles(pattern)).
                OrderBy(s => s).
                ToArray();
        }

        /// <summary>
        /// Tells whether the filename is a directory
        /// </summary>
        /// <param name="filename">Path to the file. If filename is a relative filename, it will be checked relative to the current working directory. If filename is a symbolic or hard link then the link will be resolved and checked. If you have enabled open_basedir further restrictions may apply.</param>
        /// <returns>Returns true if the filename exists and is a directory, false otherwise.</returns>
        public static bool is_dir(string filename)
        {
            return Directory.Exists(filename); // TODO: cache
        }

        /// <summary>
        /// Tells whether the filename is executable
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns></returns>
        public static bool is_executable(string filename)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return File.Exists(filename) && Path.GetExtension(filename).EndsWith(".exe", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Tells whether the filename is a regular file
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns true if the filename exists and is a regular file, false otherwise.</returns>
        public static bool is_file(string filename)
        {
            return File.Exists(filename) && new FileInfo(filename).ResolveLinkTarget(false) == null;
        }

        /// <summary>
        /// Tells whether the filename is a symbolic link
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns true if the filename exists and is a symbolic link, false otherwise.</returns>
        public static bool is_link(string filename)
        {
            return File.Exists(filename) && new FileInfo(filename).ResolveLinkTarget(false) != null;
        }

        /// <summary>
        /// Tells whether a file exists and is readable
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns true if the file or directory specified by filename exists and is readable, false otherwise.</returns>
        public static bool is_readable(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    using (var fs = File.OpenRead(filename))
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public static bool is_uploaded_file(string filename)
        {
            return false;
        }

        /// <summary>
        /// Tells whether the file was uploaded via HTTP POST
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns true if the filename exists and is writable.</returns>
        public static bool is_writable(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    using (var fs = File.OpenWrite(filename))
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Alias of is_writable
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns>Returns true if the filename exists and is writable.</returns>
        public static bool is_writeable(string filename) => is_writable(filename);

        /// <summary>
        /// Changes group ownership of symlink
        /// </summary>
        /// <param name="filename">Path to the symlink.</param>
        /// <param name="group">The group specified by name.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool lchgrp(string filename, string group) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true;
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Changes group ownership of symlink
        /// </summary>
        /// <param name="filename">Path to the symlink.</param>
        /// <param name="group">The group specified by number.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool lchgrp(string filename, int group) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true;
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Changes user ownership of symlink
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <param name="user">User name.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool lchown(string filename, string user)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true;
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Changes user ownership of symlink
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <param name="user">User number.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool lchown(string filename, int user)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return true;
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Create a hard link
        /// </summary>
        /// <param name="target">Target of the link.</param>
        /// <param name="link">The link name.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool link(string target, string link)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    CreateHardLink(target, link, IntPtr.Zero);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return true; // TODO
            }
        }

        /// <summary>
        /// Gets information about a link
        /// </summary>
        /// <param name="path">Path to the link.</param>
        /// <returns>Returns the st_dev field of the Unix C stat structure returned by the lstat system call. Returns a non-negative integer on success, -1 in case the link was not found, or false if an open.base_dir violation occurs.</returns>
        public static long linkinfo(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return (File.Exists(path) || Directory.Exists(path)) ? 1 : 0;
            }
            else
            {
                return 0; // TODO
            }
        }

        /// <summary>
        /// Gives information about a file or symbolic link
        /// </summary>
        /// <param name="filename">Path to a file or a symbolic link.</param>
        /// <returns> See the manual page for stat() for information on the structure of the array that lstat() returns. This function is identical to the stat() function except that if the filename parameter is a symbolic link, the status of the symbolic link is returned, not the status of the file pointed to by the symbolic link.<br />
        /// On failure, false is returned.</returns>
        public static Dictionary<string, string> lstat(string filename)
        {
            return stat(filename); // TODO
        } 

        static Dictionary<string, string> GetFileInformation(SafeFileHandle hFile)
        {
            var lpFileInformation = new _BY_HANDLE_FILE_INFORMATION();
            var b = GetFileInformationByHandle(hFile, out lpFileInformation);
            long ino = (lpFileInformation.nFileIndexHigh.Value << 8) + lpFileInformation.nFileIndexLow.Value;
            long lat = (lpFileInformation.ftLastAccessTime.dwHighDateTime.Value << 8) + lpFileInformation.ftLastAccessTime.dwLowDateTime.Value;
            long lwt = (lpFileInformation.ftLastWriteTime.dwHighDateTime.Value << 8) + lpFileInformation.ftLastWriteTime.dwLowDateTime.Value;
            long sz = (lpFileInformation.nFileSizeHigh.Value << 8) + lpFileInformation.nFileSizeLow.Value;

            return new Dictionary<string, string>(26)
            {
                {"0", $"{lpFileInformation.dwVolumeSerialNumber.Value}" },
                {"1", $"{ino}" },
                {"2", $"{33206}" }, // TODO
                {"3", $"{lpFileInformation.nNumberOfLinks.Value}" },
                {"4", $"{0}" },
                {"5", $"{0}" },
                {"6", $"{0}" },
                {"7", $"{sz}" },
                {"8", $"{lat}" },
                {"9", $"{lwt}" },
                {"10", $"{lat}" },
                {"11", $"{-1}" },
                {"12", $"{-1}" },
                {"dev", $"{lpFileInformation.dwVolumeSerialNumber.Value}" },
                {"ino", $"{ino}" },
                {"mode", $"{33206}" }, // TODO
                {"nlink", $"{lpFileInformation.nNumberOfLinks.Value}" },
                {"uid", $"{0}" },
                {"gid", $"{0}" },
                {"rdev", $"{0}" },
                {"size", $"{sz}" },
                {"atime", $"{lat}" },
                {"mtime", $"{lwt}" },
                {"ctime", $"{lat}" },
                {"blksize", $"{-1}" },
                {"blocks", $"{-1}" },
            };
        }

        /// <summary>
        /// Makes directory
        /// </summary>
        /// <param name="directory">The directory path.</param>
        /// <param name="permissions">The permissions are 0777 by default, which means the widest possible access. For more information on permissions, read the details on the chmod() page.</param>
        /// <param name="recursive">If true, then any parent directories to the directory specified will also be created, with the same permissions.</param>
        /// <param name="context">A context stream resource.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool mkdir(string directory, int permissions = 0777, bool recursive = false, FileStream context = null)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Directory.Exists(directory) ? true : Directory.CreateDirectory(directory).Exists;
            }
            else
            {
                return false; // TODO
            }
        }

        public void move_uploaded_file() { } // Moves an uploaded file to a new location

        /// <summary>
        /// Parse a configuration file
        /// </summary>
        /// <param name="filename">The filename of the ini file being parsed. If a relative path is used, it is evaluated relative to the current working directory, then the include_path.</param>
        /// <param name="process_sections">By setting the process_sections parameter to true, you get a multidimensional array, with the section names and settings included. The default for process_sections is false.</param>
        /// <param name="scanner_mode">Can either be INI_SCANNER_NORMAL (default) or INI_SCANNER_RAW. If INI_SCANNER_RAW is supplied, then option values will not be parsed.<br />
        /// As of PHP 5.6.1 can also be specified as INI_SCANNER_TYPED.</param>
        /// <returns>The settings are returned as an associative array on success, and false on failure.</returns>
        public static dynamic parse_ini_file(string filename, bool process_sections = false, int scanner_mode = (int)IniScannerModes.INI_SCANNER_NORMAL)
        {
            return 0; // TODO
        }

        /// <summary>
        /// Parse a configuration string
        /// </summary>
        /// <param name="ini_string">The contents of the ini file being parsed.</param>
        /// <param name="process_sections">By setting the process_sections parameter to true, you get a multidimensional array, with the section names and settings included. The default for process_sections is false.</param>
        /// <param name="scanner_mode">Can either be INI_SCANNER_NORMAL (default) or INI_SCANNER_RAW. If INI_SCANNER_RAW is supplied, then option values will not be parsed.<br />
        /// As of PHP 5.6.1 can also be specified as INI_SCANNER_TYPED.</param>
        /// <returns>The settings are returned as an associative array on success, and false on failure.</returns>
        public static dynamic parse_ini_string(string ini_string, bool process_sections = false, int scanner_mode = (int)IniScannerModes.INI_SCANNER_NORMAL)
        {
            return 0; // TODO
        }

        /// <summary>
        /// Returns information about a file path
        /// </summary>
        /// <param name="path">The path to be parsed.</param>
        /// <param name="flags">If present, specifies a specific element to be returned; one of PATHINFO_DIRNAME, PATHINFO_BASENAME, PATHINFO_EXTENSION or PATHINFO_FILENAME.</param>
        /// <returns>If the flags parameter is not passed, an associative array containing the following elements is returned: dirname, basename, extension (if any), and filename.</returns>
        public Dictionary<string, string> pathinfo(string path, int flags = (int)PathInfoElements.PATHINFO_ALL)
        {
            if (flags == (int)PathInfoElements.PATHINFO_ALL)
            {
                return new Dictionary<string, string>(4)
                {
                    {"dirname", Path.GetDirectoryName(path) },
                    {"basename", Path.GetFileName(path) },
                    {"extension", Path.GetExtension(path) },
                    {"filename", Path.GetFileNameWithoutExtension(path) },
                };
            }
            else
            {
                var result = new Dictionary<string, string>(3);
                if ((flags | (int)PathInfoElements.PATHINFO_DIRNAME) == flags)
                {
                    result.Add("dirname", Path.GetDirectoryName(path));
                }
                if ((flags | (int)PathInfoElements.PATHINFO_BASENAME) == flags)
                {
                    result.Add("dirname", Path.GetFileName(path));
                }
                if ((flags | (int)PathInfoElements.PATHINFO_EXTENSION) == flags)
                {
                    result.Add("dirname", Path.GetExtension(path));
                }
                if ((flags | (int)PathInfoElements.PATHINFO_FILENAME) == flags)
                {
                    result.Add("dirname", Path.GetFileNameWithoutExtension(path));
                }

                return result;
            }
        }

        /// <summary>
        /// Closes process file pointer
        /// </summary>
        /// <param name="handle">The file pointer must be valid, and must have been returned by a successful call to popen().</param>
        /// <returns>Returns the termination status of the process that was run. In case of an error then -1 is returned.</returns>
        public static int pclose(FileStream handle)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                handle.Close();
                return 0;
            }
            else
            {
                return -1; // TODO
            }
        }

        public void popen() { } // Opens process file pointer

        /// <summary>
        /// Outputs a file
        /// </summary>
        /// <param name="filename">The filename being read.</param>
        /// <param name="use_include_path">You can use the optional second parameter and set it to true, if you want to search for the file in the include_path, too.</param>
        /// <param name="context">A context stream resource.</param>
        /// <returns>Returns the number of bytes read from the file on success, or false on failure.</returns>
        public static string readfile(string filename, bool use_include_path = false, FileStream context = null) 
        {
            if (context == null)
            {
                if (File.Exists(filename))
                {
                    var t = File.ReadAllTextAsync(filename);
                    _ = Task.WhenAny(t);
                    return t.Result;
                }
                else
                {
                    if (use_include_path)
                    {
                        var n = Path.Combine(PHPIniFile.include_path, filename);
                        if (File.Exists(n))
                        {
                            var t = File.ReadAllTextAsync(n);
                            _ = Task.WhenAny(t);
                            return t.Result;
                        }
                    }
                }

                return null;
            }
            else
            {
                using (var r = new StreamReader(context))
                {
                    var t = r.ReadToEndAsync();
                    _ = Task.WhenAny(t);
                    return t.Result;
                }
            }
        }

        /// <summary>
        /// Returns the target of a symbolic link
        /// </summary>
        /// <param name="path">The symbolic link path.</param>
        /// <returns>Returns the contents of the symbolic link path or false on error.</returns>
        public static string readlink(string path)
        {
            return new FileInfo(path).ResolveLinkTarget(false)?.Name;
        }

        public void realpath_cache_get() { } // Get realpath cache entries

        public void realpath_cache_size() { } // Get realpath cache size

        /// <summary>
        /// Returns canonicalized absolute pathname
        /// </summary>
        /// <param name="path">The path being checked.</param>
        /// <returns>Returns the canonicalized absolute pathname on success. The resulting path will have no symbolic link, /./ or /../ components. Trailing delimiters, such as \ and /, are also removed.</returns>
        public static string realpath(string path) 
        {
            return new FileInfo(path)?.FullName;
        }

        /// <summary>
        /// Renames a file or directory
        /// </summary>
        /// <param name="from">The old name.</param>
        /// <param name="to">The new name.</param>
        /// <param name="context">A context stream resource.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool rename(string from, string to, FileInfo context = null)
        {
            if (File.Exists(from))
            {
                File.Move(from, to, true);
                return true;
            }
            else if (Directory.Exists(from))
            {
                if (Directory.Exists(to))
                {
                    //AddWarning(Resources.rsWarningTargetDirExists);
                    return false;
                }
                Directory.Move(from, to);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Rewind the position of a file pointer
        /// </summary>
        /// <param name="stream">The file pointer must be valid, and must point to a file successfully opened by fopen().</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool rewind(FileStream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes directory
        /// </summary>
        /// <param name="directory">Path to the directory.</param>
        /// <param name="context">A context stream resource.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool rmdir(string directory, FileStream context = null) 
        {
            if (context == null)
            {
                try
                {
                    Directory.Delete(directory);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false; // TODO
            }
        }

        public void set_file_buffer() { } // Alias of stream_set_write_buffer

        /// <summary>
        /// Gives information about a file
        /// </summary>
        /// <param name="filename">Path to the file.</param>
        /// <returns></returns>
        public static Dictionary<string, string> stat(string filename)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (File.Exists(filename))
                {
                    using (var hFile = File.OpenHandle(filename, FileMode.Open))
                    {
                        return GetFileInformation(hFile);
                    }
                }
                else if (Directory.Exists(filename))
                {
                    var hFile = CreateFileA(".", GENERIC_READ, FILE_SHARE_READ,
                        IntPtr.Zero,
                        OPEN_EXISTING,
                        FILE_ATTRIBUTE_NORMAL | FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero);
                    try
                    {
                        return GetFileInformation(hFile);
                    }
                    finally
                    {
                        CloseHandle(hFile);
                    }
                }
                else
                {
                    return null;
                }
            }
            else // TODO
            {
                return new Dictionary<string, string>(26)
                {
                    {"0", $"{123465}" },
                    {"1", $"{123456798}" },
                    {"2", $"{33206}" },
                    {"3", $"{1}" },
                    {"4", $"{0}" },
                    {"5", $"{0}" },
                    {"6", $"{0}" },
                    {"7", $"{1092}" },
                    {"8", $"{123456}" },
                    {"9", $"{123457}" },
                    {"10", $"{123456}" },
                    {"11", $"{-1}" },
                    {"12", $"{-1}" },
                    {"dev", $"{123456}" },
                    {"ino", $"{123456798}" },
                    {"mode", $"{33206}" },
                    {"nlink", $"{1}" },
                    {"uid", $"{0}" },
                    {"gid", $"{0}" },
                    {"rdev", $"{0}" },
                    {"size", $"{1092}" },
                    {"atime", $"{123456}" },
                    {"mtime", $"{123457}" },
                    {"ctime", $"{123456}" },
                    {"blksize", $"{-1}" },
                    {"blocks", $"{-1}" },
                };
            }
        }

        /// <summary>
        /// Creates a symbolic link
        /// </summary>
        /// <param name="target">Target of the link.</param>
        /// <param name="link">The link name.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool symlink(string target, string link) 
        {
            try
            {
                if (File.Exists(target))
                {
                    File.CreateSymbolicLink(link, target);
                }
                else if (Directory.Exists(target))
                {
                    Directory.CreateSymbolicLink(link, target);
                }
                return true;
            }
            catch (Exception)
            {
                //AddWarning(Resources.rsWarningCreateSymLink); // TODO
                return false;
            }
        }

        /// <summary>
        /// Create file with unique file name
        /// </summary>
        /// <param name="directory">The directory where the temporary filename will be created.</param>
        /// <param name="prefix">The prefix of the generated temporary filename.</param>
        /// <returns>Returns the new temporary filename (with path), or false on failure.</returns>
        public static string tempnam(string directory, string prefix) 
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return Path.GetTempFileName();
                }
                else
                {
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var name = Path.Combine(directory, $"{prefix.Take(63)}{new Guid()}");
                    using (var s = File.Create(name))
                    {
                        return s.Name;
                    }
                }
            }
            catch (Exception)
            {
                //AddWarning(Resources.rsWarningInvalidTempFile); // TODO
                return null; 
            }
        }

        /// <summary>
        /// Creates a temporary file
        /// </summary>
        /// <returns>Returns a file handle, similar to the one returned by fopen(), for the new file or false on failure.</returns>
        public static FileStream tmpfile()
        {
            try
            {
                var n = Path.GetTempFileName();
                var result = File.Open(n, FileMode.OpenOrCreate);
                OpenTempFiles.Add(n, result);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Sets access and modification time of file
        /// </summary>
        /// <param name="filename">The name of the file being touched.</param>
        /// <param name="mtime">The touch time. If mtime is null, the current system time() is used.</param>
        /// <param name="atime">If not null, the access time of the given filename is set to the value of atime. Otherwise, it is set to the value passed to the mtime parameter. If both are null, the current system time is used.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool touch(string filename, int? mtime = null, int? atime = null) 
        {
            try
            {
                if (!File.Exists(filename))
                {
                    File.Create(filename).Dispose();
                }

                var fi = new FileInfo(filename);
                fi.LastWriteTime = mtime == null ? DateTime.Now : new DateTime((long)mtime);

                if (atime != null)
                {
                    fi.LastAccessTime = new DateTime((long)atime);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the current umask
        /// </summary>
        /// <param name="mask">The new umask.</param>
        /// <returns>If mask is null, umask() simply returns the current umask otherwise the old umask is returned.</returns>
        public static int umask(int? mask = null)
        {
            return 0777; // TODO
        }

        /// <summary>
        /// Deletes a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool unlink(string filename, FileStream context = null)
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                else if (Directory.Exists(filename))
                {
                    Directory.Delete(filename);
                }

                clearstatcache(true, filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
