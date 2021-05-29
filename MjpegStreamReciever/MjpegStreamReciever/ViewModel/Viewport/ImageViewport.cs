using MjpegStreamReciever.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel.Viewport
{
    public class ImageViewport : IViewport
    {
        private readonly Image image;
        public ImageViewport(Image image)
        {
            this.image = image;
        }
        public void SetViewData(byte[] frameData)
        {
            PicFrame picture = new PicFrame(frameData);
            image.Source = picture.GetContent();
        }
    }
}
