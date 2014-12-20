using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Base class for all classes which works with plugins

namespace PluginLibrary
{
    public class PluginAdapter<T> : Plugin<T>
    {
        public Plugin<T> Plugin { get; set; }

        protected readonly T Data;  // Data to modify

        public PluginAdapter(T data, Plugin<T> plugin)
        {
            this.Plugin = plugin;
            this.Data = data;
        }

        // Display modified data in console
        public void Display()
        {
            Console.WriteLine(this.Modify(Data));
        }

       
        // Modify param using plugin
        public override T Modify(T param)
        {
            return this.Plugin.Modify(param);
        }
    }
}
