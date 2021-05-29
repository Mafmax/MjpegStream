using MjpegStreamReciever.ViewModel.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MjpegStreamReciever.ViewModel.ViewAreaParser
{
    public class TextboxSourceViewAreaParser : PictureViewAreaParser
    {
        private readonly string textBoxName;

        public TextboxSourceViewAreaParser(string textBoxName, string imageName) : base(imageName)
        {
            this.textBoxName = textBoxName;
        }

        public override ISource ParseSource(Control control)
        {
            var textBox = (TextBox)control.Template.FindName(textBoxName, control);
            return new TextBoxURLSource(textBox);
        }
    }
}
