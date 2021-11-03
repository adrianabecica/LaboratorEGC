using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorul4
{
    class Window3D : GameWindow
    {
        KeyboardState previousKeyboard;
        Randomizer rando;
        private Cub3D cub;
        private readonly Camera3D camera;
        Color DEFAULT_BKG_COLOR = Color.LightSkyBlue;
       

        public Window3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            DisplayHelp();

            rando = new Randomizer();
            cub = new Cub3D();
            camera = new Camera3D();
        }

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

            // setare perspectiva
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 250);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // setare camera
            /* Matrix4 camera = Matrix4.LookAt(10, 10, 10, 0, 0, 0, 0, 1, 0);
             GL.MatrixMode(MatrixMode.Modelview);
             GL.LoadMatrix(ref camera);*/
            camera.SetCamera();

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
                GL.ClearColor(DEFAULT_BKG_COLOR);// prin apasarea tastei R se reseteaza culoarea de fundal a camerei
            }

            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.GenerateRandomColor());// prin apasarea tastei B se pune o culoare random la fundal
            }

            if (currentKeyboard[Key.F] && !previousKeyboard[Key.F])
            {
                cub.SetareCuloareFataCub(); // prin apasarea tastei F se schimba culoarea unei fere a  cubului in ROSU
            }

            if (currentKeyboard[Key.T] && !previousKeyboard[Key.T])
            {
                cub.SchimbareCuloareFeteCubRandom();// prin apasarea tastei T se schimba random culorile cubului
            }

            // controlare camera cu ajutorul tastelor W,A,S,D,Q,E
            if (currentKeyboard[Key.W])
            {
                camera.MoveForward();// prin apasarea tastei W se muta camera in fata
            }
            if (currentKeyboard[Key.S])
            {
                camera.MoveBackward();// prin apasarea tastei S se muta camera in spate
            }
            if (currentKeyboard[Key.A])
            {
                camera.MoveLeft();// prin apasarea tastei A se muta camera spre stanga
            }
            if (currentKeyboard[Key.D])
            {
                camera.MoveRight();// prin apasarea tastei D se muta camera spre dreapta
            }
            if (currentKeyboard[Key.Q])
            {
                camera.MoveUp();// prin apasarea tastei Q se muta camera in sus
            }
            if (currentKeyboard[Key.E])
            {
                camera.MoveDown();// prin apasarea tastei E se muta camera in jos
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

            cub.Draw();
            DrawAxes();

            // END RENDER CODE

            SwapBuffers();
        }

        private void DrawAxes()
        {
            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(75, 0, 0);
            GL.End();

            // Desenează axa Oy (cu galben).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 75, 0); ;
            GL.End();

            // Desenează axa Oz (cu verde).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 75);
            GL.End();
        }

        private void DisplayHelp()
        {
            Console.WriteLine("\n      MENIU");
            Console.WriteLine(" ESC - parasire program");
            Console.WriteLine(" H - afisare meniu (help)");
            Console.WriteLine(" R - resetare scena la valori implicite");
            Console.WriteLine(" B - schimbare culoare de fundal (randomizat)");
            Console.WriteLine(" F - schimbare culoare a unei fete a cubului");
            Console.WriteLine(" T - schimbare culori pentru fiecare vertex random");
            Console.WriteLine(" W - Move Forward camera");
            Console.WriteLine(" S - Move Backward camera");
            Console.WriteLine(" A - Move Left camera");
            Console.WriteLine(" D - Move Right camera");
            Console.WriteLine(" Q - Move Up camera");
            Console.WriteLine(" E - Move Down camera");
        }

    }
}
