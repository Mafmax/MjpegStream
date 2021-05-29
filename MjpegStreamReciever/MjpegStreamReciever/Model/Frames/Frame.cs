using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.Model
{
    public abstract class Frame<T>
    {
        public byte[] Data { get; private set; }
        public Frame(byte[] data)
        {
            Data = data;
        }
        public abstract T GetContent();
    }
}
