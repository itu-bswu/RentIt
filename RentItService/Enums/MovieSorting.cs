//-------------------------------------------------------------------------------------------------
// <copyright file="MovieSorting.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Enums
{
    /// <summary>
    /// Enum representing methods for sorting movies
    /// </summary>
    public enum MovieSorting
    {
        /// <summary>
        /// Default sort, a.k.a. don't care
        /// </summary>
        Default,

        /// <summary>
        /// Newest movies first
        /// </summary>
        Newest,

        /// <summary>
        /// Most downloaded first
        /// </summary>
        MostDownloaded,
    }
}
