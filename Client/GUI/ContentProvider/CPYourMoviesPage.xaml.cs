// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CPYourMoviesPage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI.ContentProvider
{
    using System.Windows;

    using RentItClient.Types;
    using RentItClient.ViewModels.ProviderViewModels;

    /// <summary>
    /// Interaction logic for CPYourMoviesPage.xaml
    /// </summary>
    public partial class CPYourMoviesPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CPYourMoviesPage"/> class.
        /// </summary>
        public CPYourMoviesPage()
        {
            InitializeComponent();

            MovieListBox.ItemsSource = CPMoviesViewModel.GetMovies();
            MovieListBox.DisplayMemberPath = "Item1";
            MovieListBox.SelectedValuePath = "Item2";
        }
        #endregion Constructors

        #region Click methods

        /// <summary>
        /// Method invoked when the "Register Movie" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CPRegisterMoviePage());
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
        /// Method invoked when the "View Movie" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ViewClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CPViewMoviePage(((Movie)MovieListBox.SelectedValue).ID));
        }
        #endregion
    }
}
