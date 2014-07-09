namespace AttendanceRecorder.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    [Serializable]
    public class EventCollection
    {
        public ObservableCollection<Day> Days { get; set; }

        public event EventHandler SaveRequested = delegate { };

        public EventCollection()
        {
            this.Days = new ObservableCollection<Day>();
        }

        public void Add(SystemState oldState, SystemState newState)
        {
            var day = Days.FirstOrDefault((inner) => inner.Date.Date == DateTime.Now.Date);
            if (day == null)
            {
                DeleteNowEventOfLastDay();

                day = new Day { Date = DateTime.Now.Date };
                Days.Add(day);
            }

            // Delete previous event if it is a "Now" event
            DeleteNowEvent(day);

            day.Events.Add(new EventItem { OldState = oldState, NewState = newState, Time = DateTime.Now });

            // Delete old day entries
            while (Days[0].Date.Date.AddYears(1) <= DateTime.Now.Date)
            {
                Days.Remove(Days[0]);
            }

            SaveRequested.Invoke(this, EventArgs.Empty);
        }

        private static void DeleteNowEvent(Day day)
        {
            var nowEvent = day.Events.LastOrDefault((innerEvent) => innerEvent.NewState == SystemState.Now);
            if (nowEvent != null)
            {
                day.Events.Remove(nowEvent);
            }
        }

        private void DeleteNowEventOfLastDay()
        {
            var lastDay = Days.LastOrDefault();
            if (lastDay != null)
            {
                DeleteNowEvent(lastDay);
            }
        }

        public void Delete(EventItem item)
        {
            var day = Days.FirstOrDefault((innerDay) => innerDay.Events.Any((innerItem) => innerItem == item));
            if (day != null)
            {
                day.Events.Remove(item);
                if (day.Events.Count == 0)
                {
                    Days.Remove(day);
                }

                SaveRequested.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
