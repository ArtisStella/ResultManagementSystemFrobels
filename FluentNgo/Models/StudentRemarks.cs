using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNgo.Models
{
    public class StudentRemarks
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RemarkId { get; set; }
        public int Achieved { get; set; }
        public int ExamId { get; set; }

    }
}
