/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using MAGNASite.Lib.Enums;
using MAGNASite.Lib.Properties;
using System.Globalization;

namespace MAGNASite.Lib
{

    internal sealed class PHPDateTime : ErrorAwareBaseClass
    {
        public DateTimeOffset Value { get; set; }
        public PHPDateTimeZone Zone;
        string TimeZoneId;

        public PHPDateTime(string s)
        {
            if (DateTimeOffset.TryParse(s, out DateTimeOffset result))
            {
                Value = result;
            }
            else
            {
                Value = DateTimeOffset.MinValue;
            }
        }

        public PHPDateTime(DateTimeOffset dt)
        {
            Value = dt;
        }

        public PHPDateTime(DateTimeOffset dt, string zone)
        {
            Value = dt;
            Zone = new PHPDateTimeZone(zone);
        }

        public PHPDateTime add(PHPDateInterval interval)
        {
            return new PHPDateTime(Value.Add(interval.value));
        }

        public static PHPDateTime createFromFormat(string format, string datetime, PHPDateTimeZone? timezone = null)
        {
            if (DateTime.TryParseExact(datetime, format, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime result))
            {
                return new PHPDateTime(result);
            }
            else
            {
                return new PHPDateTime(DateTime.MinValue);
            }
        }

        public static bool checkdate(int month, int day, int year)
        {
            return
                year >= 1 && year <= 32767 &&
                month >= 1 && month <= 12 &&
                day >= 1 && day <= DateTime.DaysInMonth(year, month);
        }

        public PHPDateTime date_add(PHPDateInterval interval)
        {
            return add(interval);
        }

        public PHPDateTime date_create_from_format(string format, string datetime, PHPDateTimeZone? timezone = null) // Alias of DateTime::createFromFormat
        {
            return createFromFormat(format, datetime, timezone);
        }

        public PHPDateTime date_create_immutable_from_format(string format, string datetime, PHPDateTimeZone? timezone = null) // Alias of DateTimeImmutable::createFromFormat
        {
            return createFromFormat(format, datetime, timezone);
        }

        public PHPDateTime date_create_immutable() // Alias of DateTimeImmutable::__construct
        {
            return new PHPDateTime(DateTime.MinValue);
        }

        public PHPDateTime date_create(string datetime ="now", PHPDateTimeZone? timezone = null) // create a new DateTime object
        {
            if (datetime.ToUpperInvariant().Equals("NOW"))
            {
                return new PHPDateTime(DateTime.Now);
            }
            else
            {
                return new PHPDateTime(datetime);
            }
        }

        public PHPDateTime setDate(int year, int month, int day)
        {
            return new PHPDateTime(new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.Zero));
        }

        /// <summary>
        /// Alias of DateTime::setDate
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public PHPDateTime date_date_set(int year, int month, int day)
        {
            return setDate(year, month, day);
        }

        /// <summary>
        /// Gets the default timezone used by all date/time functions in a script 
        /// </summary>
        /// <param name="timezoneId"></param>
        /// <returns></returns>
        public string date_default_timezone_get(string timezoneId)
        {
            return TimeZoneId;
        }

        /// <summary>
        ///  Sets the default timezone used by all date/time functions in a script 
        /// </summary>
        /// <param name="timezoneId"></param>
        /// <returns></returns>
        public bool date_default_timezone_set(string timezoneId)
        {
            if (PHPDateTimeZone.TimeZoneIsValid(timezoneId))
            {
                TimeZoneId = timezoneId;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the difference between two DateTime objects.
        /// The return value more specifically represents the interval to apply to the original object ($this or $originObject) to arrive at the $targetObject. This process is not always reversible. 
        /// </summary>
        /// <param name="targetObject">The date to compare to. </param>
        /// <param name="absolute">Should the interval be forced to be positive? </param>
        /// <returns>The difference between two DateTimeInterface objects</returns>
        public TimeSpan diff(PHPDateTime targetObject, bool absolute)
        {
            return Value - targetObject.Value;
        }

        /// <summary>
        /// Alias of DateTime::diff
        /// </summary>
        /// <param name="targetObject">The date to compare to. </param>
        /// <param name="absolute">Should the interval be forced to be positive? </param>
        /// <returns>The difference between two DateTimeInterface objects</returns>
        public TimeSpan date_diff(PHPDateTime targetObject, bool absolute)
        {
            return diff(targetObject, absolute);
        }

        /// <summary>
        /// Returns date formatted according to given format
        /// </summary>
        /// <param name="format">The format of the outputted date string. See the formatting options below. There are also several predefined date constants that may be used instead, so for example DATE_RSS contains the format string 'D, d M Y H:i:s'.</param>
        /// <returns>Returns the formatted date string on success.</returns>
        public string format(string format)
        {
            return Value.ToString(format);
        }

        /// <summary>
        /// Alias of DateTime::format
        /// </summary>
        /// <param name="format">The format of the outputted date string. See the formatting options below. There are also several predefined date constants that may be used instead, so for example DATE_RSS contains the format string 'D, d M Y H:i:s'.</param>
        /// <returns>Returns the formatted date string on success.</returns>
        public string date_format(string format)
        {
            return this.format(format);
        }

        /// <summary>
        /// Returns the warnings and errors
        /// </summary>
        /// <returns>Returns array containing info about warnings and errors, or false if there are neither warnings nor errors.</returns>
        public string[] getLastErrors()
        {
            return Errors.ToArray();
        }

        /// <summary>
        /// Alias of DateTime::getLastErrors
        /// </summary>
        /// <returns>Returns array containing info about warnings and errors, or false if there are neither warnings nor errors.</returns>
        public string[] date_get_last_errors()
        {
            return getLastErrors();
        }

        /// <summary>
        /// Sets up a DateInterval from the relative parts of the string
        /// </summary>
        /// <param name="datetime">A date with relative parts. Specifically, the relative formats supported by the parser used for DateTimeImmutable, DateTime, and strtotime() will be used to construct the DateInterval. </param>
        /// <returns>Returns a new DateInterval instance on success, or false on failure. </returns>
        public TimeSpan createFromDateString(string datetime)
        {
            if (TimeSpan.TryParse(datetime, out TimeSpan result))
            {
                return result;
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
                AddError(Resources.rsErrorInvalidDateTimeString);
                return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Alias of DateInterval::createFromDateString
        /// </summary>
        /// <param name="datetime">A date with relative parts. Specifically, the relative formats supported by the parser used for DateTimeImmutable, DateTime, and strtotime() will be used to construct the DateInterval. </param>
        /// <returns>Returns a new DateInterval instance on success, or false on failure. </returns>
        public TimeSpan date_interval_create_from_date_string(string datetime)
        {
            return createFromDateString(datetime);
        }

        /// <summary>
        /// Sets the ISO date
        /// </summary>
        /// <param name="year">Year of the date.</param>
        /// <param name="week">Week of the date.</param>
        /// <param name="dayOfWeek">Offset from the first day of the week.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure. </returns>
        public static PHPDateTime setISODate(int year, int week, int dayOfWeek = 1)
        {
            var d = new PHPDateTime(new DateTimeOffset(year, week, dayOfWeek, 0, 0, 0, TimeSpan.Zero));
            return d;
        }

        /// <summary>
        /// Alias of DateTime::setISODate
        /// </summary>
        /// <param name="year">Year of the date.</param>
        /// <param name="week">Week of the date.</param>
        /// <param name="dayOfWeek">Offset from the first day of the week.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure. </returns>
        public static PHPDateTime date_isodate_set(int year, int week, int dayOfWeek)
        {
            return setISODate(year, week, dayOfWeek);
        }

        /// <summary>
        /// Alters the timestamp
        /// Alter the timestamp of a DateTime object by incrementing or decrementing in a format accepted by DateTimeImmutable::__construct().
        /// </summary>
        /// <param name="modifier">A date/time string. Valid formats are explained in Date and Time Formats.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure. </returns>
        public PHPDateTime modify(string modifier)
        {
            var d = new PHPDateTime(Value);
            var i = new PHPDateInterval(modifier);
            d.add(i);
            return d;
        }

        /// <summary>
        /// Alias of DateTime::modify
        /// </summary>
        /// <param name="modifier">A date/time string. Valid formats are explained in Date and Time Formats.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure. </returns>
        public PHPDateTime date_modify(string modifier)
        {
            return modify(modifier);
        }

        /// <summary>
        /// Returns the timezone offset
        /// </summary>
        /// <returns>Returns the timezone offset in seconds from UTC on success. </returns>
        public int date_offset_get() // Alias of DateTime::getOffset
        {
            return Value.Offset.Seconds;
        }

        /// <summary>
        /// Get info about given date formatted according to the specified format
        /// </summary>
        /// <param name="format">Format accepted by DateTime::createFromFormat(). </param>
        /// <param name="datetime">String representing the date/time. </param>
        /// <returns>Returns associative array with detailed info about given date/time. </returns>
        public static PHPDateTime date_parse_from_format(string format, string datetime) // Get info about given date formatted according to the specified format
        {
            // reverse engineer date formats
            var keys = new Dictionary<string, Tuple<string, string>>(17)
            {
                {"Y", new Tuple<string, string>("year", @"\d{4}") },
                {"y", new Tuple<string, string>("year", @"\d{2}")},
                {"m", new Tuple<string, string>("month", @"\d{2}")},
                {"n", new Tuple<string, string>("month", @"\d{1},2}")},
                {"M", new Tuple<string, string>("month", @"[A-Z][a-z]{3}")},
                {"F", new Tuple<string, string>("month", @"[A-Z][a-z]{2},8}")},
                {"d", new Tuple<string, string>("day", @"\d{2}")},
                {"j", new Tuple<string, string>("day", @"\d{1},2}")},
                {"D", new Tuple<string, string>("day", @"[A-Z][a-z]{2}")},
                {"l", new Tuple<string, string>("day", @"[A-Z][a-z]{6},9}")},
                {"u", new Tuple<string, string>("hour", @"\d{1},6}")},
                {"h", new Tuple<string, string>("hour", @"\d{2}")},
                {"H", new Tuple<string, string>("hour", @"\d{2}")},
                {"g", new Tuple<string, string>("hour", @"\d{1},2}")},
                {"G", new Tuple<string, string>("hour", @"\d{1},2}")},
                {"i", new Tuple<string, string>("minute", @"\d{2}") },
                {"s", new Tuple<string, string>("second", @"\d{2}") }
            };

            // convert format string to regex
            var regex = string.Empty;
            var chars = format.Split("");
            for (var n = 0; n < chars.Length; n++)
            {
                var chr = chars[n];
                var lastChar = chars[n - 1] != null ? chars[n - 1] : string.Empty;
                var skipCurrent = @"\" == lastChar;
                if (!skipCurrent && keys[chr] != null)
                {
                    regex +="(?P<" + keys[chr].Item1 +">" + keys[chr].Item2 +")";
                }
                else if (@"\" == chr) {
                    regex += chr;
                }
                else
                {
                    regex += PHPPCRE.preg_quote(chr);
                }
            }

            var dt = new PHPDateFormatInfo();

            return null;
            //var dt.error_count = 0;
                // now try to match it
/*                if (PHPPCRE.preg_match("#^" + regex +"$#", date, dt))
                {
                    foreach (dt AS k => v) {
                        if (PHPVariableHandling.is_int(k))
                        {
                            dt[k] = null;
                        }
                    }
                    if (!checkdate(dt["month"], dt["day"], dt["year"]))
                    {
                        dt.errors.Add(Resources.rsErrorInvalidDateTimeString);
                    }
                }
                else
                {
                $dt["error_count"] = 1;
                }
            //dt["errors"] = array();
            dt.fraction = string.Empty;
            //dt["warning_count"] = 0;
            //dt["warnings"] = array();
            dt["is_localtime"] = 0;
            dt["zone_type"] = 0;
            dt["zone"] = 0;
            dt["is_dst"] ="";
                return dt;
*/
        }

        public static PHPDateTime date_parse() // Returns associative array with detailed info about given date/time
        {
            return null; // TODO
        }

        /// <summary>
        /// Subtracts an amount of days, months, years, hours, minutes and seconds from a DateTime object 
        /// </summary>
        /// <param name="interval">A DateInterval object to be subtracted.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure.</returns>
        public PHPDateTime sub(PHPDateInterval interval)
        {
            return new PHPDateTime(Value.Subtract(interval.value));
        }

        /// <summary>
        /// Alias of DateTime::sub
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public PHPDateTime date_sub(PHPDateInterval interval) => sub(interval);

        /// <summary>
        /// Returns an array with information about sunset/sunrise and twilight begin/end
        /// </summary>
        /// <param name="timestamp">Unix timestamp.</param>
        /// <param name="latitude">Latitude in degrees.</param>
        /// <param name="longitude">Longitude in degrees.</param>
        /// <returns>Returns array on success or false on failure. The structure of the array is detailed in the following list:
        /// | sunrise: The timestamp of the sunrise(zenith angle = 90°35'). 
        /// | sunset: The timestamp of the sunset (zenith angle = 90°35'). 
        /// | transit: The timestamp when the sun is at its zenith, i.e.has reached its topmost point.
        /// | civil_twilight_begin: The start of the civil dawn (zenith angle = 96°). It ends at sunrise.
        /// | civil_twilight_end: The end of the civil dusk (zenith angle = 96°). It starts at sunset.
        /// | nautical_twilight_begin: The start of the nautical dawn (zenith angle = 102°). It ends at civil_twilight_begin.
        /// | nautical_twilight_end: The end of the nautical dusk (zenith angle = 102°). It starts at civil_twilight_end.
        /// | astronomical_twilight_begin: The start of the astronomical dawn (zenith angle = 108°). It ends at nautical_twilight_begin.
        /// | astronomical_twilight_end: The end of the astronomical dusk (zenith angle = 108°). It starts at nautical_twilight_end.
        /// The values of the array elements are either UNIX timestamps, false if the sun is below the respective zenith for the whole day, or true if the sun is above the respective zenith for the whole day.
        /// </returns>
        public static Dictionary<string, PHPDateTime> date_sun_info(int timestamp, float latitude, float longitude) // Returns an array with information about sunset/sunrise and twilight begin/end
        {
            DateTimeOffset sunrise = DateTimeOffset.Now;
            DateTimeOffset sunset = DateTimeOffset.Now;
            DateTimeOffset transit = DateTimeOffset.Now;
            DateTimeOffset civil_twilight_begin = DateTimeOffset.Now;
            DateTimeOffset civil_twilight_end = DateTimeOffset.Now;
            DateTimeOffset nautical_twilight_begin = DateTimeOffset.Now;
            DateTimeOffset nautical_twilight_end = DateTimeOffset.Now;
            DateTimeOffset astronomical_twilight_begin = DateTimeOffset.Now;
            DateTimeOffset astronomical_twilight_end = DateTimeOffset.Now;

            return new Dictionary<string, PHPDateTime>(9) {
                {"sunrise", new PHPDateTime(sunrise)},
                {"sunset", new PHPDateTime(sunset)},
                {"transit", new PHPDateTime(transit)},
                {"civil_twilight_begin", new PHPDateTime(civil_twilight_begin)},
                {"civil_twilight_end", new PHPDateTime(civil_twilight_end)},
                {"nautical_twilight_begin", new PHPDateTime(nautical_twilight_begin)},
                {"nautical_twilight_end", new PHPDateTime(nautical_twilight_end)},
                {"astronomical_twilight_begin", new PHPDateTime(astronomical_twilight_begin)},
                {"astronomical_twilight_end", new PHPDateTime(astronomical_twilight_end) }
            };
        }

        /// <summary>
        /// Returns time of sunrise for a given day and location.
        /// Every call to a date/time function will generate a E_WARNING if the time zone is not valid. See also date_default_timezone_set()
        /// </summary>
        /// <param name="timestamp">The timestamp of the day from which the sunrise time is taken. </param>
        /// <param name="returnFormat"></param>
        /// <param name="latitude">Defaults to North, pass in a negative value for South. See also: date.default_latitude.</param>
        /// <param name="longitude">Defaults to East, pass in a negative value for West. See also: date.default_longitude.</param>
        /// <param name="zenith">zenith is the angle between the center of the sun and a line perpendicular to earth's surface. It defaults to date.sunrise_zenith </param>
        /// <param name="utcOffset">Specified in hours. The utcOffset is ignored, if returnFormat is SUNFUNCS_RET_TIMESTAMP.</param>
        /// <returns>Returns the sunrise time in a specified returnFormat on success or false on failure. One potential reason for failure is that the sun does not rise at all, which happens inside the polar circles for part of the year.</returns>
        [Obsolete("This function has been DEPRECATED as of PHP 8.1.0. Relying on this function is highly discouraged.")]
        public static PHPDateTime date_sunrise(int timestamp,
            SunriseReturnFormats returnFormat = SunriseReturnFormats.SUNFUNCS_RET_STRING,
            float latitude = float.NaN, float longitude = float.NaN, float zenith = float.NaN, float utcOffset = float.NaN)
        {
            return new PHPDateTime(DateTimeOffset.Now);
        }

        /// <summary>
        /// Returns time of sunset for a given day and location.
        /// Every call to a date/time function will generate a E_WARNING if the time zone is not valid. See also date_default_timezone_set()
        /// </summary>
        /// <param name="timestamp">The timestamp of the day from which the sunset time is taken. </param>
        /// <param name="returnFormat"></param>
        /// <param name="latitude">Defaults to North, pass in a negative value for South. See also: date.default_latitude.</param>
        /// <param name="longitude">Defaults to East, pass in a negative value for West. See also: date.default_longitude.</param>
        /// <param name="zenith">zenith is the angle between the center of the sun and a line perpendicular to earth's surface. It defaults to date.sunset_zenith </param>
        /// <param name="utcOffset">Specified in hours. The utcOffset is ignored, if returnFormat is SUNFUNCS_RET_TIMESTAMP.</param>
        /// <returns>Returns the sunset time in a specified returnFormat on success or false on failure. One potential reason for failure is that the sun does not set at all, which happens inside the polar circles for part of the year.</returns>
        [Obsolete("This function has been DEPRECATED as of PHP 8.1.0. Relying on this function is highly discouraged.")]
        public static PHPDateTime date_sunset(int timestamp,
            SunriseReturnFormats returnFormat = SunriseReturnFormats.SUNFUNCS_RET_STRING,
            float latitude = float.NaN, float longitude = float.NaN, float zenith = float.NaN, float utcOffset = float.NaN)
        {
            return new PHPDateTime(DateTimeOffset.Now);
        }

        /// <summary>
        /// Sets the time
        /// </summary>
        /// <param name="hour">Hour of the time.</param>
        /// <param name="minute">Minute of the time.</param>
        /// <param name="second">Second of the time.</param>
        /// <param name="microsecond">Microsecond of the time.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure.</returns>
        public static PHPDateTime setTime(int hour, int minute, int second = 0, int microsecond = 0)
        {
            return new PHPDateTime(new DateTime(0, 0, 0, hour, minute, second, microsecond * 1000));
        }

        /// <summary>
        /// Alias of DateTime::setTime
        /// </summary>
        /// <param name="hour">Hour of the time.</param>
        /// <param name="minute">Minute of the time.</param>
        /// <param name="second">Second of the time.</param>
        /// <param name="microsecond">Microsecond of the time.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure.</returns>
        public static PHPDateTime date_time_set(int hour, int minute, int second = 0, int microsecond = 0) => setTime(hour, minute, second, microsecond);

        /// <summary>
        /// Gets the Unix timestamp
        /// </summary>
        /// <returns>Returns the Unix timestamp representing the date.</returns>
        public long getTimestamp()
        {
            return Value.ToUnixTimeSeconds();
        }

        /// <summary>
        /// Alias of DateTime::getTimestamp
        /// </summary>
        /// <returns>Returns the Unix timestamp representing the date.</returns>
        public long date_timestamp_get() => getTimestamp();

        /// <summary>
        ///  Sets the date and time based on an Unix timestamp
        /// </summary>
        /// <param name="timestamp">Unix timestamp representing the date. Setting timestamps outside the range of int is possible by using DateTimeImmutable::modify() with the @ format.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure.</returns>
        public static PHPDateTime setTimestamp(int timestamp)
        {
            return new PHPDateTime(DateTimeOffset.FromUnixTimeSeconds(timestamp));
        }

        /// <summary>
        /// Alias of DateTime::setTimestamp
        /// </summary>
        /// <param name="timestamp">Unix timestamp representing the date. Setting timestamps outside the range of int is possible by using DateTimeImmutable::modify() with the @ format.</param>
        /// <returns>Returns the modified DateTime object for method chaining or false on failure.</returns>
        public static PHPDateTime date_timestamp_set(int timestamp) => setTimestamp(timestamp);

        /// <summary>
        /// Return time zone relative to given DateTime
        /// </summary>
        /// <returns>Returns a DateTimeZone object on success or false on failure.</returns>
        public PHPDateTime getTimezone()
        {
            return new PHPDateTime(Value, Zone.ToString()); // TODO
        }

        /// <summary>
        /// Alias of DateTime::getTimezone
        /// </summary>
        /// <returns>Returns a DateTimeZone object on success or false on failure.</returns>
        public PHPDateTime date_timezone_get() => getTimezone();

        public PHPDateTime date_timezone_set() // Alias of DateTime::setTimezone
        {
            return null; // TODO
        }

        /// <summary>
        /// Format a Unix timestamp.
        /// Returns a string formatted according to the given format string using the given integer timestamp (Unix timestamp) or the current time if no timestamp is given. In other words, timestamp is optional and defaults to the value of time().
        /// Unix timestamps do not handle timezones.Use the DateTimeImmutable class, and its DateTimeInterface::format() formatting method to format date/time information with a timezone attached.
        /// </summary>
        /// <param name="format">Format accepted by DateTimeInterface::format().</param>
        /// <param name="timestamp">The optional timestamp parameter is an int Unix timestamp that defaults to the current local time if timestamp is omitted or null. In other words, it defaults to the value of time(). </param>
        /// <returns>Returns a formatted date string. If a non-numeric value is used for timestamp, false is returned and an E_WARNING level error is emitted.</returns>
        public static string date(string format, int? timestamp = null) // Format a Unix timestamp
        {
            return timestamp == null ?
                DateTimeOffset.Now.ToString(format) : 
                DateTimeOffset.FromUnixTimeSeconds(timestamp ?? 0L).ToString(format);
        }

        /// <summary>
        /// Get date/time information
        /// </summary>
        /// <param name="timestamp">The optional timestamp parameter is an int Unix timestamp that defaults to the current local time if timestamp is omitted or null. In other words, it defaults to the value of time().</param>
        /// <returns>Returns an associative array of information related to the timestamp. Elements from the returned associative array are as follows:</returns>
        public static Dictionary<string, string> getdate(int? timestamp = null)
        {
            var o = DateTimeOffset.FromUnixTimeSeconds(timestamp ?? 0L);

            return new Dictionary<string, string>(11)
            {
                {"seconds" , $"{o.Second}" },                                                       // Numeric representation of seconds                                                            0 to 59
                {"minutes" , $"{o.Minute}" },                                                       // Numeric representation of minutes                                                            0 to 59
                {"hours"   , $"{o.Hour}" },                                                         // Numeric representation of hours                                                              0 to 23
                {"mday"    , $"{o.Day}" },                                                          // Numeric representation of the day of the month                                               1 to 31
                {"wday"    , $"{o.DayOfWeek}" },                                                    // Numeric representation of the day of the week                                                0 (for Sunday) through 6(for Saturday)
                {"mon"     , $"{o.Month}" },                                                        // Numeric representation of a month                                                            1 through 12
                {"year"    , $"{o.Year}" },                                                         // A full numeric representation of a year,                                                     4 digits Examples: 1999 or 2003
                {"yday"    , $"{o.DayOfYear}" },                                                    // Numeric representation of the day of the year                                                0 through 365
                {"weekday" , CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(o.DayOfWeek) },   // A full textual representation of the day of the week                                         Sunday through Saturday
                {"month"   , CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(o.Month) },     // A full textual representation of a month, such as January or March                           January through December
                { "0"      , $"{timestamp ?? 0}" }                                                  // Seconds since the Unix Epoch, similar to the values returned by time() and used by date(). 	System Dependent, typically -2147483648 through 2147483647. 
            };
        }


        public static Dictionary<string, string> gettimeofday(bool as_float = false) // Get current time
        {
            var o = DateTimeOffset.Now;

            return new Dictionary<string, string>(4)
            {
                {"sec"          , $"{o.ToUnixTimeSeconds}" },
                {"usec"         , $"{o.Millisecond * 1000}" },
                {"minuteswest"  , $"{TimeZone.CurrentTimeZone.GetUtcOffset(o.LocalDateTime).Minutes}" },
                {"dsttime"      , $"{1}" } // TODO
            };
        }

        /// <summary>
        /// Format a GMT/UTC date/time
        /// </summary>
        /// <param name="format">The format of the outputted date string. See the formatting options for the date() function.</param>
        /// <param name="timestamp">The optional timestamp parameter is an int Unix timestamp that defaults to the current local time if timestamp is omitted or null. In other words, it defaults to the value of time().</param>
        /// <returns>Returns a formatted date string.</returns>
        public static string gmdate(string format, int? timestamp = null) 
        {
            var o = timestamp == null ? DateTimeOffset.UtcNow : DateTimeOffset.FromUnixTimeSeconds(timestamp ?? 0);
            return o.ToString(format);
        }

        /// <summary>
        /// Get Unix timestamp for a GMT date
        /// </summary>
        /// <param name="hour">The number of the hour relative to the start of the day determined by month, day and year. Negative values reference the hour before midnight of the day in question. Values greater than 23 reference the appropriate hour in the following day(s).</param>
        /// <param name="minute">The number of the minute relative to the start of the hour. Negative values reference the minute in the previous hour. Values greater than 59 reference the appropriate minute in the following hour(s).</param>
        /// <param name="second">The number of seconds relative to the start of the minute. Negative values reference the second in the previous minute. Values greater than 59 reference the appropriate second in the following minute(s).</param>
        /// <param name="month">The number of the month relative to the end of the previous year. Values 1 to 12 reference the normal calendar months of the year in question. Values less than 1 (including negative values) reference the months in the previous year in reverse order, so 0 is December, -1 is November, etc. Values greater than 12 reference the appropriate month in the following year(s).</param>
        /// <param name="day">The number of the day relative to the end of the previous month. Values 1 to 28, 29, 30 or 31 (depending upon the month) reference the normal days in the relevant month. Values less than 1 (including negative values) reference the days in the previous month, so 0 is the last day of the previous month, -1 is the day before that, etc. Values greater than the number of days in the relevant month reference the appropriate day in the following month(s).</param>
        /// <param name="year">The year.</param>
        /// <returns>Returns a int Unix timestamp on success, or false on failure.</returns>
        public static long? gmmktime(int hour, int? minute = null, int? second = null, int? month = null, int? day = null, int? year = null)
        {
            var o = new DateTimeOffset(year ?? 0, month ?? 0, day ?? 0, hour, minute ?? 0, second ?? 0, TimeSpan.Zero);
            return o.ToUnixTimeSeconds();
        }

        /// <summary>
        /// Format a GMT/UTC time/date according to locale settings.
        /// Behaves the same as strftime() except that the time returned is Greenwich Mean Time (GMT). For example, when run in Eastern Standard Time (GMT -0500), the first line below prints "Dec 31 1998 20:00:00", while the second prints "Jan 01 1999 01:00:00".
        /// </summary>
        /// <param name="format">The format of the outputted date string. See the formatting options for the date() function.</param>
        /// <param name="timestamp">The optional timestamp parameter is an int Unix timestamp that defaults to the current local time if timestamp is omitted or null. In other words, it defaults to the value of time().</param>
        /// <returns>Returns a string formatted according to the given format string using the given timestamp or the current local time if no timestamp is given. Month and weekday names and other language dependent strings respect the current locale set with setlocale(). On failure, false is returned.</returns>
        [Obsolete("This function has been DEPRECATED as of PHP 8.1.0. Relying on this function is highly discouraged.")]
        public static string gmstrftime(string format, int? timestamp = null)
        {
            var o = timestamp == null ? DateTimeOffset.UtcNow : DateTimeOffset.FromUnixTimeSeconds(timestamp ?? 0);
            return o.ToString(format);
        }

        /// <summary>
        /// Format a local time/date as integer
        /// </summary>
        /// <param name="format"></param>
        /// <param name="timestamp"></param>
        public static long idate(string format, int? timestamp = null)
        {
            var o = timestamp == null ? DateTimeOffset.Now : DateTimeOffset.FromUnixTimeSeconds(timestamp ?? 0);
            switch (format[0])
            {
                case 'B':   // Swatch Beat / Internet Time
                    return 0; // TODO
                case 'd':   // Day of the month
                    return o.Day;
                case 'h':   // Hour(12 hour format)
                    return o.Hour > 12 ? o.Hour - 12 : o.Hour;
                case 'H':   // Hour(24 hour format)
                    return o.Hour;
                case 'i':   // Minutes
                    return o.Minute;
                case 'I':   // (uppercase i)     returns 1 if DST is activated, 0 otherwise
                    return TimeZone.CurrentTimeZone.IsDaylightSavingTime(o.LocalDateTime) ? 1 : 0;
                case 'L':   // (uppercase l)     returns 1 for leap year, 0 otherwise
                    return DateTime.IsLeapYear(o.Year) ? 1 : 0;
                case 'm':   // Month number
                    return o.Month;
                case 'N':   // ISO - 8601 day of the week(1 for Monday through 7 for Sunday)                    
                    return (int)o.DayOfWeek - (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 1;
                case 'o':   // ISO-8601 year(4 digits)
                    return o.Year;
                case 's':   // Seconds
                    return o.Second;
                case 't':   // Days in current month
                    return DateTime.DaysInMonth(o.Year, o.Month);
                case 'U':   // Seconds since the Unix Epoch -January 1 1970 00:00:00 UTC - this is the same as time()
                    return o.ToUnixTimeSeconds();
                case 'w':   // Day of the week(0 on Sunday)
                    return (long)o.DayOfWeek;
                case 'W':   // ISO-8601 week number of year, weeks starting on Monday
                    return ISOWeek.GetWeekOfYear(o.LocalDateTime);
                case 'y':   // Year(1 or 2 digits - check note below)
                    return o.Year - 2000;
                case 'Y':   // Year(4 digits)
                    return o.Year;
                case 'z':   // Day of the year
                    return o.DayOfYear;
                case 'Z':   // Timezone offset in seconds
                    return TimeZone.CurrentTimeZone.GetUtcOffset(o.LocalDateTime).Seconds;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Get the local time
        /// </summary>
        /// <param name="timestamp">The optional timestamp parameter is an int Unix timestamp that defaults to the current local time if timestamp is omitted or null. In other words, it defaults to the value of time().</param>
        /// <param name="associative">Determines whether the function should return a regular, numerically indexed array, or an associative one.</param>
        /// <returns>If associative is set to false or not supplied then the array is returned as a regular, numerically indexed array. If associative is set to true then localtime() returns an associative array containing the elements of the structure returned by the C function call to localtime. The keys of the associative array are as follows:</returns>
        public static Dictionary<string, string> localtime(int? timestamp = null, bool associative = false)
        {
            var o = timestamp == null ? DateTimeOffset.Now : DateTimeOffset.FromUnixTimeSeconds(timestamp ?? 0);
            if (associative)
            {
                return new Dictionary<string, string>(9)
                {
                    {"tm_sec"  , $"{o.Second}" },
                    {"tm_min"  , $"{o.Minute}" },
                    {"tm_hour" , $"{o.Hour}" },
                    {"tm_mday" , $"{o.Day}" },
                    {"tm_mon"  , $"{o.Month}" },
                    {"tm_year" , $"{o.Year}" },
                    {"tm_wday" , $"{o.DayOfWeek}" },
                    {"tm_yday" , $"{o.DayOfYear}" },
                    {"tm_isdst", $"{(TimeZone.CurrentTimeZone.IsDaylightSavingTime(o.LocalDateTime) ? 1 : 0)}" }
                };
            }
            else
            {
                return new Dictionary<string, string>(9)
                {
                    {"0", $"{o.Second}" },
                    {"1", $"{o.Minute}" },
                    {"2", $"{o.Hour}" },
                    {"3", $"{o.Day}" },
                    {"4", $"{o.Month}" },
                    {"5", $"{o.Year}" },
                    {"6", $"{o.DayOfWeek}" },
                    {"7", $"{o.DayOfYear}" },
                    {"8", $"{(TimeZone.CurrentTimeZone.IsDaylightSavingTime(o.LocalDateTime) ? 1 : 0)}" }
                };
            }
        }

        public static string microtime(bool as_float = false) // Return current Unix timestamp with microseconds
        {
            var o = DateTimeOffset.Now;
            return (o.ToUnixTimeMilliseconds() * 1000).ToString();
        }

        /// <summary>
        /// Get Unix timestamp for a date.
        /// Returns the Unix timestamp corresponding to the arguments given. This timestamp is a long integer containing the number of seconds between the Unix Epoch (January 1 1970 00:00:00 GMT) and the time specified.
        /// Any arguments omitted or null will be set to the current value according to the local date and time.
        /// Calling mktime() without arguments is deprecated.time() can be used to get the current timestamp.
        /// </summary>
        /// <param name="hour">The number of the hour relative to the start of the day determined by month, day and year. Negative values reference the hour before midnight of the day in question. Values greater than 23 reference the appropriate hour in the following day(s). </param>
        /// <param name="minute">The number of the minute relative to the start of the hour. Negative values reference the minute in the previous hour. Values greater than 59 reference the appropriate minute in the following hour(s). </param>
        /// <param name="second">The number of seconds relative to the start of the minute. Negative values reference the second in the previous minute. Values greater than 59 reference the appropriate second in the following minute(s). </param>
        /// <param name="month">The number of the month relative to the end of the previous year. Values 1 to 12 reference the normal calendar months of the year in question. Values less than 1 (including negative values) reference the months in the previous year in reverse order, so 0 is December, -1 is November, etc. Values greater than 12 reference the appropriate month in the following year(s). </param>
        /// <param name="day">The number of the day relative to the end of the previous month. Values 1 to 28, 29, 30 or 31 (depending upon the month) reference the normal days in the relevant month. Values less than 1 (including negative values) reference the days in the previous month, so 0 is the last day of the previous month, -1 is the day before that, etc. Values greater than the number of days in the relevant month reference the appropriate day in the following month(s). </param>
        /// <param name="year">The number of the year, may be a two or four digit value, with values between 0-69 mapping to 2000-2069 and 70-100 to 1970-2000. On systems where time_t is a 32bit signed integer, as most common today, the valid range for year is somewhere between 1901 and 2038. </param>
        /// <returns>Returns the Unix timestamp of the arguments given.</returns>
        public static long mktime(int hour, int? minute = null, int? second = null, int? month = null, int? day = null, int? year = null) 
        {
            var o = new DateTimeOffset(year ?? 0, month ?? 0, day ?? 0, hour, minute ?? 0, second ?? 0, TimeSpan.Zero);
            return o.ToUnixTimeSeconds();
        }

        /// <summary>
        /// Format a local time/date according to locale settings
        /// </summary>
        /// <param name="format"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        [Obsolete("This function has been DEPRECATED as of PHP 8.1.0. Relying on this function is highly discouraged.")]
        public static string @stringftime(string format, int? timestamp = null) 
        {
            return string.Empty; // TODO
        }

        [Obsolete("This function has been DEPRECATED as of PHP 8.1.0. Relying on this function is highly discouraged.")]
        public static Dictionary<string, string> strptime(string timestamp, string format) // Parse a time/date generated with strftime
        {
            var o = DateTimeOffset.ParseExact(timestamp, format, CultureInfo.CurrentCulture.DateTimeFormat);
            return new Dictionary<string, string>(9)
            {
                {"tm_sec"   , $"{o.Second}"},
                {"tm_min"   , $"{o.Minute}"},
                {"tm_hour"  , $"{o.Hour}"},
                {"tm_mday"  , $"{o.Day}"},
                {"tm_mon"   , $"{o.Month}"},
                {"tm_year"  , $"{o.Year}"},
                {"tm_wday"  , $"{o.DayOfWeek}"},
                {"tm_yday"  , $"{o.DayOfYear}"},
                {"unparsed" , $""}
            };
        }


        /// <summary>
        /// Parse about any English textual datetime description into a Unix timestamp
        /// </summary>
        /// <param name="datetime">A date/time string. Valid formats are explained in Date and Time Formats.</param>
        /// <param name="baseTimestamp">The timestamp which is used as a base for the calculation of relative dates.</param>
        /// <returns>Returns a timestamp on success, false otherwise.</returns>
        public static int strtotime(string datetime, int? baseTimestamp = null)
        {
            return 0; // TODO
        }

        /// <summary>
        /// Return current Unix timestamp
        /// </summary>
        /// <returns>Returns the current timestamp.</returns>
        public static long time()
        {
            var o = DateTimeOffset.Now;
            return o.ToUnixTimeSeconds();
        }

        /// <summary>
        /// Alias of DateTimeZone::listAbbreviations
        /// </summary>
        /// <returns>Returns the array of timezone abbreviations.</returns>
        public static Dictionary<string, float> timezone_abbreviations_list() => PHPDateTimeZone.listAbbreviations();

        /// <summary>
        /// Alias of DateTimeZone::listIdentifiers
        /// </summary>
        /// <param name="timezoneGroup"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static string[] timezone_identifiers_list(int timezoneGroup = (int)PHPDateTimeZones.ALL, string countryCode = null) => PHPDateTimeZone.listIdentifiers(timezoneGroup, countryCode);

        /// <summary>
        /// Alias of DateTimeZone::getLocation
        /// </summary>
        /// <returns>Array containing location information about timezone or false on failure.</returns>
        public static Dictionary<string, string> timezone_location_get() => PHPDateTimeZone.getLocation();

        /// <summary>
        /// Returns the timezone name from abbreviation
        /// </summary>
        /// <param name="abbr">Time zone abbreviation.</param>
        /// <param name="utcOffset">Offset from GMT in seconds. Defaults to -1 which means that first found time zone corresponding to abbr is returned. Otherwise exact offset is searched and only if not found then the first time zone with any offset is returned. </param>
        /// <param name="isDST">Daylight saving time indicator. Defaults to -1, which means that whether the time zone has daylight saving or not is not taken into consideration when searching. If this is set to 1, then the utcOffset is assumed to be an offset with daylight saving in effect; if 0, then utcOffset is assumed to be an offset without daylight saving in effect. If abbr doesn't exist then the time zone is searched solely by the utcOffset and isDST.</param>
        /// <returns>Returns time zone name on success or false on failure.</returns>
        public static string timezone_name_from_abbr(string abbr, int utcOffset = -1, int isDST = -1)
        {
            return PHPDateTimeZone.listAbbreviations().FirstOrDefault(a => a.Key.Equals(abbr, StringComparison.CurrentCultureIgnoreCase)).Value.ToString();
        }

        /// <summary>
        /// Alias of DateTimeZone::getName
        /// </summary>
        /// <returns>Depending on zone type, UTC offset (type 1), timezone abbreviation (type 2), and timezone identifiers as published in the IANA timezone database (type 3), the descriptor string to create a new DateTimeZone object with the same offset and/or rules. For example 02:00, CEST, or one of the timezone names in the list of timezones. </returns>
        public string timezone_name_get() => Zone.getName();

        /// <summary>
        /// Alias of DateTimeZone::getOffset
        /// </summary>
        /// <returns>Returns time zone offset in seconds.</returns>
        public int timezone_offset_get() => Zone.getOffset(this);

        /// <summary>
        /// Alias of DateTimeZone::__construct
        /// </summary>
        /// <param name="zone">One of the supported timezone names, an offset value (+0200), or a timezone abbreviation (BST).</param>
        /// <returns>Returns DateTimeZone on success. Procedural style returns false on failure.</returns>
        public PHPDateTimeZone timezone_open(string zone)
        {
            return new PHPDateTimeZone(zone);
        }
        public HashSet<Dictionary<string, string>> timezone_transitions_get(int timestampBegin = PHPConstants.PHP_INT_MIN, int timestampEnd = PHPConstants.PHP_INT_MAX) => 
            Zone.getTransitions(timestampBegin, timestampEnd); // Alias of DateTimeZone::getTransitions

        // Gets the version of the timezonedb
        public static string timezone_version_get() 
        {
            return "2022.1"; // TODO
        }
    }
}