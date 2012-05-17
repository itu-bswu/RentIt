namespace RentItClient.GUI.User
{
    using System;
    using System.Windows;
    using Types;
    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for RentalHistory.xaml
    /// </summary>
    public partial class RentalHistoryPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalHistoryPage"/> class. 
        /// </summary>
        public RentalHistoryPage()
        {
            InitializeComponent();

            MovieListBox.ItemsSource = RentalHistoryViewModel.GetRentals();
            MovieListBox.DisplayMemberPath = "Item1";
            MovieListBox.SelectedValuePath = "Item2";
        }
        #endregion

        #region Click methods

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            if (MovieListBox.SelectedIndex != -1)
            {
                var item = (Tuple<string, int, Movie>)MovieListBox.SelectedItem;
                var editionId = item.Item2;
                var movieObject = item.Item3;

                if (MasterViewModel.IsCurrentRental(editionId))
                {
                    NavigationService.Navigate(new DownloadEditionPage(movieObject, editionId));
                }
                else
                {
                    NavigationService.Navigate(new ViewEditionPage(movieObject, editionId));
                }
            }
        }

        private void ListMoviesClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage());
        }

        private void ViewProfileClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        private void YourRentalsClick(object sender, RoutedEventArgs e)
        {
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
        #endregion
    }
}
