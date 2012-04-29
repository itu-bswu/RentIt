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
            using (var db = new RentItContext())
            {
                var set = new HashSet<string>();

                // Step 1
                var genres = Movie.GetAllGenres().ToList();

                // Step 2
                foreach (var genre in db.Movies.ToList().SelectMany(movie => movie.Genres))
                {
                    set.Add(genre);
                }

                // Step 3
                Assert.AreEqual(set.Count(), genres.Count());

                // Step 4
                Assert.IsTrue(genres.All(set.Contains));
            }
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
            List<Movie> dbmovies;

            using (var db = new RentItContext())
            {
                dbmovies = db.Movies.ToList();
            }

            var testGenre = dbmovies.First().Genres.First();

            // Step 1
            var movies = Movie.ByGenre(testGenre).ToList();

            // Step 2
            Assert.IsTrue(movies.All(movie => movie.Genres.Contains(testGenre)));

            // Step 3
            var movieCount = dbmovies.Count(movie => movie.Genres.Contains(testGenre));

            // Step 4
            Assert.AreEqual(movieCount, movies.Count());
        }

        /// <summary>
        /// Pursose: verify that trying to get movies with an unknown genre
        /// throws an exception
        /// 
        /// Steps:
        ///     1. Generate a string that doesn't exist as a genre in the database
        ///     2. Load all movies with a specific genre
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UnknownGenreException))]
        public void BrowseUnknownGenreTest()
        {
            string randString;
            var rand = new Random();

            // Step 1
            using (var db = new RentItContext())
            {
                do
                {
                    randString = rand.NextDouble().ToString("0.00");
                } while (db.Movies.ToList().Any(movie => movie.Genres.Any(genre => genre.Equals(randString))));
            }

            // Step 2
            Movie.ByGenre(randString);
        }
    }
}
