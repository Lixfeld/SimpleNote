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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
