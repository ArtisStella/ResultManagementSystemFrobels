using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNgo.Models
{
    public class StudentMark
    {
        public int StudentMarkId { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string Marks { get; set; }

        //  View Only Properties
        public string SubjectName { get; set; }
        public string SubjectMarks { get; set; }
        public string ExamTypeName { get; set; }


        public static List<StudentMark> StudentMarksGetAllByExamId(int examId)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                string query = "SELECT t.ExamId, t.StudentId, t.SubjectId, s.SubjectName, t.Marks FROM StudentMark t JOIN Subject s ON s.SubjectId = t.SubjectId WHERE t.ExamId = @examId";
                var output = Connection.Query<StudentMark>(query, new { examId });
                return output.ToList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<StudentMark>();
            }
            finally
            {
                Connection.Close();
            }
        }

        public static bool StudentMarksSave(List<StudentMark> marks)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                string queryDelete = "DELETE FROM StudentMark WHERE ExamId = @Id";
                string queryInsert = "INSERT INTO StudentMark (ExamId, StudentId, SubjectId, Marks) VALUES (@ExamId, @StudentId, @SubjectId, @Marks)";
                Connection.Execute(queryDelete, new { Id = marks[0].ExamId });
                Connection.Execute(queryInsert, marks);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }

        public static List<StudentMark> StudentMarksGetAllByExamAndStudentId(int studentId, int examId, string examType)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                string query = "" +
                    "SELECT t.ExamId, t.StudentId, t.SubjectId, s.SubjectName, t.Marks, es.SubjectMarks, et.ExamTypeName " +
                    "FROM   Exam e " +
                    "JOIN   Exam eo on eo.AcademicYear = e.AcademicYear " +
                    "JOIN   ExamType et on et.ExamTypeId = eo.ExamTypeId " +
                    "JOIN 	StudentMark t ON t.ExamId = eo.ExamId " +
                    "JOIN 	Subject s ON s.SubjectId = t.SubjectId " +
                    "JOIN 	ExamSubjects es ON es.ExamId = t.ExamId AND es.SubjectId = s.SubjectId " +
                    "WHERE	t.StudentId = @studentId AND e.ExamId = @examId AND et.ExamTypeName = @examType";

                var output = Connection.Query<StudentMark>(query, new { studentId, examId, examType });
                return output.ToList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<StudentMark>();
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
