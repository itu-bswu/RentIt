// -----------------------------------------------------------------------
// <copyright file="UserType.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Types
{
    /// <summary>
    /// The different types of users.
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// User of the system.
        /// </summary>
        User,

        /// <summary>
        /// A content provider for the system.
        /// </summary>
        ContentProvider,

        /// <summary>
        /// Administrator of the system.
        /// </summary>
        Admin
    }
}
