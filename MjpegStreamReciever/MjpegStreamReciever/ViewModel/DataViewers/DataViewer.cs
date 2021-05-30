using MjpegStreamReciever.Model;
using MjpegStreamReciever.Model.DataProviders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MjpegStreamReciever.ViewModel
{
    public abstract class DataViewer 
    {
        protected readonly DataProvider provider;
        private readonly int fps;
        private readonly DataFilter[] filters;
        private object locker = new object();
        private bool onInit = false;
        private ConcurrentQueue<byte[]> frameDataBuffer = new ConcurrentQueue<byte[]>();
      
        public DataViewer(IDataProviderFactory providerFactory, int fps, params DataFilter[] filters)
        {
            this.provider = providerFactory.CreateDataProvider();
            this.fps = fps;
            this.filters = filters ?? Enumerable.Empty<DataFilter>().ToArray();
            Paused = true;
        }
        public bool Paused { get; private set; }
        public void Pause()
        {
            SetPaused(true);
        }
        protected abstract byte[] Error { get; }
        public void Start(ISource source, IViewport viewport)
        {
            SetPaused(false);
            var progress = new Progress<byte[]>(x => viewport.SetViewData(x));
            View(source.GetSource(), progress);
        }

        private void View(string source, IProgress<byte[]> progress)
        {
            onInit = true;
            Task.Run(() => FillDataBuffer(source));
            Task.Run(() => GetDataFromBuffer(progress));
        }
        private void FillDataBuffer(string source)
        {
            while (true)
            {
                bool initProvider = false; ;
                lock (locker)
                {
                    if (Paused) break;
                    initProvider = onInit;
                    onInit = false;
                }
                frameDataBuffer.Enqueue(GetFrameData(source,initProvider));
                WaitNextFrame(fps);
            }
        }
        private void GetDataFromBuffer(IProgress<byte[]> progress)
        {
            while (true)
            {

                if (frameDataBuffer.TryDequeue(out var data))
                {
                    foreach (var filter in filters)
                    {
                        data = filter.Filtering(data);
                    }
                    progress.Report(data);
                }
                else
                {
                    lock (locker)
                    {
                        if (Paused) break;
                    }
                    WaitNextFrame(fps);
                }
            }
            progress.Report(Error);

        }

        private void WaitNextFrame(int fps)
        {

            Thread.Sleep((int)(1000.0 / fps));
        }
        private byte[] GetFrameData(string source, bool firstStart)
        {
            return provider.Provide(source,firstStart);
        }
        private void SetPaused(bool paused)
        {
            Paused = paused;
        }
    }
}
