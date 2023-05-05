using FluentNgo.Models;
using FluentNgo.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FluentNgo.Views.Pages;

public partial class RemarkView
{
    RemarkViewModel RemarkVM { get; set; }

    public List<StudentRemarks> Remarks { get; set; }

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



    private void PopulateTable()
    {
        RemarksDG.Children.Clear();



        int? StudentID = (StudentDD.SelectedItem == null ? null : (Student)StudentDD.SelectedItem).StudentId;

        List<StudentRemarks> StudentRemarksList = new List<StudentRemarks>();

        if (StudentID != null)
        {
            StudentRemarksList = StudentRemarks.RemarksGetAllByStudentId((int)StudentID);

        }
      


        foreach (string category in RemarkVM.Category)
        {
            RowDefinition newRow = new RowDefinition();

            RemarksDG.RowDefinitions.Add(newRow);

            int rowIndex = RemarksDG.RowDefinitions.IndexOf(newRow);

            TextBlock cat = new()
            {
                Text = category,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(16, 16, 16, 8),
                VerticalAlignment = VerticalAlignment.Center
            };

            Grid.SetRow(cat, rowIndex);
            Grid.SetColumn(cat, 1); // Set column index to 0

            RemarksDG.Children.Add(cat);


            foreach (string subcategory in RemarkVM.SubCategory)
            {
                bool hasMatch = false;
                foreach (Remark rem in RemarkVM.Remarks)
                {
                    if (rem.Category == category && rem.SubCategory == subcategory && rem.SubCategory != null)
                    {
                        hasMatch = true;
                        break;
                    }
                }
                if (hasMatch)
                {
                    newRow = new RowDefinition();

                    RemarksDG.RowDefinitions.Add(newRow);

                    rowIndex = RemarksDG.RowDefinitions.IndexOf(newRow);

                    TextBlock subcat = new()
                    {
                        Text = subcategory,
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(16, 8, 16, 8),
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    Grid.SetRow(subcat, rowIndex);
                    Grid.SetColumn(subcat, 1); // Set column index to 0

                    RemarksDG.Children.Add(subcat);
                }
                foreach (var remark in RemarkVM.Remarks)
                {

                    if (remark.Category == category && remark.SubCategory == subcategory)
                    {
                        int Checked = 0;
                        if (StudentRemarksList.Count > 0)
                        {

                            Checked = StudentRemarksList.Where(x => x.RemarkId == remark.RemarkId).FirstOrDefault().Achieved;

                        }

                        newRow = new RowDefinition();

                        RemarksDG.RowDefinitions.Add(newRow);

                        rowIndex = RemarksDG.RowDefinitions.IndexOf(newRow);

                        TextBlock remId = new TextBlock();
                        remId.Text = remark.RemarkId.ToString();

                        Grid.SetRow(remId, rowIndex);
                        Grid.SetColumn(remId, 0);


                        TextBlock rem = new()
                        {
                            Text = remark.Remarks,
                            FontSize = 14,
                            Margin = new Thickness(16, 8, 16, 8),
                            VerticalAlignment = VerticalAlignment.Center
                        };

                        Grid.SetRow(rem, rowIndex);
                        Grid.SetColumn(rem, 1);

                        CheckBox cb = new()
                        {
                            IsChecked = Checked == 1 ? true : false,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        Grid.SetRow(cb, rowIndex);
                        Grid.SetColumn(cb, 2);


                        RemarksDG.Children.Add(remId);
                        RemarksDG.Children.Add(rem);
                        RemarksDG.Children.Add(cb);

                    }
                }
            }
        }
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

    private void StudentDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        PopulateTable();


    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        List<StudentRemarks> studentRemarks = new List<StudentRemarks>();



        for (int row = 0; row < RemarksDG.Children.Count; row ++)
        {
            TextBlock? idTextBlock = (TextBlock?)RemarksDG.Children
                .Cast<UIElement>()
                .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == 0);

            if (idTextBlock == null) continue;

            string remarkIdStr = idTextBlock!.Text;
            int remarkId = 0;
            int.TryParse(remarkIdStr, out remarkId);


            CheckBox? checkbox = (CheckBox?)RemarksDG.Children
                .Cast<UIElement>()
                .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == 2);

            bool isChecked = checkbox!.IsChecked.HasValue && checkbox.IsChecked.Value;

            int StudentId = (int)StudentDD.SelectedValue;

            int achieved = isChecked ? 1 : 0;

            StudentRemarks stdRemark = new StudentRemarks() { StudentId = StudentId, RemarkId = remarkId, Achieved = achieved };

            studentRemarks.Add(stdRemark);

        }

        StudentRemarks.RemarksSave(studentRemarks);


    }

    private void RemarksClassDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RemarksSectionDD.IsEnabled = true;
        StudentDD.IsEnabled = true;

        string ClassName = RemarksClassDD.SelectedItem as string;
        RemarkVM.LoadSections(ClassName);
        RemarksSectionDD.ItemsSource = RemarkVM.StudentSection;

        string Section = RemarksSectionDD.SelectedItem as string;

        RemarkVM.LoadStudents(ClassName, Section);
        StudentDD.ItemsSource = RemarkVM.StudentList;


        RemarksSectionDD.SelectedIndex = -1;
        StudentDD.SelectedIndex = -1;
        RemarksDG.Children.Clear();


    }



    private void RemarksSectionDD_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string ClassName = RemarksClassDD.SelectedItem as string;
        string Section = RemarksSectionDD.SelectedItem as string;

        RemarkVM.LoadStudents(ClassName, Section);
        StudentDD.ItemsSource = RemarkVM.StudentList;

        StudentDD.SelectedIndex = -1;
        RemarksDG.Children.Clear();

    }
}
