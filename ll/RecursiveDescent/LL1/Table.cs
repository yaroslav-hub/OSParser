using System;
using System.Collections.Generic;

namespace LL1
{
    public class Table
    {
        private List<TableElem> parts = new List<TableElem>();

        public Table()
        {
        }

        public void addToTable(TableElem elem)
        {
            parts.Add(elem);
        }

        public TableElem GetElemFromTableByIndex(int Index)
        {
            return parts[Index];
        }
    }
}

