/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using MAGNASite.Lib.Properties;

namespace MAGNASite.Lib
{
    internal class PHPDateInterval : ErrorAwareBaseClass
    {
        public TimeSpan value { get; set; }

        public PHPDateInterval(string s)
        {
            if (TimeSpan.TryParse(s, out TimeSpan result))
            {
                value = result;
            }
            else
            {
                AddError(Resources.rsErrorInvalidDateTimeIntervalString);
                value = TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Formats the interval
        /// </summary>
        /// <param name="format">The format of the outputted date interval string. See the formatting options below. There are also several predefined date constants that may be used instead, so for example DATE_RSS contains the format string 'D, d M Y H:i:s'.</param>
        /// <returns>Returns the formatted interval. </returns>
        public string format(string format)
        {
            return value.ToString(format);
        }

        /// <summary>
        /// Alias of DateInterval::format
        /// </summary>
        /// <param name="format">The format of the outputted date interval string. See the formatting options below. There are also several predefined date constants that may be used instead, so for example DATE_RSS contains the format string 'D, d M Y H:i:s'.</param>
        /// <returns>Returns the formatted interval. </returns>
        public string date_interval_format(string format)
        {
            return this.format(format);
        }

    }
}