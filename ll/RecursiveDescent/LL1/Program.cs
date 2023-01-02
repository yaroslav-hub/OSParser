using System;
using System.IO;

namespace LL1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string file = Console.ReadLine();
            StreamReader fileStream = new StreamReader(file);
            string code = fileStream.ReadLine();
            LL1 ll1 = new LL1(code);
            try
            {
                ll1.Check();
                Console.WriteLine($"Parsing successful");
            }
            catch (ApplicationException e)
            {
                Console.WriteLine($"Parsing error: {e.Message}");
            }
        }
    }
}
