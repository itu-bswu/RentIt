using System.Linq;

namespace RentItClient.GUI.User
{
    using System.Windows;
    using System.Windows.Forms;

    using Types;
    using ViewModels;
    using ViewModels.UserViewModels;

    /// <summary>
    /// Interaction logic for DownloadEditionPage.xaml
    /// </summary>
    public partial class DownloadEditionPage
    {
        #region Fields

        /// <summary>
        /// The movie being displayed.
        /// </summary>
        private readonly int eId;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadEditionPage"/> class.
        /// </summary>
        /// <param name="m">The movie to show.</param>
        /// <param name="editionId">The id of the edition to show.</param>
        public DownloadEditionPage(Movie m, int editionId)
            : this()
        {
            eId = editionId;
            textBlockTitle.Text = m.Title;
            textBlockDescription.Text = m.Description;
            textBlockEdition.Text = m.Editions.First(e => e.Item2 == eId).Item1;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DownloadEditionPage"/> class from being created.
        /// </summary>
        private DownloadEditionPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Click methods

        private void ListMovies(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage());
        }

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProfilePage());
        }

        private void YourRentals(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RentalHistoryPage());
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMoviesPage(MasterViewModel.Search(textBoxSearch.Text)));
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LogOut())
            {
                NavigationService.Navigate(new LoginPage());
            }
        }

        private void DownloadClick(object sender, RoutedEventArgs e)
        {
            string path;
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }
            else
            {
                return;
            }

            DownloadEditionViewModel.DownloadFile(eId, path);
            NavigationService.Navigate(new RentalHistoryPage());
        }
        #endregion
    }
}
