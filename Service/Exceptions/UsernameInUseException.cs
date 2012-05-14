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
    /// Exception thrown when trying to create a user, 
    /// with a username already in use
    /// </summary>
    [Serializable]
    public class UsernameInUseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameInUseException"/> class.
        /// </summary>
        public UsernameInUseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameInUseException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public UsernameInUseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameInUseException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        public UsernameInUseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameInUseException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected UsernameInUseException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}