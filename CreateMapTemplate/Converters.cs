using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMapTemplate
{
    public static class Converters
    {
        public static GridBuilder.GridArguments ToGridArguments(this ApplicationArguments args)
        {
            return new GridBuilder.GridArguments()
            {
                BorderWidth = args.Padding,
                Width = args.Width,
                Height = args.Height,
                DPI = args.DPI,
                LineWidth = args.GridLineWidth,
                MapDescription = "test",
                MapName = args.MapName                                
            };
        }
    }
}
