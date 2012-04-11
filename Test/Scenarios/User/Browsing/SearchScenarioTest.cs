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
        ///     2. Verify that the movie was returned as the first one
        /// </summary>
        [TestMethod]
        public void SearchExcactTitle()
        {
            using (var db = new RentItContext())
            {
                // Step 1
                var movies = Movie.Search("The Matrix");

                // Step 2
                Assert.IsTrue(movies.First().Title.Equals("The Matrix"));
            }
        }

        /// <summary>
        /// Purpose: verify that a movie is returned when a user searches for
        /// a part of the title
        /// 
        /// Steps:
        ///     1. Perform a search for a partly title of some movies in the database
        ///     2. Verify that the movies was returned
        /// </summary>
        [TestMethod]
        public void SearchPartlyTitle()
        {
            using (var db = new RentItContext())
            {
                // Step 1
                var movies = Movie.Search("ing").ToList();

                // Step 2
                Assert.IsTrue(movies.Any(movie => movie.Title.Equals("The Lord of the Rings: The Fellowship of the Ring")));
                Assert.IsTrue(movies.Any(movie => movie.Title.Equals("The Lord of the Rings: The Return of the King")));
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
            using (var db = new RentItContext())
            {
                var movies = Movie.Search("tHE mATRIX");

                Assert.IsTrue(movies.First().Title.Equals("The Matrix"));
            }
        }

        /// <summary>
        /// Purpose: verify that an empty collection is returned when the user searches
        /// for a title not in the database
        /// 
        /// Steps:
        ///     1. Generate a string that doesn't exist in any title in the database
        ///     2. Verify that an empty collection is returned from a search
        /// </summary>
        [TestMethod]
        public void SearchWithoutResult()
        {
            string randString;
            var rand = new Random();

            // Step 1
            using (var db = new RentItContext())
            {
                do
                {
                    randString = rand.NextDouble().ToString("0.00");
                } while (db.Movies.ToList().Any(movie => movie.Title.Contains(randString)));
            }

            // Step 2
            Assert.IsFalse(Movie.Search(randString).Any());
        }
    }
}
