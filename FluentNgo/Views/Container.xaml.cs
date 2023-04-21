using System;
using Wpf.Ui.Controls;

namespace FluentNgo.Views
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : UiWindow
    {
        public Container()
        {
            InitializeComponent();
            DateTB.Text = DateTime.Now.ToString("MMM dd, yyyy");
        }

        private void UiWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainGrid.Focus();
        }
    }
}
