//-------------------------------------------------------------------------------------------------
// <copyright file="GenreScenarioTest.cs" company="RentIt">
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
    using System.Collections.Generic;

    /// <summary>
    /// Scenario tests for browsing movies by genre
    /// </summary>
    [TestClass]
    public class GenreScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: verify that all genres in the database gets
        /// returned by GetAllGenres
        /// 
        /// Steps:
        ///     1. Get all genres from the database
        ///     2. Get all movies from the database
        ///     3. Check that the number of genres match
        ///     4. Check that all movie genres are in the result
        /// </summary>
        [TestMethod]
        public void GetAllGenresTest()
        {
            // Step 1
            var genres = Genre.All().ToList();

            Assert.IsTrue(genres.Any(), "There are no genres in the data set.");

            var set = new HashSet<string>();

            // Step 2
            foreach (var genre in Genre.All())
            {
                set.Add(genre);
            }

            // Step 3
            Assert.AreEqual(set.Count(), genres.Count(), "Not the same number of genres returned");

            // Step 4
            Assert.IsTrue(genres.All(g => set.Count(g.Equals) == 1), "Not the same genres returned");
        }

        /// <summary>
        /// Purpose: verify that when the user browses for a specific
        /// known genre, all movies with that genre gets returned
        /// 
        /// Steps:
        ///     1. Get all movies for a specific genre
        ///     2. Verify that all the returned movies has that genre
        ///     3. Get the number of movies in the database with the genre
        ///     4. Verify that the number of movies is equal
        /// </summary>
        [TestMethod]
        public void BrowseKnownGenreTest()
        {
            Assert.IsTrue(Movie.All().First().Genres.Any(), "First move has no genres.");

            var testGenre = Movie.All().First().Genres.First().Name;

            // Step 1
            var movies = Movie.ByGenre(testGenre).ToList();

            // Step 2
            Assert.IsTrue(movies.All(movie => movie.HasGenre(testGenre)), "A movie doesn't have the genre.");

            // Step 3
            var movieCount = Movie.All().Count(movie => movie.HasGenre(testGenre));

            // Step 4
            Assert.AreEqual(movieCount, movies.Count(), "Not the same number of movies returned.");
        }
    }
}
