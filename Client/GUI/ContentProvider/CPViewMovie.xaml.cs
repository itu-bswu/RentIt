namespace RentItClient.GUI.ContentProvider
{
    using System.Windows;

    using RentItClient.GUI.User;

    /// <summary>
    /// Interaction logic for CPViewMovie.xaml
    /// </summary>
    public partial class CPViewMovie
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CPViewMovie"/> class.
        /// </summary>
        public CPViewMovie()
        {
            this.InitializeComponent();
        }

        private void UploadMovie(object sender, RoutedEventArgs e)
        {
            CPUploadMovies uploadMovies = new CPUploadMovies();
            this.NavigationService.Navigate(uploadMovies);
        }

        private void YourMovies(object sender, RoutedEventArgs e)
        {
            //TODO: metoden skal give en CPs liste af oploaded film med hver gang den her knap bliver trykket
            //TODO: så Listboxen i CPYourMovies kan blive lavet med de elementer
            CPYourMovies yourMovies = new CPYourMovies();
            this.NavigationService.Navigate(yourMovies);
        }

        private void RegisterMovie(object sender, RoutedEventArgs e)
        {
            CPRegisterMovie registerMovie = new CPRegisterMovie();
            this.NavigationService.Navigate(registerMovie);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
            //TODO: luk forbindelsen til servicen
        }

        private void EditInformationClick(object sender, RoutedEventArgs e)
        {
            CPEditMovie editMovie = new CPEditMovie();
            this.NavigationService.Navigate(editMovie);
        }

        private void DeleteMovieClick(object sender, RoutedEventArgs e)
        {
            const string MessageBoxText1 = "Are you sure you want to delete this movie?";
            const string Caption1 = "Delete Movie?";
            const MessageBoxButton Button1 = MessageBoxButton.YesNoCancel;
            const MessageBoxImage Icon1 = MessageBoxImage.Warning;
            MessageBox.Show(MessageBoxText1, Caption1, Button1, Icon1);

            MessageBoxResult result = MessageBox.Show(MessageBoxText1, Caption1, Button1, Icon1);

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
