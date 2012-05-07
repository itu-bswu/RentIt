using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    /// <summary>
    /// Interaction logic for CPRegisterMovie.xaml
    /// </summary>
    public partial class CPRegisterMovie : Page
    {
        public CPRegisterMovie()
        {
            InitializeComponent();
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

        private void registerMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: Register movie in database
            string messageBoxText = "You are about to register a movie, would you like to upload it right away aswell?";
            string caption = "Upload movie?";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    this.NavigationService.Navigate(new CPUploadMovies());
                    //TODO: save the changes made on the user obejct to the database
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    this.NavigationService.Navigate(new CPYourMovies());
                    break;
            }
            //TODO: du skal hente info fra TextBox: textBoxTitle, textBoxGenre, textBoxDescription
            //TODO: og du skal hente info fra DatePicker: DatePickerReleaseDate
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddGenreClick(object sender, RoutedEventArgs e)
        {
            //TODO: skal tage stringen i textBoxGenre og add den til genrelisten
            this.NavigationService.Navigate(new CPRegisterMovie());
        }
    }
}
