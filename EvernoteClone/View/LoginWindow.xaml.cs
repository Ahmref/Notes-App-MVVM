using EvernoteClone.ViewModel;
using ModernWpf;
using System;
using System.Windows;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        LoginVM viewModel;
        public LoginWindow()
        {

            InitializeComponent();
            if (ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            }
            viewModel = Resources["vm"] as LoginVM;
            viewModel.Authenticated += ViewModel_Authenticated;
        }

        private void ViewModel_Authenticated(object sender, EventArgs e)
        {
            Close();
        }
    }
}
