using FluentNgo.Reports.Models;
using SelectPdf;
using System.Diagnostics;

namespace FluentNgo.Reports
{
    public class StudentReport
    {
        private StudentReportObject StudentObject { get; set; }
        
        public StudentReport(StudentReportObject student)
        {
            StudentObject = student;
        }

        public void GenerateStudentReport()
        {
            MarksReportGenerator marksReportGenerator = new(StudentObject);
            RemarksReportGenerator remarksReportGenerator = new(StudentObject.Student.StudentId, StudentObject.ExamId);

            PdfDocument marksReport = marksReportGenerator.GenerateReport();
            PdfDocument remarksReport = remarksReportGenerator.GenerateReport();

            PdfDocument studentReport = new PdfDocument();
            foreach (PdfPage page in marksReport.Pages)
            {
                studentReport.Pages.Add(page);
            }

            foreach (PdfPage page in remarksReport.Pages)
            {
                studentReport.Pages.Add(page);
            }

            studentReport.Save("Reports/StudentReport.pdf");

            marksReport.Close();
            remarksReport.Close();
            studentReport.Close();

            Process.Start(new ProcessStartInfo
            {
                FileName = System.IO.Path.GetFullPath("Reports/StudentReport.pdf"),
                UseShellExecute = true
            });
        }
    }
}
