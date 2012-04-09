// -----------------------------------------------------------------------
// <copyright file="DeleteMovieScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentProvider
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests for the delete movie functionality.
    /// </summary>
    [TestClass]
    public class DeleteMovieScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that deletion of a movie is possible.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie with the description "testMovie1" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Get a content provider user.
        ///     3. Attempt to delete the movie.
        ///     4. Verify that the movie no longer exists in the database.
        ///     5. Add the movie back into the database.
        /// </summary>
        [TestMethod]
        public void DeleteMovieTest()
        {
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Movies.Any(m => m.Description.Equals("testMovie1")));

                var testMovie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                var user1 = db.Users.First(u => u.Username == "testContentProvider");

                Movie.DeleteMovie(user1.Token, testMovie);
            }

            using (var db = new RentItContext())
            {
                Assert.IsFalse(db.Movies.Any(m => m.Description.Equals("testMovie1")));
            }

            TestHelper.SetUpTestMovies();
        }

        /// <summary>
        /// Purpose: Verify that only Content Providers can delete movies.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie with the description "testMovie1" must exist in the database.
        ///     2. A user with the user name "testUser" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Try to delete the movie as the user.
        ///     3. Verify that InsufficientRightsException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientRightsException))]
        public void InsufficientAccessDeleteMovieTest()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Movies.Any(m => m.Description.Equals("testMovie1")));

                var testMovie = db.Movies.First(m => m.Description.Equals("testMovie1"));

                var user1 = db.Users.First(u => u.Username == "testUser");

                Movie.DeleteMovie(user1.Token, testMovie);
            }
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to delete a null movie.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user with the user name "testContentProvider" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Try to delete a null movie.
        ///     3. Verify ArgumentNullException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputDeleteMovieTest()
        {
            TestHelper.SetUpTestUsers();

            using (var db = new RentItContext())
            {
                var user1 = db.Users.First(u => u.Username == "testContentProvider");

                Movie.DeleteMovie(user1.Token, null);
            }
        }
    }
}
