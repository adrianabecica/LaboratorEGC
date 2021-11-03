///Becica Adriana-Mihaela Grupa 3131B Laborator 3 cerinta 8
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;

namespace Laboratorul3
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            using (Window3D wnd = new Window3D())
            {
                wnd.Run(30.0, 0.0);
            }
        }

    }
}
