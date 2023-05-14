using TFSResult.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TFSResult.Views.Components
{
    /// <summary>
    /// Interaction logic for ExamForm.xaml
    /// </summary>
    public partial class ExamForm : Window
    {
        public Exam exam { get; set; }
        public List<ExamType> ExamTypesList { get; set; }

        public ExamForm()
        {
            ExamTypesList = ExamType.ExamTypesGetAll();

            InitializeComponent();
            exam = new Exam();
            DataContext = this;
        }

        public ExamForm(Exam _exam)
        {
            exam = _exam;
            ExamTypesList = ExamType.ExamTypesGetAll();

            InitializeComponent();

            ExamTypeDD.SelectedValue = exam.ExamTypeId;
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

        private void ExamType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ExamType selectedExamType = (ExamType)comboBox.SelectedItem;

            exam.ExamTypeId = selectedExamType.ExamTypeId;
            exam.ExamTypeName = selectedExamType.ExamTypeName;
        }
    }
}
