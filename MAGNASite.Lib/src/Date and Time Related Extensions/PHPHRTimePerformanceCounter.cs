/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.HRTime
{
    /// <summary>
    /// PerformanceCounter class
    /// </summary>
    internal class PHPHRTimePerformanceCounter
    {
        /// <summary>
        /// Timer frequency in ticks per second
        /// </summary>
        /// <returns>Returns int indicating the timer frequency.</returns>
        public long getFrequency()
        {
            return 10000000;
        }

        /// <summary>
        /// Current ticks from the system
        /// </summary>
        /// <returns>Returns int indicating the ticks count.</returns>
        public long getTicks()
        {
            ulong UnbiasedTime = 0L;
            UnsafeNativeMethods.QueryUnbiasedInterruptTime(ref UnbiasedTime);
            return (long)UnbiasedTime;
        }

        /// <summary>
        /// Ticks elapsed since the given value
        /// </summary>
        /// <param name="start">Starting ticks value.</param>
        /// <returns>Returns int indicating the ticks count.</returns>
        public long getTicksSince(int start)
        {
            return DateTime.Now.Ticks - start;
        }
    }
}
