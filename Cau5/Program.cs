using System;
using System.Windows.Forms;

namespace Cau5
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormBanVe());
        }
    }
}
