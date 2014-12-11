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

    class Program
    {
        static void Main(string[] args)
        {
            string input;
            int i;
            string j;
            StringCollection stringCollection = new StringCollection();
            IntCollection intCollection = new IntCollection();
            Console.WriteLine("Plugin in the action");
            Console.WriteLine("Write 1 for convert String");
            Console.WriteLine("Write 2 for convert Int");
            Console.WriteLine("Write 3 for exit");
            input = Console.ReadLine();
            while (!( input.Equals("3")))
            {
                if (input.Equals("1"))
                {
                    Console.WriteLine("Write a string for convertion");
                    input = Console.ReadLine();
                    j = stringCollection.Converting(input);
                }
                else if (input.Equals("2"))
                {
                    Console.WriteLine("Write a number for convertion");
                    input = Console.ReadLine();
                    i = intCollection.Converting(Convert.ToInt32(input));
                }
                else
                {
                    Console.WriteLine("You wrote wrong number");
                }

                input = Console.ReadLine();
            }
        }
    }
}
