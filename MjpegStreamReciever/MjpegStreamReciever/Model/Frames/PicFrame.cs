using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MjpegStreamReciever.Model
{
    public class PicFrame : Frame<BitmapSource>
    {

        public PicFrame(byte[] data) : base(data)
        {
        }

        public override BitmapSource GetContent()
        {
            if (Data == null)
            {
                return BitmapImage.Create(
                    2,
                    2,
                    96,
                    96,
                    PixelFormats.Indexed1,
                    new BitmapPalette(new List<Color> { Colors.Transparent }),
                    new byte[] { 0, 0, 0, 0 },
                    1);
            }
            using (MemoryStream ms = new MemoryStream(Data))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }
    }
}
