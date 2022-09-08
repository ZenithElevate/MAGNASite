/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using MAGNASite.Lib;
using MAGNASite.Lib.Properties;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using static MAGNASite.Lib.UnsafeNativeMethods;

namespace MAGNASite.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUnicode()
        {
            var str = "Les Mise\u0301rables";

            var b = str.ReverseGraphemeClusters();
        }

        [TestMethod]
        public void TestMethodResources()
        {
            var sb = new StringBuilder(500);
            UnsafeNativeMethods.GetLocaleInfoEx(Thread.CurrentThread.CurrentCulture.Name, UnsafeNativeMethods.LCTYPE.LOCALE_SGROUPING, sb, 500);
            var p_sign_posn = sb.ToString();
            sb.Clear();
            UnsafeNativeMethods.GetLocaleInfoEx(Thread.CurrentThread.CurrentCulture.Name, UnsafeNativeMethods.LCTYPE.LOCALE_SMONGROUPING, sb, 500);
            var n_sign_posn = sb.ToString();
            sb.Clear();

            Resources.Culture = CultureInfo.GetCultureInfo("ru-RU");
            Assert.IsTrue(Resources.rsErrorInvalidDateTimeString.Equals("Предоставленная строка даты неправильная"));
            Resources.Culture = CultureInfo.GetCultureInfo("en-US");
            Assert.IsTrue(Resources.rsErrorInvalidDateTimeString.Equals("The provided date/time string is invalid"));
        }

        [TestMethod]
        public void TestMethodVariables()
        {
            ulong UnbiasedTime = 0L;
            UnsafeNativeMethods.QueryUnbiasedInterruptTime(ref UnbiasedTime);

            var ticks = UnsafeNativeMethods.GetTickCount64();

            var sw = new Stopwatch();
            sw.Start();
            Thread.Sleep(100);
            sw.Stop();
            var t = sw.Elapsed.Ticks;
            var tm = sw.Elapsed.TotalMilliseconds;
            var m = sw.Elapsed.Milliseconds;

            var s = "abc";
            var sw1 = s.StartsWith(string.Empty);
        }

        [TestMethod]
        public void TestMethodFiles()
        {
            using (var hFile = File.OpenHandle("MAGNASite.Test.runtimeconfig.json", FileMode.Open))
            {
                var lpFileInformation = new _BY_HANDLE_FILE_INFORMATION();
                var b = GetFileInformationByHandle(hFile, out lpFileInformation);
                var il = lpFileInformation.nFileIndexLow;
                var ih = lpFileInformation.nFileIndexHigh;
            }
        }

        [TestMethod]
        public void TestMethodDirs()
        {
            var hFile = CreateFileA(".", GENERIC_READ, FILE_SHARE_READ,
                IntPtr.Zero,
                OPEN_EXISTING,
                FILE_ATTRIBUTE_NORMAL | FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero);

            var lpFileInformation = new _BY_HANDLE_FILE_INFORMATION();
            var b = GetFileInformationByHandle(hFile, out lpFileInformation);
            var il = lpFileInformation.nFileIndexLow;
            var ih = lpFileInformation.nFileIndexHigh;
            var r = CloseHandle(hFile);
        }
    }
}