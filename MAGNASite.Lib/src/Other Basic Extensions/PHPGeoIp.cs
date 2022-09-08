/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */

namespace MAGNASite.Lib
{
    /// <summary>
    /// GEO IP
    /// </summary>
    internal class PHPGeoIp
    {
        public static void geoip_asnum_by_name() { } //Get the Autonomous System Numbers (ASN)
        public static void geoip_continent_code_by_name() { } //Get the two letter continent code
        public static void geoip_country_code_by_name() { } //Get the two letter country code
        public static void geoip_country_code3_by_name() { } //Get the three letter country code
        public static void geoip_country_name_by_name() { } //Get the full country name
        public static void geoip_database_info() { } //Get GeoIP Database information
        public static void geoip_db_avail() { } //Determine if GeoIP Database is available
        public static void geoip_db_filename() { } //Returns the filename of the corresponding GeoIP Database
        public static void geoip_db_get_all_info() { } //Returns detailed information about all GeoIP database types
        public static void geoip_domain_by_name() { } //Get the second level domain name
        public static void geoip_id_by_name() { } //Get the Internet connection type
        public static void geoip_isp_by_name() { } //Get the Internet Service Provider (ISP) name
        public static void geoip_netspeedcell_by_name() { } //Get the Internet connection speed
        public static void geoip_org_by_name() { } //Get the organization name
        public static void geoip_record_by_name() { } //Returns the detailed City information found in the GeoIP Database
        public static void geoip_region_by_name() { } //Get the country code and region
        public static void geoip_region_name_by_code() { } //Returns the region name for some country and region code combo
        public static void geoip_setup_custom_directory() { } //Set a custom directory for the GeoIP database
        public static void geoip_time_zone_by_country_and_region() { } //Returns the time zone for some country and region code combo
    }
}