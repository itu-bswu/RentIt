//-------------------------------------------------------------------------------------------------
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
        public void EditMovieTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            const string Title = "New title";
            var movie = Movie.All.First();
            movie.Title = Title;
            var result = ContentManagement.EditMovie(user.Token, ref movie);

            RentItContext.ReloadDb();

            Assert.IsTrue(result, "EditMovie failed");
            Assert.AreEqual(Title, Movie.All.Single(m => m.ID.Equals(movie.ID)).Title);
        }
    }
}
