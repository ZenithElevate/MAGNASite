/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Collections.Concurrent;

namespace MAGNASite.Lib
{
    /// <summary>
    /// PHP arrays
    /// </summary>
    internal partial class PHPArrays
    {
        /// <summary>
        /// Changes the case of all keys in an array
        /// </summary>
        /// <param name="array">The array to work on.</param>
        /// <param name="_case">Either CASE_UPPER or CASE_LOWER (default).</param>
        /// <returns>Returns an array with its keys lower or uppercased, or null if array is not an array.</returns>
        public static Dictionary<string, object> array_change_key_case(Dictionary<string, object> array, int _case = (int)ArrayCaseOps.CASE_LOWER)
        {
            if (array != null)
            {
                var result = new Dictionary<string, object>(array.Count);
                foreach (var k in array)
                {
                    var key = _case == (int)ArrayCaseOps.CASE_LOWER ? k.Key.ToLowerInvariant() : k.Key.ToUpperInvariant();
                    result.Add(key, k.Value);
                }
                return result;
            }

            return null;
        }

        /// <summary>
        /// Split an array into chunks
        /// </summary>
        /// <param name="array">The array to work on.</param>
        /// <param name="length">The size of each chunk.</param>
        /// <param name="preserve_keys">When set to true keys will be preserved. Default is false which will reindex the chunk numerically.</param>
        /// <returns>Returns a multidimensional numerically indexed array, starting with zero, with each dimension containing length elements.</returns>
        public static Dictionary<string, object> array_chunk(Dictionary<string, object> array, int length, bool preserve_keys = false)
        {
            if (array != null && array.Any())
            {
                var i = 0;
                IEnumerable<KeyValuePair<string, object>> slice;
                var result = new Dictionary<string, object>(array.Count / length + 1);
                do
                {
                    slice = array.Skip(i).Take(length);
                    if (slice.Any())
                    {
                        var j = 0;
                        result.Add(i.ToString(), slice.Select(i => new KeyValuePair<string, object>(preserve_keys == false ? j++.ToString() : i.Key, i.Value)));
                    }
                    i += length;
                } while (slice.Any());

                return result;
            }

            return null;
        }

        /// <summary>
        /// Return the values from a single column in the input array
        /// </summary>
        /// <param name="array">A multi-dimensional array or an array of objects from which to pull a column of values from. If an array of objects is provided, then public properties can be directly pulled. In order for protected or private properties to be pulled, the class must implement both the __get() and __isset() magic methods.</param>
        /// <param name="column_key">The column of values to return. This value may be an integer key of the column you wish to retrieve, or it may be a string key name for an associative array or property name. It may also be null to return complete arrays or objects (this is useful together with index_key to reindex the array).</param>
        /// <param name="index_key">The column to use as the index/keys for the returned array. This value may be the integer key of the column, or it may be the string key name. The value is cast as usual for array keys (however, prior to PHP 8.0.0, objects supporting conversion to string were also allowed).</param>
        /// <returns>Returns an array of values representing a single column from the input array.</returns>
        public static Dictionary<string, object> array_column(Dictionary<string, object> array, string column_key, string index_key = null)
        {
            if (array != null && array.Any())
            {
                var i = 0;
                var result = new Dictionary<string, object>(array.Count);
                foreach (var item in array)
                {
                    if (item.Value is Dictionary<string, string>)
                    {
                        var value = item.Value as Dictionary<string, string>;
                        var key = value[index_key];
                        result.Add(index_key == null ? i.ToString() : key, value[column_key]);
                    }
                    //else if (item.Value is string)
                    //{
                    //}
                }
            }

            return null;
        }

        /// <summary>
        /// Return the values from a single column in the input array
        /// </summary>
        /// <param name="array">A multi-dimensional array or an array of objects from which to pull a column of values from. If an array of objects is provided, then public properties can be directly pulled. In order for protected or private properties to be pulled, the class must implement both the __get() and __isset() magic methods.</param>
        /// <param name="column_key">The column of values to return. This value may be an integer key of the column you wish to retrieve, or it may be a string key name for an associative array or property name. It may also be null to return complete arrays or objects (this is useful together with index_key to reindex the array).</param>
        /// <param name="index_key">The column to use as the index/keys for the returned array. This value may be the integer key of the column, or it may be the string key name. The value is cast as usual for array keys (however, prior to PHP 8.0.0, objects supporting conversion to string were also allowed).</param>
        /// <returns>Returns an array of values representing a single column from the input array.</returns>
        public static Dictionary<string, object> array_column(Dictionary<string, object> array, string column_key, int? index_key = null)
        {
            if (array != null && array.Any())
            {
                var i = 0;
                var result = new Dictionary<string, object>(array.Count);
                foreach (var item in array)
                {
                    if (item.Value is Dictionary<string, string>)
                    {
                        var value = item.Value as Dictionary<string, string>;
                        var key = value.Skip(index_key ?? 0).FirstOrDefault().Key;
                        result.Add(index_key == null ? i.ToString() : key, value[column_key]);
                    }
                    //else if (item.Value is string)
                    //{
                    //}
                }
            }

            return null;
        }

        /// <summary>
        /// Return the values from a single column in the input array
        /// </summary>
        /// <param name="array">A multi-dimensional array or an array of objects from which to pull a column of values from. If an array of objects is provided, then public properties can be directly pulled. In order for protected or private properties to be pulled, the class must implement both the __get() and __isset() magic methods.</param>
        /// <param name="column_key">The column of values to return. This value may be an integer key of the column you wish to retrieve, or it may be a string key name for an associative array or property name. It may also be null to return complete arrays or objects (this is useful together with index_key to reindex the array).</param>
        /// <param name="index_key">The column to use as the index/keys for the returned array. This value may be the integer key of the column, or it may be the string key name. The value is cast as usual for array keys (however, prior to PHP 8.0.0, objects supporting conversion to string were also allowed).</param>
        /// <returns>Returns an array of values representing a single column from the input array.</returns>
        public static Dictionary<string, object> array_column(Dictionary<string, object> array, int column_key, string index_key = null)
        {
            if (array != null && array.Any())
            {
                var i = 0;
                var result = new Dictionary<string, object>(array.Count);
                foreach (var item in array)
                {
                    if (item.Value is Dictionary<string, string>)
                    {
                        var value = item.Value as Dictionary<string, string>;
                        var key = value[index_key];
                        var v = value.Skip(column_key).FirstOrDefault().Key;
                        result.Add(index_key == null ? i.ToString() : key, v);
                    }
                    //else if (item.Value is string)
                    //{
                    //}
                }
            }

            return null;
        }

        /// <summary>
        /// Return the values from a single column in the input array
        /// </summary>
        /// <param name="array">A multi-dimensional array or an array of objects from which to pull a column of values from. If an array of objects is provided, then public properties can be directly pulled. In order for protected or private properties to be pulled, the class must implement both the __get() and __isset() magic methods.</param>
        /// <param name="column_key">The column of values to return. This value may be an integer key of the column you wish to retrieve, or it may be a string key name for an associative array or property name. It may also be null to return complete arrays or objects (this is useful together with index_key to reindex the array).</param>
        /// <param name="index_key">The column to use as the index/keys for the returned array. This value may be the integer key of the column, or it may be the string key name. The value is cast as usual for array keys (however, prior to PHP 8.0.0, objects supporting conversion to string were also allowed).</param>
        /// <returns>Returns an array of values representing a single column from the input array.</returns>
        public static Dictionary<string, object> array_column(Dictionary<string, object> array, int column_key, int? index_key = null)
        {
            if (array != null && array.Any())
            {
                var i = 0;
                var result = new Dictionary<string, object>(array.Count);
                foreach (var item in array)
                {
                    if (item.Value is Dictionary<string, string>)
                    {
                        var value = item.Value as Dictionary<string, string>;
                        var key = value.Skip(index_key ?? 0).FirstOrDefault().Key;
                        var v = value.Skip(column_key).FirstOrDefault().Key;
                        result.Add(index_key == null ? i.ToString() : key, v);
                    }
                    //else if (item.Value is string)
                    //{
                    //}
                }
            }

            return null;
        }

        /// <summary>
        /// Creates an array by using one array for keys and another for its values
        /// </summary>
        /// <param name="keys">Array of keys to be used. Illegal values for key will be converted to string.</param>
        /// <param name="values">Array of values to be used</param>
        /// <returns>Returns the combined array.</returns>
        public static Dictionary<string, object> array_combine(Dictionary<string, object> keys, Dictionary<string, object> values)
        {
            if (keys != null && values != null)
            {
                return keys.
                Select(item => item.Key).
                Zip(values.Select(item => item.Key)).
                ToDictionary(x => x.First, x => (object)x.Second);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Counts all the values of an array
        /// </summary>
        /// <param name="array">The array of values to count.</param>
        /// <returns>Returns an associative array of values from array as keys and their count as value.</returns>
        public static Dictionary<string, int> array_count_values(Dictionary<string, object> array)
        {
            if (array != null)
            {
                return array.GroupBy(n => n.Key).
                    Select(n => new { Name = n.Key, Count = n.Count() }).
                    OrderBy(n => n.Name).
                    ToDictionary(x => x.Name, x => x.Count); // TODO: PHP allows repeat keys in the source
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Computes the difference of arrays with additional index check
        /// </summary>
        /// <param name="array">The array to compare from.</param>
        /// <param name="arrays">Arrays to compare against.</param>
        /// <returns>Returns an array containing all the values from array that are not present in any of the other arrays.</returns>
        public static Dictionary<string, object> array_diff_assoc(Dictionary<string, object> array, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {
                        foreach (var item in arr.Where(a => !array.ContainsKey(a.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Computes the difference of arrays using keys for comparison
        /// </summary>
        /// <param name="array">The array to compare from.</param>
        /// <param name="arrays">Arrays to compare against.</param>
        /// <returns>Returns an array containing all the entries from array whose keys are absent from all of the other arrays.</returns>
        public static Dictionary<string, object> array_diff_key(Dictionary<string, object> array, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {
                        foreach (var item in arr.Where(a => !array.ContainsKey(a.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Computes the difference of arrays with additional index check which is performed by a user supplied callback function
        /// </summary>
        /// <param name="array">The array to compare from.</param>
        /// <param name="key_compare_func">The comparison function must return an integer less than, equal to, or greater than zero if the first argument is considered to be respectively less than, equal to, or greater than the second.</param>
        /// <param name="arrays">Arrays to compare against.</param>
        /// <returns></returns>
        public static Dictionary<string, object> array_diff_uassoc(Dictionary<string, object> array, Func<string, string, int> key_compare_func, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>(new CustomComparer(key_compare_func));
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {

                        foreach (var item in arr.Where(a => !array.ContainsKey(a.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x=> x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Computes the difference of arrays using a callback function on the keys for comparison
        /// </summary>
        /// <param name="array">The array to compare from.</param>
        /// <param name="key_compare_func">The comparison function must return an integer less than, equal to, or greater than zero if the first argument is considered to be respectively less than, equal to, or greater than the second.</param>
        /// <param name="arrays">Arrays to compare against.</param>
        /// <returns>Returns an array containing all the entries from array that are not present in any of the other arrays.</returns>
        public static Dictionary<string, object> array_diff_ukey(Dictionary<string, object> array, Func<string, string, int> key_compare_func, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>(new CustomComparer(key_compare_func));
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {

                        foreach (var item in arr.Where(a => !array.ContainsKey(a.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Computes the difference of arrays
        /// </summary>
        /// <param name="array">The array to compare from.</param>
        /// <param name="arrays">Arrays to compare against.</param>
        /// <returns>Returns an array containing all the entries from array that are not present in any of the other arrays. Keys in the array array are preserved.</returns>
        public static Dictionary<string, object> array_diff(Dictionary<string, object> array, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {

                        foreach (var item in arr.Where(a => !array.ContainsKey(a.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Fill an array with values, specifying keys
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> array_fill_keys(Dictionary<string, object> array, object value)
        {
            if (array != null)
            {
                return array.
                    Select(a => new { a.Key, Value = value }).
                    ToDictionary(x => x.Key, x => x.Value);
            }

            return null;
        }

        /// <summary>
        /// Fill an array with values
        /// </summary>
        /// <param name="start_index">The first index of the returned array.<br />
        /// If start_index is negative, the first index of the returned array will be start_index and the following indices will start from zero prior to PHP 8.0.0; as of PHP 8.0.0, negative keys are incremented normally (see example).</param>
        /// <param name="count">Number of elements to insert. Must be greater than or equal to zero, and less than or equal to 2147483647.</param>
        /// <param name="value">Value to use for filling.</param>
        /// <returns>Returns the filled array.</returns>
        public Dictionary<string, object> array_fill(int start_index, int count, object value)
        {
            var result = new Dictionary<string, object>();
            for (var i = start_index; i < count; i++)
            {
                result.Add(i.ToString(), value);
            }

            return result;
        }

        /// <summary>
        /// Filters elements of an array using a callback function
        /// </summary>
        /// <param name="array">The array to iterate over.</param>
        /// <param name="key_compare_func">The callback function to use.<br />
        /// If no callback is supplied, all empty entries of array will be removed. See empty() for how PHP defines empty in this case.</param>
        /// <param name="mode">Flag determining what arguments are sent to callback:</param>
        /// <returns>Returns the filtered array.</returns>
        public static Dictionary<string, object> array_filter(Dictionary<string, object> array, Func<object, bool> callback, int mode = (int)ArrayFilterFlags.ARRAY_FILTER_USE_VALUE)
        {
            if (array != null)
            {
                return array.
                    Where(a => callback(a.Value)).
                    ToDictionary(x => x.Key, x => x.Value);
            }

            return null;
        }

        /// <summary>
        /// Filters elements of an array using a callback function
        /// </summary>
        /// <param name="array">The array to iterate over.</param>
        /// <param name="key_compare_func">The callback function to use.<br />
        /// If no callback is supplied, all empty entries of array will be removed. See empty() for how PHP defines empty in this case.</param>
        /// <param name="mode">Flag determining what arguments are sent to callback:</param>
        /// <returns>Returns the filtered array.</returns>
        public static Dictionary<string, object> array_filter(Dictionary<string, object> array, Func<string, bool> callback, int mode = (int)ArrayFilterFlags.ARRAY_FILTER_USE_KEY)
        {
            if (array != null)
            {
                return array.
                    Where(a => callback(a.Key)).
                    ToDictionary(x => x.Key, x => x.Value);
            }

            return null;
        }

        /// <summary>
        /// Filters elements of an array using a callback function
        /// </summary>
        /// <param name="array">The array to iterate over.</param>
        /// <param name="key_compare_func">The callback function to use.<br />
        /// If no callback is supplied, all empty entries of array will be removed. See empty() for how PHP defines empty in this case.</param>
        /// <param name="mode">Flag determining what arguments are sent to callback:</param>
        /// <returns>Returns the filtered array.</returns>
        public static Dictionary<string, object> array_filter(Dictionary<string, object> array, Func<string, object, bool> callback, int mode = (int)ArrayFilterFlags.ARRAY_FILTER_USE_BOTH)
        {
            if (array != null)
            {
                return array.
                    Where(a => callback(a.Key, a.Value)).
                    ToDictionary(x => x.Key, x => x.Value);
            }

            return null;
        }

        /// <summary>
        /// Exchanges all keys with their associated values in an array
        /// </summary>
        /// <param name="array">An array of key/value pairs to be flipped.</param>
        /// <returns>Returns the flipped array.</returns>
        public static Dictionary<string, object> array_flip(Dictionary<string, object> array)
        {
            if (array != null)
            {
                return array.
                    Select(a => new { Key = a.Value.ToString(), Value = (object)a.Key}).
                    ToDictionary(x => x.Key, x => x.Value);
            }

            return null;
        }

        /// <summary>
        /// Computes the intersection of arrays with additional index check
        /// </summary>
        /// <param name="array">The array with master values to check.</param>
        /// <param name="arrays">Arrays to compare values against.</param>
        /// <returns>Returns an associative array containing all the values in array that are present in all of the arguments.</returns>
        public static Dictionary<string, object> array_intersect_assoc(Dictionary<string, object> array, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {

                        foreach (var item in array.Where(x => arr.ContainsKey(x.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Computes the intersection of arrays using keys for comparison
        /// </summary>
        /// <param name="array">The array with master keys to check.</param>
        /// <param name="arrays">Arrays to compare keys against.</param>
        /// <returns>Returns an associative array containing all the entries of array which have keys that are present in all arguments.</returns>
        public static Dictionary<string, object> array_intersect_key(Dictionary<string, object> array, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {

                        foreach (var item in array.Where(x => arr.ContainsKey(x.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Computes the intersection of arrays with additional index check, compares indexes by a callback function
        /// </summary>
        /// <param name="array">Initial array for comparison of the arrays.</param>
        /// <param name="callback">The comparison function must return an integer less than, equal to, or greater than zero if the first argument is considered to be respectively less than, equal to, or greater than the second.</param>
        /// <param name="arrays">Arrays to compare keys against.</param>
        /// <returns>Returns the values of array whose values exist in all of the arguments. </returns>
        public static Dictionary<string, object> array_intersect_uassoc(Dictionary<string, object> array, Func<string, string, int> callback, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>(new CustomComparer(callback)); // Dict cant use CustomKvpComparer with Func<KeyValuePair<string, object>, KeyValuePair<string, object>, int>
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {

                        foreach (var item in arr.Where(a => !array.ContainsKey(a.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Computes the intersection of arrays using a callback function on the keys for comparison
        /// </summary>
        /// <param name="array">Initial array for comparison of the arrays.</param>
        /// <param name="callback">The comparison function must return an integer less than, equal to, or greater than zero if the first argument is considered to be respectively less than, equal to, or greater than the second.</param>
        /// <param name="arrays">Arrays to compare keys against.</param>
        /// <returns>Returns the values of array whose keys exist in all the arguments.</returns>
        public static Dictionary<string, object> array_intersect_ukey(Dictionary<string, object> array, Func<string, string, int> callback, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>(new CustomComparer(callback)); // Dict cant use CustomKvpComparer with Func<KeyValuePair<string, object>, KeyValuePair<string, object>, int>
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {

                        foreach (var item in arr.Where(a => !array.ContainsKey(a.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Computes the intersection of arrays
        /// </summary>
        /// <param name="array">The array with master values to check.</param>
        /// <param name="arrays">Arrays to compare values against.</param>
        /// <returns>Returns an array containing all of the values in array whose values exist in all of the parameters.</returns>
        public static Dictionary<string, object> array_intersect(Dictionary<string, object> array, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {
                        foreach (var item in arr.Where(a => array.ContainsKey(a.Key)))
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        public static void array_is_list() { } //Checks whether a given array is a list

        /// <summary>
        /// Checks if the given key or index exists in the array
        /// </summary>
        /// <param name="key">Value to check.</param>
        /// <param name="array">An array with keys to check. </param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool array_key_exists(string key, Dictionary<string, object> array)
        {
            return array == null ? false : array.ContainsKey(key);
        }

        /// <summary>
        /// Checks if the given key or index exists in the array
        /// </summary>
        /// <param name="key">Value to check.</param>
        /// <param name="array">An array with keys to check. </param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool array_key_exists(int key, Dictionary<string, object> array)
        {
            return array == null ? false : array.ContainsKey(key.ToString());
        }

        /// <summary>
        /// Gets the first key of an array
        /// </summary>
        /// <param name="array">An array.</param>
        /// <returns>Returns the first key of array if the array is not empty; null otherwise.</returns>
        public static string array_key_first(Dictionary<string, object> array)
        {
            return array == null ? null : array.Keys.FirstOrDefault();
        }

        /// <summary>
        /// Gets the last key of an array
        /// </summary>
        /// <param name="array">An array.</param>
        /// <returns>Returns the last key of array if the array is not empty; null otherwise.</returns>
        public static string array_key_last(Dictionary<string, object> array)
        {
            return array == null ? null : array.Keys.LastOrDefault();
        }

        public static Dictionary<string, object> array_keys(Dictionary<string, object> array) 
        {
            var i = 0;
            return array == null ? null : array.Keys.ToDictionary(x => i++.ToString(), x => (object)x);
        } //Return all the keys or a subset of the keys of an array

        public static Dictionary<string, object> array_keys(Dictionary<string, object> array, object search_value, bool strict = false)
        {
            var i = 0;
            return array == null ? null : array.Keys.Where(x => x.Equals(search_value.ToString())).ToDictionary(x => i++.ToString(), x => (object)x); // TODO strict
        } //Return all the keys or a subset of the keys of an array

        /// <summary>
        /// Applies the callback to the elements of the given arrays
        /// </summary>
        /// <param name="array"></param>
        /// <param name="callback"></param>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static Dictionary<string, object> array_map(Dictionary<string, object> array, Func<object[], object> callback, params Dictionary<string, object>[] arrays) 
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                return ((Dictionary<string, object>)callback(arrays)).ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Merge one or more arrays recursively
        /// </summary>
        /// <param name="arrays">Variable list of arrays to recursively merge.</param>
        /// <returns>An array of values resulted from merging the arguments together. If called without any arguments, returns an empty array.</returns>
        public static Dictionary<string, object> array_merge_recursive(params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {
                        foreach (var item in arr)
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Merge one or more arrays
        /// </summary>
        /// <param name="arrays">Variable list of arrays to merge.</param>
        /// <returns>Returns the resulting array. If called without any arguments, returns an empty array.</returns>
        public static Dictionary<string, object> array_merge(params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                var result = new ConcurrentDictionary<string, object>();
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {
                        foreach (var item in arr)
                        {
                            result.TryAdd(item.Key, item.Value);
                        }
                    }
                });

                return result.ToDictionary(x => x.Key, x => x.Value); // TODO
            }

            return null;
        }

        /// <summary>
        /// Sort multiple or multi-dimensional arrays
        /// </summary>
        /// <param name="sortParams">Arrays being sorted.</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public static bool array_multisort(params ArrayMultiortParam[] sortParams)
        {
            if (sortParams != null)
            {
                Parallel.ForEach(sortParams, new ParallelOptions(), (arr) =>
                {
                    if (arr != null)
                    {
                        arr.array = arr.array.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value); // TODO;
                    }
                });

                return true;
            }

            return false;
        }

        /// <summary>
        /// Pad array to the specified length with a value
        /// </summary>
        /// <param name="array">Initial array of values to pad.</param>
        /// <param name="length">New size of the array.</param>
        /// <param name="value">Value to pad if array is less than length.</param>
        /// <returns>Returns a copy of the array padded to size specified by length with value value. If length is positive then the array is padded on the right, if it's negative then on the left. If the absolute value of length is less than or equal to the length of the array then no padding takes place.</returns>
        public static Dictionary<string, object> array_pad(Dictionary<string, object> array, int length, object value) 
        {
            if (array != null && length > array.Count)
            {
                for (var i = 0; i < (length - array.Count); i++)
                {
                    array.Add(i++.ToString(), value);
                }

                return array; // TODO
            }

            return array;
        }

        /// <summary>
        /// Pop the element off the end of array
        /// </summary>
        /// <param name="array">The array to get the value from.</param>
        /// <returns>Returns the value of the last element of array. If array is empty, null will be returned.</returns>
        public static object array_pop(Dictionary<string, object> array)
        {
            var result = array.Last();
            array.Remove(result.Key);
            return result.Value;
        }

        /// <summary>
        /// Calculate the product of values in an array
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>Returns the product as an integer or float.</returns>
        public static long array_product(Dictionary<string, object> array)
        {
            if (array != null)
            {
                return array.Aggregate(1, (accumulatedValue, value) => accumulatedValue * Convert.ToInt32(value.Value)); // TODO
            }

            return 0;
        }

        /// <summary>
        /// Push one or more elements onto the end of array
        /// </summary>
        /// <param name="array">The input array.</param>
        /// <param name="values">The values to push onto the end of the array.</param>
        /// <returns>Returns the new number of elements in the array.</returns>
        public static long array_push(Dictionary<string, object> array, params object[] values)
        {
            if (array != null)
            {
                var i = array.Count;
                foreach (var v in values)
                {
                    array.Add(i++.ToString(), v);
                }

                return i;
            }

            return 0;
        }

        /// <summary>
        /// Pick one or more random keys out of an array
        /// </summary>
        /// <param name="array">The input array.</param>
        /// <param name="num">Specifies how many entries should be picked.</param>
        /// <returns>When picking only one entry, array_rand() returns the key for a random entry. Otherwise, an array of keys for the random entries is returned. This is done so that random keys can be picked from the array as well as random values. If multiple keys are returned, they will be returned in the order they were present in the original array. Trying to pick more elements than there are in the array will result in an E_WARNING level error, and NULL will be returned.</returns>
        public static (object, Dictionary<string, object>) array_rand(Dictionary<string, object> array, int num = 1)
        {
            if (array != null)
            {
                var r = new Random();
                if (num == 1)
                {
                    return (array.ElementAt(r.Next(1, array.Count)), null);
                }

                var rns = new int[num];
                int rn;
                for (var i = 1; i < num; i++)
                {
                    do
                    {
                        rn = r.Next(1, array.Count);
                    } while (rns.Contains(rn));
                    rns[i] = rn;
                }

                var result = new Dictionary<string, object>(num);
                foreach (var i in rns)
                {
                    result.Add(array.ElementAt(i).Key, array.ElementAt(i).Value);
                }

                return (null, result);
            }

            return (null, null);
        }

        /// <summary>
        /// Iteratively reduce the array to a single value using a callback function
        /// </summary>
        /// <param name="array">The input array.</param>
        /// <param name="callback">object callback(object carry, object item)</param>
        /// <param name="initial">If the optional initial is available, it will be used at the beginning of the process, or as a final result in case the array is empty.</param>
        /// <returns>Returns the resulting value.</returns>
        public static object array_reduce(Dictionary<string, object> array, Func<object, object, object> callback, object initial = null)
        {
            if (array != null)
            {
                return array.Values.Aggregate(initial, (carry, item) => callback(carry, item));
            }

            return initial;
        }

        /// <summary>
        /// Replaces elements from passed arrays into the first array recursively
        /// </summary>
        /// <param name="array">The array in which elements are replaced.</param>
        /// <param name="arrays">Arrays from which elements will be extracted. </param>
        /// <returns></returns>
        public static Dictionary<string, object> array_replace_recursive(Dictionary<string, object> array, params Dictionary<string, object>[] arrays)
        {
            if (array != null)
            {
                Parallel.ForEach(arrays, new ParallelOptions(), (arr) =>
                {
                    foreach (var item in arr)
                    {
                        if (array.ContainsKey(item.Key))
                        {
                            array[item.Key] = item.Value; // TODO: recursive
                        }
                    }
                });

                return array;
            }

            return null;
        }

        /// <summary>
        /// Replaces elements from passed arrays into the first array
        /// </summary>
        /// <param name="array">The array in which elements are replaced.</param>
        /// <param name="replacements">Arrays from which elements will be extracted. Values from later arrays overwrite the previous values.</param>
        /// <returns>The array with value replaced.</returns>
        public static Dictionary<string, object> array_replace(Dictionary<string, object> array, params Dictionary<string, object>[] replacements)
        {
            if (array != null)
            {
                Parallel.ForEach(replacements, new ParallelOptions(), (arr) =>
                {
                    foreach (var item in arr)
                    {
                        if (array.ContainsKey(item.Key))
                        {
                            array[item.Key] = item.Value; // TODO: recursive
                        }
                    }
                });

                return array;
            }

            return null;
        }

        /// <summary>
        /// Return an array with elements in reverse order
        /// </summary>
        /// <param name="array"></param>
        /// <param name="preserve_keys"></param>
        /// <returns></returns>
        public static Dictionary<string, object> array_reverse(Dictionary<string, object> array, bool preserve_keys = false)
        {
            if (array != null)
            {
                if (preserve_keys)
                {
                    var v = array.FirstOrDefault().Value;
                    for (var i = 1; i < array.Count; i++)
                    {
                        // TODO
                    }
                }

                return array.Reverse().ToDictionary(x => x.Key, x=> x.Value);
            }

            return null;
        }

        /// <summary>
        /// Searches the array for a given value and returns the first corresponding key if successful
        /// </summary>
        /// <param name="value">The searched value.</param>
        /// <param name="array">The array to search.</param>
        /// <param name="strict">If the third parameter strict is set to true then the array_search() function will search for identical elements in the haystack. This means it will also perform a strict type comparison of the needle in the haystack, and objects must be the same instance.</param>
        /// <returns>Returns the key for needle if it is found in the array, false otherwise.</returns>
        public static (int, string, bool) array_search(object value, Dictionary<string, object> array, bool strict = false)
        {
            if (array != null)
            {
                var r = array.FirstOrDefault(item => strict ? item.Value == value : item.Value.ToString() == value.ToString());
                if (r.Key is string) return (0, r.Key, true);
                try
                {
                    var i = Convert.ToInt32(r.Key);
                    return (i, null, true);
                }
                catch
                {
                }
            }

            return (0, null, false);
        }

        public static void array_shift() { } //Shift an element off the beginning of array
        public static void array_slice() { } //Extract a slice of the array
        public static void array_splice() { } //Remove a portion of the array and replace it with something else
        public static void array_sum() { } //Calculate the sum of values in an array
        public static void array_udiff_assoc() { } //Computes the difference of arrays with additional index check, compares data by a callback function
        public static void array_udiff_uassoc() { } //Computes the difference of arrays with additional index check, compares data and indexes by a callback function
        public static void array_udiff() { } //Computes the difference of arrays by using a callback function for data comparison
        public static void array_uintersect_assoc() { } //Computes the intersection of arrays with additional index check, compares data by a callback function
        public static void array_uintersect_uassoc() { } //Computes the intersection of arrays with additional index check, compares data and indexes by separate callback functions
        public static void array_uintersect() { } //Computes the intersection of arrays, compares data by a callback function
        public static void array_unique() { } //Removes duplicate values from an array
        public static void array_unshift() { } //Prepend one or more elements to the beginning of an array
        public static void array_values() { } //Return all the values of an array
        public static void array_walk_recursive() { } //Apply a user function recursively to every member of an array
        public static void array_walk() { } //Apply a user supplied function to every member of an array
        public static void array() { } //Create an array
        public static void arsort() { } //Sort an array in descending order and maintain index association
        public static void asort() { } //Sort an array in ascending order and maintain index association
        public static void compact() { } //Create array containing variables and their values
        public static void count() { } //Counts all elements in an array or in a Countable object
        public static void current() { } //Return the current element in an array
        public static void each() { } //Return the current key and value pair from an array and advance the array cursor
        public static void end() { } //Set the internal pointer of an array to its last element
        public static void extract() { } //Import variables into the current symbol table from an array
        public static void in_array() { } //Checks if a value exists in an array
        public static void key_exists() { } //Alias of array_key_exists
        public static void key() { } //Fetch a key from an array
        public static void krsort() { } //Sort an array by key in descending order
        public static void ksort() { } //Sort an array by key in ascending order
        public static void list() { } //Assign variables as if they were an array
        public static void natcasesort() { } //Sort an array using a case insensitive "natural order" algorithm
        public static void natsort() { } //Sort an array using a "natural order" algorithm
        public static void next() { } //Advance the internal pointer of an array
        public static void pos() { } //Alias of current
        public static void prev() { } //Rewind the internal array pointer
        public static void range() { } //Create an array containing a range of elements
        public static void reset() { } //Set the internal pointer of an array to its first element
        public static void rsort() { } //Sort an array in descending order
        public static void shuffle() { } //Shuffle an array
        public static void _sizeof() { } //Alias of count
        public static void sort() { } //Sort an array in ascending order
        public static void uasort() { } //Sort an array with a user-defined comparison function and maintain index association
        public static void uksort() { } //Sort an array by keys using a user-defined comparison function
        public static void usort() { } //Sort an array by values using a user-defined comparison function   
    }
}