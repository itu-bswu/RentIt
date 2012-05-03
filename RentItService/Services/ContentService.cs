// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentService.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace RentItService.Services
{
    using System;
    using System.Diagnostics.Contracts;
    using RentItService.Entities;
    using RentItService.Enums;
    using RentItService.Exceptions;
    using RentItService.Interfaces;

    /// <summary>
    /// Service for the content providers.
    /// </summary>
    public partial class Service : IContentService
    {
        /// <summary>Operation used to update movie information.</summary>
        /// <param name="token">The user token.</param>
        /// <param name="updatedMovie">The Movie object containing the ID of the movie to be changed and the updated information.</param>
        /// <exception cref="NotImplementedException">Not Yet Implemented.</exception>
        /// <author>Jacob Grooss</author>
        public void EditMovieInformation(string token, Movie updatedMovie)
        {
            Contract.Requires(token != null);
            Contract.Requires(updatedMovie.Title != null);
            Contract.Requires(updatedMovie.FilePath != null);
            Contract.Requires<UserNotFoundException>(User.GetByToken(token) != null);
            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type != UserType.User);

            var user = User.GetByToken(token);

            using (var db = new RentItContext())
            {
                var referenceMovie = db.Movies.Find(updatedMovie.ID);

                if (referenceMovie == null)
                {
                    throw new NoMovieFoundException();
                }

                if (referenceMovie.OwnerID != user.ID && user.Type != UserType.SystemAdmin)
                {
                    throw new InsufficientRightsException("Cannot edit a movie belonging to another content provider!");
                }

                referenceMovie.Title = updatedMovie.Title;
                referenceMovie.Description = updatedMovie.Description;
                referenceMovie.ImagePath = updatedMovie.ImagePath;
                referenceMovie.Released = updatedMovie.Released;

                foreach (var genre in referenceMovie.Genres.Where(genre => updatedMovie.Genres.Any(g => g.Name.Equals(genre.Name))))
                {
                    referenceMovie.RemoveGenre(genre);
                }
                
                foreach (var newgenre in updatedMovie.Genres.Select(genre => Genre.GetOrCreateGenre(genre.Name)))
                {
                    referenceMovie.AddGenre(newgenre);
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a movie from the service. 
        /// The movie is identified by the ID in the instance of the Movie class. 
        /// The other properties in the Movie instance are ignored.
        /// </summary>
        /// <param name="token">The session token.</param>
        /// <param name="movieObject">The movie to be deleted.</param>
        /// <author>Jakob Melnyk</author>
        public void DeleteMovie(string token, Movie movieObject)
        {
            Contract.Requires<ArgumentNullException>(token != null);
            Contract.Requires<ArgumentNullException>(movieObject != null);

            Contract.Requires<InsufficientRightsException>(User.GetByToken(token).Type == UserType.ContentProvider);

            Movie.DeleteMovie(token, movieObject);
        }
    }
}