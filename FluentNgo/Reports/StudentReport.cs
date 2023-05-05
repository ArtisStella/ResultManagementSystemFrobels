using SelectPdf;
using System.Diagnostics;

namespace FluentNgo.Reports
{
    public class StudentReport
    {
        private int StudentId { get; set; }
        
        public StudentReport(int studentId)
        {
            StudentId = studentId;
        }

        public void GenerateStudentReport()
        {
            MarksReportGenerator marksReportGenerator = new(StudentId);
            RemarksReportGenerator remarksReportGenerator = new(StudentId);

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
