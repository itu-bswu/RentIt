﻿//-------------------------------------------------------------------------------------------------
// <copyright file="RentalManagement.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
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

            var user = User.GetByToken(token);
            rentals = (scope == RentalScope.Current ? user.GetCurrentRentals() : user.GetRentalHistory());

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

            User.GetByToken(token).RentMovie(edition);

            return true;
        }

        /// <summary>
        /// Downloads a rented movie file.
        /// </summary>
        /// <param name="downloadRequest">RemoteFileStream containing edition and token</param>
        /// <returns>The remote filestream</returns>
        public RemoteFileStream DownloadFile(RemoteFileStream downloadRequest)
        {
            if (downloadRequest == null)
            {
                return null;
            }

            var token = downloadRequest.Token;
            var edition = downloadRequest.Edition;
            if (token == null || edition == null)
            {
                return null;
            }

            var user = User.GetByToken(token);
            var downloadEdition = Edition.Get(user, edition.ID);
            if (user == null || downloadEdition == null ||
                !user.Rentals.Any(r => r.EditionID == downloadEdition.ID && r.UserID == user.ID))
            {
                return null;
            }

            return downloadEdition.Download(user);
        }
    }
}