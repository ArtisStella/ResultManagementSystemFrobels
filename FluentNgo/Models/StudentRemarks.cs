using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TFSResult.Models
{
    public class StudentRemarks
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RemarkId { get; set; }
        public int Achieved { get; set; }
        public int ExamId { get; set; }
        public string GeneralRemark { get; set; }

        //  View Only Properties
        public string Remarks { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }


        public static bool RemarksSave(List<StudentRemarks> remarks)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                string queryDelete = "DELETE FROM StudentRemarks WHERE StudentId = @Id";
                string queryInsert = "INSERT INTO StudentRemarks (StudentId, RemarkId, Achieved, GeneralRemark) VALUES (@StudentId, @RemarkId, @Achieved, @GeneralRemark)";
                Connection.Execute(queryDelete, new { Id = remarks[0].StudentId });
                Connection.Execute(queryInsert, remarks);

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
        public static List<StudentRemarks> RemarksGetAllByStudentId(int StudentID)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<StudentRemarks>("SELECT * FROM StudentRemarks WHERE StudentId = @StudentId ",new{StudentID});

                return output.AsList();
                
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<StudentRemarks>();
            }
            finally
            {
                Connection.Close();
            }
        }

        public static List<StudentRemarks> RemarksGetAllByStudentAndExamId(int StudentId, int ExamId)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<StudentRemarks>("SELECT * FROM StudentRemarks t JOIN Remarks r on r.RemarKId = t.RemarkId WHERE t.StudentId = @StudentId", new { StudentId });

                return output.AsList();

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<StudentRemarks>();
            }
            finally
            {
                Connection.Close();
            }
        }

    } 

}
