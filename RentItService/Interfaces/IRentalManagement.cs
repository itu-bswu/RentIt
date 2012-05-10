//-------------------------------------------------------------------------------------------------
// <copyright file="IRentalManagement.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace RentItService.Interfaces
{
    using System;
    using System.Diagnostics.Contracts;
    using System.ServiceModel;
    using Entities;
    using Library;

    /// <summary>
    /// Interface for rental management.
    /// </summary>
    [ServiceContract]
    public interface IRentalManagement
    {
        /// <summary>
        /// Get the current user's rentals
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="scope">The scope of rentals to get</param>
        /// <param name="rentals">The found rentals</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool GetRentals(string token, RentalScope scope, out IEnumerable<Rental> rentals);

        /// <summary>
        /// Rents a movie
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="edition">The edition to rent</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool RentMovie(string token, Edition edition);

        /// <summary>
        /// Downloads a rented movie file.
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="edition">The edition to download</param>
        /// <param name="stream">A filestream for downloading the movie</param>
        /// <returns>Wether the request succeeded or not</returns>
        [OperationContract]
        bool DownloadFile(string token, Edition edition, out RemoteFileStream stream);
    }
}