using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// Class that contains a collection of plugins and is a plagin

namespace PluginLibrary
{
    class PluginContainer<T> : Plugin<T>
    {
        private readonly List<Plugin<T>> _pluginCollection;
 
        public PluginContainer(List<Plugin<T>> list)
        {
            this._pluginCollection = list;
        }

        public override T Modify(T param)
        {
            foreach (var plagin in _pluginCollection)
            {
                Console.WriteLine(plagin.Modify(param));
            }
            return param;
        }
    }
}
