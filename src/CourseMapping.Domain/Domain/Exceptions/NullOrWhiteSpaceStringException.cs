using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CourseMapping.Domain.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class NullOrWhiteSpaceStringException : ArgumentException
    {
        // constructors...
        #region NullOrWhiteSpaceStringException()
        /// <summary>
        /// Constructs a new NullOrWhiteSpaceStringException.
        /// </summary>
        public NullOrWhiteSpaceStringException() { }
        #endregion
        #region NullOrWhiteSpaceStringException(string message)
        /// <summary>
        /// Constructs a new NullOrWhiteSpaceStringException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public NullOrWhiteSpaceStringException(string message) : base(message) { }
        #endregion
        #region NullOrWhiteSpaceStringException(string message, Exception innerException)
        /// <summary>
        /// Constructs a new NullOrWhiteSpaceStringException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public NullOrWhiteSpaceStringException(string message, Exception innerException) : base(message, innerException) { }
        #endregion
        #region NullOrWhiteSpaceStringException(SerializationInfo info, StreamingContext context)
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        protected NullOrWhiteSpaceStringException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
