using FluentNgo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNgo.Reports.Models
{
    public class StudentReportObject
    {
        public Student Student { get; set; }
        public int ExamId { get; set; }
        public string DaysAttended { get; set; }
        public string TotalDays { get; set; }
    }
}
