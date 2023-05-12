using FluentNgo.Models;
using FluentNgo.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FluentNgo.Views.Pages;

public partial class StudentView
{
    StudentViewModel StudentVM { get; set; }
    public StudentView()
    {
        InitializeComponent();
        StudentVM = (StudentViewModel)this.DataContext;
    }

    private void StudentsDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender != null)
        {
            (sender as DataGrid)?.UnselectAll(); ;
        }
    }

    private void Row_Click(object sender, MouseButtonEventArgs e)
    {

    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        var view = CollectionViewSource.GetDefaultView(StudentsDG.ItemsSource);
        view?.SortDescriptions.Clear();

        foreach (var column in StudentsDG.Columns)
        {
            column.SortDirection = null;
        }
        StudentsDG.UnselectAll();
        SearchBox.Text = "";
    }

    private void StudentsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender != null)
        {
            DataGrid dg = (DataGrid)sender;
            StudentVM.AnyRowSelected = dg.SelectedItems.Count > 0;
        }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        StudentVM.AddStudent();
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        StudentVM.RemoveStudent(StudentsDG.SelectedItem as Student);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        StudentVM.EditStudent(StudentsDG.SelectedItem as Student);
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        StudentVM.FilterDataGrid(((TextBox)sender).Text);
    }

    private void ReportButton_Click(object sender, RoutedEventArgs e)
    {
        Student student = (Student)((FrameworkElement)sender).DataContext;
        StudentVM.GenerateReport(student);
    }

    private void UnselectAllRows(object sender, MouseButtonEventArgs e)
    {
        StudentsDG.SelectedIndex = -1;
        StudentVM.AnyRowSelected = false;
    }
}
