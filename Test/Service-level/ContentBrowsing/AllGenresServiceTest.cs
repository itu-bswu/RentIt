//-------------------------------------------------------------------------------------------------
// <copyright file="AllGenresServiceTest.cs" company="RentIt">
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
    /// Tests for ContentBrowsing.GetGenres.
    /// </summary>
    [TestClass]
    public class AllGenresServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that GetGenres return some genres.
        /// 
        /// Steps:
        ///     1. Login to the service.
        ///     2. Get all genres.
        ///     3. Verify that genres was returned.
        /// </summary>
        [TestMethod]
        public void AllGenresTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.User.Username, TestUser.User.Password);

            string[] genres;
            var result = ContentBrowsing.GetGenres(out genres, user.Token);

            Assert.IsTrue(result, "GetGenres failed");
            Assert.IsNotNull(genres, "Genres object wasn't set");
            Assert.IsTrue(genres.Any(), "No genres");
        }

        /// <summary>
        /// Purpose: Verify that without a token, GetGenres doesn't return anything.
        /// 
        /// Steps:
        ///     1. Get all genres.
        ///     2. Verify that none was returned.
        /// </summary>
        [TestMethod]
        public void AllGenresWithoutTokenTest()
        {
            string[] genres;
            var result = ContentBrowsing.GetGenres(out genres, null);

            Assert.IsFalse(result, "GetGenres didn't fail");
            Assert.IsNull(genres, "genres isn't null");
        }
    }
}
