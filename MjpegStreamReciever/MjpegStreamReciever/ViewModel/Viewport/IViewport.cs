using MjpegStreamReciever.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MjpegStreamReciever.ViewModel
{
    public interface IViewport
    {
        void SetViewData(byte[] frameData);
    }
}
