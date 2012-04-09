// -----------------------------------------------------------------------
// <copyright file="TestData.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentIt.Tests.Utils
{
    using RentItService.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class TestUser
    {
        /// <summary>
        /// Gets the login for a normal user.
        /// </summary>
        public static User User
        {
            get
            {
                return new User { ID = 1, Username = "Smith", Password = "userPassword", FullName = "James Smith" };
            }
        }

        /// <summary>
        /// Gets the login for a content provider.
        /// </summary>
        public static User ContentProvider
        {
            get
            {
                return new User { ID = 2, Username = "Universal", Password = "providerPassword", FullName = "Universal Pictures" };
            }
        }

        /// <summary>
        /// Gets the login for a system adnimistrator.
        /// </summary>
        public static User SystemAdmin
        {
            get
            {
                return new User { ID = 3, Username = "Anderson", Password = "adminPassword", FullName = "Thomas Anderson" };
            }
        }
    }
}