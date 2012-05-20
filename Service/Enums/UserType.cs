//-------------------------------------------------------------------------------------------------
// <copyright file="UserType.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Enum representing user types and rights.
    /// </summary>
    [DataContract]
    public enum UserType
    {
        /// <summary>
        /// Normal user. 
        /// Can rent movies.
        /// </summary>
        [EnumMember]
        User = 1,

        /// <summary>
        /// Content provider. 
        /// Can upload new movies.
        /// </summary>
        [EnumMember]
        ContentProvider = 2,

        /// <summary>
        /// System administrator. 
        /// Can manage users and content providers.
        /// </summary>
        [EnumMember]
        SystemAdmin = 3,
    }
}