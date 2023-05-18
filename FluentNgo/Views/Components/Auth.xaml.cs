using TFSResult.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Security;
using Wpf.Ui.Controls;
using System.Windows.Input;

namespace TFSResult.Views.Components
{
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            string password = PasswordBox.Password;
            if (password == "1234")
            {
                Application.Current.MainWindow = new Container();
                Application.Current.MainWindow.Show();
                Close();
            } else
            {
                System.Windows.MessageBox.Show("Incorrect Password", "Error");
            }
        }

        private void SF_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Login();
            }
        }
    }
}
