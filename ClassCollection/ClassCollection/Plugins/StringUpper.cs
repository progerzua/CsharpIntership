using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassCollection.Plugins
{
    class StringUpper : IPlugin<string>
    {
        public string Converting(string input)
        {
            return input.ToUpper();
        }
    }
}
