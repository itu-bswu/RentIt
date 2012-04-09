// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetMovieInformationTest.cs" company="RentIt">
//   Copyright (c) RentIt. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        ///     2. A movie with the name "testMovie1" must exist in the 
        ///        database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Call GetMovieInformation with the token from the user and
        ///        the ID from the "testMovie1".
        ///     3. Assert that the data returned is the same as the data from
        ///        testMovie1.
        /// </summary>
        [TestMethod]
        public void GetMovieInformationTest1()
        {
            Service service = new Service();

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");
                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

                Movie foundMovie = service.GetMovieInformation(testUser.Token, testMovie.ID);

                Assert.AreEqual(testMovie.ID, foundMovie.ID);
                Assert.AreEqual(testMovie.Title, foundMovie.Title);
                Assert.AreEqual(testMovie.Description, foundMovie.Description);
                Assert.AreEqual(testMovie.Genre, foundMovie.Genre);
            }
        }

        /// <summary>
        /// Purpose: Verify that the method throws the correct exception
        /// when calling it with an invalid token.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie with the name "testMovie1" must exist in the 
        ///        database.
        /// <para></para>
        /// Steps:
        ///     1. Assert that pre-conditions hold.
        ///     2. Call GetMovieInformation with the test token and
        ///        the ID from the "testMovie1".
        ///     3. Assert that a UserNotFoundException is thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void InvalidTokenGetMovieInformationTest()
        {
            Service service = new Service();

            string testToken = "Hello thar";

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                Movie testMovie = db.Movies.First(u => u.Title == "testMovie1");

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
        ///     2. Call GetMovieInformation with the user token and
        ///        the test ID.
        ///     3. Assert that the result is null
        /// </summary>
        [TestMethod]
        public void InvalidMovieIdGetMovieInformation()
        {
            Service service = new Service();

            int testID = 178915368;

            using (var db = new RentItContext())
            {
                TestHelper.SetUpTestMovies();
                TestHelper.SetUpTestUsers();

                User testUser = db.Users.First(u => u.Username == "testUser");

                Movie foundMovie = service.GetMovieInformation(testUser.Token, testID);

                Assert.IsNull(foundMovie);
            }
        }
    }
}
