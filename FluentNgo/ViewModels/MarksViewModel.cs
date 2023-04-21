using FluentNgo.Core;
using FluentNgo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;

namespace FluentNgo.ViewModels
{
    public class MarksViewModel : ObservableObject
    {
        private DataTable _marksTable;
        public DataTable MarksTable
        {
            get { return _marksTable; }
            set
            {
                if (value == _marksTable)
                    return;
                _marksTable = value;
                OnPropertyChanged("MarksTable");
            }
        }


        public List<Exam> ExamList { get; set; }
        public List<Subject> Subjects { get; set; }
        
        public MarksViewModel()
        {
            ExamList = Exam.ExamGetAll();
            MarksTable = new DataTable();
        }

        public void FillMarksTable(int examId, List<string> columnNames)
        {
            List<StudentMark> marks = StudentMark.StudentMarksGetAllByExamId(examId);
            List<Student> students = Student.StudentGetAll();

            var newTable = new DataTable();

            foreach (string columnName in columnNames)
            {
                newTable.Columns.Add(new DataColumn() { ColumnName = columnName });
            }

            foreach (Student student in students)
            {
                List<StudentMark> studentMarks = marks.Where(obj => obj.StudentId == student.StudentId).ToList();
                studentMarks = studentMarks.OrderBy(o => columnNames.IndexOf(o.SubjectName)).ToList();

                object[] data = new object[columnNames.Count];
                data[0] = student.GRNo;
                data[1] = student.StudentName;
                for (int i = 0; i < studentMarks.Count; i++)
                {
                    data[i + 2] = studentMarks[i].Marks;
                }

                newTable.Rows.Add(data);
            }

            MarksTable = newTable;
        }
    }
}
