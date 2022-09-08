/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Text;
using MAGNASite.Lib.Enums;

namespace MAGNASite.Lib
{
    /// <summary>
    /// XDiff
    /// </summary>
    internal class PHPXdiff : ErrorAwareBaseClass
    {
        public static bool FILE_USE_INCLUDE_PATH { get; set; }

        public static void xdiff_file_bdiff_size() {} // Read a size of file created by applying a binary diff

        /// <summary>
        /// Make binary diff of two files.
        /// </summary>
        /// <remarks>Unlike PHP's version of this function, it does not read the entire file contents into RAM but only reads 1 byte at a time.</remarks>
        /// <param name="old_file">Path to the first file. This file acts as "old" file.</param>
        /// <param name="new_file">Path to the second file. This file acts as "new" file.</param>
        /// <param name="dest">Path of the resulting patch file. Resulting file contains differences between "old" and "new" files. It is in binary format and is human-unreadable.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool xdiff_file_bdiff(string old_file, string new_file, string dest)
        {
            if (File.Exists(old_file) && File.Exists(new_file))
            {
                var t = Task.Run(() =>
                {
                    try
                    {
                        using (var ofs = File.Open(old_file, FileMode.Open))
                        using (var nfs = File.Open(new_file, FileMode.Open))
                        using (var dfs = File.Open(dest, FileMode.Truncate))
                        using (var or = new BinaryReader(ofs))
                        using (var nr = new BinaryReader(nfs))
                        using (var dw = new BinaryWriter(dfs))
                        {
                            long pos = 0L;
                            while (true)
                            {
                                byte? ob, nb;
                                ob = or.PeekChar() > -1 ? or.ReadByte() : null;
                                nb = nr.PeekChar() > -1 ? nr.ReadByte() : null;
                                pos++;
                                /* For lack of PECL extension the format is tentatively <pos>:old|new */
                                if (nb != null && ob != null && nb != ob)
                                {
                                    dw.Write($"{pos}:{ob}>{nb}{Environment.NewLine}"); // TODO: install PECL extension and check format
                                }
                                else if (nb == null && ob != null)
                                {
                                    dw.Write($"{pos}:{ob}|{Environment.NewLine}"); // TODO: install PECL extension and check format
                                }
                                else if (nb != null && ob == null)
                                {
                                    dw.Write($"{pos}:|{nb}{Environment.NewLine}"); // TODO: install PECL extension and check format
                                }
                                else if (nb == null && ob == null)
                                {
                                    break;
                                }
                            }
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
                _ = Task.WhenAny(t);
                return t.Result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Patch a file with a binary diff
        /// </summary>
        /// <remarks>Unlike PHP's version of this function, it does not read the entire file contents into RAM but only reads 1 byte at a time.</remarks>
        /// <param name="file">The original file.</param>
        /// <param name="patch">The binary patch file.</param>
        /// <param name="dest">Path of the resulting file.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool xdiff_file_bpatch(string file, string patch, string dest)
        {
            if (File.Exists(file) && File.Exists(patch))
            {
                var t = Task.Run(() =>
                {
                    try
                    {
                        using (var ofs = File.Open(file, FileMode.Open))
                        using (var dfs = File.Open(dest, FileMode.Truncate))
                        using (var or = new BinaryReader(ofs))
                        using (var dw = new BinaryWriter(dfs))
                        {
                            long prevPos = 0L;
                            foreach (var l in File.ReadLines(patch))
                            {
                                var ls = l.Split(':');
                                long pos = Convert.ToInt64(ls[0]);
                                ofs.CopyTo(dfs, (int)(pos - prevPos));
                                ofs.Seek(pos - prevPos, SeekOrigin.Current);
                                prevPos = pos;
                                var p = ls[1].Split('|');

                                /* For lack of PECL extension the format is tentatively <pos>:old|new */
                                if (p[0] != null && p[1] != null && p[0] != p[1])
                                {
                                    dw.Write(p[0]); // TODO: install PECL extension and check format
                                }
                                else if (p[0] == null && p[1] != null)
                                {
                                    dw.Write(p[1]); // TODO: install PECL extension and check format
                                }
                                else if (p[0] != null && p[1] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
                _ = Task.WhenAny(t);
                return t.Result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Alias of xdiff_file_bdiff
        /// </summary>
        /// <remarks>Unlike PHP's version of this function, it does not read the entire file contents into RAM but only reads 1 byte at a time.</remarks>
        /// <param name="file">The original file.</param>
        /// <param name="patch">The binary patch file.</param>
        /// <param name="dest">Path of the resulting file.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool xdiff_file_diff_binary(string file, string patch, string dest) => xdiff_file_bdiff(file, patch, dest);

        /// <summary>
        /// Make unified diff of two files
        /// </summary>
        /// <remarks>Unlike PHP's version of this function, it does not read the entire file contents into RAM but only reads 1 byte at a time.</remarks>
        /// <param name="old_file">Path to the first file. This file acts as "old" file.</param>
        /// <param name="new_file">Path to the second file. This file acts as "new" file.</param>
        /// <param name="dest">Path of the resulting patch file.</param>
        /// <param name="context">Indicates how many lines of context you want to include in diff result.</param>
        /// <param name="minimal">Set this parameter to true if you want to minimalize size of the result (can take a long time).</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool xdiff_file_diff(string old_file,string new_file,string dest,int context = 3,bool minimal = false)
        {
            if (File.Exists(old_file) && File.Exists(new_file))
            {
                var t = Task.Run(() =>
                {
                    try
                    {
                        using (var ofs = File.Open(old_file, FileMode.Open))
                        using (var nfs = File.Open(new_file, FileMode.Open))
                        using (var dfs = File.Open(dest, FileMode.Truncate))
                        using (var or = new StreamReader(ofs))
                        using (var nr = new StreamReader(nfs))
                        using (var dw = new StreamWriter(dfs))
                        {
                            long pos = 0L;
                            string ns = null;
                            while (true)
                            {
                                var ob = or.Peek() > -1 ? or.ReadLineAsync() : Task.FromResult(ns);
                                var nb = nr.Peek() > -1 ? nr.ReadLineAsync() : Task.FromResult(ns);
                                _ = Task.WhenAll(ob, nb);

                                pos++;
                                /* For lack of PECL extension the format is tentatively <pos>:old|new */
                                if (nb.Result != null && ob.Result != null && !nb.Result.Equals(ob.Result, StringComparison.CurrentCulture))
                                {
                                    dw.WriteLine($"---{ob.Result}"); // TODO: install PECL extension and check format
                                    dw.WriteLine($"+++{nb.Result}"); // TODO: install PECL extension and check format
                                }
                                else if (nb.Result == null && ob.Result != null)
                                {
                                    dw.Write($"---{ob.Result}"); // TODO: install PECL extension and check format
                                }
                                else if (nb.Result != null && ob.Result == null)
                                {
                                    dw.Write($"+++{nb.Result}"); // TODO: install PECL extension and check format
                                }
                                else if (nb.Result == null && ob.Result == null)
                                {
                                    break;
                                }
                            }
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
                _ = Task.WhenAny(t);
                return t.Result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Merge 3 files into one
        /// </summary>
        /// <remarks>Unlike PHP's version of this function, it does not read the entire file contents into RAM but only reads 1 byte at a time.</remarks>
        /// <param name="old_file">Path to the first file. It acts as "old" file.</param>
        /// <param name="new_file1">Path to the second file. It acts as modified version of old_file.</param>
        /// <param name="new_file2">Path to the third file. It acts as modified version of old_file.</param>
        /// <param name="dest">Path of the resulting file, containing merged changed from both new_file1 and new_file2.</param>
        /// <returns>Returns true if merge was successful, string with rejected chunks if it was not or false if an internal error happened.</returns>
        public static (bool, string) xdiff_file_merge3(string old_file, string new_file1, string new_file2, string dest)
        {
            if (File.Exists(old_file) && File.Exists(new_file1))
            {
                var t = Task.Run(() =>
                {
                    try
                    {
                        using (var ofs = File.Open(old_file, FileMode.Open))
                        using (var nfs = File.Open(new_file1, FileMode.Open))
                        using (var dfs = File.Open(dest, FileMode.Truncate))
                        using (var or = new StreamReader(ofs))
                        using (var nr = new StreamReader(nfs))
                        using (var dw = new StreamWriter(dfs))
                        {
                            long pos = 0L;
                            string ns = null;
                            while (true)
                            {
                                var ob = or.Peek() > -1 ? or.ReadLineAsync() : Task.FromResult(ns);
                                var nb = nr.Peek() > -1 ? nr.ReadLineAsync() : Task.FromResult(ns);
                                _ = Task.WhenAll(ob, nb);

                                pos++;
                                /* For lack of PECL extension the format is tentatively <pos>:old|new */
                                if (nb.Result != null && ob.Result != null && !nb.Result.Equals(ob.Result, StringComparison.CurrentCulture))
                                {
                                    dw.WriteLine($"---{ob.Result}"); // TODO: install PECL extension and check format
                                    dw.WriteLine($"+++{nb.Result}"); // TODO: install PECL extension and check format
                                }
                                else if (nb.Result == null && ob.Result != null)
                                {
                                    dw.Write($"---{ob.Result}"); // TODO: install PECL extension and check format
                                }
                                else if (nb.Result != null && ob.Result == null)
                                {
                                    dw.Write($"+++{nb.Result}"); // TODO: install PECL extension and check format
                                }
                                else if (nb.Result == null && ob.Result == null)
                                {
                                    break;
                                }
                            }
                        }
                        return (true, string.Empty);
                    }
                    catch (Exception)
                    {
                        return (false, string.Empty);
                    }
                });
                _ = Task.WhenAny(t);
                return t.Result;
            }
            else
            {
                return (false, string.Empty);
            }
        }

        /// <summary>
        /// Alias of xdiff_file_bpatch
        /// </summary>
        /// <remarks>Unlike PHP's version of this function, it does not read the entire file contents into RAM but only reads 1 byte at a time.</remarks>
        /// <param name="file">The original file.</param>
        /// <param name="patch">The binary patch file.</param>
        /// <param name="dest">Path of the resulting file.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static void xdiff_file_patch_binary(string file, string patch, string dest) => xdiff_file_bpatch(file, patch, dest);

        /// <summary>
        /// Patch a file with an unified diff
        /// </summary>
        /// <param name="file">The original file.</param>
        /// <param name="patch">The unified patch file. It has to be created using xdiff_string_diff(), xdiff_file_diff() functions or compatible tools.</param>
        /// <param name="dest">Path of the resulting file.</param>
        /// <param name="flags">Can be either XDIFF_PATCH_NORMAL(default mode, normal patch) or XDIFF_PATCH_REVERSE(reversed patch).<br />
        /// Starting from version 1.5.0, you can also use binary OR to enable XDIFF_PATCH_IGNORESPACE flag.</param>
        /// <returns>Returns false if an internal error happened, string with rejected chunks if patch couldn't be applied or true if patch has been successfully applied.</returns>
        public static (bool, string) xdiff_file_patch(string file, string patch, string dest, int flags = (int)PathFlags.XDIFF_PATCH_NORMAL)
        {
            if (File.Exists(file) && File.Exists(patch))
            {
                var t = Task.Run(() =>
                {
                    try
                    {
                        using (var ofs = File.Open(file, FileMode.Open))
                        using (var dfs = File.Open(dest, FileMode.Truncate))
                        using (var or = new StreamReader(ofs))
                        using (var dw = new StreamWriter(dfs))
                        {
                            foreach (var l in File.ReadLines(patch))
                            {
                                /* For lack of PECL extension the format is tentatively ---old/+++new */
                                // TODO
                            }
                        }
                        return (true, string.Empty);
                    }
                    catch (Exception)
                    {
                        return (false, string.Empty);
                    }
                });
                _ = Task.WhenAny(t);
                return t.Result;
            }
            else
            {
                return (false, string.Empty);
            }
        }

        /// <summary>
        /// Make binary diff of two files using the Rabin's polynomial fingerprinting algorithm
        /// </summary>
        /// <param name="old_file">Path to the first file. This file acts as "old" file.</param>
        /// <param name="new_file">Path to the second file. This file acts as "new" file.</param>
        /// <param name="dest">Path of the resulting patch file. Resulting file contains differences between "old" and "new" files. It is in binary format and is human-unreadable.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool xdiff_file_rabdiff(string old_file, string new_file, string dest)
        {
            return false; // TODO
        }

        /// <summary>
        /// Read a size of file created by applying a binary diff
        /// </summary>
        /// <param name="patch">The binary patch created by xdiff_string_bdiff() or xdiff_string_rabdiff() function.</param>
        /// <returns>Returns the size of file that would be created.</returns>
        public static int xdiff_string_bdiff_size(string patch)
        {
            return 0; // TODO
        }

        /// <summary>
        /// Make binary diff of two strings
        /// </summary>
        /// <param name="old_data">First string with binary data. It acts as "old" data.</param>
        /// <param name="new_data">Second string with binary data. It acts as "new" data.</param>
        /// <returns>Returns string with binary diff containing differences between "old" and "new" data or false if an internal error occurred.</returns>
        public static string xdiff_string_bdiff(string old_data, string new_data)
        {
            int pos = 0;
            var sb = new StringBuilder();
            while (true)
            { 
                char? ob = pos <= old_data.Length ? old_data[pos] : null;
                char? nb = pos <= new_data.Length ? new_data[pos] : null;

                /* For lack of PECL extension the format is tentatively <pos>:old|new */
                if (nb != null && ob != null && nb != ob)
                {
                    sb.AppendLine($"{pos}:{ob}>{nb}"); // TODO: install PECL extension and check format
                }
                else if (nb == null && ob != null)
                {
                    sb.AppendLine($"{pos}:{ob}|"); // TODO: install PECL extension and check format
                }
                else if (nb != null && ob == null)
                {
                    sb.AppendLine($"{pos}:|{nb}"); // TODO: install PECL extension and check format
                }
                else if (nb == null && ob == null)
                {
                    break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Patch a string with a binary diff
        /// </summary>
        /// <param name="str">The original binary string.</param>
        /// <param name="patch">The binary patch string.</param>
        /// <returns>Returns the patched string, or false on error.</returns>
        public static string xdiff_string_bpatch(string @string, string patch)
        {
            var sb = new StringBuilder();
            int prevPos = 0;
            foreach (var l in patch.Split(Environment.NewLine))
            {
                var ls = l.Split(':');
                int pos = Convert.ToInt32(ls[0]);
                sb.Append(@string.Substring(prevPos, pos));
                prevPos = pos;
                var p = ls[1].Split('|');

                /* For lack of PECL extension the format is tentatively <pos>:old|new */
                if (p[0] != null && p[1] != null && p[0] != p[1])
                {
                    sb.Append(p[0]); // TODO: install PECL extension and check format
                }
                else if (p[0] == null && p[1] != null)
                {
                    sb.Append(p[1]); // TODO: install PECL extension and check format
                }
                else if (p[0] != null && p[1] == null)
                {
                    continue;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Alias of xdiff_string_bdiff
        /// </summary>
        /// <param name="old_data">First string with binary data. It acts as "old" data.</param>
        /// <param name="new_data">Second string with binary data. It acts as "new" data.</param>
        /// <returns>Returns string with binary diff containing differences between "old" and "new" data or false if an internal error occurred.</returns>
        public static string xdiff_string_diff_binary(string old_data, string new_data) => xdiff_string_bdiff(old_data, new_data);

        /// <summary>
        /// Make unified diff of two strings
        /// </summary>
        /// <param name="old_data">First string with data. It acts as "old" data.</param>
        /// <param name="new_data">Second string with data. It acts as "new" data.</param>
        /// <param name="context">Indicates how many lines of context you want to include in the diff result.</param>
        /// <param name="minimal">Set this parameter to true if you want to minimalize the size of the result (can take a long time).</param>
        /// <returns>Returns string with resulting diff or false if an internal error happened.</returns>
        public static string xdiff_string_diff(string old_data, string new_data, int context = 3, bool minimal = false)
        {
            int pos = 0;
            var sb = new StringBuilder();
            var od = old_data.Split(Environment.NewLine);
            var nd = new_data.Split(Environment.NewLine);
            while (true)
            {
                var ob = pos <= od.Length ? od[pos] : null;
                var nb = pos <= nd.Length ? nd[pos] : null;

                /* For lack of PECL extension the format is tentatively <pos>:old|new */
                if (nb != null && ob != null && nb != ob)
                {
                    sb.AppendLine($"---{ob}"); // TODO: install PECL extension and check format
                    sb.AppendLine($"+++{nb}"); // TODO: install PECL extension and check format
                }
                else if (nb == null && ob != null)
                {
                    sb.AppendLine($"---{ob}"); // TODO: install PECL extension and check format
                }
                else if (nb != null && ob == null)
                {
                    sb.AppendLine($"+++{nb}"); // TODO: install PECL extension and check format
                }
                else if (nb == null && ob == null)
                {
                    break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Merge 3 strings into one
        /// </summary>
        /// <param name="old_data">First string with data. It acts as "old" data.</param>
        /// <param name="new_data1">Second string with data. It acts as modified version of old_data.</param>
        /// <param name="new_data2">Third string with data. It acts as modified version of old_data.</param>
        /// <param name="error">If provided then rejected parts are stored inside this variable.</param>
        /// <returns>Returns the merged string, false if an internal error happened, or true if merged string is empty.</returns>
        public static (bool, string) xdiff_string_merge3(string old_data, string new_data1, string new_data2, string error = null)
        {
            var sb = new StringBuilder();

            return (false, sb.ToString()); // TODO
        }

        /// <summary>
        /// Alias of xdiff_string_bpatch
        /// </summary>
        /// <param name="str">The original binary string.</param>
        /// <param name="patch">The binary patch string.</param>
        /// <returns>Returns the patched string, or false on error.</returns>
        public static string xdiff_string_patch_binary(string @string, string patch) => xdiff_string_bpatch(@string, patch);

        /// <summary>
        /// Patch a string with an unified diff
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="patch">The unified patch string. It has to be created using xdiff_string_diff(), xdiff_file_diff() functions or compatible tools.</param>
        /// <param name="flags">flags can be either XDIFF_PATCH_NORMAL(default mode, normal patch) or XDIFF_PATCH_REVERSE(reversed patch).<br />
        /// Starting from version 1.5.0, you can also use binary OR to enable XDIFF_PATCH_IGNORESPACE flag.</param>
        /// <param name="error">If provided then rejected parts are stored inside this variable.</param>
        /// <returns>Returns the patched string, or false on error.</returns>
        public static string xdiff_string_patch(string @string, string patch, int? flags = null, string error = null)
        {
            var sb = new StringBuilder();
            foreach (var l in patch.Split(Environment.NewLine))
            {
                /* For lack of PECL extension the format is tentatively <pos>:old|new */
                sb.AppendLine($""); // TODO: install PECL extension and check format
            }

            return sb.ToString(); // TODO
        }

        /// <summary>
        /// Make binary diff of two strings using the Rabin's polynomial fingerprinting algorithm
        /// </summary>
        /// <param name="old_data">First string with binary data. It acts as "old" data.</param>
        /// <param name="new_data">Second string with binary data. It acts as "new" data.</param>
        /// <returns>Returns string with binary diff containing differences between "old" and "new" data or false if an internal error occurred.</returns>
        public static (bool, string) xdiff_string_rabdiff(string old_data, string new_data)
        {
            return (false, string.Empty);
        }
    }
}
