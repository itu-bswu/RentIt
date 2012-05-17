//-------------------------------------------------------------------------------------------------
// <copyright file="RentMovieServiceTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Services;
    using Utils;
    using System.Collections.Generic;
    using RentItService.Entities;
    using System.Linq;
    using RentItService.Enums;
    using RentItService;

    /// <summary>
    /// Tests for RentalManagement.RentMovie.
    /// </summary>
    [TestClass]
    public class RentMovieServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that users can rent movies
        /// 
        /// Steps:
        ///     1. Login to the system
        ///     2. Rent a movie
        ///     3. Verify that the movie was rented
        /// </summary>
        [TestMethod]
        public void RentMovieValidServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            var result = RentalManagement.RentMovie(user.Token, Movie.All.First().Editions.First());
            RentItContext.ReloadDb();

            Assert.IsTrue(result, "RentMovie failed");
            Assert.AreNotEqual(0, user.Rentals.Count, "rentals is null");
        }

        /// <summary>
        /// Purpose: Verify that content providers cannot rent movies
        /// 
        /// Steps:
        ///     1. Login to the system as a content provider
        ///     2. Rent a movie
        ///     3. Verify that the method failed
        /// </summary>
        [TestMethod]
        public void RentMovieContentProviderServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            var result = RentalManagement.RentMovie(user.Token, Movie.All.First().Editions.First());

            Assert.IsFalse(result, "RentMovie didn't fail");
            Assert.AreEqual(0, user.Rentals.Count(), "Movie was rented");
        }
    }
}
