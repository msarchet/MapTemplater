using Fclp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CreateMapTemplate
{
    public class ApplicationArguments
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int DPI { get; set; }

        public int Padding { get; set; }

        public string MapName { get; set; }

        public string FileName { get; set; }
        public int GridLineWidth { get; set; }
        public bool Parsed { get; set; }

        public bool Elliptical { get; set; }
        public static ApplicationArguments ParseArgs(string[] args)
        {
            var p = new FluentCommandLineParser<ApplicationArguments>();

            p.Setup(arg => arg.Height)
                    .As('h', "height")
                    .WithDescription("Set the Height of the Grid")
                    .Required();

            p.Setup(arg => arg.Width)
                    .As('w', "width")
                    .WithDescription("Set the Width of the Grid")
                    .Required();

            p.Setup(arg => arg.DPI)
                .As('r', "resolution")
                .WithDescription("Set the Resolution of the Grid")
                .SetDefault(600);

            p.Setup(arg => arg.Padding)
                .As('p', "padding")
                .WithDescription("Set the Padding around the grid")
                .SetDefault(1);

            p.Setup(arg => arg.MapName)
                .As('n', "mapname")
                .WithDescription("Set the Name of the Map")
                .SetDefault("map");

            p.Setup(arg => arg.FileName)
                .As('f', "filename")
                .WithDescription("File Name for Map")
                .SetDefault("generatedMap");

            p.Setup(arg => arg.GridLineWidth)
                .As('l', "linewidth")
                .WithDescription("Set the Width of the Grid lines")
                .SetDefault(5);

            p.Setup(arg => arg.Elliptical)
                .As('e', "elliptical")
                .WithDescription("Draw as an Ellipse")
                .SetDefault(false);

            p.SetupHelp("?", "help")
                .Callback(text => {
                    Console.WriteLine(text);
                });


            var result = p.Parse(args);

            if (!result.HasErrors)
            {
                var options = p.Object;
                options.Parsed = !result.HelpCalled;
                return options;
            } else
            {
                Console.WriteLine("Unable to Parse the CLI Text");
                p.HelpOption.ShowHelp(p.Options);
                return new ApplicationArguments();
            }
        }
    }
}
