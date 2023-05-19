using TFSResult.Reports.Models;
using SelectPdf;
using System.Diagnostics;
using System.Windows;
using System.Threading.Tasks;

namespace TFSResult.Reports
{
    public class StudentReport
    {
        private StudentReportObject StudentObject { get; set; }
        
        public StudentReport(StudentReportObject student)
        {
            StudentObject = student;
        }

        public async void GenerateStudentReport()
        {
            MarksReportGenerator marksReportGenerator = new(StudentObject);
            RemarksReportGenerator remarksReportGenerator = new(StudentObject.Student.StudentId, StudentObject.ExamId);

            PdfDocument marksReport = new();
            PdfDocument remarksReport = new();

            Task marksReportTask = marksReportGenerator.GenerateReport().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    MessageBox.Show("Could not generate marks report. Please check marks for student", "Error!");
                }
                else
                {
                    marksReport = task.Result;
                }
            });

            Task remarksReportTask = remarksReportGenerator.GenerateReport().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    MessageBox.Show("Could not generate remarks report. Please check remarks for student", "Error!");
                }
                else
                {
                    remarksReport = task.Result;
                }
            });

            await Task.WhenAll(marksReportTask, remarksReportTask);

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
