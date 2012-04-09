//-------------------------------------------------------------------------------------------------
// <copyright file="SearchScenarioTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentItService;
    using RentItService.Services;
    using RentItService.Entities;

    /// <summary>
    /// Scenario tests for searching for movies
    /// </summary>
    [TestClass]
    public class SearchScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that a movie gets returned when the user
        /// searches for its exact title
        /// 
        /// Steps:
        ///     1. Perform a search for the title of a movie in the database
        ///     2. Verify that the movie was returned
        /// </summary>
        [TestMethod]
        public void SearchExcactTitle()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

                // Step 1
                var movies = Movie.Search(user.Token, "testMovie1");

                // Step 2
                Assert.IsTrue(movies.Single().Title.Equals("testMovie1"));
            }
        }

        /// <summary>
        /// Purpose: verify that a movie is returned when a user searches for
        /// a part of the title
        /// 
        /// Steps:
        ///     1. Perform a search for a partly title of a movie in the database
        ///     2. Verify that the movie was returned
        /// </summary>
        [TestMethod]
        public void SearchPartlyTitle()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

                // Step 1
                var movies = Movie.Search(user.Token, "Movie");

                // Step 2
                Assert.IsTrue(movies.Single().Title.Equals("testMovie1"));
            }
        }

        /// <summary>
        /// Purpose: verify that a movie is returned when a user searches for
        /// the title but with incorrect case
        /// 
        /// Steps:
        ///     1. Perform a search for a differently-cased title of a movie in the database
        ///     2. Verify that the movie was returned
        /// </summary>
        [TestMethod]
        public void SearchDifferenceCase()
        {
            TestHelper.SetUpTestUsers();
            TestHelper.SetUpTestMovies();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");
                var movies = Movie.Search(user.Token, "TESTmOVIE1");

                Assert.IsTrue(movies.Single().Title.Equals("testMovie1"));
            }
        }

        /// <summary>
        /// Purpose: verify that an empty collection is returned when the user searches
        /// for a title not in the database
        /// 
        /// Steps:
        ///     1. Perform a search for a movie title that doesn't exists in the database
        ///     2. Verify that an empty collection was returned
        /// </summary>
        [TestMethod]
        public void SearchWithoutResult()
        {
            TestHelper.SetUpTestUsers();

            using (var db = new RentItContext())
            {
                var user = db.Users.First(u => u.Username == "testUser");

                // Step 1
                var movies = Movie.Search(user.Token, "movie");

                // Step 2
                Assert.IsFalse(movies.Any());
            }
        }
    }
}
