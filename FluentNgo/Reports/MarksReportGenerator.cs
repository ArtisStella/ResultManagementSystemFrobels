using FluentNgo.Models;
using SelectPdf;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FluentNgo.Reports
{
    public class MarksReportGenerator
    {
        private int StudentId { get; set; }
        private int ExamId { get; set; }

        public MarksReportGenerator(int studentId, int examId)
        {
            StudentId = studentId;
            ExamId = examId;
        }

        public string GetHtmlForReport()
        {
            Student student = Student.StudentGetById(StudentId);
            string ExamType = "First Formal";

            //  Report Data
            string reportType = "First Formal Report";
            string academicYear = "Year 2023-2024";
            string? StudentName = student.StudentName;
            int GRNo = student.GRNo;
            string ClassSection = $"{student.ClassName} - {student.Section}";
            string AttendancePercent = "100.0";
            string SemesterDays = "90";
            string DaysAttended = "90";

            float TotalPercentage = 90.0f;
            string Grade = "A";

            string PreviousTerm = "1st Term";
            bool PreviousTermCleared = true;

            //  Starting
            string html = "<html lang='en'><head><style>body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }.container { padding: 32px; background-color: white; }.container header { text-align: center; }.studentInfo table { width: 100%; } .studentInfo table th{ text-align: left; } .resultTable { margin: 2rem 0; }.resultTable table { width: 100%; border-collapse: collapse; }.resultTable table th,.resultTable table td { border: 1px solid gray; padding: 8px; }.reportSummary { margin: 0 64px; align-items: center; margin-bottom: 16px; border: 1px solid gray; }.reportSummary.percentage, .reportSummary.prevTerm { display: flex; flex-wrap: wrap; }.reportSummary.percentage .grid-item { border: 1px solid gray; } .reportSummary.legend table { width: 100%; }.reportSummary .grid-item { padding: 8px; text-align: center; }.reportSummary label { font-weight: bold; }.reportSummary .clear-box { width: 24px; height: 24px; margin: 0 16px; } .reportEnd { margin: 32px 0; }</style><title>Test Report</title></head><body><div class='container'>";

            //  Body

            //  Header
            html += $"<header><h2>{reportType}</h2><h3>{academicYear}</h3></header>";

            //  Student Info
            html += "<div class='studentInfo'><table><thead><tr><td></td><td style='width: 50%'></td><td></td><td></td></tr></thead><tbody><tr>" +
                    $"<th>Name</th><td>{StudentName}</td><th>GR No</th><td>{GRNo}</td></tr><tr>" +
                    $"<th>Class</th><td>{ClassSection}</td><th>Attendance (%)</th><td>{AttendancePercent}</td></tr><tr>" +
                    $"<th>Total Days of Semester</th><td>{SemesterDays}</td><th>Days Attended</th><td>{DaysAttended}</td></tr></tbody></table></div>";


            //  Marks
            html += "<div class='resultTable'><table>";

            
            //  THead
            string ExamHeaders = "";
            if (ExamType == "First Formal") ExamHeaders = "<th>1st Formal</th>";
            
            html += $"<thead><tr><th rowspan='2'>Subject</th><th colspan='3'>Acheived Marks</th><th rowspan='2'>Maximum Marks</th><th rowspan='2'>Percentage (%)</th><th rowspan='2'>Grade</th></tr><tr>{ExamHeaders}</tr></thead>";

            //  TBody
            html += GetTBodyHtml();
            
            html += "</table></div>";


            //  Percentage Summary
            html += $"<div class='reportSummary percentage'><label style='flex-basis: 25%' class='grid-item'>Percentage</label><span style='flex-basis: 50%' class='grid-item'>{TotalPercentage}%</span><label style='flex-grow: 1' class='grid-item'>Grade</label><span style='flex-grow: 1' class='grid-item'>{Grade}</span></div>";

            //  Previous Term
            html += $"<div class='reportSummary prevTerm'><label style='flex-basis: 25%' class='grid-item'>{ PreviousTerm }</label><span style='flex-grow: 1' class='grid-item'>Cleared</span><input type='checkbox' " + (PreviousTermCleared ? "checked" : "") + " class='clear-box'/><span style='flex-grow: 1' class='grid-item'>Not Cleared</span><input type='checkbox' " + (PreviousTermCleared ? "" : "checked") + " class='clear-box'/></div>";
            
            //  Legend
            html += "<hr class='reportEnd'/><div class='reportSummary legend'><table><tbody><tr><td class='grid-item'>A+ = 90% - 100%</td><td class='grid-item'>B+ = 70% - 79.99%</td><td class='grid-item'>C+ = 55% - 59.99%</td></tr><tr><td class='grid-item'>A  = 80% - 89.99%</td><td class='grid-item'>B  = 60% - 69.99%</td><td class='grid-item'>Fail = Below 55%</td></tr></tbody></table></div>";


            //  Closing
            html += "</div></body></html>";
            return html;
        }

        public PdfDocument GenerateReport()
        {
            HtmlToPdf converter = new();
            PdfDocument doc = converter.ConvertHtmlString(GetHtmlForReport());
            return doc;
        }

        private string GetTBodyHtml()
        {
            List<StudentMark> studentMarks = StudentMark.StudentMarksGetAllByExamAndStudentId(StudentId, ExamId);
            List<StudentMark> tempMark;

            List<string> subjects = new List<string>() { "English", "Mathematics", "Urdu", "Science", "Religeous Studies", "Pakistan Studies", "Dars" };

            string Marks = "";
            string html = "<tbody>";


            //  English
            Marks = "";

            tempMark = studentMarks.Where(mark => mark.SubjectName == "English").ToList();
            foreach (StudentMark mark in tempMark) Marks += $"<td>{mark.Marks}</td>";

            html += "<tr><th>English</th>" + Marks + "</tr>";

            //  Maths
            Marks = "";

            tempMark = studentMarks.Where(mark => mark.SubjectName == "Mathematics").ToList();
            foreach (StudentMark mark in tempMark) Marks += $"<td>{mark.Marks}</td>";

            html += "<tr><th>Mathematics</th>" + Marks + "</tr>";

            //  Urdu
            Marks = "";

            tempMark = studentMarks.Where(mark => mark.SubjectName == "Urdu").ToList();
            foreach (StudentMark mark in tempMark) Marks += $"<td>{mark.Marks}</td>";

            html += "<tr><th>Urdu</th>" + Marks + "</tr>";

            //  Science
            Marks = "";

            tempMark = studentMarks.Where(mark => mark.SubjectName == "Science").ToList();
            foreach (StudentMark mark in tempMark) Marks += $"<td>{mark.Marks}</td>";

            html += "<tr><th>Science</th>" + Marks + "</tr>";

            //  Religeous Studies
            Marks = "";

            tempMark = studentMarks.Where(mark => mark.SubjectName == "Religeous Studies").ToList();
            foreach (StudentMark mark in tempMark) Marks += $"<td>{mark.Marks}</td>";

            html += "<tr><th>Religeous Studies</th>" + Marks + "</tr>";

            //  Pakistan Studies
            Marks = "";

            tempMark = studentMarks.Where(mark => mark.SubjectName == "Pakistan Studies").ToList();
            foreach (StudentMark mark in tempMark) Marks += $"<td>{mark.Marks}</td>";

            html += "<tr><th>Pakistan Studies</th>" + Marks + "</tr>";

            //  Dars
            Marks = "";

            tempMark = studentMarks.Where(mark => mark.SubjectName == "Dars").ToList();
            foreach (StudentMark mark in tempMark) Marks += $"<td>{mark.Marks}</td>";

            html += "<tr><th>Dars</th>" + Marks + "</tr>";


            html += "</tbody>";

            return html;
        }
    }
}
