//-------------------------------------------------------------------------------------------------
// <copyright file="BrowseNewestScenarioTest.cs" company="RentIt">
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
    using Utils;

    /// <summary>
    /// Scenario tests for the browsing of newest movies.
    /// </summary>
    [TestClass]
    public class BrowseNewestScenarioTest : DataTest
    {
        /// <summary>
        /// Purpose: Verify that if I add a new movie, the 
        /// first movie to be returned is the new one.
        /// 
        /// Steps:
        ///     1. Get newest movies.
        ///     2. Save the newest movie ID and the amount of movies.
        ///     3. Add a new movie.
        ///     4. Get newest movies.
        ///     5. Verify one more movie is returned.
        ///     6. Verify that the first movie returned is the new one.
        /// </summary>
        [TestMethod]
        public void AddOne()
        {
            // Step 1
            var movies = Movie.Newest().ToList();

            // Step 2
            int initialAmount = movies.Count();
            int initialNewestID = movies.First().ID;

            // Step 3
            var movie = new Movie
            {
                Title = "Some new movie with a unique name",
                OwnerID = TestUser.ContentProvider.ID,
                ReleaseDate = DateTime.Now
            };

            RentItContext.Db.Movies.Add(movie);
            RentItContext.Db.SaveChanges();
            RentItContext.ReloadDb();

            // Step 4
            movies = Movie.Newest().ToList();

            // Step 5
            Assert.AreEqual(movies.Count(), initialAmount + 1, "Movie was not added!");

            // Step 6
            Assert.AreNotEqual(movies.First().ID, initialNewestID, "Newest movie has not changed!");
            Assert.AreEqual(movies.First().ID, movie.ID, "Incorrect newest movie!");
        }

        /// <summary>
        /// Purpose: Verify that it is possible to limit 
        /// the amount of movies returned.
        /// 
        /// Steps:
        ///     1. Get newest movies.
        ///     2. Save the amount of movies returned.
        ///     3. Get newest movies, but limit it to the amount 
        ///        from step 2 minus 1.
        ///     4. Verify that the amount is limited.
        /// </summary>
        [TestMethod]
        public void Limiting()
        {
            // Step 1 + 2
            int initialAmount = Movie.Newest().Count();

            // Step 3
            var movies = Movie.Newest(initialAmount - 1);

            // Step 4
            Assert.AreEqual(initialAmount - 1, movies.Count(), "Limit does not work!");
        }

        /// <summary>
        /// Purpose: Verify that movies with a release date in 
        /// the future, will not appear in the newest movies.
        /// 
        /// Steps:
        ///     1. Create a new movie with release date in the future.
        ///     2. Get newest movies.
        ///     3. Verify that the movie from step 1 is not present.
        /// </summary>
        [TestMethod]
        public void AddOneInFuture()
        {
            Movie movie;

            // Step 1
            movie = new Movie
            {
                Title = "Some unique movie title",
                OwnerID = TestUser.ContentProvider.ID,
                ReleaseDate = DateTime.Now.AddDays(14)
            };

            RentItContext.Db.Movies.Add(movie);
            RentItContext.Db.SaveChanges();
            RentItContext.ReloadDb();

            // Step 2
            var movies = Movie.Newest();

            // Step 3
            Assert.IsFalse(movies.Any(m => m.ID == movie.ID), "Movie should not appear in result!");
        }

        /// <summary>
        /// Purpose: Verify that movies without a release date, 
        /// will not appear in the newest movies.
        /// 
        /// Steps:
        ///     1. Create a new movie without a release date.
        ///     2. Get newest movies.
        ///     3. Verify that the movie from step 1 is not present.
        /// </summary>
        [TestMethod]
        public void AddOneWithoutReleaseDate()
        {
            // Step 1
            var movie = new Movie
            {
                Title = "Some unique movie title",
                OwnerID = TestUser.ContentProvider.ID
            };

            RentItContext.Db.Movies.Add(movie);
            RentItContext.Db.SaveChanges();
            RentItContext.ReloadDb();

            // Step 2
            var movies = Movie.Newest();

            // Step 3
            Assert.IsFalse(movies.Any(m => m.ID == movie.ID), "Movie should not appear in result!");
        }
    }
}