using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel.ViewArea
{
    public interface IViewArea
    {
        bool Paused { get; }
        void Pause();
        void Start();
        Control Content { get; }
    }
}
