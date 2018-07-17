namespace GeekLearning.Domain
{
    using System;
    using System.Globalization;

    public struct Date
    {
        public Date(int year, int month, int day)
        {
            DateTime = new DateTime(year, month, day);
            Month = month;
            Day = day;
            Year = year;
        }

        public int Year { get; }

        public int Month { get; }

        public int Day { get; }

        public DateTime DateTime { get; }

        public string ToString(string format) => DateTime.ToString(format);

        public override string ToString() => DateTime.ToString(CultureInfo.InvariantCulture);

        public static bool operator ==(Date dt, Date other) => IsEqualTo(dt, other);

        public static bool operator !=(Date dt, Date other) => dt.Year != other.Year || dt.Month != other.Month || dt.Day != other.Day;

        public static bool operator <(Date dt, Date other) => dt.DateTime < other.DateTime;

        public static bool operator >(Date dt, Date other) => dt.DateTime > other.DateTime;

        public static TimeSpan operator -(Date dt, Date other) => dt.DateTime - other.DateTime;

        private static bool IsEqualTo(Date dt, Date other) => dt.Day == other.Day && dt.Month == other.Month && dt.Year == other.Year;

        public override bool Equals(object obj)
        {
            if (!(obj is Date))
            {
                throw new ArgumentException($"{nameof(obj)} should be of Date type");
            }

            return IsEqualTo(this, (Date)obj);
        }

        public override int GetHashCode() => DateTime.GetHashCode();
    }
}
