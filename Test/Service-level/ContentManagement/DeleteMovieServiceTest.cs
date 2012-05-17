//-------------------------------------------------------------------------------------------------
// <copyright file="DeleteMovieServiceTest.cs" company="RentIt">
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
    /// Tests for ContentManagement.DeleteMovie.
    /// </summary>
    [TestClass]
    public class DeleteMovieServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can delete a movie.
        /// 
        /// Steps:
        ///     1. Login to the system.
        ///     2. Delete a movie.
        ///     3. Verify that the movie is gone.
        /// </summary>
        [TestMethod]
        public void DeleteMovieTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            var movie = Movie.All.First();
            var id = movie.ID;
            var result = ContentManagement.DeleteMovie(user.Token, movie);

            RentItContext.ReloadDb();

            Assert.IsTrue(result, "DeleteMovie failed");
            Assert.IsFalse(Movie.All.Any(m => m.ID.Equals(id)), "Movie wasn't deleted");
        }
    }
}
