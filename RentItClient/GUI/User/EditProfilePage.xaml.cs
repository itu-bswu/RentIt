using System.Windows.Controls;

namespace RentItClient
{
    using System.Windows;

    using RentItClient.GUI.User;
    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page
    {
        public EditProfilePage()
        {
            InitializeComponent();
            var u = ViewProfileViewModel.GetCurrentUserInfo();
            textBoxEmail.Text = u.Email;
            textBoxFullName.Text = u.FullName;
            //password ting
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

        private void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            //TODO: hent information fra TextBox: textBoxFullName, textBoxUserName, textBoxEmail,
            //TODO: og erstat så service felterne med informationen.

            const string MessageBoxText = "Do you want to save changes?";
            const string Caption = "Save Changes?";
            const MessageBoxButton Button = MessageBoxButton.YesNoCancel;
            const MessageBoxImage Icon = MessageBoxImage.Warning;
            MessageBox.Show(MessageBoxText, Caption, Button, Icon);

            MessageBoxResult result = MessageBox.Show(MessageBoxText, Caption, Button, Icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    //TODO: save the changes made on the user obejct to the database
                    NavigationService.Navigate(new RentalHistory());
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    NavigationService.Navigate(new RentalHistory());
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    NavigationService.Navigate(new RentalHistory());
                    break;
            }
        }

    }
}
