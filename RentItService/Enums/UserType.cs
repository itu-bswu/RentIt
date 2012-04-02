//-------------------------------------------------------------------------------------------------
// <copyright file="UserType.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Enums
{
    /// <summary>
    /// Enum representing user types and rights.
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// Normal user. 
        /// Can rent movies.
        /// </summary>
        User,

        /// <summary>
        /// Content provider. 
        /// Can upload new movies.
        /// </summary>
        ContentProvider,

        /// <summary>
        /// System administrator. 
        /// Can manage users and content providers.
        /// </summary>
        SystemAdmin
    }
}