//-------------------------------------------------------------------------------------------------
// <copyright file="IRentalManagement.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities;
    using Library;
    using RentItService.Enums;

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
        /// <param name="downloadRequest">RemoteFileStream containing edition and token</param>
        /// <returns>The remote filestream</returns>
        [OperationContract]
        RemoteFileStream DownloadFile(RemoteFileStream downloadRequest);
    }
}