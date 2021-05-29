using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel
{
    public interface IViewAreaParser
    {
        ISource ParseSource(Control control);
        IViewport ParseViewport(Control control);
    }
}
