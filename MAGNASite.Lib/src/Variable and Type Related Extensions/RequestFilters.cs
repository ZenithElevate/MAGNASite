/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    //[Flags]
    internal enum RequestFilters
    {
        FILTER_FLAG_NONE = 0, // No flags.
        FILTER_REQUIRE_SCALAR = 33554432, // Flag used to require scalar as input
        FILTER_REQUIRE_ARRAY = 16777216, // Require an array as input.
        FILTER_FORCE_ARRAY = 67108864, // Always returns an array.
        FILTER_NULL_ON_FAILURE = 134217728, // Use NULL instead of FALSE on failure.
        FILTER_VALIDATE_INT = 257, // ID of "int" filter.
        FILTER_VALIDATE_BOOLEAN = 258, // ID of "boolean" filter.
        FILTER_VALIDATE_BOOL = FILTER_VALIDATE_BOOLEAN, // Alias of FILTER_VALIDATE_BOOLEAN.
        FILTER_VALIDATE_FLOAT = 259, // ID of "float" filter.
        FILTER_VALIDATE_REGEXP = 272, // ID of "validate_regexp" filter.
        FILTER_VALIDATE_URL = 273, // ID of "validate_url" filter.
        FILTER_VALIDATE_DOMAIN = 277, // ID of "validate_domain" filter. (Available as of PHP 7.0.0)
        FILTER_VALIDATE_EMAIL = 274, // ID of "validate_email" filter.
        FILTER_VALIDATE_IP = 275, // ID of "validate_ip" filter.
        FILTER_VALIDATE_MAC = 276, // ID of "validate_mac_address" filter.
        FILTER_DEFAULT = 516, // ID of default ("unsafe_raw") filter.This is equivalent to FILTER_UNSAFE_RAW.
        FILTER_UNSAFE_RAW = FILTER_DEFAULT, // ID of "unsafe_raw" filter.
        FILTER_SANITIZE_STRING = 513, // ID of "string" filter. (Deprecated as of PHP 8.1.0, use htmlspecialchars() instead.)
        FILTER_SANITIZE_STRIPPED = FILTER_SANITIZE_STRING, // ID of "stripped" filter. (Deprecated as of PHP 8.1.0, use htmlspecialchars() instead.)
        FILTER_SANITIZE_ENCODED = 514, // ID of "encoded" filter.
        FILTER_SANITIZE_SPECIAL_CHARS = 515, // ID of "special_chars" filter.
        FILTER_SANITIZE_EMAIL = 517, // ID of "email" filter.
        FILTER_SANITIZE_URL = 518, // ID of "url" filter.
        FILTER_SANITIZE_NUMBER_INT = 519, // ID of "number_int" filter.
        FILTER_SANITIZE_NUMBER_FLOAT = 520, // ID of "number_float" filter.
        FILTER_SANITIZE_MAGIC_QUOTES = 521, // ID of "magic_quotes" filter. (DEPRECATED as of PHP 7.3.0 and REMOVED as of PHP 8.0.0, use FILTER_SANITIZE_ADD_SLASHES instead.)
        FILTER_SANITIZE_ADD_SLASHES = 523, // ID of "add_slashes" filter. (Available as of PHP 7.3.0)
        FILTER_CALLBACK = 1024, // ID of "callback" filter.
        FILTER_FLAG_ALLOW_OCTAL = 1, // Allow octal notation(0[0-7]+) in "int" filter.
        FILTER_FLAG_ALLOW_HEX = 2, // Allow hex notation(0x[0-9a-fA-F]+) in "int" filter.
        FILTER_FLAG_STRIP_LOW = 4, // Strip characters with ASCII value less than 32.
        FILTER_FLAG_STRIP_HIGH = 8, // Strip characters with ASCII value greater than 127.
        FILTER_FLAG_STRIP_BACKTICK = 512, // Strips backtick characters.
        FILTER_FLAG_ENCODE_LOW = 16, // Encode characters with ASCII value less than 32.
        FILTER_FLAG_ENCODE_HIGH = 32, // Encode characters with ASCII value greater than 127.
        FILTER_FLAG_ENCODE_AMP = 64, // Encode &.
        FILTER_FLAG_NO_ENCODE_QUOTES = 128, // Don't encode \' and = ".
        FILTER_FLAG_EMPTY_STRING_NULL = 256, // (No use for now.)
        FILTER_FLAG_ALLOW_FRACTION = 4096, // Allow fractional part in "number_float" filter.
        FILTER_FLAG_ALLOW_THOUSAND = 8192, // Allow thousand separator(,) in "number_float" filter.
        FILTER_FLAG_ALLOW_SCIENTIFIC = 16384, // Allow scientific notation(e, E) in "number_float" filter.
        FILTER_FLAG_PATH_REQUIRED = 262144, // Require path in "validate_url" filter.
        FILTER_FLAG_QUERY_REQUIRED = 524288, // Require query in "validate_url" filter.
        FILTER_FLAG_SCHEME_REQUIRED = 65536, // Require scheme in "validate_url" filter. (Deprecated as of PHP 7.3.0 and removed as of PHP 8.0.0, as it is implied in the filter already.)
        FILTER_FLAG_HOST_REQUIRED = 131072, // Require host in "validate_url" filter. (Deprecated as of PHP 7.3.0 and removed as of PHP 8.0.0, as it is implied in the filter already.)
        FILTER_FLAG_HOSTNAME = 1048576, // Require hostnames to start with an alphanumeric character and contain only alphanumerics or hyphens. (Available as of PHP 7.0.0)
        FILTER_FLAG_IPV4 = 1048576, // Allow only IPv4 address in "validate_ip" filter.
        FILTER_FLAG_IPV6 = 2097152, // Allow only IPv6 address in "validate_ip" filter.
        FILTER_FLAG_NO_RES_RANGE = 4194304, // Deny reserved addresses in "validate_ip" filter.
        FILTER_FLAG_NO_PRIV_RANGE = 8388608 // Deny private addresses in "validate_ip" filter.
        //FILTER_FLAG_EMAIL_UNICODE = FILTER_FLAG_EMAIL_UNICODE, // Accepts Unicode characters in the local part in "validate_email" filter. (Available as of PHP 7.1.0)
    }
}
