using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Laboratorul3
{
    
        class Window3D : GameWindow
        {
            KeyboardState previousKeyboard;
            Randomizer rando;
            private Triunghi triunghi;
            
            Color DEFAULT_BKG_COLOR = Color.LightSkyBlue;


            public Window3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
            {
                VSync = VSyncMode.On;

                DisplayHelp();

                rando = new Randomizer();
                triunghi = new Triunghi(rando);
              
            }

        public int Xx { get; }
        public int Yy { get; }

        protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                GL.Enable(EnableCap.DepthTest);
                GL.DepthFunc(DepthFunction.Less);

                GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
                GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
            }


            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);

                // setare culoare de fundal
                GL.ClearColor(DEFAULT_BKG_COLOR);

                // setare viewport
                GL.Viewport(0, 0, this.Width, this.Height);

                // setare proiectie
                Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 250);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadMatrix(ref perspectiva);

                // setare ochi
                Matrix4 ochi = Matrix4.LookAt(10, 10, 10, 0, 0, 0, 0, 1, 0);
                 GL.MatrixMode(MatrixMode.Modelview);
                 GL.LoadMatrix(ref ochi);
                

            }

            protected override void OnUpdateFrame(FrameEventArgs e)
            {
                base.OnUpdateFrame(e);

                // LOGIC CODE
                KeyboardState currentKeyboard = Keyboard.GetState();
                MouseState currentMouse = Mouse.GetState();

                if (currentKeyboard[Key.Escape])
                {
                    Exit();// prin apasarea tastei esc se iese din program
                }

                if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
                {
                    DisplayHelp();// prin apasarea tastei H se afiseaza Meniul
                }

                if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
                {
                    GL.ClearColor(DEFAULT_BKG_COLOR);// prin apasarea tastei R se reseteaza culoarea de fundal 
                }

                if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
                {
                    GL.ClearColor(rando.GenerateRandomColor());// prin apasarea tastei B se pune o culoare random la fundal
                }

                if (currentKeyboard[Key.F] && !previousKeyboard[Key.F])
                {
                    triunghi.SetareCuloareRandomTriunghi(); // prin apasarea tastei F se schimba culoarea triunghiului
                }

               
                previousKeyboard = currentKeyboard;
                // END LOGIC CODE
            }

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                base.OnRenderFrame(e);

                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Clear(ClearBufferMask.DepthBufferBit);

                // RENDER CODE

                triunghi.Draw();
               

                // END RENDER CODE

                SwapBuffers();
            }

            

            private void DisplayHelp()
            {
                Console.WriteLine("\n      MENIU");
                Console.WriteLine(" ESC - parasire program");
                Console.WriteLine(" H - afisare meniu (help)");
                Console.WriteLine(" R - resetare scena la valori implicite");
                Console.WriteLine(" B - schimbare culoare de fundal (randomizat)");
                Console.WriteLine(" F - schimbare culoare a unei fete a cubului");
                
            }

        }
    
}
