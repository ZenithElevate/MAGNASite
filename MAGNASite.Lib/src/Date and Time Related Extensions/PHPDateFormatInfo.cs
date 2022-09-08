/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Runtime.Serialization;

namespace MAGNASite.Lib
{
    [CollectionDataContract(ItemName = "warning")]
    internal sealed class Warnings : List<string> { }

    [CollectionDataContract(ItemName = "error")]
    internal sealed class Errors : List<string> { }

    /// <summary>
    /// Associative array with detailed info about given date/time. 
    /// </summary>
    [Serializable]
    [DataContract(Name ="Array")]
    internal struct PHPDateFormatInfo
    {
        [DataMember(Order = 0)]
        readonly public int year;
        [DataMember(Order = 1)]
        readonly public int month;
        [DataMember(Order = 2)]
        readonly public int day;
        [DataMember(Order = 3)]
        readonly public int hour;
        [DataMember(Order = 4)]
        readonly public int minute;
        [DataMember(Order = 5)]
        readonly public int second;
        [DataMember(Order = 6)]
        readonly public float fraction;
        [DataMember(Order = 7)]
        readonly public Warnings warnings;
        [DataMember(Order = 8)]
        readonly public Errors errors;
        [DataMember(Order = 9)]
        readonly public int is_localtime;
        [DataMember(Order = 10)]
        readonly public int zone_type;
        [DataMember(Order = 11)]
        readonly public int zone;
        [DataMember(Order = 12)]
        readonly public bool is_dst;
        [DataMember(Order = 13)]
        public int warning_count => warnings.Count();
        [DataMember(Order = 14)]
        public int error_count => errors.Count();

        public PHPDateFormatInfo(int year, int month, int day, int hour, int minute, int second, float fraction, 
            Warnings warnings, Errors errors, 
            int is_localtime, int zone_type, int zone, bool is_dst)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.minute= minute;
            this.second = second;
            this.fraction = fraction;
            this.warnings = warnings;
            this.errors = errors;
            this.is_localtime = is_localtime;
            this.zone_type = zone_type;
            this.zone = zone;
            this.is_dst = is_dst;
        }

        public string Serialize()
        {
            return string.Empty; // TODO
        }
    }
}
