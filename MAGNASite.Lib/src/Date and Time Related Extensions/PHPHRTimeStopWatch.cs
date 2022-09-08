/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Diagnostics;
using MAGNASite.Lib.Enums;

namespace MAGNASite.Lib.HRTime
{
    /// <summary>
    /// StopWatch class
    /// </summary>
    internal class PHPHRTimeStopWatch
    {
        protected Stopwatch stopwatch;
        List<TimeSpan> elapsed;

        public PHPHRTimeStopWatch()
        {
            stopwatch = new Stopwatch();
            elapsed = new List<TimeSpan>(50);
        }

        /// <summary>
        /// Get elapsed ticks for all intervals
        /// </summary>
        /// <returns>Returns int indicating elapsed ticks.</returns>
        public long getElapsedTicks()
        {
            return elapsed.Sum(e => e.Ticks);
        }

        /// <summary>
        /// Get elapsed time for all intervals
        /// </summary>
        /// <param name="unit">Time unit represented by a HRTime\Unit constant. Default is HRTime\Unit::SECOND. </param>
        /// <returns>Returns float indicating elapsed time.</returns>
        public float getElapsedTime(int unit = (int)HRTimeUnits.SECOND)
        {
            return elapsed.Sum(e => e.Ticks) * unit / 100;
        } 

        /// <summary>
        /// Get elapsed ticks for the last interval
        /// </summary>
        /// <returns>Returns int indicating elapsed ticks. </returns>
        public long getLastElapsedTicks()
        {
            return elapsed.Last().Ticks;
        }

        /// <summary>
        /// Get elapsed time for the last interval
        /// </summary>
        /// <param name="unit">Time unit represented by a HRTime\Unit constant. Default is HRTime\Unit::SECOND. </param>
        /// <returns>Returns float indicating elapsed time.</returns>
        public float getLastElapsedTime(int unit = (int)HRTimeUnits.SECOND)
        {
            return elapsed.Last().Ticks * unit / 100;
        }

        /// <summary>
        /// Whether the measurement is running
        /// </summary>
        /// <returns>Returns bool indicating whether the measurement is running. </returns>
        public bool isRunning()
        {
            return stopwatch.IsRunning;
        }

        /// <summary>
        /// Start time measurement
        /// </summary>
        public void start() 
        {
            stopwatch.Start();
        }

        /// <summary>
        /// Stop time measurement
        /// </summary>
        public void stop() 
        {
            elapsed.Add(stopwatch.Elapsed);
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }
}
