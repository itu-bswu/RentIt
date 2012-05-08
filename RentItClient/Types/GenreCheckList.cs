// -----------------------------------------------------------------------
// <copyright file="GenreCheckList.cs" company="Hewlett-Packard">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItClient.Types
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GenreCheckList : ObservableCollection<GenreChecked>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenreCheckList"/> class.
        /// </summary>
        public GenreCheckList()
        {/*
            foreach (var g in Models.GetMovieInformationModel.AllGenres())
            {
                this.Add(new GenreChecked(g.Name, false));
            }
          * */
        }
    }
}
