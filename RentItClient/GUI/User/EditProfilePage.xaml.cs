namespace RentItClient.GUI.User
{
    using System.Windows;

    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage
    {
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

        private void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            if (!passwordBoxPassword.Password.Equals(passwordBoxConfirmPassword.Password))
            {
                MessageBox.Show("Password and Confirm password did not match.");
                return;
            }

            const string MessageBoxText = "Do you want to save changes?";
            const string Caption = "Save Changes?";
            const MessageBoxButton Button = MessageBoxButton.YesNoCancel;
            const MessageBoxImage Icon = MessageBoxImage.Warning;
            var result = MessageBox.Show(MessageBoxText, Caption, Button, Icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    EditProfileViewModel.EditUserProfile(textBoxEmail.Text, textBoxFullName.Text, passwordBoxPassword.Password);
                    NavigationService.Navigate(new ViewProfilePage());
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    NavigationService.Navigate(new ViewProfilePage());
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    break;
            }
        }
    }
}
