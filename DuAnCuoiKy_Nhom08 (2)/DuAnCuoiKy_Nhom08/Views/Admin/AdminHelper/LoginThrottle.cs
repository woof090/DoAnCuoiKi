using System;
using System.Collections.Concurrent;

namespace DuAnCuoiKy_Nhom08.Helpers
{
    /// <summary>Khóa tạm thời khi đăng nhập sai nhiều lần (theo IP + tên đăng nhập), lưu trong bộ nhớ.</summary>
    public static class LoginThrottle
    {
        private const int MaxFailures = 5;
        private static readonly TimeSpan LockTime = TimeSpan.FromMinutes(5);

        private class Entry
        {
            public int Count;
            public DateTime LockedUntil;
        }

        private static readonly ConcurrentDictionary<string, Entry> Entries =
            new ConcurrentDictionary<string, Entry>(StringComparer.OrdinalIgnoreCase);

        private static string Key(string ip, string username)
        {
            return (ip ?? "") + "|" + (username ?? "");
        }

        public static bool IsLocked(string ip, string username)
        {
            Entry e;
            if (!Entries.TryGetValue(Key(ip, username), out e)) return false;
            lock (e)
            {
                if (e.LockedUntil > DateTime.UtcNow) return true;
                if (e.LockedUntil != default(DateTime))
                {
                    e.Count = 0;
                    e.LockedUntil = default(DateTime);
                }
                return false;
            }
        }

        public static void RegisterFailure(string ip, string username)
        {
            var e = Entries.GetOrAdd(Key(ip, username), k => new Entry());
            lock (e)
            {
                e.Count++;
                if (e.Count >= MaxFailures) e.LockedUntil = DateTime.UtcNow.Add(LockTime);
            }
        }

        public static void Reset(string ip, string username)
        {
            Entry removed;
            Entries.TryRemove(Key(ip, username), out removed);
        }
    }
}
