using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLibrary
{
    public class StringPlugin : Plugin<string>
    {
        public override string Modify(string input)
        {
            return "Mad " + input + " killing me!"; 
        }

    }
}
