// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoMovieFoundException.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// <summary>
//   Exception thrown when the requested movie wasn't found
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RentItService.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown when the requested movie wasn't found
    /// </summary>
    [Serializable]
    public class NoMovieFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoMovieFoundException"/> class.
        /// </summary>
        public NoMovieFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoMovieFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public NoMovieFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoMovieFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        public NoMovieFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoMovieFoundException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected NoMovieFoundException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}