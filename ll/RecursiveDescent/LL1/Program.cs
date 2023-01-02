using System;
using System.IO;

namespace LL1
{
    public class Program
    {
        public static void InitializeTable(ref Table table)
        {
            table.addToTable(new TableElem("EXP", "(, a, b, 8, 3, -", false, true, false, false, 1));
            table.addToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, true, false, 10));
            table.addToTable(new TableElem("A", "+, ;", false, true, true, false, 4));
            table.addToTable(new TableElem(";", ";", true, true, false, true, -1));

            table.addToTable(new TableElem("A", "+", false, false, false, false, 6));
            table.addToTable(new TableElem("A", ";", false, true, false, false, 9));
            table.addToTable(new TableElem("+", "+", true, true, false, false, -1));
            table.addToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, true, false, 10));
            table.addToTable(new TableElem("A", "+, ;", false, true, false, false, 4));
            table.addToTable(new TableElem(";", ";", true, true, false, true, -1));

            table.addToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, false, false, 11));
            table.addToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, true, false, 19));
            table.addToTable(new TableElem("B", "*, ;", false, true, false, false, 13));
            table.addToTable(new TableElem("B", "*", false, false, false, false, 15));
            table.addToTable(new TableElem("B", ";", false, true, false, false, 18));
            table.addToTable(new TableElem("+", "+", true, true, false, false, -1));
            table.addToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, true, false, 19));
            table.addToTable(new TableElem("B", "*, ;", false, true, false, false, 13));
            table.addToTable(new TableElem(";", ";", true, true, false, false, -1));

            table.addToTable(new TableElem("F", "-", false, false, false, false, 25));
            table.addToTable(new TableElem("F", "(", false, false, false, false, 27));
            table.addToTable(new TableElem("F", "a", false, false, false, false, 30));
            table.addToTable(new TableElem("F", "b", false, false, false, false, 31));
            table.addToTable(new TableElem("F", "8", false, false, false, false, 32));
            table.addToTable(new TableElem("F", "3", false, true, false, false, 33));

            table.addToTable(new TableElem("-", "-", true, true, false, false, -1));
            table.addToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, true, false, 19));
            table.addToTable(new TableElem("(", "(", true, true, false, false, -1));
            table.addToTable(new TableElem("EXP", "(, a, b, 8, 3, -", false, true, true, false, 0));
            table.addToTable(new TableElem(")", ")", true, true, false, false, -1));
            table.addToTable(new TableElem("a", "a", true, true, false, false, -1));
            table.addToTable(new TableElem("b", "b", true, true, false, false, -1));
            table.addToTable(new TableElem("8", "8", true, true, false, false, -1));
            table.addToTable(new TableElem("3", "3", true, true, false, false, -1));
           
        }

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
