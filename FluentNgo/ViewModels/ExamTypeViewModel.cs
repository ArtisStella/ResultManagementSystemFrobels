using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Effects;
using System.Windows;
using FluentNgo.Core;
using FluentNgo.Models;
using FluentNgo.Views.Components;

namespace FluentNgo.ViewModels
{
    public class ExamTypeViewModel : ObservableObject
    {
        private ICollectionView _examTypesCollection;
        public ICollectionView ExamTypesCollection
        {
            get { return _examTypesCollection; }
            set { _examTypesCollection = value; }
        }

        private ObservableCollection<ExamType> _examTypes;
        public ObservableCollection<ExamType> ExamTypes
        {
            get { return _examTypes; }
            set
            {
                if (value == _examTypes)
                    return;
                _examTypes = value;
                OnPropertyChanged("ExamTypes");
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

        public ExamTypeViewModel()
        {
            // ExamTypes = new ObservableCollection<ExamType>(ExamType.ExamTypeGetAll());

            AnyRowSelected = false;

            ExamTypesCollection = CollectionViewSource.GetDefaultView(ExamTypes);
        }

        public void Add()
        {
            var examForm = new ExamForm();
            var rootWindow = (Views.Container)Window.GetWindow(Application.Current.MainWindow);
            rootWindow.MainGrid.Effect = new BlurEffect();
            examForm.Owner = rootWindow;

            if ((bool)examForm.ShowDialog())
            {
                //ExamType oExamType = examForm.examType;
                //if (oExamType.ExamTypeSave())
                //{
                //    ExamTypes.Add(oExamType);
                //} else
                //{
                //    MessageBox.Show("Unable to save data, try again.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
            }

            rootWindow.MainGrid.Effect = null;
        }
    }
}
