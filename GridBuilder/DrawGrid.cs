using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBuilder
{
    public static class DrawGrid
    {
        public static Bitmap DrawRectangularGrid(GridArguments args)
        {
            return DrawBitmap(args);
        }

        public static Bitmap DrawEllipticalGrid(GridArguments args)
        {
            return DrawBitmap(args, true);
        }

        public static Bitmap DrawBitmap(GridArguments args, bool elliptical = false)
        {
            var bitmap = new Bitmap(args.GridSize.Width, args.GridSize.Height);
            bitmap.SetResolution(args.DPI, args.DPI);
            var graphics = Graphics.FromImage(bitmap);
            Pen blackPen = new Pen(Color.Black, args.LineWidth);

            graphics.Clear(Color.White);
            Pen borderPen = new Pen(Color.Black, 10);
            var borderWidth = args.BorderWidth * args.DPI;

            var boundingRectangle = new Rectangle(new Point(borderWidth, borderWidth), args.Size);

            DrawInchLinesInRectangle(graphics, blackPen, boundingRectangle, args.DPI);

            if (elliptical)
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
            graphics.DrawString(args.MapName, f, new SolidBrush(Color.Black), new Point(borderWidth, 150));
            return bitmap;
        }

        private static void DrawInchLinesInRectangle(Graphics graphics, Pen pen, Rectangle boundingRectangle, int pixelsPerInch)
        {
            for (var currentX = boundingRectangle.Left + pixelsPerInch; currentX < boundingRectangle.Right; currentX += pixelsPerInch)
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
