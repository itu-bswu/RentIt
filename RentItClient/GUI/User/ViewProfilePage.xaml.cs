namespace RentItClient.GUI.User
{
    using System.Windows;

    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ViewProfilePage.xaml
    /// </summary>
    public partial class ViewProfilePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProfilePage"/> class.
        /// </summary>
        public ViewProfilePage()
        {
            InitializeComponent();
            var u = ViewProfileViewModel.GetCurrentUserInfo();
            textBoxEmail.Text = u.Email;
            textBoxFullName.Text = u.FullName;
            textBoxUserName.Text = u.Username;
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

        private void EditProfileClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfilePage());
        }
    }
}
