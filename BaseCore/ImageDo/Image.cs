using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System.Threading.Tasks;
using System.IO;
using SixLabors.ImageSharp.Formats.Jpeg;
using ImageMagick;

namespace BaseCore.ImageDo
{
    public class Images
    {
        public async Task<Stream> Compress(Stream imagStream)
        {
            MemoryStream outpuStream = new MemoryStream();
            using (Image<Rgba32> img = Image.Load(imagStream))
            {
                img.Mutate(z => { z.Resize(100, 85); });
                img.SaveAsJpeg(outpuStream, new JpegEncoder { Quality = 80 });
            }
            return await Task.FromResult(outpuStream);
        }       
    }
}
