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
            // Step 1
            var movies = Movie.Search("The Matrix").ToList();

            // Step 2
            Assert.IsTrue(movies.First().Title.Equals("The Matrix"), "Movie was not found.");
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
            // Step 1
            var movies = Movie.Search("ing").ToList();

            // Step 2
            Assert.IsTrue(movies.Any(movie => movie.Title.Equals("The Lord of the Rings: The Fellowship of the Ring")), "First movie was not found.");
            Assert.IsTrue(movies.Any(movie => movie.Title.Equals("The Lord of the Rings: The Return of the King")), "Second movie was not found.");
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
            var movies = Movie.Search("tHE mATRIX");

            Assert.IsTrue(movies.First().Title.Equals("The Matrix"), "Movie was not found.");
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
                }
                while (db.Movies.ToList().Any(movie => movie.Title.Contains(randString)));
            }

            // Step 2
            Assert.IsFalse(Movie.Search(randString).Any(), "One or more movies was returned, should not return any.");
        }

        /// <summary>
        /// Purpose: verify that a movie will be returned, even if tokens not in the name
        /// is part of the search string
        /// 
        /// Steps:
        ///     1. Search for a movie title with an extra token added
        ///     2. Verify that the movie is returned
        /// </summary>
        [TestMethod]
        public void SearchMoreTokens()
        {
            var movies = Movie.Search("The Mask <random token>");

            Assert.IsTrue(movies.Any(movie => movie.Title.Equals("The Mask")), "Movie was not found.");
        }

        /// <summary>
        /// Purpose: verify that the exact movie title match will be ordered before a partly match
        /// 
        /// Steps:
        ///     1. Search for a specific move title, which will also return partly matches
        ///     2. Find the index of the exactly matched movie and the partly matched movie
        ///     3. Verify that the exact match is at a lower index in the list
        /// </summary>
        [TestMethod]
        public void SearchOrder()
        {
            var movies = Movie.Search("Die Hard").ToList();
            var index1 = movies.FindIndex(movie => movie.Title.Equals("Die Hard"));
            var index2 = movies.FindIndex(movie => movie.Title.Equals("Die Hard 2"));

            Assert.IsTrue(index1 < index2, "Movie order incorrect.");
        }

        /// <summary>
        /// Purpose: verify that the search results is ordered by the number of token matches
        /// 
        /// Steps:
        ///     1. Search for a title that contains tokens from multiple movies
        ///     2. Verify that the first movie has more token matches than the second
        /// </summary>
        [TestMethod]
        public void SearchTokenMatchCountOrder()
        {
            var movies = Movie.Search("Pie 2").ToList();
            var index1 = movies.FindIndex(movie => movie.Title.Equals("American Pie 2"));
            var index2 = movies.FindIndex(movie => movie.Title.Equals("American Pie"));

            Assert.IsTrue(index1 < index2, "Movie order incorrect.");
        }

        /// <summary>
        /// Purpose: verify that search results includes movies with spelling errors in the title
        /// 
        /// Steps:
        ///     1. Search for a movie with title spelling errors
        ///     2. Verify that the movie was returned
        /// </summary>
        [TestMethod]
        public void SearchBadSpelling()
        {
            var movies = Movie.Search("grinsh stoe cistmas").ToList();

            Assert.IsTrue(movies.Any(movie => movie.Title.Equals("How the Grinch Stole Christmas")));
        }

        /// <summary>
        /// Purpose: verify that the search result does not include words that are too badly spelled
        /// 
        /// Steps:
        ///     1. Search for a very bad misspelling of a movie title
        ///     2. Verify that the movie was not returned
        /// </summary>
        [TestMethod]
        public void SearchVeryBadSpelling()
        {
            var movies = Movie.Search("graounz spove sismast").ToList();

            Assert.IsFalse(movies.Any(movie => movie.Title.Equals("How the Grinch Stole Christmas")));
        }

        /// <summary>
        /// Purpose: verify that putting a limit on the search results actually limits the number of returned movies
        /// 
        /// Steps:
        ///     1. Perform a search and note the number of movies returned
        ///     2. Perform the search again with a limit on the number found in step 1 subtracted by one
        ///     3. Verify that the number of movies returned matches the limit
        /// </summary>
        [TestMethod]
        public void SearchLimit()
        {
            const string search_string = "the";

            var movie_count = Movie.Search(search_string).Count();

            var search_limit = movie_count - 1;

            Assert.AreEqual(search_limit, Movie.Search(search_string, search_limit).Count());
        }
    }
}
