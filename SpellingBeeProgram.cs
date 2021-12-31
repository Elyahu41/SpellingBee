using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpellingBeeWindow
{
    public static class SpellingBeeProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            List<string> list = new List<string>();
            list.Add("M");
            list.Add("N");
            list.Add("C");
            list.Add("O");
            list.Add("I");
            list.Add("H");//based on today's words and letters

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SpellingBee(list,"P","PINCHONM"));
        }
    }
}
