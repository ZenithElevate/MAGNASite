<?php
/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
echo "const int INPUT_POST = ".INPUT_POST."; //     POST variables."."<br>";
echo "const int INPUT_GET = ".INPUT_GET."; //     GET variables."."<br>";
echo "const int INPUT_COOKIE = ".INPUT_COOKIE."; //     COOKIE variables."."<br>";
echo "const int INPUT_ENV = ".INPUT_ENV."; //     ENV variables."."<br>";
echo "const int INPUT_SERVER = ".INPUT_SERVER."; //     SERVER variables."."<br>";
echo "const int INPUT_SESSION = ".INPUT_SESSION."; //     SESSION variables. (not implemented yet)"."<br>";
echo "const int INPUT_REQUEST = ".INPUT_REQUEST."; //     REQUEST variables. (not implemented yet)"."<br>";
echo "const int FILTER_FLAG_NONE = ".FILTER_FLAG_NONE."; //     No flags."."<br>";
echo "const int FILTER_REQUIRE_SCALAR = ".FILTER_REQUIRE_SCALAR."; //     Flag used to require scalar as input"."<br>";
echo "const int FILTER_REQUIRE_ARRAY = ".FILTER_REQUIRE_ARRAY."; //     Require an array as input."."<br>";
echo "const int FILTER_FORCE_ARRAY = ".FILTER_FORCE_ARRAY."; //     Always returns an array."."<br>";
echo "const int FILTER_NULL_ON_FAILURE = ".FILTER_NULL_ON_FAILURE."; //     Use NULL instead of FALSE on failure."."<br>";
echo "const int FILTER_VALIDATE_INT = ".FILTER_VALIDATE_INT."; //     ID of \"int\" filter."."<br>";
echo "const int FILTER_VALIDATE_BOOL = ".FILTER_VALIDATE_BOOL."; //     Alias of FILTER_VALIDATE_BOOLEAN."."<br>";
echo "const int FILTER_VALIDATE_BOOLEAN = ".FILTER_VALIDATE_BOOLEAN."; //     ID of \"boolean\" filter."."<br>";
echo "const int FILTER_VALIDATE_FLOAT = ".FILTER_VALIDATE_FLOAT."; //     ID of \"float\" filter."."<br>";
echo "const int FILTER_VALIDATE_REGEXP = ".FILTER_VALIDATE_REGEXP."; //     ID of \"validate_regexp\" filter."."<br>";
echo "const int FILTER_VALIDATE_URL = ".FILTER_VALIDATE_URL."; //     ID of \"validate_url\" filter."."<br>";
echo "const int FILTER_VALIDATE_DOMAIN = ".FILTER_VALIDATE_DOMAIN."; //     ID of \"validate_domain\" filter. (Available as of PHP 7.0.0) "."<br>";
echo "const int FILTER_VALIDATE_EMAIL = ".FILTER_VALIDATE_EMAIL."; //     ID of \"validate_email\" filter."."<br>";
echo "const int FILTER_VALIDATE_IP = ".FILTER_VALIDATE_IP."; //     ID of \"validate_ip\" filter."."<br>";
echo "const int FILTER_VALIDATE_MAC = ".FILTER_VALIDATE_MAC."; //     ID of \"validate_mac_address\" filter."."<br>";
echo "const int FILTER_DEFAULT = ".FILTER_DEFAULT."; //     ID of default (\"unsafe_raw\") filter.This is equivalent to FILTER_UNSAFE_RAW."."<br>";
echo "const int FILTER_UNSAFE_RAW = ".FILTER_UNSAFE_RAW."; //     ID of \"unsafe_raw\" filter."."<br>";
echo "const int FILTER_SANITIZE_STRING = ".FILTER_SANITIZE_STRING."; //     ID of \"string\" filter. (Deprecated as of PHP 8.1.0, use htmlspecialchars() instead.) "."<br>";
echo "const int FILTER_SANITIZE_STRIPPED = ".FILTER_SANITIZE_STRIPPED."; //     ID of \"stripped\" filter. (Deprecated as of PHP 8.1.0, use htmlspecialchars() instead.) "."<br>";
echo "const int FILTER_SANITIZE_ENCODED = ".FILTER_SANITIZE_ENCODED."; //     ID of \"encoded\" filter."."<br>";
echo "const int FILTER_SANITIZE_SPECIAL_CHARS = ".FILTER_SANITIZE_SPECIAL_CHARS."; //     ID of \"special_chars\" filter."."<br>";
echo "const int FILTER_SANITIZE_EMAIL = ".FILTER_SANITIZE_EMAIL."; //     ID of \"email\" filter."."<br>";
echo "const int FILTER_SANITIZE_URL = ".FILTER_SANITIZE_URL."; //     ID of \"url\" filter."."<br>";
echo "const int FILTER_SANITIZE_NUMBER_INT = ".FILTER_SANITIZE_NUMBER_INT."; //     ID of \"number_int\" filter."."<br>";
echo "const int FILTER_SANITIZE_NUMBER_FLOAT = ".FILTER_SANITIZE_NUMBER_FLOAT."; //     ID of \"number_float\" filter."."<br>";
echo "const int FILTER_SANITIZE_MAGIC_QUOTES = ".FILTER_SANITIZE_MAGIC_QUOTES."; //     ID of \"magic_quotes\" filter. (DEPRECATED as of PHP 7.3.0 and REMOVED as of PHP 8.0.0, use FILTER_SANITIZE_ADD_SLASHES instead.)"."<br>";
echo "const int FILTER_SANITIZE_ADD_SLASHES = ".FILTER_SANITIZE_ADD_SLASHES."; //     ID of \"add_slashes\" filter. (Available as of PHP 7.3.0) "."<br>";
echo "const int FILTER_CALLBACK = ".FILTER_CALLBACK."; //     ID of \"callback\" filter."."<br>";
echo "const int FILTER_FLAG_ALLOW_OCTAL = ".FILTER_FLAG_ALLOW_OCTAL."; //     Allow octal notation(0[0-7]+) in \"int\" filter."."<br>";
echo "const int FILTER_FLAG_ALLOW_HEX = ".FILTER_FLAG_ALLOW_HEX."; //         Allow hex notation(0x[0-9a-fA-F]+) in \"int\" filter."."<br>";
echo "const int FILTER_FLAG_STRIP_LOW = ".FILTER_FLAG_STRIP_LOW."; //     Strip characters with ASCII value less than 32. "."<br>";
echo "const int FILTER_FLAG_STRIP_HIGH = ".FILTER_FLAG_STRIP_HIGH."; //     Strip characters with ASCII value greater than 127. "."<br>";
echo "const int FILTER_FLAG_STRIP_BACKTICK = ".FILTER_FLAG_STRIP_BACKTICK."; //     Strips backtick characters."."<br>";
echo "const int FILTER_FLAG_ENCODE_LOW = ".FILTER_FLAG_ENCODE_LOW."; //     Encode characters with ASCII value less than 32. "."<br>";
echo "const int FILTER_FLAG_ENCODE_HIGH = ".FILTER_FLAG_ENCODE_HIGH."; //     Encode characters with ASCII value greater than 127. "."<br>";
echo "const int FILTER_FLAG_ENCODE_AMP = ".FILTER_FLAG_ENCODE_AMP."; //     Encode &. "."<br>";
echo "const int FILTER_FLAG_NO_ENCODE_QUOTES = ".FILTER_FLAG_NO_ENCODE_QUOTES."; //     Don't encode \' and  = \". "."<br>";
echo "const int FILTER_FLAG_EMPTY_STRING_NULL = ".FILTER_FLAG_EMPTY_STRING_NULL."; //     (No use for now.) "."<br>";
echo "const int FILTER_FLAG_ALLOW_FRACTION = ".FILTER_FLAG_ALLOW_FRACTION."; //     Allow fractional part in \"number_float\" filter."."<br>";
echo "const int FILTER_FLAG_ALLOW_THOUSAND = ".FILTER_FLAG_ALLOW_THOUSAND."; //     Allow thousand separator(,) in \"number_float\" filter."."<br>";
echo "const int FILTER_FLAG_ALLOW_SCIENTIFIC = ".FILTER_FLAG_ALLOW_SCIENTIFIC."; //     Allow scientific notation(e, E) in \"number_float\" filter."."<br>";
echo "const int FILTER_FLAG_PATH_REQUIRED = ".FILTER_FLAG_PATH_REQUIRED."; //     Require path in \"validate_url\" filter."."<br>";
echo "const int FILTER_FLAG_QUERY_REQUIRED = ".FILTER_FLAG_QUERY_REQUIRED."; //     Require query in \"validate_url\" filter."."<br>";
echo "const int FILTER_FLAG_SCHEME_REQUIRED = ".FILTER_FLAG_SCHEME_REQUIRED."; //     Require scheme in \"validate_url\" filter. (Deprecated as of PHP 7.3.0 and removed as of PHP 8.0.0, as it is implied in the filter already.) "."<br>";
echo "const int FILTER_FLAG_HOST_REQUIRED = ".FILTER_FLAG_HOST_REQUIRED."; //     Require host in \"validate_url\" filter. (Deprecated as of PHP 7.3.0 and removed as of PHP 8.0.0, as it is implied in the filter already.) "."<br>";
echo "const int FILTER_FLAG_HOSTNAME = ".FILTER_FLAG_HOSTNAME."; //     Require hostnames to start with an alphanumeric character and contain only alphanumerics or hyphens. (Available as of PHP 7.0.0)"."<br>";
echo "const int FILTER_FLAG_IPV4 = ".FILTER_FLAG_IPV4."; //     Allow only IPv4 address in \"validate_ip\" filter."."<br>";
echo "const int FILTER_FLAG_IPV6 = ".FILTER_FLAG_IPV6."; //     Allow only IPv6 address in \"validate_ip\" filter."."<br>";
echo "const int FILTER_FLAG_NO_RES_RANGE = ".FILTER_FLAG_NO_RES_RANGE."; //     Deny reserved addresses in \"validate_ip\" filter."."<br>";
echo "const int FILTER_FLAG_NO_PRIV_RANGE = ".FILTER_FLAG_NO_PRIV_RANGE."; //     Deny private addresses in \"validate_ip\" filter."."<br>";
echo "const int FILTER_FLAG_EMAIL_UNICODE = ".FILTER_FLAG_EMAIL_UNICODE."; //     Accepts Unicode characters in the local part in \"validate_email\" filter. (Available as of PHP 7.1.0) "."<br>";
?>