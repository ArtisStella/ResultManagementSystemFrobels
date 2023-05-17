using TFSResult.Models;
using SelectPdf;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace TFSResult.Reports
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
            List<StudentRemarks> studentRemarks = StudentRemarks.RemarksGetAllByStudentAndExamId(StudentId, ExamId);
            List<string> Categories = studentRemarks.Select(rem => rem.Category).Distinct().ToList();
            DateTime curDate = DateTime.Now;

            //  Starting
            string html = "<html lang='en'><head><style>@page {size: letter;margin: 0.25in;margin-top: 0.5in;}body { font-family: 'Times New Roman', Tahoma, Geneva, Verdana, sans-serif;print-color-adjust: exact;}.container {    padding: 32px;    background-color: white;}.container header{    text-align: center;} table { border-collapse: collapse; width: 100%; } th, td { text-align: left; font-size: 16px; padding: 2px; padding-top: 4px; border-bottom: 1px darkgray solid; } tr td:not(:first-child) { width:100px; text-align: center; } input[type='checkbox'] {transform: scale(1.25); }th {background-color: #f2f2f2;text-align: center;}.category {/* font-size: 18px; */}.sub-category{text-align: left;/* font-size: 16px; */} .signatureSection { margin-top: 16px; } .signatureSection table td { padding-top: 64px; }</style><title>Test Report</title></head><body><div class='container'>";

            //  Body

            //  Header
            html += $"<header><h2 style='margin-top: 8px;'>Remarks</h2></header>";

            foreach (string category in Categories)
            {
                List<string> SubCategories = studentRemarks
                                                .Where(rem => rem.Category == category)
                                                .Select(rem => rem.SubCategory).Distinct().ToList();

                html += $"<table style='page-break-after: auto; page-break-inside: avoid;'><thead> <tr> <th class='category' colspan='2'>{category}</th></tr></thead>";
                html += "<tbody>";
                foreach (string subcategory in SubCategories)
                {
                    List<StudentRemarks> remarks = studentRemarks.Where(rem => rem.Category == category && rem.SubCategory == subcategory).ToList();
                    
                    if (subcategory != null) html += $"<tr><th class='sub-category' colspan='2'>{subcategory}</th></tr>";

                    if (category == "General Remarks")
                    {
                        string generalRemark = remarks.FirstOrDefault()!.GeneralRemark.Replace("\n", "<br />");
                        html += $"<tr><td>{generalRemark}</td></tr>";
                        continue;
                    }
                    foreach (StudentRemarks remark in remarks)
                    {
                        html += $"<tr><td>{remark.Remarks}</td><td><input type='checkbox'" + (remark.Achieved == 1 ? "checked":"") + "></td></tr>";
                    }
                }

                html += "</tbody>";


                html += "</table>";
            }

            //  Signatures
            html += $"<div class='signatureSection'><table><tbody><tr><td style='border: none;'></td><td style='width: 150px;'>Teacher's Signature</td><td style='width: 150px;'></td><td style='border: none;'></td><td style='width: 150px;'>Head's Signature</td><td style='width: 150px;'></td><td style='border: none;'></td></tr><tr><td style='border: none;'></td><td style='width: 150px;'>Date Issued</td><td style='width: 150px;'>{curDate:dd-MM-yyyy}</td><td style='border: none;'></td><td style='width: 150px;'>Parent's Signature</td><td style='width: 150px;'></td><td style='border: none;'></td></tr></tbody></table></div>";

            //  Closing
            html += "</div></body></html>";
            return html;
        }


        public PdfDocument GenerateReport()
        {
            HtmlToPdf converter = new();

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.MarginLeft = 18;
            converter.Options.MarginRight = 18;
            converter.Options.MarginTop = 24;
            converter.Options.MarginBottom = 24;

            // File.WriteAllText("Reports/testRemark.html", GetHtmlForRemarks());

            PdfDocument doc = converter.ConvertHtmlString(GetHtmlForRemarks());

            return doc;
        }
    }
}

