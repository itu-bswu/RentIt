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

        private void uploadMovie(object sender, RoutedEventArgs e)
        {
            CPUploadMovies uploadMovies = new CPUploadMovies();
            this.NavigationService.Navigate(uploadMovies);
        }

        private void yourMovies(object sender, RoutedEventArgs e)
        {
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

        }

        private void editInformationClick(object sender, RoutedEventArgs e)
        {
            CPEditMovie editMovie = new CPEditMovie();
            this.NavigationService.Navigate(editMovie);
        }

        private void deleteMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: Delete movie from database
        }
    }
}
