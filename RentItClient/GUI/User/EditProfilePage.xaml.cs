using System.Windows.Controls;

namespace RentItClient
{
    using System.Windows;

    using RentItClient.GUI.User;

    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class EditProfilePage : Page
    {
        public EditProfilePage()
        {
            InitializeComponent();
        }

        private void mostRented(object sender, RoutedEventArgs e)
        {
            //TODO: skal hente en liste over mest downloadet film og give den videre som parameter
            this.NavigationService.Navigate(new MostRentedPage());
        }

        private void viewProfile(object sender, RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes personlige oplysninger med som parameter
            this.NavigationService.Navigate(new ViewProfilePage());
        }

        private void yourRentals(object sender, RoutedEventArgs e)
        {
            //TODO: skal tjekke hvilken bruger der logget ind og så give vedkommendes list af rentals med som parameter
            this.NavigationService.Navigate(new RentalHistory());
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage informationen fra textBoxSearch og så giv det videre til servicen så der kan sendes en liste af resultater til ViewMovieListPage
            this.NavigationService.Navigate(new ViewMovieListPage());
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal lukke connectionen til servicen ned
            this.NavigationService.Navigate(new LoginPage());
        }

        private void saveChangesClick(object sender, RoutedEventArgs e)
        {
            //TODO: hent information fra TextBox: textBoxFullName, textBoxUserName, textBoxEmail,
            //TODO: og erstat så service felterne med informationen.

            string messageBoxText = "Do you want to save changes?";
            string caption = "Save Changes?";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    this.NavigationService.Navigate(new ViewProfilePage());
                    //TODO: save the changes made on the user obejct to the database
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    this.NavigationService.Navigate(new ViewProfilePage());
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    this.NavigationService.Navigate(new EditProfilePage());
                    break;
            }
        }

    }
}
