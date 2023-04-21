using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNgo.Models
{
    public class Remark
    {
        public int RemarkId { get; set; }
        public string? Remarks { get; set; }
        public string? Category { get; set; }
        public string? Sub_Category { get; set; }




        public static List<Remark> RemarksGetAll()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<Remark>("SELECT * FROM Remarks", new DynamicParameters());

                return output.AsList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<Remark>();
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
