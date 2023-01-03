﻿using System;
using System.IO;

namespace LL1
{
    public class Program
    {
        public static void InitializeTable(Table table)
        {
            table.addToTable(new TableElem("EXP", "(, a, b, 8, 3, -", false, true, false, false, 1));// 0
            table.addToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, true, false, 10));// 1
            table.addToTable(new TableElem("A", "+, E", false, true, true, false, 4));// 2
            table.addToTable(new TableElem("E", "E", true, true, false, true, -1));// 3

            table.addToTable(new TableElem("A", "+", false, false, false, false, 6));// 4
            table.addToTable(new TableElem("A", "E", false, true, false, false, 9));// 5
            table.addToTable(new TableElem("+", "+", true, true, true, false, -1));// 6
            table.addToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, true, false, 10));// 7
            table.addToTable(new TableElem("A", "+, E", false, true, false, false, 4));// 8
            table.addToTable(new TableElem("E", "E", true, true, false, false, -1));// 9

            table.addToTable(new TableElem("T", "(, a, b, 8, 3, -", false, true, false, false, 11));// 10
            table.addToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, true, false, 19));// 11
            table.addToTable(new TableElem("B", "*, E", false, true, false, false, 13));// 12
            table.addToTable(new TableElem("B", "*", false, false, false, false, 15));// 13
            table.addToTable(new TableElem("B", "E", false, true, false, false, 18));// 14
            table.addToTable(new TableElem("*", "*", true, true, true, false, -1));// 15
            table.addToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, true, false, 19));// 16
            table.addToTable(new TableElem("B", "*, E", false, true, false, false, 13));// 17
            table.addToTable(new TableElem("E", "E", true, true, false, false, -1)); // 18

            table.addToTable(new TableElem("F", "-", false, false, false, false, 25));// 19
            table.addToTable(new TableElem("F", "(", false, false, false, false, 27));// 20
            table.addToTable(new TableElem("F", "a", false, false, false, false, 30));// 21
            table.addToTable(new TableElem("F", "b", false, false, false, false, 31));// 22
            table.addToTable(new TableElem("F", "8", false, false, false, false, 32));// 23
            table.addToTable(new TableElem("F", "3", false, true, false, false, 33));// 24

            table.addToTable(new TableElem("-", "-", true, true, true, false, -1));// 25
            table.addToTable(new TableElem("F", "(, a, b, 8, 3, -", false, true, false, false, 19));// 26
            table.addToTable(new TableElem("(", "(", true, true, true, false, -1));// 27
            table.addToTable(new TableElem("EXP", "(, a, b, 8, 3, -", false, true, true, false, 0));// 28
            table.addToTable(new TableElem(")", ")", true, true, false, false, -1));// 29
            table.addToTable(new TableElem("a", "a", true, true, false, false, -1));// 30
            table.addToTable(new TableElem("b", "b", true, true, false, false, -1));// 31
            table.addToTable(new TableElem("8", "8", true, true, false, false, -1));// 32
            table.addToTable(new TableElem("3", "3", true, true, false, false, -1));// 33
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
