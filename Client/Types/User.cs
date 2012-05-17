// -----------------------------------------------------------------------
// <copyright file="User.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Types
{
    /// <summary>
    /// Holds user information.
    /// </summary>
    public class User
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="email">The email of the user.</param>
        /// <param name="fullName">The full name of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="type">The user type.</param>
        public User(string userName, string email, string fullName, string password, UserType type)
        {
            Username = userName;
            Email = email;
            FullName = fullName;
            Password = password;
            Type = type;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets Username of the user.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets Email of the user.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets full name of the user.
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Gets Password of the user.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the type of the user.
        /// </summary>
        public UserType Type { get; private set; }
        #endregion

        #region Static Methods
        /// <summary>
        /// Converts a user from the service user type to the client user type.
        /// </summary>
        /// <param name="user">The user to convert.</param>
        /// <returns>The converted user object.</returns>
        public static User ConvertServiceUser(RentItService.User user)
        {
            return new User(user.Username, user.Email, user.FullName, user.Password, (UserType)user.TypeValue);
        }
        #endregion
    }
}
