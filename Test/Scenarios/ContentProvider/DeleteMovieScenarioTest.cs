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
        /// Test for successful deletion of a movie.
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
        /// Test for a non-content provider trying to delete a movie
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InsufficientAccessLevelException))]
        public void InsufficientAccessDeleteMovieTest()
        {
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
        /// Test for invalid input when trying to delete a movie.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidInputDeleteMovieTest()
        {
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                var user1 = db.Users.First(u => u.Username == "testContentProvider");

                Movie.DeleteMovie(user1.Token, null);
            }
        }
    }
}
