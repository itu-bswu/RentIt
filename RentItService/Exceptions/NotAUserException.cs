//-------------------------------------------------------------------------------------------------
// <copyright file="NotAUserException.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown when the requested user was not found.
    /// Used in login and token validation.
    /// </summary>
    [Serializable]
    public class NotAUserException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotAUserException"/> class.
        /// </summary>
        public NotAUserException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotAUserException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public NotAUserException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotAUserException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        public NotAUserException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotAUserException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected NotAUserException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}