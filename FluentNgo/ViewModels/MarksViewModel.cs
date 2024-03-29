﻿using TFSResult.Core;
using TFSResult.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace TFSResult.ViewModels
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
        public List<string> StudentClass { get; set; } 
        public List<string> StudentSection { get; set; }


        public MarksViewModel()
        {
            ExamList = Exam.ExamGetAll();
            MarksTable = new DataTable();
            Students = Student.StudentGetAll();

            StudentClass = Students.Select(x => x.ClassName).Distinct().ToList();
        }

        public List<ExamSubjects> GetExamSubjects(int examId)
        {
            Subjects = ExamSubjects.ExamSubjectGetAllByExamId(examId);
            return Subjects;
        }

        public void FillMarksTable(int examId, List<string> columnNames,string studentClass, string studentSection)
        {
            List<StudentMark> marks = StudentMark.StudentMarksGetAllByExamId(examId);
            List<Student> students = Student.StudentGetAll().Where(s => s.ClassName == studentClass && s.Section == studentSection).ToList();


            var newTable = new DataTable();

            foreach (string columnName in columnNames)
            {
                newTable.Columns.Add(new DataColumn() { ColumnName = columnName });
            }
            foreach (Student student in students)
            {
                List<StudentMark> studentMarks = marks.Where(obj => obj.StudentId == student.StudentId).ToList();

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

        public bool SaveMarks(int examId)
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
                    string marks = row[col.ColumnName].ToString()!;

                    if (subjectId == 0 || !subjectId.HasValue || marks == "") continue;

                    StudentMark studentMark = new StudentMark();
                    studentMark.ExamId = examId;
                    studentMark.StudentId = (int)studentId!;
                    studentMark.SubjectId = (int)subjectId!;
                    studentMark.Marks = marks;

                    StudentMarks.Add(studentMark);
                }
            }

            return StudentMark.StudentMarksSave(StudentMarks);
        }

        public void ClearMarks()
        {
            MarksTable.Clear();
        }

        public void LoadSections(string ClassName)
        {
            StudentSection = Students.Where(x => x.ClassName == ClassName).Select(x => x.Section).Distinct().ToList();
        }
    }
}
