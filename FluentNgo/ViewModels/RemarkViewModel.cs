using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using FluentNgo.Core;
using FluentNgo.Models;


namespace FluentNgo.ViewModels
{
    public class RemarkViewModel : ObservableObject
    {
        public List<string> Category { get; set; }
        public List<string> SubCategory { get; set; }

        private ICollectionView _remarksCollection;

        public ICollectionView RemarksCollection
        {
            get { return _remarksCollection; }
            set { _remarksCollection = value; }
        }

        private ObservableCollection<Remark> _remarks;
        public ObservableCollection<Remark> Remarks
        {
            get { return _remarks; }
            set
            {
                if (value == _remarks)
                    return;
                _remarks = value;
                OnPropertyChanged("Remarks");
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

        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                if (value == _filterString) return;
                _filterString = value;
                OnPropertyChanged("FilterString");
                FilterDataGrid(_filterString);
            }
        }
        public List<Student> StudentList{ get; set; }
        public List<string> StudentClass { get; set; }
        public List<string> StudentSection { get; set; }

        public RemarkViewModel()
        {
            Remarks = new ObservableCollection<Remark>(Remark.RemarksGetAll());

            Category = Remarks.Select(r => r.Category).Distinct().ToList();
            SubCategory = Remarks.Select(r => r.SubCategory).Distinct().ToList();
            AnyRowSelected = false;
            RemarksCollection = CollectionViewSource.GetDefaultView(Remarks);

            StudentList = Student.StudentGetAll();


            StudentClass = StudentList.Select(x => x.ClassName).Distinct().ToList();


        }

        public void LoadSections(string ClassName)
        {

            StudentSection = Student.StudentGetAll().Where(x => x.ClassName == ClassName).Select(x => x.Section).Distinct().ToList();

        }

        public void FilterDataGrid(string query)
        {
            FilterString = query;
            bool containsInt = FilterString.Any(char.IsDigit);

            if (FilterString == "")
            {
                RemarksCollection.Filter = null;
            }
            else
            {
                RemarksCollection.Filter = new System.Predicate<object>(FilterRemark);
            }
        }

        private bool FilterRemark(object remarkObject)
        {
            Remark remark = remarkObject as Remark;
            if (remark == null)
            {
                return false;
            }

            bool remarkFilter = string.IsNullOrEmpty(remark.Remarks) ? false : remark.Remarks.ToLower().Contains(FilterString.ToLower());

            return remarkFilter;
        }

        public void LoadStudents( string ClassName , string Section)
        {
            StudentList = Student.StudentGetAll().Where(x => x.ClassName == ClassName && x.Section == Section).ToList();
        }

    }

}