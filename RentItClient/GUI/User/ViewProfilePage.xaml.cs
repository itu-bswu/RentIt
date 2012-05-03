using System.Windows.Controls;

namespace RentItClient
{
    using System.Windows;

    using RentItClient.GUI.User;
    using RentItClient.Types;
    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ViewProfilePage.xaml
    /// </summary>
    public partial class ViewProfilePage : Page
    {
        public ViewProfilePage()
        {
            InitializeComponent();
            var u = ViewProfileViewModel.GetCurrentUserInfo();
            textBoxEmail.Text = u.Email;
            // TODO: Full name issue
            textBoxUserName.Text = u.Username;
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

        private void editProfileClick(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfilePage());
        }
    }
}
