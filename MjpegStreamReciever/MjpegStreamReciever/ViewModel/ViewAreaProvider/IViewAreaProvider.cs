using MjpegStreamReciever.Model;
using MjpegStreamReciever.Model.DataProviders;
using MjpegStreamReciever.ViewModel.ViewArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.ViewModel.ViewAreaProvider
{
    public interface IViewAreaProvider
    {
        IViewArea Provide();
    }
}
