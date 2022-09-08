/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    /// <summary>
    /// Arrays being sorted
    /// </summary>
    internal class ArrayMultiortParam
    {
        /// <summary>
        /// Constructs multisort parameter
        /// </summary>
        /// <param name="array">The array to sort.</param>
        /// <param name="sortOrder">Sort order or flags.</param>
        /// <param name="sortFlags">Flags or sort order.</param>
        public ArrayMultiortParam(Dictionary<string, object> array, int sortOrder, int sortFlags)
        {
            this.array = array;
            SortOrder = (ArraySortOrders)sortOrder == ArraySortOrders.SORT_ASC || (ArraySortOrders)sortOrder == ArraySortOrders.SORT_DESC ?
                (ArraySortOrders)sortOrder : (ArraySortOrders)SortFlags; // Swap if reversed
            SortFlags = (ArraySortFlags)sortFlags == ArraySortFlags.SORT_REGULAR || (ArraySortFlags)sortFlags == ArraySortFlags.SORT_NUMERIC || (ArraySortFlags)sortFlags == ArraySortFlags.SORT_STRING2 ||
                (ArraySortFlags)sortFlags == ArraySortFlags.SORT_LOCALE_STRING || (ArraySortFlags)sortFlags == ArraySortFlags.SORT_NATURAL || (ArraySortFlags)sortFlags == ArraySortFlags.SORT_FLAG_CASE ?
                (ArraySortFlags)sortFlags : (ArraySortFlags)sortOrder; // Swap if reversed
        }

        /// <summary>
        /// An array being sorted.
        /// </summary>
        public Dictionary<string, object> array { get; set; }

        /// <summary>
        /// The order used to sort the previous array argument.Either SORT_ASC to sort ascendingly or SORT_DESC to sort descendingly.<br />
        /// This argument can be swapped with array1_sort_flags or omitted entirely, in which case SORT_ASC is assumed.
        /// </summary>
        public ArraySortOrders SortOrder { get; private set; }

        /// <summary>
        /// Sort options for the previous array argument.
        /// This argument can be swapped with array1_sort_order or omitted entirely, in which case SORT_REGULAR is assumed.
        /// </summary>
        public ArraySortFlags SortFlags { get; private set; }
    }
}