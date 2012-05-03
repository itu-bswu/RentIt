using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    using System;
    using System.Collections.Generic;

    using RentItClient.GUI.User;
    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for RentalHistory.xaml
    /// </summary>
    public partial class RentalHistory : Page
    {
        /// <summary>
        /// The movies in the listbox.
        /// </summary>
        private readonly List<Tuple<string, int, bool>> movies;

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
            if (MovieListBox.SelectedIndex != -1)
            {
                if (movies[MovieListBox.SelectedIndex].Item3)
                {
                    NavigationService.Navigate(new DownloadMoviePage(movies[MovieListBox.SelectedIndex].Item2));
                }
                else
                {
                    NavigationService.Navigate(new ViewMoviePage(movies[MovieListBox.SelectedIndex].Item2));
                }
            }
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
            MasterViewModel.LogOut();
            NavigationService.Navigate(new LoginPage());
        }
    }
}
