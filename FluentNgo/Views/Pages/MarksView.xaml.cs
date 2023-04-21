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

namespace FluentNgo.Views.Pages;

public partial class MarksView
{
    MarksViewModel MarksVM;
    public MarksView()
    {
        InitializeComponent();
        MarksVM = (MarksViewModel)DataContext;
    }

    private void Exam_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int examId = (((ComboBox)sender).SelectedItem as Exam).ExamId;
        
        FillTableColumns(examId);

        List<string> columnNames = MarksDG.Columns.Cast<DataGridColumn>().Select(column => (string)column.Header).ToList();

        MarksVM.FillMarksTable(examId, columnNames);
    }


    private void FillTableColumns(int examId)
    {
        List<ExamSubjects> subjects = ExamSubjects.ExamSubjectGetAllByExamId(examId);
        MarksDG.Columns.Clear();

        DataGridTextColumn col = new DataGridTextColumn() { Header = "GR No", CanUserResize = false, Width = DataGridLength.Auto, Binding = new Binding("GR No") };
        MarksDG.Columns.Add(col);
        col = new DataGridTextColumn() { Header = "Student Name", CanUserResize = false, Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Student Name") };
        MarksDG.Columns.Add(col);

        foreach (ExamSubjects subject in subjects)
        {
            col = new DataGridTextColumn() { Header = subject.SubjectName, CanUserResize = false, Width = DataGridLength.Auto, Binding = new Binding(subject.SubjectName) };
            MarksDG.Columns.Add(col);
        }
    }
    private void Student_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
