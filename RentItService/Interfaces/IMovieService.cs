// -----------------------------------------------------------------------
// <copyright file="IMovieService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItService.Interfaces
{
    using System.ServiceModel;

    using RentItService.Entities;

    using Tools;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ServiceContract(Namespace=Constants.MovieNamespace)]
    public interface IMovieService
    {
        [OperationContract]
        Movie GetMovieInformation(int id);

        [OperationContract]
        Movie GetMovieInformation(string movieName);
    }
}
