using Dapper;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows;
using System;

namespace FluentNgo.Models
{
    public class Student
    {
        public int StudentId { get ; set;}
        public int GRNo { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? ClassName { get; set; }
        public string? Section { get; set; }



        public static List<Student> StudentGetAll()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                var output = Connection.Query<Student>("SELECT * FROM Students", new DynamicParameters());

                return output.AsList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return new List<Student>();
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool StudentSave()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                Connection.Execute("INSERT INTO Students (GRNo, StudentName, FatherName, ClassName,Section) " +
                    "VALUES (@GRNo, @StudentName, @FatherName, @ClassName, @Section)", this);
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

        public bool UpdateStudent()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                Connection.Execute("UPDATE Students SET StudentName = @StudentName, FatherName = @FatherName, FatherName = @FatherName, ClassName = @ClassName,  Section = @Section WHERE StudentId = @StudentId", this);
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
        
        public bool DeleteStudent()
        {
            var Connection = new SQLiteConnection(App.ConnectionString);
            Connection.Open();
            try
            {
                Connection.Execute("DELETE FROM Students WHERE StudentId = @StudentId", this);
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

