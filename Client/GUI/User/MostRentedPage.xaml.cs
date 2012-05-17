namespace RentItClient.GUI.User
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for UserMostRented.xaml
    /// </summary>
    public partial class MostRentedPage
    {
        /// <summary>
        /// The movies in the listbox.
        /// </summary>
        private readonly List<Tuple<string, int>> movies;

        /// <summary>
        /// Initializes a new instance of the <see cref="MostRentedPage"/> class.
        /// </summary>
        public MostRentedPage()
        {
            InitializeComponent();
            movies = MostRentedViewModel.MostRentedMovies();
            foreach (var t in movies)
            {
                MovieListBox.Items.Add(t.Item1);
            }
        }

        private void ViewMovieClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewMoviePage(movies[MovieListBox.SelectedIndex].Item2));
        }

        private void MostRented(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MostRentedPage());
        }

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        private void YourRentals(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistory());
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewMovieListPage(MasterViewModel.Search(textBoxSearch.Text)));
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new LoginPage());
        }
    }
}
