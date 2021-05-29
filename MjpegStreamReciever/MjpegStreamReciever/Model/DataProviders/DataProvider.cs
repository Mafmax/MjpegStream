using MjpegStreamReciever.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.Model
{
    public abstract class DataProvider
    {
      
        public abstract byte[] Provide(string source);
    }
}
