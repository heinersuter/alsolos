namespace Alsolos.AttendanceRecorder.WebApiModel
{
    using System;
    using Newtonsoft.Json;

    [JsonConverter(typeof(DateConverter))]
    public class Date : IComparable<Date>
    {
        private readonly DateTime _dateTime;

        public Date(DateTime date)
        {
            _dateTime = date;
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
        }

        public Date(int year, int month, int day)
        {
            _dateTime = new DateTime(year, month, day);
            Year = _dateTime.Year;
            Month = _dateTime.Month;
            Day = _dateTime.Day;
        }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }

        public DateTime GetDateTime()
        {
            return _dateTime;
        }

        protected bool Equals(Date other)
        {
            return Year == other.Year && Month == other.Month && Day == other.Day;
        }

        public int CompareTo(Date other)
        {
            if (this < other)
            {
                return -1;
            }
            if (this > other)
            {
                return 1;
            }
            return 0;
        }

        public int CompareTo(object obj)
        {
            return CompareTo((Date)obj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Date)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ Month;
                hashCode = (hashCode * 397) ^ Day;
                return hashCode;
            }
        }

        public static bool operator ==(Date left, Date right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Date left, Date right)
        {
            return !Equals(left, right);
        }

        public static bool operator >(Date left, Date right)
        {
            return left.Year > right.Year ||
                (left.Year == right.Year && left.Month > right.Month) ||
                (left.Year == right.Year && left.Month == right.Month && left.Day > right.Day);
        }

        public static bool operator <(Date left, Date right)
        {
            return left.Year < right.Year ||
                (left.Year == right.Year && left.Month < right.Month) ||
                (left.Year == right.Year && left.Month == right.Month && left.Day < right.Day);
        }

        public static bool operator >=(Date left, Date right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(Date left, Date right)
        {
            return left < right || left == right;
        }
    }
}