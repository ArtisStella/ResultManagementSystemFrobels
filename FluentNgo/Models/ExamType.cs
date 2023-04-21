using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNgo.Models
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
    }
}
