using SimpleNote.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SimpleNote
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            XmlConfiguration xmlConfiguration = XmlConfiguration.Load();
            new MainWindow(xmlConfiguration).Show();
        }
    }
}
