//-------------------------------------------------------------------------------------------------
// <copyright file="RegisterMovieServiceTest.cs" company="RentIt">
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
    /// Tests for ContentManagement.RegisterMovie.
    /// </summary>
    [TestClass]
    public class RegisterMovieServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can register a movie
        /// 
        /// Steps:
        ///     1. login to the system
        ///     2. Register a new movie
        ///     3. Verify that the movie was assigned an ID and is in the database
        /// </summary>
        [TestMethod]
        public void RegisterMovieTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            const string Title = "My amazing movie";
            var movie = new Movie { Title = Title };
            var result = ContentManagement.RegisterMovie(user.Token, ref movie);

            Assert.IsTrue(result, "RegisterMovie failed");
            Assert.IsNotNull(movie.ID, "Movie has no ID");
            Assert.IsNotNull(Movie.All.Single(m => m.Title.Equals(Title)));
        }

        /// <summary>
        /// Purpose: Verify that normal users cannot register movies
        /// 
        /// Steps:
        ///     1. Login to the system as a normal user
        ///     2. Register a movie
        ///     3. Verify that it fails
        /// </summary>
        [TestMethod]
        public void RegisterMovieInsufficientRightsTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            const string title = "My amazing movie";
            var movie = new Movie { Title = title };
            var result = ContentManagement.RegisterMovie(user.Token, ref movie);

            Assert.IsFalse(result, "RegisterMovie didn't fail");
            Assert.IsFalse(Movie.All.Any(m => m.Title.Equals(title)));
        }
    }
}
