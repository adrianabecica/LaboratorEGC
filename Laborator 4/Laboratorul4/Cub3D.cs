using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorul4
{
    class Cub3D
    {
        private const String FISIER = "coordonatecub.txt";

        private List<Vector3> listaCoordonateCub;
        char[] sep = { ' ' };
        Randomizer rando;
        bool showCube = true;
        private Color[] ColorV2 = new Color[39];

        public Cub3D()
        {
            listaCoordonateCub = CitireFisier(FISIER);
            rando = new Randomizer();
        }

        public void SetareCuloareFataCub()
        {
            //   ColorV2[0] = ColorV2[1] = ColorV2[2] = Color.FromKnownColor(KnownColor.Yellow);
            //  ColorV2[3] = ColorV2[4] = ColorV2[5] = Color.FromKnownColor(KnownColor.Yellow);
            //   ColorV2[6] = ColorV2[7] = ColorV2[8] = Color.FromKnownColor(KnownColor.Green);
            //   ColorV2[9] = ColorV2[10] = ColorV2[11] = Color.FromKnownColor(KnownColor.Green);
            ColorV2[12] = ColorV2[13] = ColorV2[14] = Color.FromKnownColor(KnownColor.Red);
            ColorV2[15] = ColorV2[16] = ColorV2[17] = Color.FromKnownColor(KnownColor.Red);
            //   ColorV2[18] = ColorV2[19] = ColorV2[20] = Color.FromKnownColor(KnownColor.Blue);
            //    ColorV2[21] = ColorV2[22] = ColorV2[23] = Color.FromKnownColor(KnownColor.Blue);
            //    ColorV2[24] = ColorV2[25] = ColorV2[26] = Color.FromKnownColor(KnownColor.Violet);
            //  ColorV2[27] = ColorV2[28] = ColorV2[29] = Color.FromKnownColor(KnownColor.Violet);
            //  ColorV2[30] = ColorV2[31] = ColorV2[32] = Color.FromKnownColor(KnownColor.Silver);
            //  ColorV2[33] = ColorV2[34] = ColorV2[35] = Color.FromKnownColor(KnownColor.Silver);
            // ColorV2[36] = ColorV2[37] = ColorV2[38] = Color.FromKnownColor(KnownColor.Silver);
        }

        public void SchimbareCuloareFeteCubRandom()
        {
            Color color1;
            for (int i = 0; i < 38; i++)
            {
                color1 = rando.GenerateRandomColor();
                ColorV2[i] = color1;
                Console.WriteLine(ColorV2[i] + "  ");
            }
            Console.WriteLine("\n");
        }


        public void Draw()
        {
            int j = 0;
            GL.Begin(PrimitiveType.Triangles);
            foreach (var vert in listaCoordonateCub)
            {
                GL.Color3(ColorV2[j]);
                GL.Vertex3(vert);
                j++;
            }
            GL.End();

        }

        private List<Vector3> CitireFisier(string fis)
        {
            List<Vector3> vlc3 = new List<Vector3>();

            var lines = File.ReadLines(fis);
            foreach (var line in lines)
            {
                string[] numere = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                Vector3 vec = new Vector3(Convert.ToInt32(numere[0]), Convert.ToInt32(numere[1]), Convert.ToInt32(numere[2]));
                vlc3.Add(vec);
            }
            return vlc3;
        }
    }
}
