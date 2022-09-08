/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using Microsoft.Win32;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace MAGNASite.Lib
{
    internal class PHPFunctionHandling : ErrorAwareBaseClass
    {
        public static bool FILE_USE_INCLUDE_PATH { get; set; }

        public static void call_user_func_array() {} // Call a callback with an array of parameters
        public static void call_user_func() {} // Call the callback given by the first parameter
        public static void create_function() {} // Create a function dynamically by evaluating a string of code
        public static void forward_static_call_array() {} // Call a static method and pass the arguments as array
        public static void forward_static_call() {} // Call a static method
        public static void func_get_arg() {} // Return an item from the argument list
        public static void func_get_args() {} // Returns an array comprising a function's argument list
        public static void func_num_args() {} // Returns the number of arguments passed to the function
        public static void function_exists() {} // Return true if the given function has been defined
        public static void get_defined_functions() {} // Returns an array of all defined functions
        public static void register_shutdown_function() {} // Register a function for execution on shutdown
        public static void register_tick_function() {} // Register a function for execution on each tick
        public static void unregister_tick_function() {} // De-register a function for execution on each tick
    }
}
