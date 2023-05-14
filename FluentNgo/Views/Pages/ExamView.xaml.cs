using TFSResult.Models;
using TFSResult.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace TFSResult.Views.Pages;

public partial class ExamView
{
    ExamViewModel ExamVM { get; set; }
    public ExamView()
    {
        InitializeComponent();
        ExamVM = (ExamViewModel)DataContext;
    }

    private void ExamsDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender != null)
        {
            (sender as DataGrid)?.UnselectAll();
        }
    }

    private void Row_Click(object sender, MouseButtonEventArgs e)
    {

    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        var view = CollectionViewSource.GetDefaultView(ExamsDG.ItemsSource);
        view?.SortDescriptions.Clear();

        foreach (var column in ExamsDG.Columns)
        {
            column.SortDirection = null;
        }
        ExamsDG.UnselectAll();
        SearchBox.Text = "";
    }

    private void ExamsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender != null)
        {
            DataGrid dg = (DataGrid)sender;
            ExamVM.AnyRowSelected = dg.SelectedItems.Count > 0;
        }

    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        ExamVM.AddExam();
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        ExamVM.RemoveExam(ExamsDG.SelectedItem as Exam);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        ExamVM.EditExam(ExamsDG.SelectedItem as Exam);
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ExamVM.FilterDataGrid(((TextBox)sender).Text);
    }

    private void UnselectAllRows(object sender, MouseButtonEventArgs e)
    {
        ExamsDG.SelectedIndex = -1;
        ExamVM.AnyRowSelected = false;
    }
}
