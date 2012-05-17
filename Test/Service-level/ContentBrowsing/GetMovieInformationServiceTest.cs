//-------------------------------------------------------------------------------------------------
// <copyright file="GetMovieInformationServiceTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Entities;
    using Utils;

    /// <summary>
    /// Tests for ContentBrowsing.GetMovieInformation.
    /// </summary>
    [TestClass]
    public class GetMovieInformationServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that GetMovieInformation gets information about a movie.
        /// 
        /// Steps:
        ///     1. Login to the system.
        ///     2. Get information about a movie.
        ///     3. Verify that the movie object was populated with information.
        /// </summary>
        [TestMethod]
        public void GetMovieInformationTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            var movie = new Movie { ID = Movie.All.First().ID };
            var result = ContentBrowsing.GetMovieInformation(user.Token, ref movie);

            Assert.IsTrue(result, "GetMovieInformation failed");
            Assert.IsNotNull(movie, "movie returned is null");
            Assert.IsNotNull(movie.Title, "movie doesn't have a title");
        }

        /// <summary>
        /// Purpose: Vecrify that without a token, GetMovieInformation doesn't return anything.
        /// 
        /// Steps: 
        ///     1. Get information about a movie.
        ///     2. Verify that the object wasn't populated.
        /// </summary>
        [TestMethod]
        public void GetMovieInformationWithoutTokenTest()
        {
            var movie = new Movie { ID = Movie.All.First().ID };
            var result = ContentBrowsing.GetMovieInformation(null, ref movie);

            Assert.IsFalse(result, "GetMovieInformation didn't fail");
            Assert.IsNull(movie.Title, "movie has a title");
        }

        /// <summary>
        /// Purpose: Verify that GetMovieInformation doesn't return 
        ///          anything when an unknown movie is referred to.
        /// 
        /// Steps:
        ///     1. Login to the system.
        ///     2. Get information about a non-exsiting movie.
        ///     3. Verify no information was returned.
        /// </summary>
        [TestMethod]
        public void GetMovieInformationUnknownMovieTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            var movie = new Movie { ID = -1 };
            var result = ContentBrowsing.GetMovieInformation(user.Token, ref movie);

            Assert.IsFalse(result, "GetMovieInformation didn't fail");
            Assert.IsNull(movie.Title, "Movie information has been set");
        }
    }
}
