/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using Microsoft.AspNetCore.Http;
using System.Net.Mail;

namespace MAGNASite.Lib
{
    /// <summary>
    /// Regular expressions
    /// </summary>
    internal class PHPFilter
    {
        delegate string FilterFunc(string value);

        /// <summary>
        /// Full list of filters
        /// </summary>
        readonly static Dictionary<RequestFilters, FilterFunc> Filters = new Dictionary<RequestFilters, FilterFunc>()
        {
            {(RequestFilters)257, new FilterFunc(_int               )},
            {(RequestFilters)258, new FilterFunc(_boolean           )},
            {(RequestFilters)259, new FilterFunc(_float             )},
            {(RequestFilters)272, new FilterFunc(_validate_regexp   )},
            {(RequestFilters)277, new FilterFunc(_validate_domain   )},
            {(RequestFilters)273, new FilterFunc(_validate_url      )},
            {(RequestFilters)274, new FilterFunc(_validate_email    )},
            {(RequestFilters)275, new FilterFunc(_validate_ip       )},
            {(RequestFilters)276, new FilterFunc(_validate_mac      )},
            //{(RequestFilters)513, new FilterFunc(_string            )}, // TODO
            {(RequestFilters)513, new FilterFunc(_stripped          )},
            {(RequestFilters)514, new FilterFunc(_encoded           )},
            {(RequestFilters)515, new FilterFunc(_special_chars     )},
            {(RequestFilters)522, new FilterFunc(_full_special_chars)},
            {(RequestFilters)516, new FilterFunc(_unsafe_raw        )},
            {(RequestFilters)517, new FilterFunc(_email             )},
            {(RequestFilters)518, new FilterFunc(_url               )},
            {(RequestFilters)519, new FilterFunc(_number_int        )},
            {(RequestFilters)520, new FilterFunc(_number_float      )},
            {(RequestFilters)521, new FilterFunc(_magic_quotes      )},
            {(RequestFilters)1024,new FilterFunc(_callback          )}
        };

        readonly static int maxInt = int.MaxValue.ToString().Length;
        readonly static int maxFloat = float.MaxValue.ToString().Length;

        /// <summary>
        /// Filters an integer
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _int(string value)
        {
            for (int i = 0; i < Math.Min(value.Length, maxInt); i++)
            {
                if (value[i] < '0' || value[i] > '9')
                {
                    return string.Empty;
                }
            }

            return value; 
        }

        /// <summary>
        /// Filters a boolean
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _boolean(string value)
        {
            return value[0] == '0' || value[0] == '1' || value.ToUpperInvariant().Equals("TRUE") || value.ToUpperInvariant().Equals("FALSE") ?
                value : 
                null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _float(string value) 
        {
            if (value.Length <= maxFloat)
            {
                if (float.TryParse(value, out _))
                {
                    return value;
                }
            }

            return null; 
        }

        static string _validate_regexp(string value) { return value; }
        static string _validate_domain(string value) { return value; }
        static string _validate_url(string value) { return value; }
        static string _validate_email(string value) { return value; }
        static string _validate_ip(string value) { return value; }
        static string _validate_mac(string value) { return value; }
        static string _string(string value) { return value; }
        static string _stripped(string value) { return value; }

        /// <summary>
        /// Filters HTML-encoded
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _encoded(string value) 
        {
            return value.Contains('<') || value.Contains('>') ?
                null :
                value;
        }

        static string _special_chars(string value) { return value; }
        static string _full_special_chars(string value) { return value; }

        /// <summary>
        /// Unfiltered
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _unsafe_raw(string value) 
        {
            return value; 
        }

        /// <summary>
        /// Filters an email
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _email(string value)
        {
            if (value.Length <= 320) // TODO: 253 or 320?
            {
                return MailAddress.TryCreate(value, null, out _) ? value : null;                                    
            }

            return null;
        }

        /// <summary>
        /// Filters a URL
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _url(string value) 
        {
            if(value.Length <= 2000) // TODO
            {
                if (Uri.TryCreate(value, new UriCreationOptions() { DangerousDisablePathAndQueryCanonicalization = true }, out Uri uri))
                {                    
                    return uri.Query.Split(';').Any(param => param.Length > 512) ? null : value; // TODO
                }
            }

            return null; 
        }

        /// <summary>
        /// Filters integer number
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _number_int(string value) 
        {
            if (value.Trim().Length <= int.MaxValue.ToString().Length)
            {
                if (int.TryParse(value, out _))
                {
                    return value;
                }
            }

            return null;
        }

        /// <summary>
        /// Filters float number
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The filtered value.</returns>
        static string _number_float(string value)
        {
            if (value.Trim().Length <= float.MaxValue.ToString().Length)
            {
                if (float.TryParse(value, out _))
                {
                    return value;
                }
            }

            return null;
        }

        static string _magic_quotes(string value) { return value; }
        static string _callback(string value) { return value; }

        readonly HttpRequest Request;

        public PHPFilter(HttpRequest request)
        {
            Request = request;
        }

        /// <summary>
        /// Checks if variable of specified type exists
        /// </summary>
        /// <param name="input_type">One of INPUT_GET, INPUT_POST, INPUT_COOKIE, INPUT_SERVER, or INPUT_ENV.</param>
        /// <param name="var_name">Name of a variable to check.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public bool filter_has_var(int input_type, string var_name)
        {
            switch ((RequestInputTypes)input_type)
            {
                case RequestInputTypes.INPUT_GET:
                    return Request.Query.Keys.Contains(var_name);
                case RequestInputTypes.INPUT_POST:
                    return Request.Form.Keys.Contains(var_name);
                case RequestInputTypes.INPUT_COOKIE:
                    return Request.Cookies.Keys.Contains(var_name);
                case RequestInputTypes.INPUT_SERVER:
                    //switch (var_name.ToUpperInvariant())
                    //{
                    //    case "IP":
                    //        return Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
                    //}
                    return false; // TODO
                case RequestInputTypes.INPUT_ENV:
                    return Environment.GetEnvironmentVariables().Contains(var_name);
                default:
                    return false;
            }
        }

        public (int,bool) filter_id(string name)
        {
            KeyValuePair<RequestFilters, FilterFunc>? filter = Filters.FirstOrDefault(f => f.Value.Method.Name.EndsWith(name));
            return filter.HasValue ? (0, false) : ((int)filter.Value.Key, true); // TODO
        } // Returns the filter ID belonging to a named filter

        public bool filter_input_array()
        {
            return false; // TODO
        } // Gets external variables and optionally filters them

        /// <summary>
        /// Gets a specific external variable by name and optionally filters it
        /// </summary>
        /// <param name="type">One of INPUT_GET, INPUT_POST, INPUT_COOKIE, INPUT_SERVER, or INPUT_ENV.</param>
        /// <param name="var_name">Name of a variable to get.</param>
        /// <param name="filter">The ID of the filter to apply.The Types of filters manual page lists the available filters.<br/>
        /// If omitted, FILTER_DEFAULT will be used, which is equivalent to FILTER_UNSAFE_RAW.This will result in no filtering taking place by default.</param>
        /// <param name="options">Associative array of options or bitwise disjunction of flags. If filter accepts options, flags can be provided in "flags" field of array.</param>
        /// <returns>Value of the requested variable on success, false if the filter fails, or null if the var_name variable is not set. If the flag FILTER_NULL_ON_FAILURE is used, it returns false if the variable is not set and null if the filter fails.</returns>
        public string filter_input(int type, string var_name, int filter = (int)RequestFilters.FILTER_DEFAULT, Dictionary<string, object> options = null)
        {
            switch ((RequestInputTypes)type)
            {
                case RequestInputTypes.INPUT_GET:
                    return Request.Query.Keys.FirstOrDefault(var_name);
                case RequestInputTypes.INPUT_POST:
                    return Request.Form.Keys.FirstOrDefault(var_name);
                case RequestInputTypes.INPUT_COOKIE:
                    return Request.Cookies.Keys.FirstOrDefault(var_name);
                case RequestInputTypes.INPUT_SERVER:
                    //switch (var_name.ToUpperInvariant())
                    //{
                    //    case "IP":
                    //        return Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
                    //}
                    return string.Empty; // TODO
                case RequestInputTypes.INPUT_ENV:
                    return Environment.GetEnvironmentVariables()[var_name].ToString();
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// Returns a list of all supported filters
        /// </summary>
        /// <returns>Returns an array of names of all supported filters, empty array if there are no such filters. Indexes of this array are not filter IDs, they can be obtained with filter_id() from a name instead.</returns>
        public Dictionary<string, string> filter_list()
        {
            return Filters.Select((f, i) => new { Key = i.ToString(), Value = f.Value.Method.Name }).ToDictionary(x => x.Key, x => x.Value);
        }

        public bool filter_var_array()
        {
            return false; // TODO
        } // Gets multiple variables and optionally filters them

        public bool filter_var()
        {
            return false; // TODO
        } // Filters a variable with a specified filter
    }
}
