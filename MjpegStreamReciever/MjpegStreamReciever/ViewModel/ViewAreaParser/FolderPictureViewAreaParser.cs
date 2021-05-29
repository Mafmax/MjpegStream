using MjpegStreamReciever.ViewModel.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel.ViewAreaParser
{
    public class FolderPictureViewAreaParser : PictureViewAreaParser
    {
        private readonly string folderPath;

        public FolderPictureViewAreaParser(string folderPath, string imageName) : base(imageName)
        {
            this.folderPath = folderPath;
        }

        public override ISource ParseSource(Control control)
        {
            return new FolderWithPicturesSource(folderPath);
        }
    }
}
