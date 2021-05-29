using MjpegStreamReciever.Model;
using MjpegStreamReciever.Model.DataProviders;
using MjpegStreamReciever.ViewModel.DataViewers;
using MjpegStreamReciever.ViewModel.ViewArea;
using MjpegStreamReciever.ViewModel.ViewAreaProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.ViewModel
{
    public class SimpleViewAreaProvider:IViewAreaProvider
    {
        protected readonly ViewAreaControlProvider controlProvider;
        protected readonly IViewAreaParser parser;
        private readonly IDataViewerFactory viewerFactory;

        public SimpleViewAreaProvider(ViewAreaControlProvider controlProvider,
            IViewAreaParser parser,
            IDataViewerFactory viewerFactory)
        {
            this.controlProvider = controlProvider;
            this.parser = parser;
            this.viewerFactory = viewerFactory;
        }

      

        public IViewArea Provide()
        {
            return new SimpleViewArea(controlProvider.Provide(), parser, viewerFactory);
        }
    }
}
