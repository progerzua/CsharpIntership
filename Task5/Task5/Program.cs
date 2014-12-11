// Task5

/*
Create generator of random number sequence. Generator must return a sequence of integer numbers 
from O to N, where N is a method`s parameter. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace Task5
{
    class Program
    {
        public static ConcurrentQueue<int> numbers = new ConcurrentQueue<int>();
        public static Random rnd = new Random();
        public static bool working = false;

        public static async Task RequestData(string url)
        {
            WebClient client = new WebClient();
            string data = await client.DownloadStringTaskAsync(url);
            string[] parsedData = data.Split('\n');
            foreach (string element in parsedData)
            {
                if (!(String.IsNullOrEmpty(element)))
                {
                    numbers.Enqueue(Convert.ToInt32(element));
                }
            }
            working = false;
            

        }

        public static int GenerateRandom(int n)
        {
            string url = string.Format("https://www.random.org/integers/?num=10&min=0&max={0}&col=1&base=10&format=plain&rnd=new", n);
            if (numbers.Count == 0 && working == false)
            {
                try
                {
                    working = true;
                    Task mytask = Task.Run(async() =>
                    {
                        Console.WriteLine("\nMaking request...");
                        await RequestData(url);
                    });
                    return BCLrand(n);
                }
                catch
                {
                    return BCLrand(n);
                }
            }
            else
            {
                int result;
                if (numbers.TryDequeue(out result))
                {
                    Console.WriteLine("Using Queue");
                    return result;
                }
                else
                {
                    return BCLrand(n);
                }
            }

        }

        // DRY 
        public static int BCLrand(int n)
        {
            Console.WriteLine("Using BCL");
            return rnd.Next(0, n + 1);
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Generation Random numbers from 0 to 10");
            Console.WriteLine("Press enter to generate number or write 'exit' to close the program");
            while (!(Console.ReadLine().Equals("exit")))
            {
                Console.WriteLine(GenerateRandom(10));
            }
        }
    }
}