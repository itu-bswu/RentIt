//-------------------------------------------------------------------------------------------------
// <copyright file="InsufficientAccessLevelException.cs" company="RentIt">
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
    public class InsufficientAccessLevelException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientAccessLevelException"/> class.
        /// </summary>
        public InsufficientAccessLevelException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientAccessLevelException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public InsufficientAccessLevelException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientAccessLevelException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        public InsufficientAccessLevelException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientAccessLevelException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected InsufficientAccessLevelException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}