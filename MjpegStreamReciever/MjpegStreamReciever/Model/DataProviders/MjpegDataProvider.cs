using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MjpegStreamReciever.Model.DataProviders
{
    public class MjpegDataProvider : DataProvider
    {
        private object locker = new object();
        private string lastSource = "";
        private byte[] lastDataFrame;
        private Stopwatch timer = new Stopwatch();
        private CancellationTokenSource cts = new CancellationTokenSource();
        private const int timeout = 5000;
       
        public override byte[] Provide(string source)
        {

            if (!lastSource.Equals(source) || isInited)
            {
                isInited = false;
                lastSource = source;
                cts.Cancel();
                cts = new CancellationTokenSource();
                StartParseStream(source);

            }
            lock (locker)
            {
                timer.Restart();
            }
            return lastDataFrame;
        }
        private void StartParseStream(string source)
        {
            HttpClient www = new HttpClient();
            var progress = new Progress<byte[]>(x => lastDataFrame = x);
            Task.Run(() => ParseStreamAsync(progress, www.GetStreamAsync(source), cts.Token, timeout));
        }
        private async Task ParseStreamAsync(IProgress<byte[]> progress,
            Task<Stream> streamProvider, CancellationToken token, int timeout)
        {
            long elapsed = 0;
            lock (locker)
            {
                elapsed = timer.ElapsedMilliseconds;
            }
            if (elapsed > timeout)
            {
                cts.Cancel();
            }
            List<int> last4Bytes = new List<int> { 0, 0, 0, 0 };
            List<byte> data = new List<byte>();
            bool started = false;

            string esc = "\r\n\r\n";
            await streamProvider;

            using (var stream = streamProvider.Result)
            {
                while (stream.CanRead)
                {
                    int currentByte = stream.ReadByte();
                    if (currentByte == -1) break;
                    last4Bytes.RemoveAt(0);
                    last4Bytes.Add(currentByte);
                    bool flag = IsEscape(last4Bytes, esc);

                    if (flag)
                    {
                        if (started)
                        {
                            lock (locker)
                            {
                                elapsed = timer.ElapsedMilliseconds;
                            }
                            if (elapsed > timeout || token.IsCancellationRequested)
                            {

                                progress.Report(null);
                                return;
                            }
                            int needToRemove = esc.Length - 1;
                            data.RemoveRange(data.Count - needToRemove - 1, needToRemove);

                            progress.Report(data.ToArray());
                            last4Bytes = new List<int> { 0, 0, 0, 0 };
                            data = new List<byte>();
                            started = false;

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