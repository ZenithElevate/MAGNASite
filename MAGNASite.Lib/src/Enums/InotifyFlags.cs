/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
namespace MAGNASite.Lib.Enums
{
    [Flags]
    internal enum InotifyFlags : uint // TODO: values
    {
        IN_ACCESS = 1,              // File was accessed(read) (*)
        IN_MODIFY = 2,              // File was modified(*)
        IN_ATTRIB = 4,              // Metadata changed(e.g.permissions, mtime, etc.) (*)
        IN_CLOSE_WRITE = 8,         // File opened for writing was closed(*)
        IN_CLOSE_NOWRITE = 16,      // File not opened for writing was closed(*)
        IN_OPEN = 32,               // File was opened(*)
        IN_MOVED_TO = 128,          // File moved into watched directory(*)
        IN_MOVED_FROM = 64,         // File moved out of watched directory(*)
        IN_CREATE = 256,            // File or directory created in watched directory(*)
        IN_DELETE = 512,            // File or directory deleted in watched directory(*)
        IN_DELETE_SELF = 1024,      // Watched file or directory was deleted
        IN_MOVE_SELF = 2048,        // Watch file or directory was moved
        IN_CLOSE = 24,              // Equals to IN_CLOSE_WRITE | IN_CLOSE_NOWRITE
        IN_MOVE = 192,              // Equals to IN_MOVED_FROM | IN_MOVED_TO
        IN_ALL_EVENTS = 4095,       // Bitmask of all the above constants
        IN_UNMOUNT = 8192,          // File system containing watched object was unmounted
        IN_Q_OVERFLOW = 16384,      // Event queue overflowed(wd is -1 for this event)
        IN_IGNORED = 32768,         // Watch was removed(explicitly by inotify_rm_watch() or because file was removed or filesystem unmounted
        IN_ISDIR = 1073741824,      // Subject of this event is a directory
        IN_ONLYDIR = 16777216,      // Only watch pathname if it is a directory (Since Linux 2.6.15)
        IN_DONT_FOLLOW = 33554432,  // Do not dereference pathname if it is a symlink(Since Linux 2.6.15)
        IN_MASK_ADD = 536870912,    // Add events to watch mask for this pathname if it already exists (instead of replacing mask). 
        IN_ONESHOT = 2147483648     // Monitor pathname for one event, then remove from watch list.
    }
}