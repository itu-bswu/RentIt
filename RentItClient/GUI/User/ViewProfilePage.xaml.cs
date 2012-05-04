namespace RentItClient.GUI.User
{
    using System.Windows;
    using System.Windows.Controls;

    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ViewProfilePage.xaml
    /// </summary>
    public partial class ViewProfilePage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProfilePage"/> class.
        /// </summary>
        public ViewProfilePage()
        {
            this.InitializeComponent();
            var u = ViewProfileViewModel.GetCurrentUserInfo();
            this.textBoxEmail.Text = u.Email;
            this.textBoxFullName.Text = u.FullName;
            this.textBoxUserName.Text = u.Username;
        }

        private void MostRented(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MostRentedPage());
        }

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewProfilePage());
        }

        private void YourRentals(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RentalHistory());
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewMovieListPage(MasterViewModel.Search(this.textBoxSearch.Text)));
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            MasterViewModel.LogOut();
            this.NavigationService.Navigate(new LoginPage());
        }

        private void EditProfileClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditProfilePage());
        }
    }
}
