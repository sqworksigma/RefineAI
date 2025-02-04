using System;
using System.Windows.Forms;
using RefineAI;

namespace TextRefinerBot
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (SplashScreen splash = new SplashScreen())
            {
                splash.ShowDialog();
            }

            Application.Run(new MainForm());
        }
    }
}
