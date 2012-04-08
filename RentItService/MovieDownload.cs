namespace RentItService
{
    using System;

    using RentItService.Entities;

    /// <summary>
    /// movieDownloads used for GetMostDownloaded service
    /// </summary>
    public class MovieDownload : IComparable
    {
        public int numberOfDownloads;

        public Movie movie;

        public MovieDownload(Movie m, int downloads)
        {
            this.movie = m;
            this.numberOfDownloads = downloads;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
