using System;
using System.Collections.Generic;

namespace LL1
{
    public class LL1
    {
        private readonly string _code;
        private int _currentReadIndex;
        private readonly List<string> _lexems;
        private string _currentLexem;
        private int _currentTableElemIndex;
        private Table _table;

        private const string ERROR_MESSAGE_BASE_TEXT = "Parsing error: ";

        public LL1()
        {

        }

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
            _code = code;
            _currentReadIndex = 0;
            _lexems = Split(code);

            _currentTableElemIndex = 0;
            _table = table;
        }

        #region Lexem
        private void MoveLexem()
        {
            if (_currentReadIndex == _lexems.Count)
            {
                _currentLexem = "E";
                throw new System.IO.EndOfStreamException();
            }

            while(_lexems[_currentReadIndex] == " ")
            {
                _currentReadIndex++;
            }

            _currentLexem = _lexems[_currentReadIndex++];
        }

        private string GetCurrentLexem()
        {
            return _currentLexem.ToLower();
        }

        private void CheckCurrentLexem(string waitingLexem)
        {
            if (GetCurrentLexem() != waitingLexem)
            {
                throw new ApplicationException(ERROR_MESSAGE_BASE_TEXT +
                    $"'{waitingLexem}' does not exists");
            }
        }

        private void CheckNextLexem(string waitingLexem)
        {
            MoveLexem();
            CheckCurrentLexem(waitingLexem);
        }
        #endregion

        public void Check()
        {
            bool isEndOfChecking = false;
            bool isEndOfStream = false;
            Stack<int> addresses = new Stack<int>();
            MoveLexem();

            while (!isEndOfChecking)
            {
                TableElem tableRow = _table.GetElemtFromTableByIndex(_currentTableElemIndex);

                int shit = tableRow.PtrCharSet.IndexOf(GetCurrentLexem());
                int shit2 = tableRow.PtrCharSet.IndexOf("E");

                if (shit != -1 || shit2 != -1)
                {
                    if (tableRow.IsEndOfAction && addresses.Count == 0)
                    {
                        isEndOfChecking = true;
                    }
                    else
                    {
                        if (tableRow.IsNeedToAddToStack)
                        {
                            addresses.Push(_currentTableElemIndex + 1);
                        }

                        if (shit2 == -1 && tableRow.IsShift)
                        {
                            try
                            {
                                MoveLexem();
                            } catch(System.IO.EndOfStreamException ex)
                            {
                                isEndOfStream = true;
                            }
                        }

                        _currentTableElemIndex = tableRow.NextElem != -1 ? tableRow.NextElem : addresses.Pop();
                    }
                } else
                {
                    if (tableRow.IsError)
                    {
                        throw new ApplicationException("Error on " + (_currentReadIndex - 1).ToString() + " index");
                    }
                    else
                    {
                        _currentTableElemIndex++;
                    }
                }
            }

            if (!isEndOfStream)
            {
                throw new ApplicationException("Error on " + (_currentReadIndex - 1).ToString() + " index");
            }
        }
    }
}
