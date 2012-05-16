namespace RentItClient.GUI.User
{
    using System.Windows;

    using RentItClient.GUI.ContentProvider;
    using RentItClient.Types;
    using RentItClient.ViewModels.AdministrationViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            LoginViewModel.Login(textBoxUsername.Text, passwordBox.Password);

            if (LoginViewModel.LoggedInUser == UserType.User)
            {
                NavigationService.Navigate(new ListMoviesPage());
            }
            else if (LoginViewModel.LoggedInUser == UserType.ContentProvider)
            {
                NavigationService.Navigate(new CPYourMovies());
            }
            else
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        private void SignupClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
