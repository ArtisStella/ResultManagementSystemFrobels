using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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


        public static bool RemarksSave(List<StudentRemarks> remarks)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                    Connection.Execute("INSERT INTO StudentRemarks (StudentId, RemarkId, Achieved) VALUES (@StudentId, @RemarkId, @Achieved)",remarks);

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

    } 

}
