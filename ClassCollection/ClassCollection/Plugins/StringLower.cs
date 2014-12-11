using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassCollection.Plugins
{
    class StringLower : IPlugin<string>
    {
        public string Converting(string input)
        {
            return input.ToLower();
        }
    }
}
