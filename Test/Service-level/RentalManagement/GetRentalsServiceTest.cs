﻿namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Services;
    using Utils;
    using System.Collections.Generic;
    using RentItService.Entities;
    using System.Linq;
    using RentItService.Enums;
    using RentItService;
    
    [TestClass]
    public class GetRentalsServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can get a users rentals
        /// 
        /// Steps:
        ///     1. Login to the system
        ///     2. Rent a movie
        ///     3. Verify that the movie was rented
        /// </summary>
        [TestMethod]
        public void GetRentalsTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            user.RentMovie(Movie.All.First().Editions.First());
            RentItContext.ReloadDb();

            Rental[] rentals;
            var result = RentalManagement.GetRentals(out rentals, user.Token, RentalScope.All);

            Assert.IsTrue(result, "GetRentals failed");
            Assert.IsNotNull(rentals, "rentals is null");
            Assert.IsTrue(rentals.Any(), "received no rentals");
        }

        /// <summary>
        /// Purpose: Verify that GetRentals fail without a user token
        /// 
        /// Steps:
        ///     1. Get rentals from a null user
        ///     2. Verify that the method failed
        /// </summary>
        [TestMethod]
        public void GetRentalsNullTest()
        {
            Rental[] rentals;
            var result = RentalManagement.GetRentals(out rentals, null, RentalScope.All);

            Assert.IsFalse(result, "GetRentals didn't fail");
        }
    }
}