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
    public class MostDownloadedScenario : DataTest
    {
        /// <summary>
        /// Purpose: Verify that it is possible to get a list of rented movies
        /// 
        /// Steps:
        ///     1: Create instances of movies and fill with valid information (see TestHelper.SetupTestMovies).
        ///     2: Create instances of user and fill with valid information ( see TestHelper.SetupMoviesForRentalTest).
        ///     3: Create instaces of rentals and fill with valid information (see TestRentalMostDownloaded).
        ///     4: Verify that the first element of the list is the top rented movie.
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

                Assert.AreEqual(3, db.Rentals.Count(r => r.MovieID == 1));

                // Assert
                Assert.AreEqual(1, result.First().ID, "The first element of the list is not the most rented");
            }
        }

        /// <summary>
        /// Purpose: Verify that the service will return the same list to multiple users.
        /// 
        /// Steps:
        ///     1: Create instances of movies and fill with valid information (see TestHelper.SetupTestMovies).
        ///     2: Create instances of user and fill with valid information (see TestHelper.SetupRentalTestUsers).
        ///     3:Verify that the MostDownloaded movies list will be same for multiple users.
        /// </summary>
        [TestMethod]
        public void MostDownloadedWithTwoUsers()
        {
            TestHelper.SetUpRentalTestUsers();
            TestHelper.SetUpMoviesForRentalTest();

            using (var db = new RentItContext())
            {
                User user1 = db.Users.First(u => u.Username == "testUserRent1");

                User user2 = db.Users.First(u => u.Username == "testUserRent2");

                var result1 = Movie.MostDownloaded(user1.Token).ToList();

                var result2 = Movie.MostDownloaded(user2.Token).ToList();

                // Assert
                Assert.AreEqual(result1.Count, result2.Count, "The list is not the same for two diffrent users");
            }
        }
    }
}
