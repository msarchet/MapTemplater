
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace CreateMapTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 24;
            var resolution = 600;
            var padding = 1;
            var pixels = (size + padding * 2) * resolution;
            var lineWidth = 5;
            using (var bitmap = new Bitmap(pixels, pixels))
            {
                bitmap.SetResolution(resolution, resolution);
                var graphics = Graphics.FromImage(bitmap);
                Pen blackPen = new Pen(Color.Black, lineWidth);

                graphics.Clear(Color.White);
                Pen borderPen = new Pen(Color.Black, 10);
                var borderWidth = padding * resolution;
                var width = size * resolution;
                var height = size * resolution;
                DrawInchLinesInRectangle(graphics, blackPen, borderWidth, borderWidth, width, height, resolution);
                graphics.DrawRectangle(borderPen, new Rectangle(borderWidth, borderWidth, width, height));
                bitmap.Save("./output.png", ImageFormat.Png);
            }

        }

        private static void DrawInchLinesInRectangle(Graphics graphics, Pen pen, int x, int y, int width, int height, int pixelsPerInch)
        {
            // draw vertical lines
            for(var currentX = x; currentX <= x + width; currentX += pixelsPerInch)
            {
                var drawnX = currentX;
                var startY = y;
                var endY = y + height;

                graphics.DrawLine(pen, drawnX , startY, drawnX, endY);
            }

            for(var currentY = y; currentY <= y + height; currentY += pixelsPerInch)
            {
                var drawnY = currentY;
                var startX = x;
                var endX = x + width;
                graphics.DrawLine(pen, startX,  drawnY, endX, drawnY);
            }
        }

    }
}
