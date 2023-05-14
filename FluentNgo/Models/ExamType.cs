using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSResult.Models
{
    public class ExamType
    {
        public int ExamTypeId { get; set; }
        public string? ExamTypeName { get; set; }

        public static List<ExamType> ExamTypesGetAll()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<ExamType>("SELECT * FROM ExamType", new DynamicParameters());

                return output.AsList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<ExamType>();
            }
            finally
            {
                Connection.Close();
            }
        }

        public static ExamType ExamTypeGetByExamId(int ExamId)
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<ExamType>("SELECT et.* FROM Exam t JOIN ExamType et on et.ExamTypeId = t.ExamTypeId WHERE t.ExamId = @ExamId", new { ExamId });

                return output.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new ExamType();
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
