using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    /// <summary>
    /// Interaction logic for CPViewMovie.xaml
    /// </summary>
    public partial class CPViewMovie : Page
    {
        public CPViewMovie()
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

        private void UploadMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: Upload movie
            this.NavigationService.Navigate(new CPYourMovies());
        }

        private void editInformationClick(object sender, RoutedEventArgs e)
        {
            CPEditMovie editMovie = new CPEditMovie();
            this.NavigationService.Navigate(editMovie);
        }

        private void deleteMovieClick(object sender, RoutedEventArgs e)
        {
            string messageBoxText1 = "Are you sure you want to delete this movie?";
            string caption1 = "Delete Movie?";
            MessageBoxButton button1 = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon1 = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(messageBoxText1, caption1, button1, icon1);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    CPYourMovies yourMovies = new CPYourMovies();
                    this.NavigationService.Navigate(yourMovies);
                    //TODO: Remove the movie from the database
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
