using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RentItClient.GUI.User;
    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for UserMostRented.xaml
    /// </summary>
    public partial class MostRentedPage : Page
    {
        /// <summary>
        /// The movies in the listbox.
        /// </summary>
        private readonly List<Tuple<string, int>> movies;

        public MostRentedPage()
        {
            InitializeComponent();
            movies = MostRentedViewModel.MostRentedMovies();
            foreach (var t in movies)
            {
                MovieListBox.Items.Add(t.Item1);
            }
        }

        private void viewMovieClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewMoviePage(this.movies[MovieListBox.SelectedIndex].Item2));
        }

        private void mostRented(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MostRentedPage());
        }

        private void viewProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        private void yourRentals(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistory());
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewMovieListPage(MasterViewModel.Search(textBoxSearch.Text)));
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new LoginPage());
        }
    }
}
