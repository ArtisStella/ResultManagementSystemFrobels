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
        public int Marks { get; set; }

        //  View Only Properties
        public string SubjectName { get; set; }

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
    }
}
