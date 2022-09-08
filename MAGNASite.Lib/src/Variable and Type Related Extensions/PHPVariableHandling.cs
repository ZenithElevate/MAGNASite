/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using MAGNASite.Lib.Properties;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace MAGNASite.Lib
{
    /// <summary>
    /// PHP variable handling
    /// </summary>
    internal class PHPVariableHandling : ErrorAwareBaseClass
    {
        /// <summary>
        /// Get the boolean value of a variable
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool boolval(object value) => Convert.ToBoolean(value);

        /// <summary>
        /// Dumps a string representation of an internal zval structure to output
        /// </summary>
        /// <returns></returns>
        public static string debug_zval_dump(params object[] list) => var_dump(list);

        /// <summary>
        /// Alias of floatval
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double doubleval(object value) => floatval(value);

        /// <summary>
        /// Determine whether a variable is empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool empty(object value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is string || string.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get float value of a variable
        /// </summary>
        /// <param name="value">May be any scalar type. floatval() should not be used on objects, as doing so will emit an E_NOTICE level error and return 1.</param>
        /// <returns>The float value of the given variable. Empty arrays return 0, non-empty arrays return 1.</returns>
        public static double floatval (object value)
        {
            if (value is Array)
            {
                var a = value as Array;
                return a == null ? 0 : a.Length == 0 ? 0 : 1;
            }

            if (value is IEnumerable)
            {
                var e = value as IEnumerable<object>;
                return e == null ? 0 : e.Any() ? 1 : 0;
            }

            try
            {
                return Convert.ToDouble(value);
            }
            catch (Exception)
            {
                //AddError(Resources.rsErrorInvalidDoubleValue); // TODO
                return double.NaN;
            }
        }

        /// <summary>
        /// Gets the type name of a variable in a way that is suitable for debugging
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string get_debug_type (object value, [CallerArgumentExpression("value")] string valueName = null)
        {
            return valueName;
        }

        /// <summary>
        /// Returns an array of all defined variables
        /// </summary>
        /// <returns>A multidimensional array with all the variables.</returns>
        public static object[] get_defined_vars ()
        {
            return Array.Empty<object>();
        }

        /// <summary>
        /// Returns an integer identifier for the given resource. 
        /// This function is essentially an int cast of resource to make it easier to retrieve the resource ID.
        /// </summary>
        /// <param name="resource">The evaluated resource handle.</param>
        /// <returns>The int identifier for the given resource.</returns>
        public static int get_resource_id (PHPResource resource)
        {
            return 0;
        }

        /// <summary>
        /// Returns the resource type. 
        /// This function will return null and generate an error if resource is not a resource. 
        /// </summary>
        /// <param name="resource">The evaluated resource handle.</param>
        /// <returns>If the given resource is a resource, this function will return a string representing its type. If the type is not identified by this function, the return value will be the string Unknown. </returns>
        public static string get_resource_type (PHPResource resource)
        {
            return string.Empty;
        }

        /// <summary>
        /// Get the type of a variable
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string gettype(object value)
        {
            if (value is bool) return "boolean";
            if (value is int || value is short || value is long || value is uint || value is ushort || value is ulong) return "integer";
            if (value is double || value is float) return "double"; // (for historical reasons "double" is returned in case of a float, and not simply "float")
            if (value is string) return "string";
            if (value is Array || value is IEnumerable) return "array";
            if (value is PHPResource) return "resource"; //"resource (closed)" as of PHP 7.2.0
            if (value is object || value != null) return "object";
            if (value == null) return "NULL";
            return "unknown type";
        }

        /// <summary>
        /// Get the integer value of a variable
        /// </summary>
        /// <param name="value"></param>
        /// <param name="intbase"></param>
        /// <returns></returns>
        public static int intval (object value, int intbase = 10)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                if (value is string)
                {
                    if (value.ToString().StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    {
                        return Convert.ToInt32(value.ToString(), 16);
                    }
                    else
                    {
                        if (value.ToString().StartsWith("0"))
                        {
                            return Convert.ToInt32(value.ToString(), 8);
                        }
                    }

                    return Convert.ToInt32(value.ToString(), intbase);
                }
                else
                {
                    return Convert.ToInt32(value);
                }
            }
        }

        /// <summary>
        /// Finds whether a variable is an array
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is an array, false otherwise.</returns>
        public static bool is_array(object value)
        {
            return value is Array || value is IEnumerable;
        }

        /// <summary>
        /// Verify that a value can be called as a function from the current scope.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="syntax_only">If set to true the function only verifies that value might be a function or method. It will only reject simple variables that are not strings, or an array that does not have a valid structure to be used as a callback. The valid ones are supposed to have only 2 entries, the first of which is an object or a string, and the second a string.</param>
        /// <param name="callable_name">Receives the "callable name". In the example below it is "someClass::someMethod". Note, however, that despite the implication that someClass::SomeMethod() is a callable static method, this is not the case.</param>
        /// <returns>Returns true if value is callable, false otherwise.</returns>
        public static bool is_callable (object value, bool syntax_only = false, string callable_name = null)
        {
            return true;
        }

        /// <summary>
        /// Verify that the contents of a variable is a countable value
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>Returns true if value is countable, false otherwise. </returns>
        public static bool is_countable (object value)
        {
            return value is Array || value is IEnumerable;
        }

        /// <summary>
        /// Finds whether the type of the given variable is integer. 
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is an int, false otherwise.</returns>
        public static bool is_int(object value)
        {
            if (value is int || value is uint)
            {
                return true;
            }

            if (value is string)
            {
                if (int.TryParse(value as string, out _))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Alias of is_int
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is an int, false otherwise.</returns>
        public static bool is_integer(object value) => is_int(value);

        /// <summary>
        /// Alias of is_int
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is an int, false otherwise.</returns>
        public static bool is_long(object value)
        {
            if (value is int || value is uint || value is long || value is ulong)
            {
                return true;
            }

            if (value is string)
            {
                if (int.TryParse(value as string, out _) || uint.TryParse(value as string, out _) || long.TryParse(value as string, out _) || ulong.TryParse(value as string, out _))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Finds out whether a variable is a boolean
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is a boolean, false otherwise.</returns>
        public static bool is_bool(object value)
        {
            if (value is bool)
            {
                return true;
            }

            if (value is string)
            {
                if (bool.TryParse(value as string, out _))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Finds whether the type of a variable is float
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is a float, false otherwise.</returns>
        public static bool is_float(object value)
        {
            if (value is float)
            {
                return true;
            }

            if (value is string)
            {
                if (float.TryParse(value as string, out _))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///  Alias of is_float
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is a float, false otherwise.</returns>
        public static bool is_double(object value) => is_float(value);

        /// <summary>
        /// Finds whether a variable is null
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is null, false otherwise.</returns>
        public static bool is_null(object value)
        {
            return value is null;
        }

        /// <summary>
        /// Finds whether a variable is a number or a numeric string
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is number or a numeric string, false otherwise.</returns>
        public static bool is_numeric(object value) 
        {
            if (value is int || value is float || value is double || value is long || value is ulong || value is uint || value is short || value is ushort)
            {
                return true;
            }

            if (value is string || value is char)
            {
                if (double.TryParse(value.ToString(), out _))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Alias of is_float
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is a float, false otherwise.</returns>
        public static bool is_real(object value) => is_float(value);

        /// <summary>
        ///  Finds whether a variable is a resource
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool is_resource(object value)
        {
            return true;
        }

        /// <summary>
        /// Finds whether a variable is a scalar
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool is_scalar(object value)
        {
            if (value is Array || value is IEnumerable)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Find whether the type of a variable is string
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is a string, false otherwise.</returns>
        public static bool is_string(object value) 
        {
            return value is string;
        }

        /// <summary>
        /// Finds whether a variable is an object
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is an object, false otherwise.</returns>
        public static bool is_object(object value) 
        {
            return value is object;
        }

        /// <summary>
        /// Determine if a variable is declared and is different than null
        /// </summary>
        /// <param name="value">The variable being evaluated.</param>
        /// <returns>Returns true if value is not null, false otherwise.</returns>
        public static bool isset(object value) => !is_null(value);

        /// <summary>
        ///  Prints human-readable information about a variable
        /// </summary>
        /// <param name="value"></param>
        /// <param name="toreturn"></param>
        /// <returns></returns>
        public static string print_r(object value, bool toreturn = false)
        {
            return $@"<pre>{value}</pre>";
        }

        /// <summary>
        ///  Generates a storable representation of a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string serialize(object value)
        {
            return string.Empty;
        }

        /// <summary>
        ///  Set the type of a variable
        /// </summary>
        /// <param name="value">The variable being converted.</param>
        /// <param name="type"> Possibles values of type are: "boolean" or "bool", "integer" or "int", "float" or "double", "string", "array", "object", "null"</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool settype(ref object value, string type)
        {
            try
            {
                switch (type.ToLowerInvariant())
                {
                    case "boolean":
                    case "bool":
                        value = Convert.ToBoolean(value);
                        break;
                    case "integer":
                    case "int":
                        value = Convert.ToInt32(value);
                        break;
                    case "float":
                    case "double":
                        value = Convert.ToDouble(value);
                        break;
                    case "string":
                        value = value.ToString();
                        break;
                    case "array":
                        value = (value as IEnumerable<object>).ToArray();
                        break;
                    case "null":
                        value = null;
                        break;
                    case "object":
                    default:
                        return true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get string value of a variable
        /// </summary>
        /// <param name="value">The variable that is being converted to a string.</param>
        /// <returns>The string value of value.</returns>
        public static string strval(object value)
        {
            return value.ToString();
        }

        public static object unserialize(string data, Type[] options) // Creates a PHP value from a stored representation
        {
            return new object();
        }
        public static bool unset(ref object value) // Unset a given variable
        {
            value = null;
            return true;
        }

        /// <summary>
        /// Dumps information about a variable
        /// </summary>
        /// <param name="list">The expressions to dump.</param>
        /// <returns>Structured information about expressions.</returns>
        public static string var_dump(params object[] list)
        {
            var sb = new StringBuilder();
            foreach (var o in list)
            {
                sb.AppendLine(print_r(o));
            }
            return sb.ToString();
        }

        public static string var_export(object value, bool toreturn = false) // Outputs or returns a parsable string representation of a variable
        {
            return print_r(value);
        }
    }
}
