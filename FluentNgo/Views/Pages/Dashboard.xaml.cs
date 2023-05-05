using FluentNgo.Core;
using FluentNgo.Reports;

namespace FluentNgo.Views.Pages;

public partial class Dashboard
{
    public Dashboard()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        StudentReport report = new(1);
        report.GenerateStudentReport();
    }
}

