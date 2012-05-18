// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewEditionPage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI.User
{
    using System.Linq;
    using System.Windows;
    using Types;
    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ViewEditionPage.xaml
    /// </summary>
    public partial class ViewEditionPage
    {
        #region Fields

        /// <summary>
        /// The edition being shown.
        /// </summary>
        private readonly int eId;

        /// <summary>
        /// The movie begin shown.
        /// </summary>
        private readonly Movie movie;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewEditionPage"/> class.
        /// </summary>
        /// <param name="m">
        /// The movie the edition belongs to.
        /// </param>
        /// <param name="editionId">
        /// The edition id of the edition to show.
        /// </param>
        public ViewEditionPage(Movie m, int editionId)
            : this()
        {
            eId = editionId;
            movie = m;
            textBlockTitle.Text = m.Title;
            textBlockDescription.Text = m.Description;
            textBlockEdition.Text = m.Editions.First(e => e.Item2 == eId).Item1;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ViewEditionPage"/> class from being created.
        /// </summary>
        private ViewEditionPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Click methods

        /// <summary>
        /// Method invoked when the "List Movies" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ListMovieClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage());
        }

        /// <summary>
        /// Method invoked when the "View Profile" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ViewProfileClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        /// <summary>
        /// Method invoked when the "Your rentals" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void YourRentalsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistoryPage());
        }

        /// <summary>
        /// Method invoked when the "Search" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void SearchClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage(MasterViewModel.Search(textBoxSearch.Text)));
        }

        /// <summary>
        /// Method invoked when the "Logout" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LogOut())
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        /// <summary>
        /// Method invoked when the "Rent mMovie" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void RentMovieClick(object sender, RoutedEventArgs e)
        {
            ViewEditionViewModel.RentEdition(eId);
            NavigationService.Navigate(new DownloadEditionPage(movie, eId));
        }
        #endregion
    }
}
