using FluentNgo.Models;
using System.IO;

namespace FluentNgo.Reports
{
    public class TestReportGenerator
    {
        public static string GetHtmlForReport()
        {
            //  Report Data
            string reportType = "Mid Term Report";
            string academicYear = "Year 2023-2024";
            string StudentName = "Huzaifa";
            string GRNo = "3556";
            string ClassSection = "V - Carnation";
            string AttendancePercent = "100.0";
            string SemesterDays = "90";
            string DaysAttended = "90";

            //  Starting
            string html = "<html lang='en'><head><style>body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }.container { padding: 32px; background-color: white; }.container header { text-align: center; }.studentInfo table { width: 100%; }.resultTable { margin: 2rem 0; }.resultTable table { width: 100%; border-collapse: collapse; }.resultTable table th,.resultTable table td { border: 1px solid gray; padding: 8px; }.reportSummary { margin: 0 64px; align-items: center; margin-bottom: 16px; border: 1px solid gray; }.reportSummary.percentage { display: grid; grid-template-columns: 25% 50% auto auto; }.reportSummary.percentage .grid-item { border: 1px solid gray; }.reportSummary.prevTerm { display: grid; grid-template-columns: 25% auto auto auto auto; }.reportSummary.legend { display: grid; grid-template-columns: auto auto auto; }.reportSummary .grid-item { padding: 8px; text-align: center; }.reportSummary label { font-weight: bold; }.reportSummary .clear-box { width: 24px; height: 24px; background-color: gray; }.reportSummary .clear-box.active { background-color: green; }.reportEnd { margin: 32px 0; }</style><title>Test Report</title></head><body><div class='container'>";

            //  Body

            //  Header
            html += $"<header><h2>{reportType }</h2><h3>{academicYear }</h3></header>";

            //  Student Info
            html += "<div class='studentInfo'><table><thead><tr><td></td><td style='width: 50%'></td><td></td><td></td></tr></thead><tbody><tr><td>Name</td>" +
                    $"<td>{StudentName }</td><td>GR No</td><td>${GRNo }</td>" +
                    $"</tr><tr><td>Class</td><td>{ClassSection }</td><td>Attendance (%)</td><td>{AttendancePercent }</td>" +
                    $"</tr><tr><td>Total Days of Semester</td><td>{SemesterDays }</td><td>Days Attended</td><td>{DaysAttended }</td></tr></tbody></table></div>";

            //  Legend
            html += "<hr class='reportEnd' /><div class='reportSummary legend'><span class='grid-item'>A+ = 90% - 100%</span><span class='grid-item'>B+ = 70% - 79.99%</span><span class='grid-item'>C+ = 55% - 59.99%</span><span class='grid-item'>A  = 80% - 89.99%</span><span class='grid-item'>B  = 60% - 69.99%</span><span class='grid-item'>Fail = Below 55%</span></div>";


            //  Closing
            html += "</div></body></html>";
            return html;
         }


        public static void GenerateReport()
        {
            string path = Path.GetFullPath("Reports/TestReport.html");

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(GetHtmlForReport());
            doc.Save("Reports/TestReport.pdf");
            doc.Close();

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(GetHtmlForReport());
            }
         }
     }
 }
