﻿using FluentNgo.Models;
using FluentNgo.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FluentNgo.Views.Pages;

public partial class ExamTypeView
{
    ExamTypeViewModel ExamTypeVM { get; set; }
    public object ExamTypesDG { get; private set; }

    public ExamTypeView()
    {
        InitializeComponent();
        ExamTypeVM = (ExamTypeViewModel)this.DataContext;

    }
    private void ExamTypeDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender != null)
        {
            (sender as DataGrid)?.UnselectAll(); ;
        }
    }

    private void Row_Click(object sender, MouseButtonEventArgs e)
    {

    }
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        ExamTypeVM.Add();
    }

}
