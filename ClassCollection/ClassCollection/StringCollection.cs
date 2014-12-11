using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClassCollection
{
    class StringCollection : IPlugin<string>
    {
        public string Converting(string input)
        {
            string output = null;

            var ruleType = typeof(IPlugin<string>);

            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "ClassCollection.Plugins" && ruleType.IsAssignableFrom(t) && t.Name != "StringCollection"
                    select t;
            try
            {
                q.ToList().ForEach(t =>
                {

                    ConstructorInfo constructor = t.GetConstructor(Type.EmptyTypes);
                    object classObject = constructor.Invoke(new object[] { });

                    MethodInfo info = t.GetMethod("Converting");

                    Console.WriteLine(string.Concat(output, "\n", "Method invoked from class: ", t.Name));

                    object returningValue = info.Invoke(classObject, new object[] { input });
                    Console.WriteLine(string.Concat(output, "\n", returningValue));

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:");

                Console.WriteLine(ex.Message);
            }

            return output;
        }
    }
}
