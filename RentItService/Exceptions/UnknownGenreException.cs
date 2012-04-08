//-------------------------------------------------------------------------------------------------
// <copyright file="UnknownGenreException.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown when the requested genre was not found.
    /// Used in browsing by genre.
    /// </summary>
    [Serializable]
    public class UnknownGenreException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownGenreException"/> class.
        /// </summary>
        public UnknownGenreException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownGenreException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public UnknownGenreException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownGenreException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        public UnknownGenreException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownGenreException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected UnknownGenreException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}