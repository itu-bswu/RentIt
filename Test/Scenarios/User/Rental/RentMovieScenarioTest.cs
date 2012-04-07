//-------------------------------------------------------------------------------------------------
// <copyright file="RentMovieScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Rental
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario test for the Rent Movie functionality.
    /// </summary>
    [TestClass]
    public class RentMovieScenarioTest : DataTest
    {
        /// <summary>
        /// Tests a successful rent of a movie.
        /// </summary>
        [TestMethod]
        public void RentMovieTest()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            string testToken;
            int testID;
            int testTokenID;

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testUser");
                Movie movie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                testToken = user.Token;
                testID = movie.ID;
                testTokenID = User.GetByToken(testToken).ID;
                Assert.IsFalse(db.Rentals.Any(r => r.UserID == testTokenID & r.MovieID == testID));

                User.RentMovie(testToken, testID);
            }

            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Rentals.Any(r => r.UserID == testTokenID & r.MovieID == testID));

                db.Rentals.Remove(db.Rentals.First(r => r.UserID == testTokenID & r.MovieID == testID));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Tests for a rent where the user renting is not of type "User".
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotAUserException))]
        public void NotAUserRentMovieTest()
        {
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                User user = db.Users.First(u => u.Username == "testContentProvider");
                Movie movie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                string testToken = user.Token;
                int testID = movie.ID;

                User.RentMovie(testToken, testID);
            }
        }

        /// <summary>
        /// Tests for invalid input in a rent movie action.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputRentMovieTest()
        {
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                Movie movie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                int testID = movie.ID;

                User.RentMovie(null, testID);
            }
        }
    }
}
