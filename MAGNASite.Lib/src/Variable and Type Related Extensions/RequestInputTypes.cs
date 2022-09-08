/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    internal enum RequestInputTypes
    {
        INPUT_POST = 0, // POST variables.
        INPUT_GET = 1, // GET variables.
        INPUT_COOKIE = 2, // COOKIE variables.
        INPUT_ENV = 4, // ENV variables.
        INPUT_SERVER = 5, // SERVER variables.
        INPUT_SESSION = 6, // SESSION variables. (not implemented yet)
        INPUT_REQUEST = 99, // REQUEST variables. (not implemented yet)
    }
}
