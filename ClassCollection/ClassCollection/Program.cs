using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace ClassCollection
{

    public interface IPlugin<T>
    {
        T Converting(T input);
    }

    class StringUpper : IPlugin<string>
    {
        public string Converting(string input)
        {
            return input.ToUpper();
        }
    }

    class StringLower : IPlugin<string>
    {
        public string Converting(string input)
        {
            return input.ToLower();
        }
    }

    class StringTransform : IPlugin<string>
    {
        public string Converting(string input)
        {
            return input += "\nYou shall not pass!";
        }
    }

    class StringCollection : IPlugin<string>
    {
        public string Converting(string input)
        {
            string output = "";
            string filePath = @"C:\Error.txt";   // For Debugging

            var ruleType = typeof(IPlugin<string>);

            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "ClassCollection" && ruleType.IsAssignableFrom(t) && t.Name != "StringCollection"
                    select t;
            try
            {
                q.ToList().ForEach(t =>
                {

                    ConstructorInfo constructor = t.GetConstructor(Type.EmptyTypes);
                    object classObject = constructor.Invoke(new object[] { });

                    MethodInfo info = t.GetMethod("Converting");
                    
                    output = string.Concat(output, "\n" , "Method invoked from class: " , t.Name);

                    object returningValue = info.Invoke(classObject, new object[] { input });
                    output = string.Concat(output,"\n", returningValue);

                });
            }
                // Writing exception to file for better reading
            catch (Exception ex)
            {
                Console.WriteLine("Errroooor");

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }

            return output;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Class in the action");
            StringCollection collection = new StringCollection();
            var result = collection.Converting("blaAAAAaaAbla");
            Console.WriteLine(result);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
