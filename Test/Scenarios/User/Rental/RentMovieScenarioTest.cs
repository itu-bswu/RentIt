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
        /// Purpose: Verify that it is possible to rent a movie.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user called "testUser" exists in the database.
        ///     2. A movie called "testMovie1" exists in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure the user and movie exists in the database.
        ///     2. Make sure the rental does not already exist.
        ///     3. Rent movie.
        ///     4. Make sure the new rental is in database.
        ///     5. Remove rental from database.
        /// </summary>
        [TestMethod]
        public void RentMovieTest()
        {
            // Arrange
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
                Assert.IsFalse(db.Rentals.Any(r => r.UserID == testTokenID & r.MovieID == testID), "Rental exists before call of RentMovie.", "Rental already exists in the database, so test is not valid.");
            }

            // Act
            User.RentMovie(testToken, testID);

            // Assert and clean up
            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Rentals.Any(r => r.UserID == testTokenID & r.MovieID == testID), "The rental was not created.");

                db.Rentals.Remove(db.Rentals.First(r => r.UserID == testTokenID & r.MovieID == testID));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Purpose: Verify that only users can rent movies.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user with user name "testContentProvider" must exist in the database.
        ///     2. A movie with the title "testMovie1" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure the movie and the user exist in the database.
        ///     2. Attempt to rent the movie.
        ///     3. Catch the expected NotAUserException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotAUserException))]
        public void NotAUserRentMovieTest()
        {
            TestHelper.SetUpTestUsers();
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
        /// Purpose: 
        /// <para></para>
        /// Pre-condtions:
        ///     1.
        /// <para></para>
        /// Steps:
        ///     1.
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
