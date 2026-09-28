using System;
using System.Windows.Forms;

namespace Cau1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormDangKy());
        }
    }
}
