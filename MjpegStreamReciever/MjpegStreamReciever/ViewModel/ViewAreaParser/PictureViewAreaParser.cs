using MjpegStreamReciever.ViewModel.Viewport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel.ViewAreaParser
{

    public abstract class PictureViewAreaParser : IViewAreaParser
    {

        private readonly string imageName;

        public PictureViewAreaParser(string imageName)
        {
            this.imageName = imageName ?? throw new ArgumentNullException(nameof(imageName));
        }

        public abstract ISource ParseSource(Control control);

        public IViewport ParseViewport(Control control)
        { 
            var image = (Image)control.Template.FindName(imageName, control); ;
            return new ImageViewport(image);
        }

    }
}

