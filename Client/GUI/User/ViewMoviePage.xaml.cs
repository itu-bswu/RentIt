namespace RentItClient.GUI.User
{
    using System.Windows;

    using RentItClient.Types;
    using RentItClient.ViewModels;
    using RentItClient.ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for ViewMoviePage.xaml
    /// </summary>
    public partial class ViewMoviePage
    {
        /// <summary>
        /// The movie the page is showing.
        /// </summary>
        private readonly Movie movie;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMoviePage"/> class.
        /// </summary>
        /// <param name="mId">
        /// The id of the movie to display.
        /// </param>
        public ViewMoviePage(int mId)
            : this()
        {
            movie = ViewMovieViewModel.GetMovieInfo(mId);
            textBoxDescription.Text = movie.Description;
            textBlockRelease.Text = movie.ReleaseDate.Year != 0001 ? movie.ReleaseDate.ToLongDateString() : "Not yet released";
            textBlockTitle.Text = movie.Title;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ViewMoviePage"/> class from being created.
        /// </summary>
        private ViewMoviePage()
        {
            InitializeComponent();
        }

        private void MostRented(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MostRentedPage());
        }

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        private void YourRentals(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistory());
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewMovieListPage(MasterViewModel.Search(textBoxSearch.Text)));
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            MasterViewModel.LogOut();
            NavigationService.Navigate(new LoginPage());
        }

        private void RentMovieClick(object sender, RoutedEventArgs e)
        {
            ViewMovieViewModel.RentMovie(movie.ID);
            NavigationService.Navigate(new DownloadMoviePage(movie.ID));
        }
    }
}
