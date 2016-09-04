using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBuilder
{
    public class GridArguments
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Size GridSize { get { return new Size { Width = (BorderWidth * 2 + Width) * DPI, Height = (BorderWidth * 2 + Height) * DPI }; } }
        public Size Size { get { return new Size { Width = Width * DPI, Height = Height * DPI }; } }
        public int DPI { get; set; }
        public int BorderWidth { get; set; }
        public int LineWidth { get; set; }
        public string MapName { get; set; }
        public string MapDescription { get; set; }
    }
}
