
namespace RentItClient.GUI
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            const string MessageBoxText = "Are you sure you want to close the application. All unsaved data will be lost?";
            const string Caption = "Close application?";
            const MessageBoxButton Button = MessageBoxButton.YesNo;
            const MessageBoxImage Icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(MessageBoxText, Caption, Button, Icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    e.Cancel = true;
                    break;
            }
        }
    }
}
