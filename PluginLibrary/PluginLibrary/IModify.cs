using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

// Our interface for classes that modify data

namespace PluginLibrary
{
    public interface IModifiable<T>
    {
        T Modify(T param);
    }
}
