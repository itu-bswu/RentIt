//-------------------------------------------------------------------------------------------------
// <copyright file="BrowseByGenreScenario.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Exceptions;

    /// <summary>
    /// Scenario tests for browsing movies by genre
    /// </summary>
    [TestClass]
    public class BrowseByGenreScenario
    {
        /// <summary>
        /// Purpose: verify that when the user browses for a specific
        /// known genre, all movies with that genre gets returned
        /// 
        /// Steps:
        ///     1. Get all movies for a specific genre
        ///     2. Verify the number of movies returned and their genre
        /// </summary>
        [TestMethod]
        public void BrowseKnownGenreTest()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();
            const string testGenre = "testGenre";

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

                // Step 1
                var movies = Movie.ByGenre(user.Token, testGenre);

                // Step 2
                Assert.IsTrue(movies.Single().Genre == testGenre);
            }
        }

        /// <summary>
        /// Pursose: verify that trying to get movies with an unknown genre
        /// throws an exception
        /// 
        /// Steps:
        ///     1. Verify that there is no movies in the database, and therefore no genres
        ///     2. Load all movies with a specific genre
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnknownGenreException))]
        public void BrowseUnknownGenreTest()
        {
            TestHelper.SetUpTestUsers();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

                // Step 1
                Assert.IsFalse(db.Movies.Any());

                // Step 2
                Movie.ByGenre(user.Token, "testGenre");
            }
        }
    }
}
