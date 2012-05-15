// -----------------------------------------------------------------------
// <copyright file="MostDownloadedScenario.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentIt.Tests.Scenarios.User.Browsing
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Entities;
    using Utils;

    /// <summary>
    /// Scenario tests for the mostdownloaded feature.
    /// </summary>
    [TestClass]
    public class MostDownloadedScenario : DataTest
    {
        /// <summary>
        /// Purpose: Verify that when trying to get the most downloaded 
        ///          movies, the right movies are returned in the right order.
        /// 
        /// Pre-conditions:
        ///     1. A user is logged in.
        ///     2. Rentals are added for some movies.
        /// 
        /// Steps:
        ///     1. Get most downloaded movies.
        ///     2. Verify that the most downloaded movie is the one with the most rentals.
        /// </summary>
        [TestMethod]
        public void MostDownloadedWithRentals()
        {
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            // Get movie editions
            var movieList = Movie.All.ToList();
            var movie1Edition = movieList.ElementAt(0).Editions.First();
            var movie2Edition = movieList.ElementAt(1).Editions.First();
            var movie3Edition = movieList.ElementAt(2).Editions.First();
            
            // Setup rentals
            user.RentMovie(movie1Edition);
            user.RentMovie(movie1Edition);
            user.RentMovie(movie1Edition);
            user.RentMovie(movie2Edition);
            user.RentMovie(movie2Edition);
            user.RentMovie(movie3Edition);
            
            // Step 1
            var movies = Movie.MostDownloaded();
            var mostDownloaded = movies.First();

            // Step 2
            movie1Edition = Movie.Get(user, movie1Edition.MovieID).Editions.First();
            Assert.AreEqual(movie1Edition.Rentals.Count, mostDownloaded.Rentals.Count(), "Amount of rentals do not match!");
            Assert.AreEqual(movie1Edition.MovieID, mostDownloaded.ID, "The first element of the list is not the most rented!");
        }

        /// <summary>
        /// Purpose: Verify that even though the rentals are split between 
        ///          multiple editions, the right movies in the right order 
        ///          is still returned.
        /// 
        /// Pre-conditions:
        ///     1. A user is logged in.
        ///     2. A movie with several editions exists in the database.
        ///     3. A movie with only one edition exists in the database.
        /// 
        /// Steps:
        ///     1. Create multiple rentals for different editions for movie in pre-condition 2.
        ///     2. Create rentals for another movie (less rentals than the total amount in step 1, 
        ///        but more rentals than for one single edition in step 1).
        ///     3. Get most downloaded movies.
        ///     4. Verify the movie from pre-condition 2 is first in the list.
        /// </summary>
        [TestMethod]
        public void MostDownloadedMultipleEditions()
        {
            // Pre-condition 1
            var user = User.Login(TestUser.User.Username, TestUser.User.Password);

            // Pre-condition 2 + 3
            IEnumerable<Movie> movies = Movie.All.ToList();
            var movieMultipleEditions = movies.First(m => m.Editions.Count > 1);
            var movieSingleEdition = movies.First(m => m.Editions.Count == 1);

            // Step 1
            var firstEdition = movieMultipleEditions.Editions.First();
            var secondEdition = movieMultipleEditions.Editions.Last();

            user.RentMovie(firstEdition);
            user.RentMovie(firstEdition);
            user.RentMovie(firstEdition);
            user.RentMovie(secondEdition);
            user.RentMovie(secondEdition);

            // Step 2
            firstEdition = movieSingleEdition.Editions.First();

            user.RentMovie(firstEdition);
            user.RentMovie(firstEdition);
            user.RentMovie(firstEdition);
            user.RentMovie(firstEdition);

            // Step 3
            movies = Movie.MostDownloaded();

            // Step 4
            Assert.AreEqual(movieMultipleEditions.ID, movies.First().ID, "Wrong movie with most rentals!");
        }
    }
}
