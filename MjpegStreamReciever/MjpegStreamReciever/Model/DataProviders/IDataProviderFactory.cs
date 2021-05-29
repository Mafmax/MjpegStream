using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.Model.DataProviders
{
    public interface IDataProviderFactory
    {
        DataProvider CreateDataProvider();
    }
}
