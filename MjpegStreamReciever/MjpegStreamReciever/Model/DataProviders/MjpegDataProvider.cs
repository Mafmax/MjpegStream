using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MjpegStreamReciever.Model.DataProviders
{
    public class MjpegDataProvider : DataProvider
    {
        public override byte[] Provide(string source)
        {
            HttpClient www = new HttpClient();
            var task = Task.Run(() => { return www.GetByteArrayAsync(source); });
            return task.Result;
        }
    }
}