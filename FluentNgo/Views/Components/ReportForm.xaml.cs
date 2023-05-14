using TFSResult.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TFSResult.Views.Components
{
    public partial class ReportForm : Window
    {
        public string TotalDays { get; set; }
        public string DaysAttended { get; set; }
        public int ExamId { get; set; } 
        public List<Exam> ExamList { get; set; }

        public ReportForm()
        {
            ExamList = Exam.ExamGetAll();

            InitializeComponent();
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

        private void SF_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }


        private void Exam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ExamId = (int)comboBox.SelectedValue;
        }
    }
}
