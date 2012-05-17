// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CPUploadEditionPage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI.ContentProvider
{
    using System.IO;
    using System.Windows;

    using RentItClient.Types;
    using RentItClient.ViewModels.ProviderViewModels;

    /// <summary>
    /// Interaction logic for CPUploadEditionPage.xaml
    /// </summary>
    public partial class CPUploadEditionPage
    {
        #region Fields

        private Movie associatedMovie;
        #endregion

        #region Constructors

        public CPUploadEditionPage(Movie movie)
            : this()
        {
            associatedMovie = movie;
            movieTextBox.Text = associatedMovie.Title;
        }

        private CPUploadEditionPage()
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
        private void YourMovies(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new CPYourMoviesPage());
            }
        }

        /// <summary>
        /// Method invoked when the "Register Movie" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new CPRegisterMoviePage());
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
        /// Method invoked when the "Upload Edition" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void UploadEditionClick(object sender, RoutedEventArgs e)
        {
            FileInfo fi = new FileInfo(textBoxFiletoUpload.Text);
            if (CPUploadEditionViewModel.UploadEdition(associatedMovie, textBoxTitle.Text, fi))
            {
                MessageBox.Show("Edition was uploaded successfully!");
                NavigationService.Navigate(new CPViewMoviePage(associatedMovie.ID));
            }
            else
            {
                MessageBox.Show("Something went wrong when trying to edit the movie. " +
                                "This error may occur if there is something wrong with the data you input" +
                                " or if there is a problem with the service." +
                                "\n If all data appears ok, please restart the client.");
            }
        }

        /// <summary>
        /// Method invoked when the "Browse" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".avi", Filter = "movie files (.avi)|*.avi" };

            // Display OpenFileDialog by calling ShowDialog method
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result.Value)
            {
                // Open document
                textBoxFiletoUpload.Text = dlg.FileName;
            }
        }
        #endregion
    }
}
