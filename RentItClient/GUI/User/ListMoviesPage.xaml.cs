// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListMoviesPage.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI.User
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ListMoviesPage.xaml
    /// </summary>
    public partial class ListMoviesPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMoviesPage"/> class with the given movies listed.
        /// </summary>
        /// <param name="movies">The movies to display.</param>
        public ListMoviesPage(IEnumerable<Tuple<string, int>> movies)
            : this()
        {
            // Movie list
            MovieListBox.ItemsSource = movies;
            MovieListBox.DisplayMemberPath = "Item1";
            MovieListBox.SelectedValuePath = "Item2";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMoviesPage"/> class. 
        /// </summary>
        public ListMoviesPage()
        {
            InitializeComponent();

            // Movie list
            MovieListBox.ItemsSource = ListMovieViewModel.GetNewestMovies();
            MovieListBox.DisplayMemberPath = "Item1";
            MovieListBox.SelectedValuePath = "Item2";

            // Genre combo box
            var genres = ListMovieViewModel.GetGenres();
            genres.Insert(0, Tuple.Create("All", 0));
            genreComboBox.ItemsSource = genres;
            genreComboBox.DisplayMemberPath = "Item1";
            genreComboBox.SelectedValuePath = "Item2";

            // Sort mode combo box
            var sortModes = new List<Tuple<string, int>>();
            sortModes.Add(Tuple.Create("Newest", 1));
            sortModes.Add(Tuple.Create("Most downloaded", 2));

            sortModeComboBox.ItemsSource = sortModes;
            sortModeComboBox.DisplayMemberPath = "Item1";
            sortModeComboBox.SelectedValuePath = "Item2";
        }

        #endregion

        #region Click methods

        /// <summary>
        /// Method invoked when the "View Movie" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void ViewMovieClick(object sender, RoutedEventArgs e)
        {
            if (MovieListBox.SelectedIndex != -1)
            {
                var selectedId = (int)MovieListBox.SelectedValue;
                NavigationService.Navigate(new ViewMoviePage(selectedId));
            }
        }

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
        /// Method invoked when the "Sort movies" button is clicked.
        /// </summary>
        /// <param name="sender">The object invoking the method.</param>
        /// <param name="e">The event arguments.</param>
        private void SortClick(object sender, RoutedEventArgs e)
        {
            List<Tuple<string, int>> movieList;
            var genre = genreComboBox.Text;

            if (genre.Equals("All"))
            {
                genre = null;
            }

            switch ((int)sortModeComboBox.SelectedValue)
            {
                case 1:
                    movieList = ListMovieViewModel.GetNewestMovies(genre);
                    break;
                case 2:
                    movieList = ListMovieViewModel.GetMostPopularMovies(genre);
                    break;
                default:
                    movieList = ListMovieViewModel.GetNewestMovies(genre);
                    break;
            }

            NavigationService.Navigate(new ListMoviesPage(movieList));
        }

        #endregion
    }
}
