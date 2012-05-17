namespace RentItClient.GUI.User
{
    using System.Windows;

    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProfilePage"/> class.
        /// </summary>
        public EditProfilePage()
        {
            InitializeComponent();
            var u = ViewProfileViewModel.GetCurrentUserInfo();
            textBoxEmail.Text = u.Email;
            textBoxFullName.Text = u.FullName;
            passwordBoxPassword.Password = u.Password;
            passwordBoxConfirmPassword.Password = u.Password;
        }
        #endregion

        #region Click methods

        private void ListMoviesClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new ListMoviesPage());
            }
        }

        private void ViewProfileClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new ViewProfilePage());
            }
        }

        private void YourRentalsClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.ChangeWindow())
            {
                NavigationService.Navigate(new RentalHistoryPage());
            }
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

        private void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            if (!passwordBoxPassword.Password.Equals(passwordBoxConfirmPassword.Password))
            {
                MessageBox.Show("Password and Confirm password did not match.");
                return;
            }

            const string messageBoxText = "Do you want to save changes?";
            const string caption = "Save Changes?";
            const MessageBoxButton button = MessageBoxButton.YesNoCancel;
            const MessageBoxImage icon = MessageBoxImage.Warning;
            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (EditProfileViewModel.EditUserProfile(textBoxEmail.Text, textBoxFullName.Text, passwordBoxPassword.Password))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Something went wrong when trying edit your information. " +
                                "This error may occur if there is something wrong with the data you input" +
                                " or if there is a problem with the service." +
                                "\n If all data appears ok, please restart the client.");
                    }
                    NavigationService.Navigate(new ViewProfilePage());
                    break;
                case MessageBoxResult.No:
                    NavigationService.Navigate(new ViewProfilePage());
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
        #endregion
    }
}
