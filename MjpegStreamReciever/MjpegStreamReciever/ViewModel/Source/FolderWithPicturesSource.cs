using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.ViewModel.Source
{
    public class FolderWithPicturesSource : ISource
    {
        private readonly string folderPath;

        public FolderWithPicturesSource(string folderPath)
        {
            this.folderPath = folderPath;
        }


        public string GetSource()
        {
            return folderPath;
        }
    }
}
