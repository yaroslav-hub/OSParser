using System.Collections.Generic;

namespace LL1
{
    public class Table
    {
        private List<TableElem> elements = new List<TableElem>();

        public Table()
        {
        }

        public void AddToTable(TableElem elem)
        {
            elements.Add(elem);
        }

        public TableElem GetElementFromTableByIndex(int index)
        {
            return elements[index];
        }

        public List<TableElem> GetElementsFromTableByChar(string symbol)
        {
            List<TableElem> result = new List<TableElem>();
            foreach (TableElem elem in elements)
            {
                if (elem.Char == symbol)
                {
                    result.Add(elem);
                }
               
            }

            return result;
        }
    }
}

