using TFSResult.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSResult.ViewModels
{
    public class ViewModelRoot : ObservableObject
    {
        public static StudentViewModel? StudentVM { get; set; }
        public static ExamViewModel? ExamVM { get; set; }
        public static RemarkViewModel? RemarkVM { get; set; }
        public static MarksViewModel? MarksVM { get; set; }


        public ViewModelRoot()
        {
            StudentVM = new StudentViewModel();
            ExamVM = new ExamViewModel();
            RemarkVM = new RemarkViewModel();
            MarksVM = new MarksViewModel();

        }

        public static ObservableObject? GetViewModel(string viewmodel)
        {
            switch (viewmodel)
            {
                case "Student":
                    return StudentVM;
                case "Exam":
                    return ExamVM;
                case "Remark":
                    return RemarkVM;
                case "Marks":
                    return MarksVM;
                default:
                    return null;
            }
        }
    }
  
}
