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
        protected bool isInited;
        public abstract byte[] Provide(string source);

        public  byte[] Provide(string source, bool init)
        {
            if (init) Init();
            return Provide(source);
        }
        protected virtual void Init() { isInited = true; }
    }
}
