using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace WindowsApiMessageBox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern MessageBoxResult MessageBox(IntPtr hWnd, string text, string caption, uint type);

        private void ShowHelloWorldMessageBox_Click(object sender, RoutedEventArgs e)
        {
            IntPtr mainWindowHandle = new WindowInteropHelper(this).Handle;
            MessageBox(mainWindowHandle, "Hello, World!", "MessageBox з Windows API", 0x00000040 /* MB_ICONINFORMATION */);
        }
    }
}

