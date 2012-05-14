//-------------------------------------------------------------------------------------------------
// <copyright file="RentalManagement.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System.Collections.Generic;
    using Entities;
    using FunctionClasses;
    using Interfaces;
    using Library;
    using RentItService.Enums;

    /// <summary>
    /// The download service class.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public partial class Service : IRentalManagement
    {
        /// <summary>
        /// Get the current user's rentals
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="scope">The scope of rentals to get</param>
        /// <param name="rentals">The found rentals</param>
        /// <returns>Wether the request succeeded or not</returns>
        public bool GetRentals(string token, RentalScope scope, out IEnumerable<Rental> rentals)
        {
            if (token == null || User.GetByToken(token) == null)
            {
                rentals = null;
                return false;
            }

            rentals = (scope == RentalScope.Current ? User.GetCurrentRentals(token) : User.GetRentalHistory(token));

            return true;
        }

        /// <summary>
        /// Rents a movie
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="edition">The edition to rent</param>
        /// <returns>Wether the request succeeded or not</returns>
        public bool RentMovie(string token, Edition edition)
        {
            if (token == null || User.GetByToken(token) == null)
            {
                return false;
            }

            User.RentMovie(token, edition.ID);

            return true;
        }

        /// <summary>
        /// Downloads a rented movie file.
        /// </summary>
        /// <param name="token">The user token</param>
        /// <param name="edition">The edition to download</param>
        /// <param name="stream">A filestream for downloading the movie</param>
        /// <returns>Wether the request succeeded or not</returns>
        public bool DownloadFile(string token, Edition edition, out RemoteFileStream stream)
        {
            if (token == null || User.GetByToken(token) == null)
            {
                stream = null;
                return false;
            }

            stream = UploadDownload.DownloadFile(token, edition);

            return true;
        }
    }
}