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
using System.Windows;
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
        public List<ExamSubjects> Subjects { get; set; }
        public List<Student> Students { get; set; }
        public List<StudentMark> StudentMarks { get; set; }


        public MarksViewModel()
        {
            ExamList = Exam.ExamGetAll();
            MarksTable = new DataTable();
        }


        public List<ExamSubjects> GetExamSubjects(int examId)
        {
            Subjects = ExamSubjects.ExamSubjectGetAllByExamId(examId);
            return Subjects;
        }


        public void FillMarksTable(int examId, List<string> columnNames)
        {
            List<StudentMark> marks = StudentMark.StudentMarksGetAllByExamId(examId);
            Students = Student.StudentGetAll().Where(stud => stud.GRNo != 0).ToList();

            var newTable = new DataTable();

            foreach (string columnName in columnNames)
            {
                newTable.Columns.Add(new DataColumn() { ColumnName = columnName });
            }

            foreach (Student student in Students)
            {
                List<StudentMark> studentMarks = marks.Where(obj => obj.StudentId == student.StudentId).ToList();
                // studentMarks = studentMarks.OrderBy(o => columnNames.IndexOf(o.SubjectName)).ToList();

                object[] data = new object[columnNames.Count];
                data[0] = student.GRNo;
                data[1] = student.StudentName!;
                
                for (int i = 0; i < columnNames.Count - 2; i++)
                {
                    var value = studentMarks.Where(mark => mark.SubjectName == columnNames[i + 2]).FirstOrDefault()?.Marks;

                    data[i + 2] = value;
                }

                newTable.Rows.Add(data);
            }

            MarksTable = newTable;
        }

        public void SaveMarks(int examId)
        {
            StudentMarks = new List<StudentMark>();

            foreach (DataRow row in MarksTable.Rows)
            {
                int? grNo = int.Parse(row["GR No"].ToString()!);
                if (grNo == 0) continue; 
                int? studentId = Students.Where(stud => stud.GRNo == grNo).FirstOrDefault()?.StudentId;
                foreach (DataColumn col in MarksTable.Columns)
                {
                    int? subjectId = Subjects.Where(sub => sub.SubjectName == col.ColumnName).FirstOrDefault()?.SubjectId;
                    int marks;
                    try
                    {
                        marks = int.Parse(row[col.ColumnName].ToString()!);
                    } catch
                    {
                        marks = 0;
                    }

                    if (subjectId == 0 || !subjectId.HasValue || marks == 0) continue;

                    StudentMark studentMark = new StudentMark();
                    studentMark.ExamId = examId;
                    studentMark.StudentId = (int)studentId!;
                    studentMark.SubjectId = (int)subjectId!;
                    studentMark.Marks = marks;

                    StudentMarks.Add(studentMark);
                }
            }

            if (StudentMark.StudentMarksSave(StudentMarks))
            {
                MessageBox.Show("Saved Succesfully!");
                return;
            }
            MessageBox.Show("Something went wrong!");
        }
    }
}
