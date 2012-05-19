//-------------------------------------------------------------------------------------------------
// <copyright file="IContentManagement.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;
    using Entities;
    using Library;

    /// <summary>
    /// Interface for content management.
    /// </summary>
    [ServiceContract]
    public interface IContentManagement
    {
        /// <summary>
        /// Register a new movie. Must have atleast a title. ReleaseDate can be set 
        /// to null or a date in the future, meaning the movie has not yet been released.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="movie">The movie to register. Should have a title</param>
        /// <returns>True on success; false otherwise.</returns>
        [OperationContract]
        bool RegisterMovie(string token, ref Movie movie);

        /// <summary>
        /// Edit a movie's information. Title, Description, ImagePath, ReleaseDate 
        /// and Genres will be updated. Movie will be identified by the ID in the 
        /// Movie instance passed to the method. The user editing (identified by 
        /// the token) must be the owner of the movie.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="movie">The movie to edit. Should at least have an ID</param>
        /// <returns>True on success; false otherwise.</returns>
        [OperationContract]
        bool EditMovie(string token, ref Movie movie);

        /// <summary>
        /// Delete a movie. Movie to delete will be identified by the ID in the 
        /// Movie instance passed to the method. Nothing more than the ID has to 
        /// be filled out (will just be ignored).
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="movie">The movie to delete (identified by ID).</param>
        /// <returns>True on success; false otherwise.</returns>
        [OperationContract]
        bool DeleteMovie(string token, Movie movie);

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
        [OperationContract]
        void UploadEdition(RemoteFileStream uploadRequest);

        /// <summary>
        /// Delete a movie edition. Will remove the movie and all editions of 
        /// the movie. The user deleting the movie (identified by the session token) 
        /// must be the owner of the movie.
        /// </summary>
        /// <param name="token">The user's session token.</param>
        /// <param name="edition">The edition to delete</param>
        /// <returns>True on success; false otherwise.</returns>
        [OperationContract]
        bool DeleteEdition(string token, Edition edition);
    }
}
