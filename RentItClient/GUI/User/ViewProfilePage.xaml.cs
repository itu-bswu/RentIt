namespace RentItClient.GUI.User
{
    using System.Windows;

    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ViewProfilePage.xaml
    /// </summary>
    public partial class ViewProfilePage
    {
        #region Constructors

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
        #endregion

        #region Click methods

        private void ListMoviesClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage());
        }

        private void ViewProfileClick(object sender, RoutedEventArgs e)
        {
        }

        private void YourRentalsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistoryPage());
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

        private void EditProfileClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProfilePage());
        }
        #endregion
    }
}
