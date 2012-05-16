namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Services;
    using Utils;
    using System.Collections.Generic;
    using RentItService.Entities;
    using System.Linq;
    using RentItService.Enums;
    using RentItService;
    
    [TestClass]
    public class EditMovieServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can edit movies
        /// 
        /// Steps:
        ///     1. Login to the system
        ///     2. Edit a movie
        ///     3. Verify that the movie was edited
        /// </summary>
        [TestMethod]
        public void EditMovieTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            const string title = "New title";
            var movie = Movie.All.First();
            movie.Title = title;
            var result = ContentManagement.EditMovie(user.Token, ref movie);

            RentItContext.ReloadDb();

            Assert.IsTrue(result, "EditMovie failed");
            Assert.AreEqual(title, Movie.All.Single(m => m.ID.Equals(movie.ID)).Title);
        }
    }
}
