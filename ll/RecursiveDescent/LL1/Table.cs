using System;
using System.Collections.Generic;

namespace LL1
{
    public class Table
    {
        private List<TableElem> elements = new List<TableElem>();

        public Table()
        {
            TableElem elem_1 = new TableElem("EXP", "(, a, b, 8, 3, -", false ,true ,false, false, 2); // 2+
            TableElem elem_2 = new TableElem("T", "(, a, b, 8, 3, -", false, true, true, false, 11); // 11

            TableElem elem_3 = new TableElem("A", "+, ;", false, true, true, false, 5); // 5+
            TableElem elem_4 = new TableElem(";", ";", true, true, false, true, -1); // null+

            TableElem elem_5 = new TableElem("A", "+", false, false, false, false, 7); // 7+
            TableElem elem_6 = new TableElem("A", ";", false, true, false, false, 10); // 10+


            TableElem elem_7 = new TableElem("+", "+", true, true, false, false); // null+


            TableElem elem_8 = elem_2;
            TableElem elem_9 = elem_3;
            TableElem elem_10 = elem_4;





        }

        public void addToTable(TableElem elem)
        {
            elements.Add(elem);
        }

        public TableElem GetElemtFromTableByIndex(int Index)
        {
            return elements[Index];
        }

        public List<TableElem> GetElemtsFromTableByChar(string Simbol)
        {
            List <TableElem> result = new List<TableElem>();
            foreach(TableElem elem in elements)
            {
                if(elem.Char == Simbol)
                {
                    result.Add(elem);
                }
               
            }

            return result;
        }
    }
}

