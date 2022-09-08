/* Copyright © Alex Malmyguine, 2022
 * All right reserved
 * This program remains the property of the copyright owner at all times.
 * It cannot be used for any purposes without express, written permission from the copyright owner.
 * Intellectual property contained in this program is protected by Canadian, US, and international copyright laws.
 */
using MAGNASite.Lib.Enums;
using MAGNASite.Lib.Properties;

namespace MAGNASite.Lib
{
    /// <summary>
    /// Inotify
    /// </summary>
    internal class PHPInotify : ErrorAwareBaseClass
    {
        List<EventWatcher> Watchers;
        Queue<EventArgs> EventQueue;

        public static bool FILE_USE_INCLUDE_PATH { get; set; }

        public PHPInotify()
        {
            Watchers = new List<EventWatcher>();
            EventQueue = new Queue<EventArgs>();
        }

        /// <summary>
        /// Add a watch to an initialized inotify instance
        /// </summary>
        /// <param name="inotify_instance">Resource returned by inotify_init()</param>
        /// <param name="pathname">File or directory to watch.</param>
        /// <param name="mask">Events to watch for. See Predefined Constants.</param>
        /// <returns>The return value is a unique (inotify instance wide) watch descriptor.</returns>
        public int inotify_add_watch(FileSystemWatcher inotify_instance, string pathname, int mask)
        {
            var i = Watchers.FindIndex(t => t.FileName.Equals(pathname, StringComparison.OrdinalIgnoreCase));
            if (i >= -1)
            {
                return i;
            }
            else
            {
                var fsw = new FileSystemWatcher(pathname);
                if ((mask | (uint)InotifyFlags.IN_ATTRIB) == mask) { fsw.NotifyFilter = NotifyFilters.Attributes; }
                if ((mask | (uint)InotifyFlags.IN_CREATE) == mask) { fsw.NotifyFilter = NotifyFilters.CreationTime; }
                if ((mask | (uint)InotifyFlags.IN_ISDIR) == mask) { fsw.NotifyFilter = NotifyFilters.DirectoryName; }
                if ((mask | (uint)InotifyFlags.IN_CREATE) == mask) { fsw.NotifyFilter = NotifyFilters.FileName; }
                if ((mask | (uint)InotifyFlags.IN_ACCESS) == mask) { fsw.NotifyFilter = NotifyFilters.LastAccess; }
                if ((mask | (uint)InotifyFlags.IN_MODIFY) == mask) { fsw.NotifyFilter = NotifyFilters.LastWrite; }
                if ((mask | (uint)InotifyFlags.IN_ATTRIB) == mask) { fsw.NotifyFilter = NotifyFilters.Security; }
                if ((mask | (uint)InotifyFlags.IN_CLOSE_WRITE) == mask) { fsw.NotifyFilter = NotifyFilters.Size; }

                if ((mask | (uint)InotifyFlags.IN_ALL_EVENTS | (uint)InotifyFlags.IN_MODIFY | (uint)InotifyFlags.IN_CLOSE_WRITE) == mask) { fsw.Changed += OnChanged; }
                if ((mask | (uint)InotifyFlags.IN_ALL_EVENTS | (uint)InotifyFlags.IN_MOVED_TO | (uint)InotifyFlags.IN_MOVED_FROM | (uint)InotifyFlags.IN_MOVE_SELF) == mask) { fsw.Renamed += OnRenamed; }
                if ((mask | (uint)InotifyFlags.IN_ALL_EVENTS | (uint)InotifyFlags.IN_CREATE) == mask) { fsw.Created += OnCreated; }
                if ((mask | (uint)InotifyFlags.IN_ALL_EVENTS | (uint)InotifyFlags.IN_DELETE) == mask) { fsw.Deleted += OnDeleted; }
                fsw.Error += OnError;

                Watchers.Add(new EventWatcher(pathname, fsw));
                return Watchers.Count - 1; // TODO
            }
        }

        void OnChanged(object sender, FileSystemEventArgs e)
        {
            EventQueue.Enqueue(e);
        }

        void OnRenamed(object sender, FileSystemEventArgs e)
        {
            EventQueue.Enqueue(e);
        }

        void OnDeleted(object sender, FileSystemEventArgs e)
        {
            EventQueue.Enqueue(e);
        }

        void OnCreated(object sender, FileSystemEventArgs e)
        {
            EventQueue.Enqueue(e);
        }

        void OnError(object sender, ErrorEventArgs e)
        {
            EventQueue.Enqueue(e);
        }

        /// <summary>
        /// Initialize an inotify instance
        /// </summary>
        /// <returns></returns>
        public static FileStream inotify_init()
        {
            return null; // TODO
        }

        /// <summary>
        /// Return a number upper than zero if there are pending events
        /// </summary>
        /// <param name="inotify_instance">Resource returned by inotify_init()</param>
        /// <returns>Returns a number upper than zero if there are pending events.</returns>
        public int inotify_queue_len(FileSystemWatcher inotify_instance)
        {
            return EventQueue.Count;
        }

        /// <summary>
        /// Read events from an inotify instance
        /// </summary>
        /// <param name="inotify_instance">Resource returned by inotify_init()</param>
        /// <returns>An array of inotify events or false if no events was pending and inotify_instance is non-blocking. Each event is an array with the following keys:</returns>
        public EventArgs inotify_read(FileSystemWatcher inotify_instance)
        {
            return EventQueue.Dequeue();
        }

        /// <summary>
        /// Remove an existing watch from an inotify instance
        /// </summary>
        /// <param name="inotify_instance"></param>
        /// <param name="watch_descriptor"></param>
        public void inotify_rm_watch(FileSystemWatcher inotify_instance, int watch_descriptor)
        {
            Watchers[watch_descriptor].Dispose();
        }
    }
}