/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib
{
    internal class PHPDirectIo : ErrorAwareBaseClass
    {
        public static bool FILE_USE_INCLUDE_PATH { get; set; }

        public const int
            F_DUPFD = 0, // 
            F_GETFD = 0, // 
            F_GETFL = 0, // 
            F_GETLK = 0, // 
            F_GETOWN = 0, // 
            F_RDLCK = 0, // 
            F_SETFL = 0, // 
            F_SETLK = 0, // 
            F_SETLKW = 0, // 
            F_SETOWN = 0, // 
            F_UNLCK = 0, // 
            F_WRLCK = 0, // 
            O_APPEND = 0, // 
            O_ASYNC = 0, // 
            O_CREAT = 0, // 
            O_EXCL = 0, // 
            O_NDELAY = 0, // 
            O_NOCTTY = 0, // 
            O_NONBLOCK = 0, // 
            O_RDONLY = 0, // 
            O_RDWR = 0, // 
            O_SYNC = 0, // 
            O_TRUNC = 0, // 
            O_WRONLY = 0, // 
            S_IRGRP = 0, // 
            S_IROTH = 0, // 
            S_IRUSR = 0, // 
            S_IRWXG = 0, // 
            S_IRWXO = 0, // 
            S_IRWXU = 0, // 
            S_IWGRP = 0, // 
            S_IWOTH = 0, // 
            S_IWUSR = 0, // 
            S_IXGRP = 0, // 
            S_IXOTH = 0, // 
            S_IXUSR = 0; // 

        public static void dio_close() {} // Closes the file descriptor given by fd
        public static void dio_fcntl() {} // Performs a c library fcntl on fd
        public static void dio_open() {} // Opens a file(creating it if necessary) at a lower level than the C library input/ouput stream functions allow
        public static void dio_read() {} // Reads bytes from a file descriptor
        public static void dio_seek() {} // Seeks to pos on fd from whence
        public static void dio_stat() {} // Gets stat information about the file descriptor fd
        public static void dio_tcsetattr() {} // Sets terminal attributes and baud rate for a serial port
        public static void dio_truncate() {} // Truncates file descriptor fd to offset bytes
        public static void dio_write() {} // Writes data to fd with optional truncation at length
    }
}
