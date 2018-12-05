using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Advent2018
{
    public static class Day4
    {
        public static void Run()
        {
            string[] lines = Input.Get(4);

            Record[] records = lines.Select(r => new Record(r)).OrderBy(r => r.Time).ToArray();
            part2(records);
        }

        private static void part1(Record[] records)
        {
            Dictionary<int, Guard> guards = new Dictionary<int, Guard>();
            int currentGuard = 0;
            DateTime sleepStart = DateTime.MinValue;
            foreach (var record in records)
            {
                if (record.Text.StartsWith("Guard", StringComparison.InvariantCulture))
                {
                    string idStr = record.Text.Substring(record.Text.IndexOf("#", StringComparison.InvariantCulture) + 1, 4).TrimEnd();
                    currentGuard = int.Parse(idStr);
                    continue;
                }
                if (record.Text.StartsWith("falls", StringComparison.InvariantCulture))
                {
                    sleepStart = record.Time;
                    continue;
                }
                var sleepTime = (int)(record.Time - sleepStart).TotalMinutes;
                guards.TryGetValue(currentGuard, out Guard guard);
                if (guard == null) guard = new Guard { ID = currentGuard};
                guard.SleepTime += sleepTime;
                guards[currentGuard] = guard;
                while (sleepStart < record.Time)
                {
                    guard.Minutes.TryGetValue(sleepStart.Minute, out int count);
                    guard.Minutes[sleepStart.Minute] = count + 1;
                    sleepStart = sleepStart.AddMinutes(1);
                }
            }
            var maxSleepTime = guards.Values.Max(g => g.SleepTime);
            Guard maxGuard = guards.Values.First(g => g.SleepTime == maxSleepTime);
            var maxCount = maxGuard.Minutes.Values.Max();
            var minute = maxGuard.Minutes.Keys.First(k => maxGuard.Minutes[k] == maxCount);
            Console.WriteLine(maxGuard.ID * minute);
        }

        private static void part2(Record[] records)
        {
            int currentGuard = 0;
            DateTime sleepStart = DateTime.MinValue;
            Dictionary<int, Minute> minutes = new Dictionary<int, Minute>();
            foreach (var record in records)
            {
                if (record.Text.StartsWith("Guard", StringComparison.InvariantCulture))
                {
                    string idStr = record.Text.Substring(record.Text.IndexOf("#", StringComparison.InvariantCulture) + 1, 4).TrimEnd();
                    currentGuard = int.Parse(idStr);
                    continue;
                }
                if (record.Text.StartsWith("falls", StringComparison.InvariantCulture))
                {
                    sleepStart = record.Time;
                    continue;
                }
                var sleepTime = (int)(record.Time - sleepStart).TotalMinutes;
                while (sleepStart < record.Time)
                {
                    minutes.TryGetValue(sleepStart.Minute, out var minute);
                    if (minute == null) minute = new Minute();
                    minute.Guards.TryGetValue(currentGuard, out var count);
                    minute.Guards[currentGuard] = count + 1;
                    minutes[sleepStart.Minute] = minute;
                    sleepStart = sleepStart.AddMinutes(1);
                }
            }
            var maxCount = minutes.SelectMany(m => m.Value.Guards.Values).Max();
            var maxMinute = minutes.Keys.First(m => minutes[m].Guards.Values.Contains(maxCount));
            var guard = minutes[maxMinute].Guards.Keys.First(g => minutes[maxMinute].Guards[g] == maxCount);
            Console.WriteLine(maxMinute * guard);
        }

        private class Minute
        {
            public Dictionary<int, int> Guards { get; set; } = new Dictionary<int, int>();
        }

        private class Guard
        {
            public int ID { get; set; }
            public int SleepTime { get; set; }
            public Dictionary<int, int> Minutes { get; set; } = new Dictionary<int, int>();
        }

        private class Record
        {
            public Record(string line)
            {
                string timeStr = line.Substring(1, 16);
                Text = line.Substring(19);
                Time = DateTime.ParseExact(timeStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            }

            public DateTime Time { get; }
            public string Text { get; }
        }
    }
}
