using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Abstract class for plagins

namespace PluginLibrary
{
    public abstract class Plugin<T> : IModifiable<T>
    {
        public abstract T Modify(T param);
    }
}
