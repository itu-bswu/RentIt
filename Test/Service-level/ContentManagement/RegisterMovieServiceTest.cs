namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Services;
    using Utils;
    using System.Collections.Generic;
    using RentItService.Entities;
    using System.Linq;
    using RentItService.Enums;
    
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

            const string title = "My amazing movie";
            var movie = new Movie { Title = title };
            var result = ContentManagement.RegisterMovie(user.Token, ref movie);

            Assert.IsTrue(result, "RegisterMovie failed");
            Assert.IsNotNull(movie.ID, "Movie has no ID");
            Assert.IsNotNull(Movie.All.Single(m => m.Title.Equals(title)));
        }
    }
}
