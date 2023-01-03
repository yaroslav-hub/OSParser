using System;
using System.IO;

namespace LL1
{
    public class Program
    {
        public static void InitializeTable(Table table)
        {
            table.AddToTable(new TableElem("EXP", "(, a, b, 8, 3, -", false, true, false, false, 1));// 0
            table.AddToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, true, false, 10));// 1
            table.AddToTable(new TableElem("A", "+, E", false, true, true, false, 4));// 2
            table.AddToTable(new TableElem("E", "E", true, true, false, true, -1));// 3

            table.AddToTable(new TableElem("A", "+", false, false, false, false, 6));// 4
            table.AddToTable(new TableElem("A", "E", false, true, false, false, 9));// 5
            table.AddToTable(new TableElem("+", "+", true, true, true, false, -1));// 6
            table.AddToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, true, false, 10));// 7
            table.AddToTable(new TableElem("A", "+, E", false, true, false, false, 4));// 8
            table.AddToTable(new TableElem("E", "E", true, true, false, false, -1));// 9

            table.AddToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, false, false, 11));// 10
            table.AddToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, true, false, 19));// 11
            table.AddToTable(new TableElem("B", "*, E", false, true, false, false, 13));// 12
            table.AddToTable(new TableElem("B", "*", false, false, false, false, 15));// 13
            table.AddToTable(new TableElem("B", "E", false, true, false, false, 18));// 14
            table.AddToTable(new TableElem("*", "*", true, true, true, false, -1));// 15
            table.AddToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, true, false, 19));// 16
            table.AddToTable(new TableElem("B", "*, E", false, true, false, false, 13));// 17
            table.AddToTable(new TableElem("E", "E", true, true, false, false, -1)); // 18

            table.AddToTable(new TableElem("F", "-", false, false, false, false, 25));// 19
            table.AddToTable(new TableElem("F", "(", false, false, false, false, 27));// 20
            table.AddToTable(new TableElem("F", "a", false, false, false, false, 30));// 21
            table.AddToTable(new TableElem("F", "b", false, false, false, false, 31));// 22
            table.AddToTable(new TableElem("F", "8", false, false, false, false, 32));// 23
            table.AddToTable(new TableElem("F", "3", false, true, false, false, 33));// 24

            table.AddToTable(new TableElem("-", "-", true, true, true, false, -1));// 25
            table.AddToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, false, false, 19));// 26
            table.AddToTable(new TableElem("(", "(", true, true, true, false, -1));// 27
            table.AddToTable(new TableElem("EXP", "(, a, b, 8, 3, -", false, true, true, false, 0));// 28
            table.AddToTable(new TableElem(")", ")", true, true, false, false, -1));// 29
            table.AddToTable(new TableElem("a", "a", true, true, false, false, -1));// 30
            table.AddToTable(new TableElem("b", "b", true, true, false, false, -1));// 31
            table.AddToTable(new TableElem("8", "8", true, true, false, false, -1));// 32
            table.AddToTable(new TableElem("3", "3", true, true, false, false, -1));// 33
        }

        public static void Main(string[] args)
        {
            StreamReader fileStream = new StreamReader(args[0]);
            Table table = new Table();
            InitializeTable(table);
            string code = fileStream.ReadLine();
            LL1 ll1 = new LL1(code, table);
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
