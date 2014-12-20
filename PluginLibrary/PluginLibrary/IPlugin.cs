using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Modify data by another plugin and by itself

namespace PluginLibrary
{
    public class Pluginable : PluginAdapter<int>
    {
        public Pluginable(int data, Plugin<int> plugin) : base(data, plugin)
        {
        }

        public override int Modify(int input)
        {
            return base.Plugin.Modify(input)*8;
        }

        public int Modify()
        {
            return Modify(base.Data);
        }
    }
}
