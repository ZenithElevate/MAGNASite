/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    internal enum FileSeekWhence
    {
        SEEK_SET, // Set position equal to offset bytes.
        SEEK_CUR, // Set position to current location plus offset.
        SEEK_END  // Set position to end-of-file plus offset.
    }
}
