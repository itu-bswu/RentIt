//-------------------------------------------------------------------------------------------------
// <copyright file="MovieSorting.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Enum representing methods for sorting movies
    /// </summary>
    [DataContract]
    public enum MovieSorting
    {
        /// <summary>
        /// Default sort, a.k.a. don't care
        /// </summary>
        [EnumMember]
        Default,

        /// <summary>
        /// Newest movies first
        /// </summary>
        [EnumMember]
        Newest,

        /// <summary>
        /// Most downloaded first
        /// </summary>
        [EnumMember]
        MostDownloaded,
    }
}
