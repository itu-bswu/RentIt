// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetMovieInformationTest.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;
    using RentItService.Services;

    /// <summary>
    /// Class for testing the GetMovieInformation method
    /// </summary>
    [TestClass]
    public class GetMovieInformationTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that the method returns the correct data.
        /// <para></para>
        /// Pre-condtions:
        ///     1. At least one user must exist in the database.
        ///     2. A movie must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Login in with the user
        ///     3. Find a movie in the database.
        ///     4. Call GetMovieInformation with the token from the user and
        ///        the ID from the movie found.
        ///     5. Assert that the data returned is the same as the data from
        ///        testMovie1.
        /// </summary>
        [TestMethod]
        public void GetMovieInformationValidTest()
        {
            var service = new Service();

            using (var db = new RentItContext())
            {
                var testUser = TestUser.User;
                var testMovie = db.Movies.First();
                var loggedinUser = User.Login(testUser.Username, testUser.Password);

                var foundMovie = service.GetMovieInformation(loggedinUser.Token, testMovie.ID);

                Assert.AreEqual(testMovie.ID, foundMovie.ID, "The IDs doesn't match");
                Assert.AreEqual(testMovie.Title, foundMovie.Title, "The title doesn't match");
                Assert.AreEqual(testMovie.Description, foundMovie.Description, "The description doesn't match");
                Assert.AreEqual(testMovie.Genre, foundMovie.Genre, "The genre doesn't match");
            }
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when calling it with an invalid token.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie must exist in the databse
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Find a movie in the database.
        ///     3. Call GetMovieInformation with the test token and
        ///        the ID from the found movie.
        ///     4. Assert that a UserNotFoundException is thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetMovieInformationInvalidTokenTest()
        {
            var service = new Service();

            string testToken = "Hello thar";

            using (var db = new RentItContext())
            {
                var testMovie = db.Movies.First();

                service.GetMovieInformation(testToken, testMovie.ID);
            }
        }

        /// <summary>
        /// Purpose: Verify that the method returns null when called with
        /// a movie ID that doesn't corrospond to a movie in the database
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user must exist in the databse.
        ///     2. The test ID must not corrospond to a a movie in the
        ///        databse.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Login with the user.
        ///     3. Call GetMovieInformation with the user token and
        ///        the test ID.
        ///     4. Assert that the result is null.
        /// </summary>
        [TestMethod]
        public void GetMovieInformationInvalidMovieIdTest()
        {
            var service = new Service();

            int testID = 178915368;

            using (var db = new RentItContext())
            {
                var testUser = TestUser.User;
                var loggedinUser = User.Login(testUser.Username, testUser.Password);

                Movie foundMovie = service.GetMovieInformation(loggedinUser.Token, testID);

                Assert.IsNull(foundMovie);
            }
        }
    }
}