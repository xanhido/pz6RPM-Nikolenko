
using pz6RPM_Nikolenko.Pages;
using System;
using System.Windows;

namespace pz6RPM_Nikolenko
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fr_content.Content = new Autho();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (fr_content.CanGoBack)
                fr_content.GoBack();
        }

        private void fr_content_ContentRendered(object sender, EventArgs e)
        {
            if (fr_content.CanGoBack)
                btnBack.Visibility = Visibility.Visible;
            else
                btnBack.Visibility = Visibility.Hidden;
        }
    }
}