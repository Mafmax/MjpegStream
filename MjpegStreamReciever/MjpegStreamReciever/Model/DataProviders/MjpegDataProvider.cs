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
        private string lastSource;
        private byte[] lastDataFrame;
        public override byte[] Provide(string source)
        {
            List<int> last4Bytes = new List<int> { 0, 0, 0, 0 };
            List<byte> data = new List<byte>();
            HttpClient www = new HttpClient();
            string esc = "\r\n\r\n";
            bool flag = false;
            bool started = false;

            using (var stream = www.GetStreamAsync(source).Result)
            {
                while (stream.CanRead)
                {
                    int currentByte = stream.ReadByte();
                    if (currentByte == -1) break;
                    last4Bytes.RemoveAt(0);
                    last4Bytes.Add(currentByte);
                    flag = IsEscape(last4Bytes, esc);

                    if (flag)
                    {
                        if (started)
                        {
                            int needToRemove = esc.Length - 1;
                            data.RemoveRange(data.Count - needToRemove - 1, needToRemove);
                            var str = Encoding.Default.GetString(data.ToArray());
                            return data.ToArray();
                        }
                        else
                        {
                            started = true;
                            continue;
                        }
                    }

                    if (started)
                    {
                        data.Add((byte)currentByte);
                    }
                }

            }
            return null;
        }
        private bool IsEscape(List<int> bytes, string esc)
        {
            if (bytes.Count != esc.Length) return false;
            int[] escape = esc.Select(x => (int)x).ToArray();

            for (int i = 0; i < bytes.Count; i++)
            {
                if (bytes[i] != escape[i]) return false;
            }
            return true;
        }

    }
}