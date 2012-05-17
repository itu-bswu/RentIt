// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CPRegisterMoviePage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI.ContentProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    using RentItClient.Types;
    using RentItClient.ViewModels.ProviderViewModels;

    /// <summary>
    /// Interaction logic for CPRegisterMoviePage.xaml
    /// </summary>
    public partial class CPRegisterMoviePage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CPRegisterMoviePage"/> class.
        /// </summary>
        public CPRegisterMoviePage()
        {
            InitializeComponent();
        }
        #endregion

        #region Click methods

        /// <summary>
        /// Method invoked when the "Your Movies" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void YourMoviesClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new CPYourMoviesPage());
            }
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
        /// Method invoked when the "Register Movie" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            var genres = (from GenreChecked gc in GenreCheckList.Items where gc.Checked select gc.GenreName).ToList();

            var movie = new Movie(
                0,
                textBoxTitle.Text,
                textBoxDescription.Text,
                DatePickerReleaseDate.SelectedDate,
                genres,
                new List<Tuple<string, int>>());

            if (CPRegisterViewModel.RegisterMovie(ref movie))
            {
                const string messageBoxText =
                    "You have successfully registered a movie. Would you like to upload an edition right away?";
                const string caption = "Upload edition?";
                const MessageBoxButton button = MessageBoxButton.YesNo;

                var result = MessageBox.Show(messageBoxText, caption, button);

                // Process message box results
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        NavigationService.Navigate(new CPUploadEditionPage(movie));
                        break;
                    case MessageBoxResult.No:
                        NavigationService.Navigate(new CPYourMoviesPage());
                        break;
                }
            }
            else
            {
                MessageBox.Show("Something went wrong when trying to register the movie. " +
                                "This error may occur if there is something wrong with the data you input" +
                                " or if there is a problem with the service." +
                                "\n If all data appears ok, please restart the client.");
            }
        }
        #endregion
    }
}
