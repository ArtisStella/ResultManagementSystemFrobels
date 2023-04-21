﻿using System;
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
    public class RemarkViewModel : ObservableObject
    {
        public int RowCount { get; set; }
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

        public RemarkViewModel()
        {
            Remarks = new ObservableCollection<Remark>(Remark.RemarksGetAll());

            AnyRowSelected = false;
            RowCount = 2;
            RemarksCollection = CollectionViewSource.GetDefaultView(Remarks);
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
    }
}