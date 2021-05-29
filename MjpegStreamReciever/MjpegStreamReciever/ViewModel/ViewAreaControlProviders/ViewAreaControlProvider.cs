using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel
{
    public abstract class ViewAreaControlProvider
    {
        public abstract Control Provide();
    }
}
