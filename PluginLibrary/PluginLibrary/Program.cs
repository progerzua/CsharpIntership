using PluginLibrary;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is Plugin Container demo!");
            Console.WriteLine("Press 1 to use String Plugin");
            Console.WriteLine("Or press 2 to use Integer Plugin");
            Console.WriteLine("Write 'exit' if you bother");

            string answer = "empty string";
            while (answer != "exit")
            {
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        var strPlugin = new StringPlugin();

                        Console.WriteLine("Nice choice!");
                        Console.WriteLine("Enter some string: ");
                        Console.WriteLine("Modified string(toLower): {0}", strPlugin.Modify(Console.ReadLine()));
                        break;

                    case "2":
                        var intPlugin = new IntegerPlugin();

                        Console.WriteLine("I see,that you like numbers");
                        Console.WriteLine("Enter some int: ");
                        try
                        {
                            Console.WriteLine("Modified integer: {0}", intPlugin.Modify(Convert.ToInt32(Console.ReadLine())));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
            }

        }
    }
}
