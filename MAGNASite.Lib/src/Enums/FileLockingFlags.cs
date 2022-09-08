/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum FileLockingFlags
    {
        LOCK_SH = 1,  // to acquire a shared lock (reader)
        LOCK_EX = 2, // to acquire an exclusive lock (writer)
        LOCK_UN = 4   // to release a lock (shared or exclusive)
    }
}
