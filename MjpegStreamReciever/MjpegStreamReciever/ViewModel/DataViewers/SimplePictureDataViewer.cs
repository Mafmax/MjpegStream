using MjpegStreamReciever.Model;
using MjpegStreamReciever.Model.DataProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.ViewModel
{
    public class SimplePictureDataViewer : DataViewer
    {
        private readonly string errorPicturePath;

        public SimplePictureDataViewer(IDataProviderFactory providerFactory, int fps, string errorPicturePath) : base(providerFactory, fps, null)
        {
            this.errorPicturePath = errorPicturePath;
            Error = GetErrorBytes();
        }
        protected override byte[] Error { get; }

        private byte[] GetErrorBytes()
        {
            if (!File.Exists(errorPicturePath))
            {
                return new byte[] { };
            }
            else
            {
                using (var stream = File.OpenRead(errorPicturePath))
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    return data;
                }
            }
        }
    }
}
