//-------------------------------------------------------------------------------------------------
// <copyright file="MovieDownload.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Library
{
    using System;
    using RentItService.Entities;

    /// <summary>
    /// MovieDownload class
    /// </summary>
    public class MovieDownload : IComparable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieDownload"/> class.
        /// </summary>
        /// <param name="mov">The movie.</param>
        /// <param name="downloads">The number of downloads.</param>
        public MovieDownload(Movie mov, int downloads)
        {
            this.Movie = mov;
            this.NumberOfDownloads = downloads;
        }

        /// <summary>
        /// Gets the number of downloads associated with the movie associated with this object.
        /// </summary>
        public int NumberOfDownloads { get; private set; }

        /// <summary>
        /// Gets the Movie associated with this object.
        /// </summary>
        public Movie Movie { get; private set; }

        /// <summary>
        /// Compares two MovieDownload objects.
        /// </summary>
        /// <param name="obj">The object to be compared.</param>
        /// <returns>Less than zero if this precedes obj in the sor order. Zero if this is in the same position as obj in the sor order. Greater than zero if this follows obj in the sort order.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var otherMovieDownload = obj as MovieDownload;
            if (otherMovieDownload == null)
            {
                throw new ArgumentException("Object is not a MovieDownload combination.");
                
            }

            return this.NumberOfDownloads.CompareTo(otherMovieDownload.NumberOfDownloads);
        }
    }
}