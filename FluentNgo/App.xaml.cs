using TFSResult.ViewModels;
using TFSResult.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TFSResult.Views.Components;

namespace TFSResult
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string ConnectionString { get { return @"Data Source=.\Data\MainDatabase.db"; } }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new Auth();

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
