//-------------------------------------------------------------------------------------------------
// <copyright file="UnitTest2.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentIt.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RentItService;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Services;

    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void EditUserProfileTest()
        {
            Service service = new Service();
            using (var db = new RentItContext())
            {
                if (!db.Users.Any(a => a.Username == "testUserEdit"))
                {
                    User u = new User()
                        {
                            Email = "testUser@testing.dk",
                            FullName = "Test User",
                            Password = "test.dk",
                            Type = UserType.User,
                            Token = "testUserEditToken",
                            Username = "testUserEdit"
                        };
                    db.Users.Add(u);
                    db.SaveChanges();
                }

                User user = db.Users.First(u => u.Username == "testUserEdit");
                User user2 = new User()
                    {
                        Email = user.Email,
                        FullName = user.FullName,
                        ID = user.ID,
                        Password = user.Password,
                        Rentals = user.Rentals,
                        Token = user.Token,
                        Type = user.Type,
                        TypeValue = user.TypeValue,
                        Username = user.Username
                    };

                string oldPassword = user.Password;
                user2.Password = oldPassword.ToUpper();

                service.EditProfile(user2.Token, user2);

                user = db.Users.First(u => u.Username == "testUserEdit");
                string newPassword = user.Password;

                user.Password = oldPassword;
                db.SaveChanges();

                Assert.IsTrue(newPassword.Equals(oldPassword.ToUpper()));

            }
        }
    }
}