using FluentNgo.Models;
using FluentNgo.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using md = MaterialDesignThemes.Wpf;
using FluentNgo.Controls;

namespace FluentNgo.Views.Pages;

public partial class MarksView
{
    MarksViewModel MarksVM;
    public int ExamId { get; set; }


    public MarksView()
    {
        InitializeComponent();
        MarksVM = (MarksViewModel)DataContext;
    }

    private void Exam_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int examId = (((ComboBox)sender).SelectedItem as Exam).ExamId;
        ExamId = examId;

        FillTableColumns();

        List<string> columnNames = MarksDG.Columns.Cast<DataGridColumn>().Select(column => (string)column.Header).ToList();

        MarksVM.FillMarksTable(ExamId, columnNames);
    }


    private void FillTableColumns()
    {
        List<ExamSubjects> subjects = MarksVM.GetExamSubjects(ExamId);
        MarksDG.Columns.Clear();

        DataGridTextColumn col = new DataGridTextColumn() { Header = "GR No", IsReadOnly = true, CanUserResize = false, Width = DataGridLength.Auto, Binding = new Binding("GR No") };
        MarksDG.Columns.Add(col);
        col = new DataGridTextColumn() { Header = "Student Name", IsReadOnly = true, CanUserResize = false, Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Student Name") };
        MarksDG.Columns.Add(col);

        foreach (ExamSubjects subject in subjects)
        {
            //col = new DataGridTextColumn() { Header = subject.SubjectName, CanUserResize = false, Width = DataGridLength.Auto, Binding = new Binding(subject.SubjectName) };
            DataGridTemplateColumn subCol = new NumericColumnGenerator().GenerateNumericColumn(subject.SubjectName, subject.SubjectName, 100);
            MarksDG.Columns.Add(subCol);
        }
    }

    private void Student_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        MarksVM.SaveMarks(ExamId);
    }
}
