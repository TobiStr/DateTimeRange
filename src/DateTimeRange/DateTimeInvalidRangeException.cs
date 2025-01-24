namespace System
{
    /// <summary>
    /// Represents an exception that is thrown when an invalid or inconsistent date and time range is encountered.
    /// </summary>
    public class DateTimeInvalidRangeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeInvalidRangeException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DateTimeInvalidRangeException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeInvalidRangeException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.
        /// </param>
        public DateTimeInvalidRangeException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeInvalidRangeException"/> class with a specified error message.
        /// </summary>
        public DateTimeInvalidRangeException()
            : base() { }
    }
}
