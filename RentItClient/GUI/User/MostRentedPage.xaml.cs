using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RentItClient.GUI.User;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for UserMostRented.xaml
    /// </summary>
    public partial class MostRentedPage : Page
    {
        private readonly List<Tuple<string, int>> movies;

        public MostRentedPage()
        {
            InitializeComponent();
            this.movies = MostRentedViewModel.MostRentedMovies();
            foreach (var t in movies)
            {
                MovieListBox.Items.Add(t.Item1);
            }
        }

        private void viewMovieClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewMoviePage(this.movies[MovieListBox.SelectedIndex].Item2));
        }

        private void mostRented(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MostRentedPage());
        }

        private void viewProfile(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewProfilePage());
        }

        private void yourRentals(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RentalHistory());
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage informationen fra textBoxSearch og så giv det videre til servicen så der kan sendes en liste af resultater til ViewMovieListPage
            this.NavigationService.Navigate(new ViewMovieListPage());
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new LoginPage());
        }
    }
}
