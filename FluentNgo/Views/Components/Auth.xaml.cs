using TFSResult.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Security;

namespace TFSResult.Views.Components
{
    public partial class Auth : Window
    {
        public SecureString Password { get; set; }

        public Auth()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        private void Login(object sender, RoutedEventArgs e)
        {
            if (Password.ToString() == "1234")
            {
                new Container().Show();
            }
        }

        private void SF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
