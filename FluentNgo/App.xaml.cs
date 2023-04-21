using FluentNgo.ViewModels;
using FluentNgo.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FluentNgo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string ConnectionString { get { return @"Data Source=.\Data\MainDatabase.db"; } }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new Container()
            {
                DataContext = new ViewModelRoot()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
