using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{

    /// <summary>
    /// Interaction logic for CPEditMovie.xaml
    /// </summary>
    public partial class CPEditMovie : Page
    {
        public CPEditMovie()
        {
            InitializeComponent();
        }

        private void uploadMovie(object sender, RoutedEventArgs e)
        {
            CPUploadMovies uploadMovies = new CPUploadMovies();
            this.NavigationService.Navigate(uploadMovies);
        }

        private void yourMovies(object sender, RoutedEventArgs e)
        {
            //TODO: metoden skal give en CPs liste af oploaded film med hver gang den her knap bliver trykket
            //TODO: så Listboxen i CPYourMovies kan blive lavet med de elementer
            CPYourMovies yourMovies = new CPYourMovies();
            this.NavigationService.Navigate(yourMovies);
        }

        private void registerMovie(object sender, RoutedEventArgs e)
        {
            CPRegisterMovie registerMovie = new CPRegisterMovie();
            this.NavigationService.Navigate(registerMovie);
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
            //TODO: luk forbindelsen til servicen
        }

        private void saveChangesClick(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Do you want to save changes?";
            string caption = "Save Changes?";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(messageBoxText, caption, button, icon);

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    CPYourMovies yourMovies = new CPYourMovies();
                    this.NavigationService.Navigate(yourMovies);
                    //TODO: save the changes made on the movie obejct to the database
                    //TODO: Du skal hente info fra: TextBox: textBoxTitle, textBoxGenre og textBoxDescription
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    CPYourMovies yourMovies1 = new CPYourMovies();
                    this.NavigationService.Navigate(yourMovies1);
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    CPYourMovies yourMovies2 = new CPYourMovies();
                    this.NavigationService.Navigate(yourMovies2);
                    break;
            }
        }
    }
}
