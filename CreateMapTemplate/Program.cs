
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CreateMapTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsed = ApplicationArguments.ParseArgs(args);
            if (parsed.Parsed)
            {
                DrawMap(parsed);
            }
        }

        private static void DrawMap(ApplicationArguments parsed)
        {
            Bitmap bitmap;
            var gridArgs = parsed.ToGridArguments();

            if (parsed.Elliptical)
            {
                bitmap = GridBuilder.DrawGrid.DrawEllipticalGrid(gridArgs);
            }
            else
            {
                bitmap = GridBuilder.DrawGrid.DrawRectangularGrid(gridArgs);
            }

            bitmap.Save($"./{parsed.FileName}.png", ImageFormat.Png);
        }
    }
}
