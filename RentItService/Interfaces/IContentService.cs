// -----------------------------------------------------------------------
// <copyright file="IContentService.cs" company="">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;

    using RentItService.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ServiceContract]
    public interface IContentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="movieObject"></param>
        [OperationContract]
        void EditMovieInformation(string token, Movie movieObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="movieObject"></param>
        [OperationContract]
        void DeleteMovie(string token, Movie movieObject);
    }
}
