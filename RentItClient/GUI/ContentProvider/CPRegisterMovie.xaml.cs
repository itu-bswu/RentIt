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

        private void registerMovieClick(object sender, RoutedEventArgs e)
        {
            //TODO: Register movie in database
        }
    }
}
