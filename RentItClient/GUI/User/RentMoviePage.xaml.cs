using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RentItClient.GUI.User
{
    using RentItClient.ViewModels;

    /// <summary>
    /// Interaction logic for RentMoviePage.xaml
    /// </summary>
    public partial class RentMoviePage : Page
    {
        public RentMoviePage()
        {
            InitializeComponent();
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
            MasterViewModel.LogOut();
            NavigationService.Navigate(new LoginPage());
        }

        private void RentMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal rettes så den tager den korrekte parameter
            //NavigationService.Navigate(new DownloadMoviePage());
        }

    }
}
