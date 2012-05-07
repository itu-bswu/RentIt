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

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            //TODO: Den skal på en eller anden måde give det element med der er selected i listboxen.
            this.NavigationService.Navigate(new CPViewMovie());
        }
    }
}
