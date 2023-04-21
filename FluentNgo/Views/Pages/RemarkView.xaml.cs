using FluentNgo.Models;
using FluentNgo.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FluentNgo.Views.Pages;

public partial class RemarkView
{
    RemarkViewModel RemarkVM { get; set; }

    public RemarkView()
    {
        InitializeComponent();
        RemarkVM = (RemarkViewModel)this.DataContext;
    }

    private void RemarksDG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender != null)
        {
            (sender as DataGrid)?.UnselectAll(); ;
        }
    }

    private void Row_Click(object sender, MouseButtonEventArgs e)
    {

    }

  
    private void RemarksDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender != null)
        {
            DataGrid dg = (DataGrid)sender;
            RemarkVM.AnyRowSelected = dg.SelectedItems.Count > 0;
        }

    }
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        RemarkVM.FilterDataGrid(((TextBox)sender).Text);
    }
}
