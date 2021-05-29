using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel.ViewAreaControlProviders
{
    class TemplatedViewAreaControlProvider : ViewAreaControlProvider
    {
        private ControlTemplate template;
        public TemplatedViewAreaControlProvider(Window window, string templateName)
        {
            template = window.FindResource(templateName) as ControlTemplate;
        }
        public override Control Provide()
        {
           var contentControl = new ContentControl { Template = template };
            contentControl.ApplyTemplate();
            return contentControl;
        }
    }
}
