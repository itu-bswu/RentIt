// -----------------------------------------------------------------------
// <copyright file="Validator.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Tools
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Helper class for validation.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validate email address.
        /// </summary>
        /// <param name="email">Email to verify.</param>
        /// <returns>True for valid email; false otherwise.</returns>
        public static bool ValidateEmail(string email)
        {
            if (email == null)
            {
                return false;
            }

            var re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase);
            return re.IsMatch(email);
        }
    }
}
