using System;
using System.Collections.Generic;
using System.IO;

namespace LL1
{
    public class LL1
    {
        private int _currentReadIndex;
        private readonly List<string> _lexems;
        private string _currentLexem;

        private int _currentTableElemIndex;
        private Table _table;

        private List<string> Split(string code)
        {
            List<string> elems = new List<string>();

            foreach (char symbol in code)
            {
                elems.Add(symbol.ToString());
            }

            return elems;
        }

        public LL1(string code, Table table)
        {
            _currentReadIndex = 0;
            _lexems = Split(code);

            _currentTableElemIndex = 0;
            _table = table;
        }

        private void ThrowErrorAtIndex()
        {
            throw new ApplicationException("Error on " + (_currentReadIndex - 1).ToString() + " index");
        }

        #region Lexem
        private void MoveLexem()
        {
            if (_currentReadIndex == _lexems.Count)
            {
                _currentLexem = "E";
                throw new EndOfStreamException();
            }

            while (_lexems[_currentReadIndex].Equals(" "))
            {
                _currentReadIndex++;
            }

            _currentLexem = _lexems[_currentReadIndex++];
        }

        private string GetCurrentLexem()
        {
            return _currentLexem.ToLower();
        }
        #endregion

        public void Check()
        {
            bool isReachedEndOfStream = false;
            Stack<int> addresses = new Stack<int>();
            MoveLexem();

            while (true)
            {
                TableElem tableRow = _table.GetElementFromTableByIndex(_currentTableElemIndex);

                bool isCurrentLexemInPtrCharSet = tableRow.PtrCharSet.IndexOf(GetCurrentLexem()) != -1;
                bool isEmptySymbolInPtrCharSet = tableRow.PtrCharSet.IndexOf("E") != -1;

                if (isCurrentLexemInPtrCharSet || isEmptySymbolInPtrCharSet)
                {
                    if (tableRow.IsEndOfAction && addresses.Count == 0)
                    {
                        break;
                    }

                    if (tableRow.IsNeedToAddToStack)
                    {
                        addresses.Push(_currentTableElemIndex + 1);
                    }

                    if (isCurrentLexemInPtrCharSet && tableRow.IsShift)
                    {
                        try
                        {
                            MoveLexem();
                        }
                        catch (EndOfStreamException)
                        {
                            isReachedEndOfStream = true;
                        }
                    }

                    _currentTableElemIndex = tableRow.NextElem != -1 ? tableRow.NextElem : addresses.Pop();
                } 
                else
                {
                    if (tableRow.IsError)
                    {
                        ThrowErrorAtIndex();
                    }

                    _currentTableElemIndex++;
                }
            }

            if (!isReachedEndOfStream)
            {
                ThrowErrorAtIndex();
            }
        }
    }
}
