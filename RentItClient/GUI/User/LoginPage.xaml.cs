namespace RentItClient
{
    using System.Windows;
    using System.Windows.Controls;

    using RentItClient.Types;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 

    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            LoginViewModel.Login(textBoxUsername.Text, passwordBox.Password);

            if (LoginViewModel.LoggedInUser == UserType.User)
            {
                this.NavigationService.Navigate(new MostRentedPage());
            }
            else if (LoginViewModel.LoggedInUser == UserType.ContentProvider)
            {
                this.NavigationService.Navigate(new CPYourMovies());
            }
            else
            {
                this.NavigationService.Navigate(new LoginPage());
            }
        }

        private void SignupClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegistrationPage());
        }
    }
}
