using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace UpdateNotepadTitleWPF
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, string lParam);

        private const int WM_SETTEXT = 0x000C;

        private IntPtr notepadWindowHandle;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            notepadWindowHandle = FindNotepadWindow();
            if (notepadWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("Notepad window not found.");
                Close();
            }
            else
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += UpdateNotepadTitle;
                timer.Start();
            }
        }

        private IntPtr FindNotepadWindow()
        {
            string notepadWindowName = "Untitled - Notepad"; // Замените на заголовок окна Notepad

            return FindWindow(null, notepadWindowName);
        }

        private void UpdateNotepadTitle(object sender, EventArgs e)
        {
            if (notepadWindowHandle != IntPtr.Zero)
            {
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                SendMessage(notepadWindowHandle, WM_SETTEXT, 0, currentTime);
                currentTimeTextBlock.Text = "Notepad Title Updated: " + currentTime;
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Notepad window was closed.");
                Close();
            }
        }
    }
}

