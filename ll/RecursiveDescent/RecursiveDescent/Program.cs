using System;
using System.IO;

namespace RecursiveDescent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string file = Console.ReadLine();
            StreamReader fileStream = new StreamReader(file);
            string code = fileStream.ReadLine();
            RecursiveDescent recursiveDescent = new RecursiveDescent();
            try
            {
                bool success = recursiveDescent.Check(code);
                Console.WriteLine($"Parsing success: {success}");
            }
            catch (ApplicationException e)
            {
                Console.WriteLine($"Parsing error: {e}");
            }
        }
    }
}
