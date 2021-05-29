using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel.Source
{
    public class TextBoxURLSource : ISource
    {
        public TextBoxURLSource(TextBox textBox)
        {
            TextBox = textBox;
        }

        public TextBox TextBox { get; }

        public string GetSource()
        {
            return TextBox.Text;
        }
    }
}
