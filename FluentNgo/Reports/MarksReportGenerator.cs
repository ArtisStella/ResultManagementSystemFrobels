﻿using TFSResult.Models;
using TFSResult.Reports.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace TFSResult.Reports
{
    public class MarksReportGenerator
    {
        private StudentReportObject StudentObject { get; set; }
        public ExamType Type { get; set; }  

        public string ReportType
        {
            get 
            {
                return Type.ExamTypeName!.Contains("Annual") ? "Annual Assessment" : Type.ExamTypeName; ; 
            }
        }


        private Dictionary<decimal, string> GradeMap = new()
        {
            { 90.0m, "A+" },
            { 80.0m, "A" },
            { 70.0m, "B+" },
            { 60.0m, "B" },
            { 55.0m, "C+" }
        };

        private float MarksObtained;
        private float TotalMarks;


        public MarksReportGenerator(StudentReportObject studObject)
        {
            StudentObject = studObject;

            Type = ExamType.ExamTypeGetByExamId(StudentObject.ExamId);
        }

        public string GetHtmlForReport()
        {
            Student student = StudentObject.Student;
            Exam exam = Exam.ExamGetById(StudentObject.ExamId);
            TotalMarks = 0f;
            MarksObtained = 0f;

            //  Report Data
            string reportType = ReportType + " Report";
            string academicYear = "Year " + exam.AcademicYear;
            string? StudentName = student.StudentName;
            int GRNo = student.GRNo;
            string ClassSection = $"{student.ClassName} - {student.Section}";
            string SemesterDays = StudentObject.TotalDays;
            string DaysAttended = StudentObject.DaysAttended;
            decimal AttendancePercent = Math.Round(((decimal)int.Parse(DaysAttended) / int.Parse(SemesterDays) * 100), 2);


            //  Starting
            string html = "<html lang='en'><head><style>body { }.container { padding: 32px; background-color: white; }.container header { text-align: center; }.studentInfo table { width: 100%; } .studentInfo table th { text-align: left; } .resultTable { margin: 2rem 0; }.resultTable table { width: 100%; border-collapse: collapse; } .resultTable th, .resultTable td { border: 1px solid gray; padding: 8px; }.reportSummary { margin: 0 64px; align-items: center; margin-bottom: 16px; border: 1px solid gray; }.reportSummary.percentage, .reportSummary.prevTerm { display: flex; flex-wrap: wrap; }.reportSummary.percentage .grid-item { border: 1px solid gray; } .reportSummary.legend table { width: 100%; }.reportSummary .grid-item { padding: 8px; text-align: center; }.reportSummary label { font-weight: bold; }.reportSummary .clear-box { width: 24px; height: 24px; margin: 0 16px; } .reportEnd { margin: 32px 0; }</style><title>Test Report</title></head><body><div class='container'>";

            //  Body

            //  Header
            html += $"<header><h2>{reportType}</h2><h3>{academicYear}</h3></header>";

            //  Student Info
            html += "<div class='studentInfo'><table><thead><tr><td></td><td style='width: 50%'></td><td></td><td></td></tr></thead><tbody><tr>" +
                    $"<th>Name</th><td>{StudentName}</td><th>GR No</th><td>{GRNo}</td></tr><tr>" +
                    $"<th>Class</th><td>{ClassSection}</td><th>Attendance (%)</th><td>{AttendancePercent}</td></tr><tr>" +
                    $"<th>Total Days of Semester</th><td>{SemesterDays}</td><th>Days Attended</th><td>{DaysAttended}</td></tr></tbody></table></div>";


            //  Marks
            html += GetTableHtml();


            decimal TotalPercentage = Math.Round((decimal)(MarksObtained / TotalMarks * 100), 2);
            
            string Grade = GetGradeForPercentage(TotalPercentage);

            //  Percentage Summary
            html += $"<div class='reportSummary percentage'><label style='flex-basis: 25%' class='grid-item'>Percentage</label><span style='flex-basis: 50%' class='grid-item'>{TotalPercentage}%</span><label style='flex-grow: 1' class='grid-item'>Grade</label><span style='flex-grow: 1' class='grid-item'>{Grade}</span></div>";

            //  Clear
            html += $"<div class='reportSummary prevTerm'><label style='flex-basis: 25%' class='grid-item'>{ReportType}</label><span style='flex-grow: 1' class='grid-item'>Cleared</span><input type='checkbox' " + (TotalPercentage > 55m ? "checked" : "") + " class='clear-box'/><span style='flex-grow: 1' class='grid-item'>Not Cleared</span><input type='checkbox' " + (TotalPercentage < 55m ? "checked" : "") + " class='clear-box'/></div>";

            //  Legend
            html += "<hr class='reportEnd'/><h4 style='margin: 16px 64px;'>Grading Key</h4><div class='reportSummary legend'><table><tbody><tr><td class='grid-item'>A+ = 90% - 100%</td><td class='grid-item'>B+ = 70% - 79.99%</td><td class='grid-item'>C+ = 55% - 59.99%</td></tr><tr><td class='grid-item'>A  = 80% - 89.99%</td><td class='grid-item'>B  = 60% - 69.99%</td><td class='grid-item'>Fail = Below 55%</td></tr></tbody></table></div>";


            //  Closing
            html += "</div></body></html>";
            return html;
        }


        private string GetTableHtml()
        {
            int ExamCount = 1;

            string html = "<div class='resultTable'><table>";

            //  THead
            string ExamHeaders = "";
            if (Type.ExamTypeName == "First Formal") ExamHeaders = "<th>1st Formal</th>";
            if (Type.ExamTypeName == "Second Formal") ExamHeaders = "<th>2nd Formal</th>";
            if (Type.ExamTypeName == "Mid Term") ExamHeaders = "<th>Mid Terms</th>";
            if (Type.ExamTypeName == "Annual Examination") {
                ExamCount = 3;
                ExamHeaders = "<th>Formal</th><th>Annual</th><th>Total</th>"; 
            }


            html += $"<thead><tr><th rowspan='2'>Subject</th><th rowspan='2'>Max Marks</th><th colspan='{ExamCount}'>Marks Acheived</th><th rowspan='2'>Percentage (%)</th><th rowspan='2'>Grade</th></tr><tr>{ExamHeaders}</tr></thead>";

            //  TBody
            html += GetTBodyHtml();

            html += "</table></div>";

            return html;
        }


        private string GetTBodyHtml()
        {
            List<string> exams = new() { "First Formal" };
            if (Type.ExamTypeName == "Annual Examination")
            {
                exams = new() { "Third Formal", "Fourth Formal", "Annual Examination" };
            }

            StudentMark? tempMark;

            List<string> subjects = new List<string>() { "English", "Mathematics", "Urdu", "Science", "Religious Studies", "Pakistan Studies", "Dars" };
            
            List<StudentMark> studentMarks = new();

            string Marks = "";
            string html = "<tbody>";
            foreach (string exam in exams)
            {
                studentMarks = studentMarks.Concat(StudentMark.StudentMarksGetAllByExamAndStudentId(StudentObject.Student.StudentId, StudentObject.ExamId, exam)).ToList();
            }
            

            foreach (string subject in subjects)
            {
                Marks = "";
                float marksTotal = 0f;
                float subjectTotal = 0f;
                float formalMarks = 0f;
                float otherMarks = 0f;

                foreach (string exam in exams)
                {

                    tempMark = studentMarks.Where(mark => mark.SubjectName == subject && mark.ExamTypeName == exam).ToList().FirstOrDefault();

                    if (tempMark != null)
                    {
                        _ = float.TryParse(tempMark.Marks, out float marks);

                        marksTotal += marks;
                        subjectTotal += float.Parse(tempMark.SubjectMarks);

                        if (exam.Contains("Formal"))
                        {
                            formalMarks += marks;
                        } else
                        {
                            otherMarks += marks;
                        }
                    }
                }

                Marks += $"<td>{subjectTotal}</td>";

                if (exams.Count > 1) Marks += $"<td>{formalMarks}</td><td>{otherMarks}</td><td>{marksTotal}</td>";
                // else Marks += $"<td>{formalMarks}</td><td>{marksTotal}</td>";

                MarksObtained += marksTotal;
                TotalMarks += subjectTotal;
                    
                decimal percentage = (decimal)Math.Round((marksTotal / subjectTotal * 100), 2);
                
                string grade = GetGradeForPercentage(percentage);
                
                Marks += $"<td>{percentage}</td><td>{grade}</td>";

                html += $"<tr><th>{subject}</th>{Marks}</tr>";
            }

            if (Type.ExamTypeName == "Annual Examination" ) html += $"<td></td><td>{TotalMarks}</td><td>50</td><td>50%</td><td>{MarksObtained}</td><td></td><td></td>";

            html += "</tbody>";

            return html;
        }

        private string GetGradeForPercentage(decimal percentage)
        {
            foreach (var item in GradeMap)
            {
                if (percentage >= item.Key)
                {
                    return item.Value;
                }
            }
            return "F";
        }

        public async Task<PdfDocument> GenerateReport()
        {
            return (
                await Task.Run(() =>
                {
                    HtmlToPdf converter = new();

                    converter.Options.MarginTop = 100;

                    // File.WriteAllText("Reports/TestMark.html", GetHtmlForReport());

                    PdfDocument doc = converter.ConvertHtmlString(GetHtmlForReport());

                    return doc;
                })
            );
        }
    }
}
