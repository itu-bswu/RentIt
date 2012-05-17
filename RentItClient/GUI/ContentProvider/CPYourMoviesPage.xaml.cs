using RentItClient.Types;

namespace RentItClient.GUI.ContentProvider
{
    using System.Windows;
    using ViewModels.ProviderViewModels;

    /// <summary>
    /// Interaction logic for CPYourMoviesPage.xaml
    /// </summary>
    public partial class CPYourMoviesPage
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CPYourMoviesPage"/> class.
        /// </summary>
        public CPYourMoviesPage()
        {
            InitializeComponent();

            MovieListBox.ItemsSource = CPMoviesViewModel.GetMovies();
            MovieListBox.DisplayMemberPath = "Item1";
            MovieListBox.SelectedValuePath = "Item2";
        }
        #endregion Constructors

        #region Click methods

        private void RegisterMovieClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CPRegisterMoviePage());
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LogOut())
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CPViewMoviePage(((Movie)MovieListBox.SelectedValue).ID));
        }
        #endregion
    }
}
