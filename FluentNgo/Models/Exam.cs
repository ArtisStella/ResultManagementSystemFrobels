using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace FluentNgo.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public int ExamTypeId { get; set;  }
        public string? AcademicYear { get; set; }
        public string? StartingDate { get; set; }
        public string? EndingDate { get; set; }

        //  View Only Properties
        public string? ExamTypeName { get; set; }
        public string ExamName
        {
            get { return ExamTypeName + " | " + AcademicYear; }
        }


        public static List<Exam> ExamGetAll()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<Exam>("SELECT t.ExamId, t.ExamTypeId, t.AcademicYear, t.StartingDate, t.EndingDate, et.ExamTypeName FROM Exam t JOIN ExamType et on et.ExamTypeId = t.ExamTypeId", new DynamicParameters());

                return output.AsList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<Exam>();
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool ExamSave()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                Connection.Execute("INSERT INTO Exam (ExamTypeId, AcademicYear, StartingDate, EndingDate) " +
                    "VALUES (@ExamTypeId, @AcademicYear, @StartingDate, @EndingDate)", this);
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

        public bool UpdateExam()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                Connection.Execute("UPDATE Exam SET ExamTypeId = @ExamTypeId, AcademicYear = @AcademicYear, StartingDate = @StartingDate, EndingDate = @EndingDate WHERE ExamId = @ExamId", this);
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

        public bool DeleteExam()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                Connection.Execute("DELETE FROM Exam WHERE ExamId = @ExamId", this);
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
