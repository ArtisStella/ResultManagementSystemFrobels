using FluentNgo.Core;
using FluentNgo.Models;
using FluentNgo.Views.Components;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace FluentNgo.ViewModels
{
    public class ExamViewModel : ObservableObject
    {
        private ICollectionView _examsCollection;

        public ICollectionView ExamsCollection
        {
            get { return _examsCollection; }
            set { _examsCollection = value; }
        }

        private ObservableCollection<Exam> _exams;
        public ObservableCollection<Exam> Exams
        {
            get { return _exams; }
            set
            {
                if (value == _exams)
                    return;
                _exams = value;
                OnPropertyChanged("Exams");
            }
        }

        private bool _anyRowSelected;
        public bool AnyRowSelected
        {
            get { return _anyRowSelected; }
            set
            {
                if (value == _anyRowSelected) return;
                _anyRowSelected = value;
                OnPropertyChanged("AnyRowSelected");
            }
        }

        private string FilterString { get; set; }

        public ExamViewModel()
        {
            Exams = new ObservableCollection<Exam>(Exam.ExamGetAll());

            AnyRowSelected = false;

            ExamsCollection = CollectionViewSource.GetDefaultView(Exams);
        }

        public void AddExam()
        {
            var examForm = new ExamForm();
            var rootWindow = (Views.Container)Window.GetWindow(Application.Current.MainWindow);
            rootWindow.MainGrid.Effect = new BlurEffect();
            examForm.Owner = rootWindow;

            if ((bool)examForm.ShowDialog())
            {
                Exam oExam = examForm.exam;
                if (oExam.ExamSave())
                {
                    Exams.Add(oExam);
                }
                else
                {
                    MessageBox.Show("Unable to save data, try again.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            rootWindow.MainGrid.Effect = null;
        }

        public void RemoveExam(Exam exam)
        {
            if (exam.DeleteExam())
            {
                Exams.Remove(exam);
            }
        }

        public void EditExam(Exam exam)
        {
            var examForm = new ExamForm(exam);
            var rootWindow = (Views.Container)Window.GetWindow(Application.Current.MainWindow);

            rootWindow.MainGrid.Effect = new BlurEffect();
            examForm.Owner = rootWindow;

            if ((bool)examForm.ShowDialog())
            {
                Exam oExam = examForm.exam;
                if (!oExam.UpdateExam())
                {
                    MessageBox.Show("Unable to save data, try again.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            rootWindow.MainGrid.Effect = null;
        }

        public void FilterDataGrid(string query)
        {

            FilterString = query;
            bool containsInt = FilterString.Any(char.IsDigit);


            if (FilterString == "")
            {
                ExamsCollection.Filter = null;
            }
            else
            {
                ExamsCollection.Filter = new System.Predicate<object>(FilterExams);
            }
        }

        private bool FilterExams(object stud)
        {
            Exam? exam = stud as Exam;

            bool nameFilter = exam.ExamTypeName == null ? false : exam.ExamTypeName.ToLower().Contains(FilterString.ToLower());
            //bool idFilter = exam.GRNo.ToString().Contains(FilterString.ToLower());

            //return nameFilter || idFilter;

            return nameFilter;
        }
    }
}
