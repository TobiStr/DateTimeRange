namespace System
{
    public class DateTimeInvalidRangeException : Exception
    {
        public DateTimeInvalidRangeException(string message) : base(message)
        {
        }

        public DateTimeInvalidRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
