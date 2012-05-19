// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadEditionPage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI.User
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;

    using Types;
    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for DownloadEditionPage.xaml
    /// </summary>
    public partial class DownloadEditionPage
    {
        #region Fields

        /// <summary>
        /// The movie being displayed.
        /// </summary>
        private readonly int eId;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadEditionPage"/> class.
        /// </summary>
        /// <param name="m">The movie to show.</param>
        /// <param name="editionId">The id of the edition to show.</param>
        public DownloadEditionPage(Movie m, int editionId)
            : this()
        {
            eId = editionId;
            textBlockTitle.Text = m.Title;
            textBoxDescription.Text = m.Description;
            textBlockEdition.Text = m.Editions.First(e => e.Item2 == eId).Item1;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DownloadEditionPage"/> class from being created.
        /// </summary>
        private DownloadEditionPage()
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
        private void ListMovies(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage());
        }

        /// <summary>
        /// Method invoked when the "View Profile" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        /// <summary>
        /// Method invoked when the "Your Rentals" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void YourRentals(object sender, RoutedEventArgs e)
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
        /// Method invoked when the "Download Movie" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void DownloadClick(object sender, RoutedEventArgs e)
        {
            string path;
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }
            else
            {
                return;
            }

            DownloadEditionViewModel.DownloadFile(eId, path);
            NavigationService.Navigate(new RentalHistoryPage());
        }
        #endregion
    }
}
