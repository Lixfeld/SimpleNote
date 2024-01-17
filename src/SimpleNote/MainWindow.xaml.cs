using SimpleNote.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleNote
{
    public partial class MainWindow : Window
    {
        public MainWindow(XmlConfiguration xmlConfiguration)
        {
            InitializeComponent();
            SynchronizationContext uiContext = SynchronizationContext.Current!;

            Top = xmlConfiguration.WindowTop;
            Left = xmlConfiguration.WindowLeft;
            Height = xmlConfiguration.WindowHeight;
            Width = xmlConfiguration.WindowWidth;
            noteTextBox.Text = xmlConfiguration.NoteText;

            // Save last position
            var windowLoaded = Observable.FromEventPattern(this, nameof(this.Loaded));
            Observable.FromEventPattern(this, nameof(this.LocationChanged))
                .SkipUntil(windowLoaded)
                .Throttle(TimeSpan.FromMilliseconds(800))
                .ObserveOn(uiContext) // or .Subscribe(x => Dispatcher.Invoke(...))
                .Subscribe(x => SaveSettings());

            // Save last text edit
            Observable.FromEventPattern(noteTextBox, nameof(noteTextBox.TextChanged))
                .Throttle(TimeSpan.FromMilliseconds(800))
                .ObserveOn(uiContext)
                .Subscribe(x => SaveSettings());
        }

        private void SaveSettings()
        {
            XmlConfiguration xmlConfiguration = new XmlConfiguration(Top, Left, Height, Width, noteTextBox.Text);
            xmlConfiguration.Save();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Can only call DragMove when the primary mouse button is down
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
