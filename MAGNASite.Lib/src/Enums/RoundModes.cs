/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */

namespace MAGNASite.Lib
{
    internal enum RoundModes
    {
        PHP_ROUND_HALF_UP = 1,   // Rounds num away from zero when it is half way there, making 1.5 into 2 and -1.5 into -2.
        PHP_ROUND_HALF_DOWN = 2,   // Rounds num towards zero when it is half way there, making 1.5 into 1 and -1.5 into -1.
        PHP_ROUND_HALF_EVEN = 3,   // Rounds num towards the nearest even value when it is half way there, making both 1.5 and 2.5 into 2.
        PHP_ROUND_HALF_ODD = 4    // Rounds num towards the nearest odd value when it is half way there, making 1.5 into 1 and 2.5 into 3. 
    }
}
