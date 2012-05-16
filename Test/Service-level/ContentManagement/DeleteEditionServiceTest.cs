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
    public class DeleteEditionServiceTest : ServiceTest
    {
        /// <summary>
        /// Purpose: Verify that you can delete a movie edition
        /// 
        /// Steps:
        ///     1. Login to the system
        ///     2. Delete an edition
        ///     3. Verify that the edition is gone
        /// </summary>
        [TestMethod]
        public void DeleteEditionTest()
        {
            User user;
            UserManagement.Login(out user, TestUser.ContentProvider.Username, TestUser.ContentProvider.Password);

            var edition = Movie.All.First().Editions.First();
            var id = edition.ID;
            var result = ContentManagement.DeleteEdition(user.Token, edition);

            RentItContext.ReloadDb();
            
            Assert.IsTrue(result, "DeleteEdition failed");
            Assert.IsFalse(Movie.All.Any(m => m.Editions.Any(e => e.ID.Equals(id))), "Edition wasn't deleted");
        }
    }
}
