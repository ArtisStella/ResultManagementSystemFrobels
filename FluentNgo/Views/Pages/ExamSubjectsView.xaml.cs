using FluentNgo.Models;
using FluentNgo.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace FluentNgo.Views.Pages
{
    /// <summary>
    /// Interaction logic for ExamSubjectsView.xaml
    /// </summary>
    public partial class ExamSubjectsView
    {
        ExamSubjectsViewModel ExamSubjectsVM { get; set; }
        public ExamSubjectsView()
        {
            InitializeComponent();
            ExamSubjectsVM = (ExamSubjectsViewModel)DataContext;
        }

        private void ExamSubjectsDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                (sender as DataGrid)?.UnselectAll();
            }
        }

        private void ExamSubjectsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Exam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Exam exam = ((ComboBox)sender).SelectedItem as Exam;
            ExamSubjectsVM.ExamSubjectsGetAll(exam.ExamId);

            SubjectSelection.Visibility = Visibility.Visible;
            SaveBtn.IsEnabled = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Subject selectedSubject = (Subject)SubjectDD.SelectedItem;
            int examId = ((Exam)ExamDD.SelectedItem).ExamId;

            ExamSubjectsVM.AddExamSubject(examId, selectedSubject);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ExamSubjects examSubject = ((FrameworkElement)sender).DataContext as ExamSubjects;
            ExamSubjectsVM.RemoveExamSubject(examSubject);
        }

        private void Marks_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Marks_LostFocus(object sender, RoutedEventArgs e)
        {
            ExamSubjects examSubject = ((FrameworkElement)sender).DataContext as ExamSubjects;
            if (examSubject.SubjectMarks > 100)
            {
                ((TextBox)sender).Text = "100";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string message = "Something went wrong!";

            if (ExamSubjectsVM.SaveExamSubjects()) message = "Succesfully saved!";

            FeedbackSB.MessageQueue?.Enqueue(
                message,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(2));
        }
    }
}

