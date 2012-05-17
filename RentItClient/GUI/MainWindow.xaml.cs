// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="RentIt">
// Copyright (c) RentIt. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace RentItClient.GUI
{
    using System.Windows;
    using System.Windows.Threading;
    using ViewModels;

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

            Application.Current.DispatcherUnhandledException += HandleExceptions;
        }

        /// <summary>
        /// Asks the user if he wishes to change window.
        /// </summary>
        /// <returns>True if the user chose to change window, false if not.</returns>
        public static bool ChangeWindow()
        {
            const string messageBoxText = "Are you sure you want to change window? All unsaved information will be lost.";
            const string caption = "Change window?";
            const MessageBoxButton button = MessageBoxButton.YesNo;
            const MessageBoxImage icon = MessageBoxImage.Warning;

            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Asks the user if he wishes to log out.
        /// </summary>
        /// <returns>True if the user chose to log out, false if not.</returns>
        public static bool LogOut()
        {
            const string messageBoxText = "Are you sure you want to log out? All unsaved changes will be lost.";
            const string caption = "Change window?";
            const MessageBoxButton button = MessageBoxButton.YesNo;
            const MessageBoxImage icon = MessageBoxImage.Warning;

            var result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MasterViewModel.LogOut();
                    return true;
                case MessageBoxResult.No:
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Happens when main window closes.
        /// </summary>
        /// <param name="e">The eventargs.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (MasterViewModel.SkipClosingMessage)
            {
                return;
            }

            const string messageBoxText = "Are you sure you want to close the application? All unsaved data will be lost.";
            const string caption = "Close application?";
            const MessageBoxButton button = MessageBoxButton.YesNo;
            const MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

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

        /// <summary>
        /// Handles unhandled exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void HandleExceptions(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show("An error occured. The client is forced to shut down.");
            MasterViewModel.SkipClosingMessage = true;
            Application.Current.MainWindow.Close();
        }
    }
}
