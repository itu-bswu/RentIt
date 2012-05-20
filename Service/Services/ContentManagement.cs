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

            movie.Edit(user, movie);
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

            var m = Movie.Get(user, movie.ID);

            m.Delete(user);
            return true;
        }

        /// <summary>
        /// Upload a new movie edition. Must set Name and MovieID in the 
        /// Edition instance passed to the RemoteFileStream. Please be aware 
        /// that edition name is unique for the movie - meaning that it is not 
        /// possible to have two different editions for the same movie with the 
        /// same name. 
        /// The user creating the edition (identified by the session token in 
        /// the RemoteFileStream) must be the owner of the movie.
        /// </summary>
        /// <param name="uploadRequest">The file stream</param>
        /// <returns>True on success; false otherwise.</returns>
        public void UploadEdition(RemoteFileStream uploadRequest)
        {
            if (uploadRequest == null)
            {
                return;
            }

            var token = uploadRequest.Token;
            var edition = uploadRequest.Edition;

            if (token == null || edition == null ||
                uploadRequest.FileByteStream == null || uploadRequest.FileName == null)
            {
                return;
            }

            var user = User.GetByToken(token);
            var movie = Movie.Get(user, edition.MovieID);

            if (user == null ||
                movie == null ||
                user.Type != UserType.ContentProvider ||
                movie.OwnerID != user.ID)
            {
                return;
            }

            movie.UploadEdition(user, edition.Name, uploadRequest);
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