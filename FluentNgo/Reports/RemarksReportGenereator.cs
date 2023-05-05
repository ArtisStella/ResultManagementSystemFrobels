using SelectPdf;

namespace FluentNgo.Reports
{
    public class RemarksReportGenerator
    {
        private int StudentId { get; set; }
        private int ExamId { get; set; }

        public RemarksReportGenerator(int studentId, int examId)
        {
            StudentId = studentId;
            ExamId = examId;
        }

        public string GetHtmlForRemarks()
        {
            //  Starting
            string html = "<html lang='en'><head><style>body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }.container { padding: 32px; background-color: white; }.container header { text-align: center; }</style><title>Test Report</title></head><body><div class='container'>";

            //  Body

            //  Header
            html += $"<header><h2>Remarks</h2></header>";


            //  Closing
            html += "</div></body></html>";
            return html;
        }


        public PdfDocument GenerateReport()
        {
            HtmlToPdf converter = new();
            PdfDocument doc = converter.ConvertHtmlString(GetHtmlForRemarks());
            return doc;
        }
    }
}
