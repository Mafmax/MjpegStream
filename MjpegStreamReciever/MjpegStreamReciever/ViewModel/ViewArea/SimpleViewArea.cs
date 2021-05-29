using MjpegStreamReciever.Model;
using MjpegStreamReciever.ViewModel.DataViewers;
using MjpegStreamReciever.ViewModel.ViewArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel
{
    public  class SimpleViewArea:IViewArea
    {
     
        private readonly DataViewer viewer;

        protected ISource Source { get; }
        protected IViewport Viewport { get; }

        public bool Paused => viewer.Paused;
        public void Pause()
        {
            viewer.Pause();
        }

        public void Start()
        {
            viewer.Start(Source, Viewport);
        }
        public SimpleViewArea(Control content, 
            IViewAreaParser parser,
            IDataViewerFactory viewControllerFactory)
        {
            Content = content;
            this.viewer = viewControllerFactory.CreateDataViewer();
            Source = parser.ParseSource(content);
            Viewport = parser.ParseViewport(Content);
        }

        public Control Content { get; }
    }
}
