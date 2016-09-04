using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HttpBuilder.Controllers
{
    public class MapController : ApiController
    {
        // GET: api/Map/5
        [Route("map")]
        public HttpResponseMessage Get(int Width, int Height, string MapName, int DPI = 600, bool Elliptical = false)
        {
            GridBuilder.GridArguments args = new GridBuilder.GridArguments() {
                Width = Width,
                Height = Height,
                BorderWidth = 1,
                DPI = DPI,
                LineWidth = 5,
                MapDescription = "API",
                MapName = MapName
            };

            using (var map = GridBuilder.DrawGrid.DrawBitmap(args, Elliptical))
            {
                var ms = new MemoryStream();
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                HttpResponseMessage r = Request.CreateResponse();
                ms.Position = 0;
                r.Content = new StreamContent(ms);
                r.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                r.Content.Headers.ContentLength = ms.Length;
                return r;
            }
        }

    }
}
