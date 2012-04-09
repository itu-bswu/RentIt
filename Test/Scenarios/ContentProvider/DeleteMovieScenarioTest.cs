// -----------------------------------------------------------------------
// <copyright file="DeleteMovieScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.ContentProvider
{
    using System;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt.Tests.Utils;

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
        ///     1. A movie with the description "The Matrix" must exist in the database.
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
            FileInfo fi;

            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Movies.Any(m => m.Description.Equals("The Matrix")), "Pre-condition 1 does not hold.");

                var testMovie = db.Movies.First(m => m.Description.Equals("The Matrix"));

                var user1 = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

                fi = new FileInfo(testMovie.FilePath);

                Movie.DeleteMovie(user1.Token, testMovie);
            }

            using (var db = new RentItContext())
            {
                Assert.IsFalse(db.Movies.Any(m => m.Description.Equals("The Matrix")), "Movie is still in the database.");
            }

            Assert.IsFalse(fi.Exists, "The file still exists in the file system.");
        }

        /// <summary>
        /// Purpose: Verify that only Content Providers can delete movies.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A movie with the description "The Matrix" must exist in the database.
        ///     2. A user with the user name "Smith" must exist in the database.
        /// <para></para>
        /// Steps:
        ///     1. Make sure pre-conditions hold.
        ///     2. Try to delete the movie as the user.
        ///     3. Verify that InsufficientAccessLevelException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InsufficientAccessDeleteMovieTest()
        {
            using (var db = new RentItContext())
            {
                Assert.IsTrue(db.Movies.Any(m => m.Description.Equals("The Matrix")), "Pre-condition 1 does not hold.");

                var testMovie = db.Movies.First(m => m.Description.Equals("The Matrix"));

                var user1 = User.Login(TestUser.User.Username, TestUser.User.Password);

                Movie.DeleteMovie(user1.Token, testMovie);
            }
        }

        /// <summary>
        /// Purpose: Verify that it is not possible to delete a null movie.
        /// <para></para>
        /// Pre-condtions:
        ///     1. A user with the user name "Universal" must exist in the database.
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
                var user1 = User.Login(TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

                Movie.DeleteMovie(user1.Token, null);
            }
        }
    }
}
