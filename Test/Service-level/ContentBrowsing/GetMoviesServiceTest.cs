//-------------------------------------------------------------------------------------------------
// <copyright file="GetMoviesServiceTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Entities;
    using RentItService.Enums;
    using Utils;

    /// <summary>
    /// Tests for ContentBrowsing.GetMovies.
    /// </summary>
    [TestClass]
    public class GetMoviesServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that movies are returned on valid input to GetMovies.
        /// 
        /// Steps:
        ///     1. Login to the service.
        ///     2. Get all movies.
        ///     3. Verify that movies was returned.
        /// </summary>
        [TestMethod]
        public void GetAllMoviesValidServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            Movie[] movies;
            var result = ContentBrowsing.GetMovies(out movies, user.Token, MovieSorting.Default, null, 0);

            Assert.IsTrue(result, "Result is false");
            Assert.IsNotNull(movies, "Movies wasn't set by GetMovies call");
            Assert.IsTrue(movies.Any(), "No movies was returned");
        }

        /// <summary>
        /// Purpose: Verify that movies can be sorted by newest.
        /// 
        /// Steps:
        ///     1. Login to the service.
        ///     2. Get newest movies.
        ///     3. Check that the first is newer than the second.
        /// </summary>
        [TestMethod]
        public void GetAllMoviesNewestServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            Movie[] movies;
            var result = ContentBrowsing.GetMovies(out movies, user.Token, MovieSorting.Newest, null, 0);

            Assert.IsTrue(result, "Result is false");
            Assert.IsNotNull(movies, "Movies wasn't set by GetMovies call");

            IList<Movie> movieList = movies.ToList();
            var releaseDate1 = movieList[0].ReleaseDate;
            var releaseDate2 = movieList[1].ReleaseDate;
            Assert.IsTrue(releaseDate1 != null && releaseDate2 != null);
            Assert.IsTrue(releaseDate1.Value < releaseDate2.Value, "First movie is older than the second");
        }

        /// <summary>
        /// Purpose: Verify that movies can be sorted by number of rentals.
        /// 
        /// Steps:
        ///     1. Login to the service.
        ///     2. Get the most downloaded movies.
        ///     3. Check that the first is more downloaded than the second.
        /// </summary>
        [TestMethod]
        public void GetAllMoviesMostDownloadedServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            Movie[] movies;
            var result = ContentBrowsing.GetMovies(out movies, user.Token, MovieSorting.MostDownloaded, null, 0);

            Assert.IsTrue(result, "Result is false");
            Assert.IsNotNull(movies, "Movies wasn't set by GetMovies call");

            IList<Movie> movieList = movies.ToList();
            Assert.IsTrue(movieList[0].Rentals.Count() > movieList[1].Rentals.Count(), "First movie is less downloaded than the second");
        }

        /// <summary>
        /// Purpose: Verify that movies can be limited to a specific genre.
        /// 
        /// Steps:
        ///     1. Login to the service
        ///     2. Get all movies with a specific genre
        ///     3. Verify that only movies in that genre was returned
        /// </summary>
        [TestMethod]
        public void GetAllMoviesGenreServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            var genre = Genre.All.First();
            Movie[] movies;
            var result = ContentBrowsing.GetMovies(out movies, user.Token, MovieSorting.Default, genre, 0);

            Assert.IsTrue(result, "Result is false");
            Assert.IsNotNull(movies, "Movies wasn't set by GetMovies call");
            Assert.IsTrue(movies.All(movie => movie.HasGenre(genre)), "Not all movies has the genre");
        }

        /// <summary>
        /// Purpose: Verify that you can limit the number of movies returned
        /// 
        /// Steps:
        ///     1. Login to the service.
        ///     2. Get all movies with a limit.
        ///     3. Verify that only (limit) movies was returned.
        /// </summary>
        [TestMethod]
        public void GetAllMoviesLimitServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            const int Limit = 5;
            Movie[] movies;
            var result = ContentBrowsing.GetMovies(out movies, user.Token, MovieSorting.Default, null, Limit);

            Assert.IsTrue(result, "Result is false");
            Assert.IsNotNull(movies, "Movies wasn't set by GetMovies call");
            Assert.AreEqual(Limit, movies.Count(), "Movie limit doesn't work");
        }

        /// <summary>
        /// Purpose: Verify that a token is needed to browse movies.
        /// 
        /// Steps:
        ///     1. Get all movies.
        ///     2. Verify that no movies is returned.
        /// </summary>
        [TestMethod]
        public void GetAllMoviesWithoutTokenServiceTest()
        {
            Movie[] movies;
            var result = ContentBrowsing.GetMovies(out movies, null, MovieSorting.Default, null, 0);

            Assert.AreEqual(false, result, "GetMovies didn't fail");
            Assert.IsNull(movies, "Movies are not null");
        }
    }
}
