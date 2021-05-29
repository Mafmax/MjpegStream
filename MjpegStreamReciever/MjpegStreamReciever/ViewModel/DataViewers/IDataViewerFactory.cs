using MjpegStreamReciever.Model;
using MjpegStreamReciever.Model.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.ViewModel.DataViewers
{
    public interface IDataViewerFactory
    {
        DataViewer CreateDataViewer();
    }
}
