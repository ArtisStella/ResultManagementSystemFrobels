using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSResult.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public static List<Subject> SubjectGetAll()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<Subject>("SELECT * FROM Subject", new DynamicParameters());

                return output.AsList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<Subject>();
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
