// -----------------------------------------------------------------------
// <copyright file="Hash.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Tools.Encryption
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Oftenly used hashing methods.
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// Computes the SHA384 value of the input.
        /// </summary>
        /// <param name="input">Input to hash.</param>
        /// <returns>Hashed string of length 96 or more.</returns>
        public static string Sha384(string input)
        {
            Contract.Requires(input != null);
            Contract.Ensures(Contract.Result<string>() != null);
            Contract.Ensures(Contract.Result<string>().Length >= 96);

            var salted = Encoding.ASCII.GetBytes(input);
            var hashed = SHA384.Create().ComputeHash(salted);
            return Convert.ToString(hashed);
        }
    }
}
