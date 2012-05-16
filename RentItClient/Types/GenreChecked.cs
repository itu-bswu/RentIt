// -----------------------------------------------------------------------
// <copyright file="GenreChecked.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Types
{
    /// <summary>
    /// Used to construct a list of genres.
    /// </summary>
    public class GenreChecked
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenreChecked"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="isChecked">
        /// The is checked.
        /// </param>
        public GenreChecked(string name, bool isChecked)
        {
            GenreName = name;
            Checked = isChecked;
        }

        /// <summary>
        /// Gets or sets GenreName.
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Checked.
        /// </summary>
        public bool Checked { get; set; }

        
    }
}
