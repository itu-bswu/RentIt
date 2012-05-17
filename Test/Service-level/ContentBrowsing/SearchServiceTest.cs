﻿namespace RentIt.Tests.Service_level.ContentBrowsing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService.Services;
    using Utils;
    using System.Collections.Generic;
    using RentItService.Entities;
    using System.Linq;
    using RentItService.Enums;

    [TestClass]
    public class SearchServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verifies that movies are returned from a search
        /// 
        /// Steps:
        ///     1. Login to the service
        ///     2. Perform a search
        ///     3. Verify that movies was returned
        /// </summary>
        [TestMethod]
        public void SearchTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            Movie[] movies;
            var result = ContentBrowsing.Search(out movies, user.Token, "the");

            Assert.IsTrue(result, "Result is false");
            Assert.IsNotNull(movies, "Movie object wasn't set");
            Assert.IsTrue(movies.Any(), "No movies returned");
        }

        /// <summary>
        /// Purpose: Verifies that nothing is returned without a user token
        /// 
        /// Steps:
        ///     1. Perform a search
        ///     2. Verify that nothing is returned
        /// </summary>
        [TestMethod]
        public void SearchWithoutTokenTest()
        {
            Movie[] movies;
            var result = ContentBrowsing.Search(out movies, null, "the");

            Assert.IsFalse(result, "Result is true");
            Assert.IsNull(movies, "Movie object wast set");
        }

        /// <summary>
        /// Purpose: Verify that nothing is returned for a null query
        /// 
        /// Steps:
        ///     1. Login to the service
        ///     2. Perform a null search
        ///     3. Verify that nothing was returned
        /// </summary>
        [TestMethod]
        public void SearchWithoutQuery()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            Movie[] movies;
            var result = ContentBrowsing.Search(out movies, user.Token, null);

            Assert.IsTrue(result, "Result is false");
            Assert.IsNotNull(movies, "Movie object wasn't set");
            Assert.IsTrue(movies.Any(), "No movies returned");
        }
    }
}