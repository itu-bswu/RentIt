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
            this.NavigationService.Navigate(new CPEditMovie());
        }

        private void DeleteMovieClick(object sender, RoutedEventArgs e)
        {
            const string MessageBoxText1 = "Are you sure you want to delete this movie, and all the editions aassociated with it?";
            const string Caption1 = "Delete Movie?";
            const MessageBoxButton Button1 = MessageBoxButton.YesNo;
            const MessageBoxImage Icon1 = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(MessageBoxText1, Caption1, Button1, Icon1);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    this.NavigationService.Navigate(new CPYourMovies());
                    //TODO: Remove the movie from the database
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
            }
        }

        //TODO: check om der er selected et element i listboxen hvis ikke skal DeleteEditionClick ikke gøre noget
        private void DeleteEditionClick(object sender, RoutedEventArgs e)
        {
            const string MessageBoxText1 = "Are you sure you want to delete the selected edition?";
            const string Caption1 = "Delete edition?";
            const MessageBoxButton Button1 = MessageBoxButton.YesNo;
            const MessageBoxImage Icon1 = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(MessageBoxText1, Caption1, Button1, Icon1);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    this.NavigationService.Navigate(new CPViewMovie());
                    //TODO: Remove the edition from the database
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
            }
        }

        private void UploadEditionClick(object sender, RoutedEventArgs e)
        {
            const string MessageBoxText = "Do you want to add the edition?";
            const string Caption = "Add edition?";
            const MessageBoxButton Button = MessageBoxButton.YesNo;
            const MessageBoxImage Icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(MessageBoxText, Caption, Button, Icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    this.NavigationService.Navigate(new CPYourMovies());
                    //TODO: add editionen til movie objectet
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
            }
        }
    }
}
