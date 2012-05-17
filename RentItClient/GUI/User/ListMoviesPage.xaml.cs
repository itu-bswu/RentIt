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

        private void ViewMovieClick(object sender, RoutedEventArgs e)
        {
            if (MovieListBox.SelectedIndex != -1)
            {
                var selectedId = (int)MovieListBox.SelectedValue;
                NavigationService.Navigate(new ViewMoviePage(selectedId));
            }
        }

        private void ListMoviesClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage());
        }

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        private void YourRentals(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistoryPage());
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage(MasterViewModel.Search(textBoxSearch.Text)));
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LogOut())
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

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
