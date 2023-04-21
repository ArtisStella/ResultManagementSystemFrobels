using FluentNgo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNgo.ViewModels
{
    public class ViewModelRoot : ObservableObject
    {
        public StudentViewModel StudentVM { get; set; }
        public ExamViewModel ExamTypeVM { get; set; }
        public RemarkViewModel RemarkVM { get; set; }
        public MarksViewModel MarksVM { get; set; }


        public ViewModelRoot()
        {
            StudentVM = new StudentViewModel();
            ExamTypeVM = new ExamViewModel();
            RemarkVM = new RemarkViewModel();
            MarksVM = new MarksViewModel();

        }
    }
  
}
