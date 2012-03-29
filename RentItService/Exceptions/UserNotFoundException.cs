//-------------------------------------------------------------------------------------------------
// <copyright file="UserNotFoundException.cs" company="RentIt">
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
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException"/> class.
        /// </summary>
        public UserNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public UserNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        public UserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected UserNotFoundException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}