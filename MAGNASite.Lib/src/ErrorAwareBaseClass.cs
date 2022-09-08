/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using System.Collections.Concurrent;

namespace MAGNASite.Lib
{
    internal abstract class ErrorAwareBaseClass
    {
        protected readonly ConcurrentBag<string> Errors;
        protected readonly ConcurrentBag<string> Warnings;

        protected ErrorAwareBaseClass()
        {
            Errors = new ConcurrentBag<string>();
            Warnings = new ConcurrentBag<string>();
        }

        /// <summary>
        /// Each class has to have this method, for adding errors.
        /// </summary>
        /// <param name="error">The error to add</param>
        protected void AddError(string error)
        {
            Localization.Localize();
            Errors.Add(error);
        }

        /// <summary>
        /// Each class has to have this method, for adding warnings.
        /// </summary>
        /// <param name="warning">The warning to add</param>
        protected void AddWarning(string warning)
        {
            Localization.Localize();
            Warnings.Add(warning);
        }
    }
}
