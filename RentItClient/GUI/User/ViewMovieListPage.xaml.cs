using System.Windows;
using System.Windows.Controls;

namespace RentItClient.GUI.User
{
    using System;
    using System.Collections.Generic;

    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ViewMovieListPage.xaml
    /// </summary>
    public partial class ViewMovieListPage : Page
    {
        /// <summary>
        /// The movies in the listbox.
        /// </summary>
        private readonly List<Tuple<string, int, bool>> movies;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMovieListPage"/> class.
        /// </summary>
        /// <param name="movies">
        /// The movies to be put in the listbox.
        /// </param>
        public ViewMovieListPage(List<Tuple<string, int, bool>> movies)
            : this()
        {
            this.movies = movies;
            foreach (var t in movies)
            {
                MovieListBox.Items.Add(t.Item1);
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ViewMovieListPage"/> class from being created.
        /// </summary>
        private ViewMovieListPage()
        {
            InitializeComponent();
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
    }
}