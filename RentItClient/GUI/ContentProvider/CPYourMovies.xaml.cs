using System.Windows;
using System.Windows.Controls;

namespace RentItClient
{
    /// <summary>
    /// Interaction logic for CPYourMovies.xaml
    /// </summary>
    public partial class CPYourMovies : Page
    {
        public CPYourMovies()
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

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CPViewMovie());
        }
    }
}
