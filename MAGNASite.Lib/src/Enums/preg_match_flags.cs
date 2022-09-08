/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum preg_match_flags
    {
        PREG_OFFSET_CAPTURE = 1,    // If this flag is passed, for every occurring match the appendant string offset (in bytes) will also be returned. Note that this changes the value of matches into an array where every element is an array consisting of the matched string at offset 0 and its string offset into subject at offset 1.
        PREG_UNMATCHED_AS_NULL = 2  // If this flag is passed, unmatched subpatterns are reported as null; otherwise they are reported as an empty string.
    }
}
