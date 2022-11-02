using lab2.Machines;
using System;
using System.IO;

namespace lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string mode = args[0];
            string input = args[1];
            string output = args[2];

            using (StreamReader reader = new StreamReader(input))
            {
                using (StreamWriter writer = new StreamWriter(output))
                {
                    if (mode.Equals("moore"))
                    {
                        new MooreMachine(reader, writer).Minimize();
                    }
                    else if (mode.Equals("mealy"))
                    {
                        new MealyMachine(reader, writer).Minimize();
                    }
                    else
                    {
                        Console.WriteLine("Unknown mode");
                    }
                }
            }
        }
    }
}
