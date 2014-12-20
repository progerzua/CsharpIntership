using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLibrary
{
    class IntegerPlugin : Plugin<int>
    {
        public override int Modify(int input)
        {
            return input + 7;
        }
    }
}
