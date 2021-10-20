///Becica Adriana-Mihaela Grupa 3131B Laborator 2 cerinta 2
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

namespace Laboratorul2
{
    class SimpleWindow : GameWindow
    {

        // Constructor.
        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
        }

        // Tratează evenimentul generat de apăsarea unei taste. Mecanismul standard oferit de .NET
        // este cel utilizat.

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;

            if (e.Key == Key.A) // prin apasarea tastei A culoarea de fundal se face alba
            {
                GL.ClearColor(Color.White);
            }
            if (e.Key == Key.B) // prin apasarea tastei B culoarea de fundal se face negru
            {
                GL.ClearColor(Color.Black);
            }
        }
        // Setare mediu OpenGL și încarcarea resurselor (dacă e necesar) - de exemplu culoarea de
        // fundal a ferestrei 3D.
        // Atenție! Acest cod se execută înainte de desenarea efectivă a scenei 3D.
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
        }

        // Inițierea afișării și setarea viewport-ului grafic. Metoda este invocată la redimensionarea
        // ferestrei. Va fi invocată o dată și imediat după metoda ONLOAD!
        // Viewport-ul va fi dimensionat conform mărimii ferestrei active (cele 2 obiecte pot avea și mărimi 
        // diferite). 
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

        }

        // Secțiunea pentru "game logic"/"business logic". Tot ce se execută în această secțiune va fi randat
        // automat pe ecran în pasul următor - control utilizator, actualizarea poziției obiectelor, etc.
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            

            MouseState mouse = Mouse.GetState();
            if (mouse.IsAnyButtonDown)
                Console.WriteLine(mouse.X + " " + mouse.Y);

            if (mouse[MouseButton.Left]) // prin apasarea click stanga background-ul se face verde
            {
                GL.ClearColor(Color.Green);
            }
            if (mouse[MouseButton.Right]) // prin apasarea click dreapta background-ul se face rosu
            {
                GL.ClearColor(Color.Red);
            }
            if (mouse[MouseButton.Middle]) // prin apasarea butonului din centru  background-ul se face roz
            {
                GL.ClearColor(Color.Pink);
            }
        }

        // Secțiunea pentru randarea scenei 3D. Controlată de modulul logic din metoda ONUPDATEFRAME.
        // Parametrul de intrare "e" conține informatii de timing pentru randare.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Modul imediat! Suportat până la OpenGL 3.5 (este ineficient din cauza multiplelor apeluri de
            // funcții).
            GL.Begin(PrimitiveType.Triangles);

               GL.Color3(Color.Red);
               GL.Vertex2(-1.0f, 1.0f);
               GL.Color3(Color.Blue);
              GL.Vertex2(0.0f, -1.0f);
              GL.Color3(Color.Green);
              GL.Vertex2(1.0f, 1.0f);

            
            // Sfârșitul modului imediat!

            


            GL.End();

            this.SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {

            // Utilizarea cuvântului-cheie "using" va permite dealocarea memoriei o dată ce obiectul nu mai este
            // în uz (vezi metoda "Dispose()").
            // Metoda "Run()" specifică cerința noastră de a avea 30 de evenimente de tip UpdateFrame per secundă
            // și un număr nelimitat de evenimente de tip randare 3D per secundă (maximul suportat de subsistemul
            // grafic). Asta nu înseamnă că vor primi garantat respectivele valori!!!
            // Ideal ar fi ca după fiecare UpdateFrame să avem si un RenderFrame astfel încât toate obiectele generate
            // în scena 3D să fie actualizate fără pierderi (desincronizări între logica aplicației și imaginea randată
            // în final pe ecran).
            using (SimpleWindow example = new SimpleWindow())
            {

                // Verificați semnătura funcției în documentația inline oferită de IntelliSense!
                example.Run(30.0, 0.0);
            }
        }
    }
}
