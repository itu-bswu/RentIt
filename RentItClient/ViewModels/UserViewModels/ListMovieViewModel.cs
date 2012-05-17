// -----------------------------------------------------------------------
// <copyright file="ListMovieViewModel.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.ViewModels.UserViewModels
{
    using System;
    using System.Collections.Generic;

    using Models;
    using System.Linq;

    /// <summary>
    /// Viewmodel for the ListMovie page.
    /// </summary>
    public static class ListMovieViewModel
    {
        /// <summary>
        /// Gets all the genres from the model.
        /// </summary>
        /// <returns>The available genres.</returns>
        public static List<Tuple<string, int>> GetGenres()
        {
            IEnumerable<string> genres;
            var result = new List<Tuple<string, int>>();

            var success = MovieInformationModel.AllGenres(out genres);

            if (success)
            {
                var id = 1;
                foreach (var genre in genres)
                {
                    result.Add(Tuple.Create(genre, id));
                    id++;
                }

                return result;
            }

            MasterViewModel.AuthenticationError();
            return null;
        }

        /// <summary>
        /// Gets the newest movies (with or without a genre filter).
        /// </summary>
        /// <param name="genre">The genre to filter the movies by. Default is null, which means no genre filter.</param>
        /// <returns>A list of the newest movies.</returns>
        public static List<Tuple<string, int>> GetNewestMovies(string genre = null)
        {
            IEnumerable<RentItService.Movie> res;
            var result = new List<Tuple<string, int>>();

            var success = MovieInformationModel.Newest(out res, genre);
            if (success)
            {
                result.AddRange(res.Select(m => Tuple.Create(m.Title, m.ID)));

                return result;
            }

            MasterViewModel.AuthenticationError();
            return null;
        }

        /// <summary>
        /// Gets the most popular movies (with or without a genre filter).
        /// </summary>
        /// <param name="genre">The genre to filter the movies by. Default is null, which means no genre filter.</param>
        /// <returns>A list of the most popular movies.</returns>
        public static List<Tuple<string, int>> GetMostPopularMovies(string genre = null)
        {
            IEnumerable<RentItService.Movie> res;
            var result = new List<Tuple<string, int>>();

            var success = MovieInformationModel.MostDownloaded(out res, genre);
            if (success)
            {
                result.AddRange(res.Select(m => Tuple.Create(m.Title, m.ID)));

                return result;
            }

            MasterViewModel.AuthenticationError();
            return null;
        }
    }
}
