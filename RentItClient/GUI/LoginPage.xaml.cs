namespace RentItClient.GUI
{
    using System.Windows;
    using ContentProvider;

    using Types;
    using User;
    using ViewModels.AdministrationViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class LoginPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Click methods

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var success = LoginViewModel.Login(textBoxUsername.Text, passwordBox.Password);
            if (success)
            {
                switch (LoginViewModel.LoggedInUser)
                {
                    case UserType.User:
                        NavigationService.Navigate(new ListMoviesPage());
                        break;

                    case UserType.ContentProvider:
                        NavigationService.Navigate(new CPYourMoviesPage());
                        break;

                    default:
                        NavigationService.Navigate(new LoginPage());
                        break;
                }
            }
            else
            {
                MessageBox.Show("Login was not successful. Your username and/or password may be incorrect.");
                NavigationService.Navigate(new LoginPage());
            }
        }

        private void SignupClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
        #endregion
    }
}
