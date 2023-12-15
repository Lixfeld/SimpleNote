using SimpleNote.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SimpleNote
{
    public partial class MainWindow : Window
    {
        public MainWindow(XmlConfiguration xmlConfiguration)
        {
            InitializeComponent();

            Top = xmlConfiguration.WindowTop;
            Left = xmlConfiguration.WindowLeft;
            Height = xmlConfiguration.WindowHeight;
            Width = xmlConfiguration.WindowWidth;
            noteTextBox.Text = xmlConfiguration.NoteText;
        }

        private void SaveSettings()
        {
            XmlConfiguration xmlConfiguration = new XmlConfiguration(Top, Left, Height, Width, noteTextBox.Text);
            xmlConfiguration.Save();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
