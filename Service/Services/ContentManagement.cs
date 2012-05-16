//-------------------------------------------------------------------------------------------------
// <copyright file="ContentManagement.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Services
{
    using Entities;
    using Enums;
    using Interfaces;
    using Library;

    /// <summary>
    /// The RentIt service class.
    /// </summary>
    /// <author>Jakob Melnyk</author>
    public partial class Service : IContentManagement
    {
        /// <summary>
        /// Register a new movie. Must have atleast a title. ReleaseDate can be set 
        /// to null or a date in the future, meaning the movie has not yet been released.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="movie">The movie to register. Should have a title</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool RegisterMovie(string token, ref Movie movie)
        {
            if (token == null || movie == null || string.IsNullOrEmpty(movie.Title))
            {
                return false;
            }

            var user = User.GetByToken(token);
            if (user == null || user.Type != UserType.ContentProvider)
            {
                return false;
            }

            movie = Movie.Register(user, movie);
            return true;
        }

        /// <summary>
        /// Edit a movie's information. Title, Description, ImagePath, ReleaseDate 
        /// and Genres will be updated. Movie will be identified by the ID in the 
        /// Movie instance passed to the method. The user editing (identified by 
        /// the token) must be the owner of the movie.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="movie">The movie to edit. Should at least have an ID</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool EditMovie(string token, ref Movie movie)
        {
            if (token == null || movie == null || string.IsNullOrEmpty(movie.Title))
            {
                return false;
            }

            var user = User.GetByToken(token);
            if (user == null || 
                user.Type != UserType.ContentProvider || 
                Movie.Get(user, movie.ID).OwnerID != user.ID)
            {
                return false;
            }

            movie = Movie.Edit(user, movie);
            return true;
        }

        /// <summary>
        /// Delete a movie. Movie to delete will be identified by the ID in the 
        /// Movie instance passed to the method. Nothing more than the ID has to 
        /// be filled out (will just be ignored).
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="movie">The movie to delete (identified by ID).</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool DeleteMovie(string token, Movie movie)
        {
            if (token == null || movie == null)
            {
                return false;
            }

            var user = User.GetByToken(token);
            if (user == null ||
                user.Type != UserType.ContentProvider ||
                Movie.Get(user, movie.ID).OwnerID != user.ID)
            {
                return false;
            }

            Movie.Delete(user, movie);
            return true;
        }

        /// <summary>
        /// Upload a new movie edition. Must set Name and MovieID in the 
        /// Edition instance passed to the method. The rest will be filled 
        /// out by the service, and ready to use afterwards. Please be 
        /// aware that edition name is unique for the movie - meaning that 
        /// it is not possible to have two different editions for the same 
        /// movie with the same name. 
        /// The user creating the edition (identified by the session token) 
        /// must be the owner of the movie.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="stream">The file stream</param>
        /// <param name="edition">The edition. Should have title and movie</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool UploadEdition(string token, RemoteFileStream stream, ref Edition edition)
        {
            /*if (token == null || movie == null)
            {
                return false;
            }

            var user = User.GetByToken(token);
            if (user == null ||
                user.Type != UserType.ContentProvider ||
                Movie.Get(movie.ID).OwnerID != user.ID)
            {
                return false;
            }

            UploadDownload.UploadFile(token, edition.Name, stream, edition.MovieID);
            return true;*/
            return false;
        }

        /// <summary>
        /// Delete a movie edition. Will remove the movie and all editions of 
        /// the movie. The user deleting the movie (identified by the session token) 
        /// must be the owner of the movie.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="edition">The edition to delete</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool DeleteEdition(string token, Edition edition)
        {
            if (token == null || edition == null)
            {
                return false;
            }

            var user = User.GetByToken(token);
            var realEdition = Edition.Get(user, edition.ID);
            if (user == null ||
                realEdition == null ||
                realEdition.Movie.OwnerID != user.ID)
            {
                return false;
            }

            realEdition.Delete(user);
            return true;
        }
    }
}