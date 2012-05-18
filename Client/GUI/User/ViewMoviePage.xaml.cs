// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewMoviePage.xaml.cs" company="RentIt">
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
    /// Interaction logic for ViewMoviePage.xaml
    /// </summary>
    public partial class ViewMoviePage
    {
        #region Fields

        /// <summary>
        /// The movie the page is showing.
        /// </summary>
        private readonly Movie movie;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMoviePage"/> class.
        /// </summary>
        /// <param name="mId">The id of the movie to display.</param>
        public ViewMoviePage(int mId)
            : this()
        {
            movie = ViewMovieViewModel.GetMovieInfo(mId);
            var genres = movie.Genres.Aggregate(string.Empty, (current, g) => current + (g + ", "));

            textBoxRelease.Text = movie.ReleaseDate != null ? movie.ReleaseDate.Value.ToLongDateString() : "Not Yet Released";
            textBoxGenre.Text = genres;
            textBoxDescription.Text = movie.Description;
            textBoxTitle.Text = movie.Title;

            // Edition list box
            EditionListBox.ItemsSource = movie.Editions;
            EditionListBox.DisplayMemberPath = "Item1";
            EditionListBox.SelectedValuePath = "Item2";
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ViewMoviePage"/> class from being created.
        /// </summary>
        private ViewMoviePage()
        {
            InitializeComponent();
        }
        #endregion

        #region Click Methods

        /// <summary>
        /// Method invoked when the "List Movies" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ListMoviesClick(object sender, RoutedEventArgs e)
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
        /// Method invoked when the "Your Rentals" button is clicked.
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
        /// Method invoked when the "Select edition" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void SelectEditionClick(object sender, RoutedEventArgs e)
        {
            if (EditionListBox.SelectedIndex != -1)
            {
                var selectedId = (int)EditionListBox.SelectedValue;

                if (MasterViewModel.IsCurrentRental(selectedId))
                {
                    NavigationService.Navigate(new DownloadEditionPage(movie, selectedId));
                }
                else
                {
                    NavigationService.Navigate(new ViewEditionPage(movie, selectedId));
                }
            }
        }
        #endregion
    }
}
