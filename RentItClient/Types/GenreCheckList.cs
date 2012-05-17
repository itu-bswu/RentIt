// -----------------------------------------------------------------------
// <copyright file="GenreCheckList.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.Types
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A list of genres and bool values indicating if a genre has been checked or not.
    /// </summary>
    public class GenreCheckList : ObservableCollection<GenreChecked>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenreCheckList"/> class.
        /// </summary>
        public GenreCheckList()
        {
            IEnumerable<string> genres;
            Models.MovieInformationModel.AllGenres(out genres);

            foreach (var g in genres)
            {
                Add(new GenreChecked(g, false));
            }
        }
    }
}
