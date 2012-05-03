using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    using System;
    using System.Collections.Generic;

    using RentItClient.GUI.User;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for RentalHistory.xaml
    /// </summary>
    public partial class RentalHistory : Page
    {
        private List<Tuple<string, int, bool>> movies;

        public RentalHistory()
        {
            InitializeComponent();
            this.movies = RentalHistoryViewModel.GetRentals();
            foreach (var t in movies)
            {
                MovieListBox.Items.Add(t.Item1);
            }
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            if (this.movies[MovieListBox.SelectedIndex].Item3)
            {
                this.NavigationService.Navigate(new DownloadMoviePage(this.movies[MovieListBox.SelectedIndex].Item2));
            }
            else
            {
                this.NavigationService.Navigate(new ViewMoviePage(this.movies[MovieListBox.SelectedIndex].Item2));
            }
        }

        private void mostRented(object sender, RoutedEventArgs e)
        {
            //TODO: skal hente en liste over mest downloadet film og give den videre som parameter
            this.NavigationService.Navigate(new MostRentedPage());
        }

        private void viewProfile(object sender, RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes personlige oplysninger med som parameter
            this.NavigationService.Navigate(new ViewProfilePage());
        }

        private void yourRentals(object sender, RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes list af rentals med som parameter
            this.NavigationService.Navigate(new RentalHistory());
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage informationen fra textBoxSearch og så giv det videre til servicen så der kan sendes en liste af resultater til ViewMovieListPage
            this.NavigationService.Navigate(new ViewMovieListPage());
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal lukke connectionen til servicen ned
            this.NavigationService.Navigate(new LoginPage());
        }
    }
}
