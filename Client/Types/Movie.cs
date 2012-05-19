// -----------------------------------------------------------------------
// <copyright file="Movie.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using RentItService;

    /// <summary>
    /// Represents a movie in the client.
    /// </summary>
    public class Movie
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Movie"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the movie.
        /// </param>
        /// <param name="title">
        /// The title of the movie.
        /// </param>
        /// <param name="description">
        /// The description of the movie.
        /// </param>
        /// <param name="released">
        /// The release date of the movie.
        /// </param>
        /// <param name="genres">
        /// The genres of the movie.
        /// </param>
        /// <param name="editions">
        /// The editions.
        /// </param>
        public Movie(int id, string title, string description, DateTime? released, IEnumerable<string> genres, IEnumerable<Tuple<string, int>> editions)
        {
            ID = id;
            Title = title;
            Description = description;
            ReleaseDate = released;
            Genres = genres;
            Editions = editions;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the id of the movie.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets the title of the movie.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the description of the movie.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the date the movie was released.
        /// </summary>
        public DateTime? ReleaseDate { get; private set; }

        /// <summary>
        /// Gets the genres of the movie.
        /// </summary>
        public IEnumerable<string> Genres { get; private set; }

        /// <summary>
        /// Gets the editions of the movie.
        /// </summary>
        public IEnumerable<Tuple<string, int>> Editions { get; private set; }
        #endregion

        #region Static Methods
        /// <summary>
        /// Converts a movie of the service's Movie type to the client's Movie type.
        /// </summary>
        /// <param name="movie">The movie to convert.</param>
        /// <returns>The converted movie.</returns>
        public static Movie ConvertServiceMovie(RentItService.Movie movie)
        {
            Movie result;

            var genres = movie.Genres.Select(g => g.Name).ToList();

            var editions = movie.Editions.Select(e => Tuple.Create(e.Name, e.ID)).ToList();

            if (movie.ReleaseDate != null)
            {
                result = new Movie(
                movie.ID,
                movie.Title,
                movie.Description,
                movie.ReleaseDate.Value,
                genres,
                editions);
            }
            else
            {
                result = new Movie(
                movie.ID,
                movie.Title,
                movie.Description,
                new DateTime(0001, 01, 01, 00, 00, 00),
                genres,
                editions);
            }

            return result;
        }

        /// <summary>
        /// Converts a movie of the client type to the service movie type.
        /// </summary>
        /// <param name="m">The movie to convert.</param>
        /// <returns>The converted movie.</returns>
        public static RentItService.Movie ConvertClientMovie(Movie m)
        {
            var x = new RentItService.Movie();

            if (m.Editions.Count() != 0)
            {
                MovieInformationModel.GetMovieInfo(m.ID, out x);
            }

            var result =
                new RentItService.Movie
                {
                    ID = m.ID,
                    Description = m.Description,
                    Genres = m.Genres.Select(g => new Genre
                                                      {
                                                          Name = g
                                                      }).ToArray(),
                    Editions = x.Editions,
                    ReleaseDate = m.ReleaseDate,
                    Title = m.Title
                };

            return result;
        }
        #endregion
    }
}
