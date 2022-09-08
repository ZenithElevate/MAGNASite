/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using Force.Crc32;
using OpenSSL.Crypto;
using System.Text;
using Vereyon.Web;

namespace MAGNASite.Test.AdHoc
{
    [TestClass]
    public class UnitTestAdHoc
    {
        [TestMethod]
        public void TestCryptoOpenSsl()
        {
            var i = 0;
            var ca = Cipher.AllNamesSorted.ToDictionary(x => i++, x => x);
            i = 0;
            ca = MessageDigest.AllNamesSorted.ToDictionary(x => i++, x => x);
        }

        [TestMethod]
        public void TestHtmlSan()
        {
            var san = HtmlSanitizer.SimpleHtml5DocumentSanitizer();
            foreach (var t in "p br i b tt strong ul ol li".Split(" "))
            {
                san.Tag(t).RemoveEmpty();
            }
            var s = san.Sanitize("<html><script src=\"abc\"><body><p>ABC<b>abc</b><p>XYZ<b>xyz</p><ul><li>abc<li>xyz</li></ul></body></html>");
        }

        [TestMethod]
        public void TestMethoFS()
        {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes("masavÃ¤g");
            byte[] ISO88591 = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("ISO-8859-1"), utf8Bytes);
            var sss = Encoding.UTF8.GetString(ISO88591);
            var ssss = Encoding.GetEncoding("ISO-8859-1").GetString(ISO88591);

            var cnt = "amamamamamamamama".Split("ama").Count();

            var comp = "abc".CompareTo(null);

            var sim = "abc".Intersect("bcd").Count();

            var buf = "AbC".ToCharArray();
            buf[0] = char.ToLowerInvariant(buf[0]);
            var r = new string(buf);


            var s = new string[] { "a", "aa", "aaa", "aaaa", "aaaaa"};
            var l = s.Aggregate(0, (l, s) => l+s.Length);

            var crc32 = new Crc32Algorithm();
            using (var input = new MemoryStream(Encoding.Default.GetBytes("abcdef")))
            {
                var t = crc32.ComputeHashAsync(input);
            }


            var a = new Dictionary<int, string>() { { 1, "a" }, { 10, "g" } };
            var b = new Dictionary<string, string>() { { "n", "a" }, { "a", "g" } };

            //var a = Convert.ToInt32("one");
            var di = new DirectoryInfo(Path.GetPathRoot(@"C:\"));
            var fs = di.GetFileSystemInfos();
        }

        [TestMethod]
        public void TestMethodFullName()
        {
            var path = ".";
            var n = new FileInfo(path)?.FullName ?? new DirectoryInfo(path).FullName;
            path = "..";
            n = new FileInfo(path)?.FullName ?? new DirectoryInfo(path).FullName;
            path = @"C:\Users\use0\Application Data";
            n = new FileInfo(path).ResolveLinkTarget(false)?.Name ?? new DirectoryInfo(path).ResolveLinkTarget(false)?.Name;
        }

        [TestMethod]
        public void TestMethodMove()
        {
            using (var s = File.Create("~1"))
            {
                var a = s.Name;
            }
            File.Delete("~1");

            string from = "C:/1/_1", to = "C:/1/_2";
            Directory.CreateDirectory(from);
            Directory.Move(from, to);
            Directory.CreateDirectory(from);
            File.Move(from, to, true);
        }
    }
}