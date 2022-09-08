/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    [Flags]
    internal enum PHPDateTimeZones
    {
        AFRICA = 1,
        AMERICA = 2,
        ANTARCTICA = 4,
        ARCTIC = 8,
        ASIA = 16,
        ATLANTIC = 32,
        AUSTRALIA = 64,
        EUROPE = 128,
        INDIAN = 256,
        PACIFIC = 512,
        UTC = 1024,
        ALL = 2047,
        ALL_WITH_BC = 4095,
        PER_COUNTRY = 4096
    }
}