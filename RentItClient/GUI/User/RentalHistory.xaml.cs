namespace RentItClient.GUI.User
{
    using System.Windows;
    using System;
    using System.Collections.Generic;

    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for RentalHistory.xaml
    /// </summary>
    public partial class RentalHistory
    {
        /// <summary>
        /// The movies in the listbox.
        /// </summary>
        private readonly List<Tuple<string, int, bool>> movies;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalHistory"/> class.
        /// </summary>
        public RentalHistory()
        {
            InitializeComponent();
            movies = RentalHistoryViewModel.GetRentals();
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

        private void MostRented(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage());
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
            MasterViewModel.LogOut();
            NavigationService.Navigate(new LoginPage());
        }
    }
}
