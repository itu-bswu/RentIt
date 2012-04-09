﻿// -----------------------------------------------------------------------
// <copyright file="Hash.cs" company="RentIt">
// Encryption class
// </copyright>
// -----------------------------------------------------------------------

namespace Tools.Encryption
{
    using System;
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
        public static string Sha512(string input)
        {
            Contract.Requires(input != null);
            Contract.Ensures(Contract.Result<string>() != null);

            var salted = Encoding.Default.GetBytes(input);
            var hashed = SHA512.Create().ComputeHash(salted);
            return Convert.ToBase64String(hashed);
        }
    }
}