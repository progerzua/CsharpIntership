// Task6
// Without Demo
/*
For each assembly in AppDomain and for each assembly, that will be loaded in future, collect dictio-
nary, where key is Type and value is reference to default ctor. If there is no default ctor, ignore type. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class DomainManager
    {
        public AppDomain Domain { get; private set; }
        public Dictionary<Type, ConstructorInfo> TypeDictionary { get; private set; }
        public Assembly[] Assemblies { get; private set; }

        public DomainManager()
        {
            Domain = AppDomain.CurrentDomain;
            TypeDictionary = new Dictionary<Type, ConstructorInfo>();
            Assemblies = Domain.GetAssemblies();
            InitialTypes();

        }

        public void InitialTypes()
        {
            foreach (var assembly in Assemblies)
            {
                foreach (var type in assembly.DefinedTypes)
                {
                    // clear type from trash
                    var typeString = type.ToString().Split('+')[0].Split('`')[0];
                    var newType = Type.GetType(typeString);

                    //get default ctor or null
                    var ctor = type.GetConstructors().FirstOrDefault(x => x.GetParameters().Length == 0);

                    // put type and ctor info into the dict
                    if (ctor != null && newType != null && !TypeDictionary.ContainsKey(newType))
                        TypeDictionary.Add(newType, ctor);
                }
            }
        }
        public object Create(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public void Test()
        {
            // get last not abstract type
            Type t = TypeDictionary.Last(x => x.Key.IsAbstract == false).Key;

            Console.WriteLine(t.ToString());

            // create object of choosen type
            object a = Create(t);

            // print type of created object
            Console.WriteLine(a.GetType());

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
