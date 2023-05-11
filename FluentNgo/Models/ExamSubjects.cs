using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace FluentNgo.Models
{
    public class ExamSubjects
    {
        public int ExamSubjectId { get; set; }
        public int ExamId { get; set; }
        public int SubjectId { get; set; }
        public float SubjectMarks { get; set; }

        //  View Only Properties
        public string? SubjectName { get; set; }


        public static List<ExamSubjects> ExamSubjectGetAllByExamId(int ExamId)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<ExamSubjects>("SELECT t.ExamId, t.SubjectId, t.SubjectMarks, s.SubjectName FROM ExamSubjects t JOIN Subject s on s.SubjectId = t.SubjectId WHERE t.ExamId = @ExamId", new { ExamId });

                return output.AsList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<ExamSubjects>();
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool ExamSubjectSave()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                Connection.Execute("INSERT INTO ExamSubjects (ExamId, SubjectId, SubjectMarks) " +
                                   "VALUES (@ExamId, @SubjectId, @SubjectMarks)", this);
                return true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }

        public static bool UpdateExamSubjects(List<ExamSubjects> examSubjects)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                string queryDelete = "DELETE FROM ExamSubjects WHERE ExamId = @Id";
                string queryInsert = "INSERT INTO ExamSubjects (ExamId, SubjectId, SubjectMarks) VALUES (@ExamId, @SubjectId, @SubjectMarks)";
                Connection.Execute(queryDelete, new { Id = examSubjects[0].ExamId });
                Connection.Execute(queryInsert, examSubjects);
                return true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool DeleteExamSubject()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                Connection.Execute("DELETE FROM ExamSubjects WHERE ExamId = @ExamId", this);
                return true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }

    }
}
