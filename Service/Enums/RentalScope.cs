//-------------------------------------------------------------------------------------------------
// <copyright file="RentalScope.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Enum representing rental scopes.
    /// </summary>
    [DataContract]
    public enum RentalScope
    {
        /// <summary>
        /// All rentals
        /// </summary>
        [EnumMember]
        All,

        /// <summary>
        /// Only current rentals
        /// </summary>
        [EnumMember]
        Current,
    }
}
