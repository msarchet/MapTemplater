
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
            var widthPixels = (parsed.Width + parsed.Padding * 2) * parsed.DPI;
            var heightPixels = (parsed.Height + parsed.Padding * 2) * parsed.DPI;
            var lineWidth = parsed.GridLineWidth;

            using (var bitmap = new Bitmap(widthPixels, heightPixels))
            {
                bitmap.SetResolution(parsed.DPI, parsed.DPI);
                var graphics = Graphics.FromImage(bitmap);
                Pen blackPen = new Pen(Color.Black, lineWidth);

                graphics.Clear(Color.White);
                Pen borderPen = new Pen(Color.Black, 10);
                var borderWidth = parsed.Padding * parsed.DPI;
                var width = parsed.Width * parsed.DPI;
                var height = parsed.Height * parsed.DPI;

                var boundingRectangle = new Rectangle(new Point(borderWidth, borderWidth), new Size(width, height));

                DrawInchLinesInRectangle(graphics, blackPen, boundingRectangle, parsed.DPI);

                if (parsed.Elliptical)
                {
                    var path1 = new GraphicsPath();
                    var path2 = new GraphicsPath();

                    path1.AddRectangle(boundingRectangle);
                    path2.AddArc(boundingRectangle, 0, 360);

                    var region = new Region(path1);
                    region.Exclude(path2);

                    graphics.FillRegion(new SolidBrush(Color.White), region);
                    graphics.DrawArc(blackPen, boundingRectangle, 0, 360);
                }
                else
                {
                    graphics.DrawRectangle(borderPen, boundingRectangle);
                }

                var fontFamily = new FontFamily("Arial");
                var f = new Font(fontFamily, 200, FontStyle.Regular, GraphicsUnit.Pixel);
                graphics.DrawString(parsed.MapName, f, new SolidBrush(Color.Black), new Point(borderWidth, 150));
                bitmap.Save($"./{parsed.FileName}.png", ImageFormat.Png);
            }
        }

        private static void DrawInchLinesInRectangle(Graphics graphics, Pen pen, Rectangle boundingRectangle, int pixelsPerInch)
        {
            for (var currentX = boundingRectangle.Left + pixelsPerInch ; currentX < boundingRectangle.Right; currentX += pixelsPerInch)
            {
                graphics.DrawLine(pen, currentX, boundingRectangle.Top, currentX, boundingRectangle.Bottom);
            }

            for (var currentY = boundingRectangle.Top + pixelsPerInch; currentY < boundingRectangle.Bottom; currentY += pixelsPerInch)
            {
                graphics.DrawLine(pen, boundingRectangle.Left, currentY, boundingRectangle.Right, currentY);
            }
        }

    }
}
