// -----------------------------------------------------------------------
// <copyright file="MostDownloadedScenario.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;

    /// <summary>
    /// Scenario tests for the mostdownloaded feature.
    /// </summary>
    [TestClass]
    public class MostDownloadedScenario
    {
        /// <summary>
        /// Purpose: Verify that it is possible to get a list of rented movies
        /// 
        /// Steps:
        ///     1: Create instances of movies and fill with valid information (see TestHelper.SetupTestMovies).
        ///     2: Create instaces of rentals and fill with valid information (see TestRentalMostDownloaded).
        ///     3: Verify that the first element of the list is the top rented movie.
        /// </summary>
        [TestMethod]
        public void MostDownloadedWithRentals()
        {
            TestHelper.SetUpRentalTestUsers();
            TestHelper.SetUpMoviesForRentalTest();
            TestHelper.TestRentalsMostDownloaded();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testContentRent");
                var result = Movie.MostDownloaded(user.Token);

                // Assert
                Assert.AreEqual(result.First(a => a.Title == "batman"), "batman", "The first element of the list is not the most rented");
            }
        }

        /// <summary>
        /// Purpose: Verify that the service returns the top most rented movies
        ///          when there has been rented more than 10 unique movies.
        /// </summary>
        [TestMethod]
        public void MostDownloadedWithMaxRentals()
        {

        }

        /// <summary>
        /// Purpose: Verify that the service won't return a list of top most
        ///          rented movies if there has been no rentals.
        /// 
        /// Steps:
        ///     1: Create instances of movies and fill with valid information (see TestHelper.SetupTestMovies).
        ///     2:Verify that the MostDownloaded movies list will be empty when there has been no rentals.
        /// </summary>
        [TestMethod]
        public void MostDownloadedWithoutRentals()
        {
            TestHelper.SetUpRentalTestUsers();
            TestHelper.SetUpMoviesForRentalTest();
            TestHelper.TestRentalsMostDownloaded();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUserRent1");
                var result = Movie.MostDownloaded(user.Token);

                // Assert
                Assert.AreEqual(null, result, "The list is not empty");
            }
        }
    }
}
