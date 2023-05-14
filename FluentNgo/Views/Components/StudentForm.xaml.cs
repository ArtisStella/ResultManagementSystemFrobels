using TFSResult.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TFSResult.Views.Components
{
    /// <summary>
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : Window
    {
        public Student student { get; set; }
        public StudentForm()
        {
            InitializeComponent();
            student = new Student();
            DataContext = this;
        }

        public StudentForm(Student stud)
        {
            InitializeComponent();
            student = stud;
            DataContext = this;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SubmitForm(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void SF_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RootGrid.Focus();
        }
    }
}
