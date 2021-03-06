﻿//-------------------------------------------------------------------------------------------------
// <copyright file="EditMovieServiceTest.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using Utils;
    
    /// <summary>
    /// Tests for ContentManagement.EditMovie.
    /// </summary>
    [TestClass]
    public class EditMovieServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can edit movies.
        /// 
        /// Steps:
        ///     1. Login to the system.
        ///     2. Edit a movie.
        ///     3. Verify that the movie was edited.
        /// </summary>
        [TestMethod]
        public void EditMovieValidServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            const string title = "New title";
            var movie = Movie.All.First();
            movie.Title = title;
            var result = ContentManagement.EditMovie(user.Token, ref movie);

            RentItContext.ReloadDb();

            Assert.IsTrue(result, "EditMovie failed");
            Assert.AreEqual(title, Movie.All.Single(m => m.ID.Equals(movie.ID)).Title, "Movie title wasn't changed");
        }

        /// <summary>
        /// Purpose: Verify that normal users cannot edit movies
        /// 
        /// Steps:
        ///     1. Login to the system as a normal user
        ///     2. Edit a movie
        ///     3. Verify that the method failed
        /// </summary>
        [TestMethod]
        public void EditMovieInsufficientRightsServiceTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            const string title = "New title";
            var movie = Movie.All.First();
            movie.Title = title;
            var result = ContentManagement.EditMovie(user.Token, ref movie);

            RentItContext.ReloadDb();

            Assert.IsFalse(result, "EditMovie didn't fail");
            Assert.AreNotEqual(title, Movie.All.Single(m => m.ID.Equals(movie.ID)).Title, "Movie title was changed");
        }
    }
}
