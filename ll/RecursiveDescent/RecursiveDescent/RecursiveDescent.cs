using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecursiveDescent
{
    public class RecursiveDescent
    {
        private readonly string _code;
        private int _currentReadIndex;
        private readonly List<string> _lexems;
        private string _currentLexem;

        public RecursiveDescent(string code)
        {
            _code = code;
            _currentReadIndex = 0;
            _lexems = _code.Split(" ").ToList();
        }

        private void MoveLexem()
        {
            if (_currentReadIndex == _lexems.Count())
            {
                throw new EndOfStreamException();
            }

            while (_lexems[_currentReadIndex] == "")
            {
                _currentReadIndex++;
            }

            _currentLexem = _lexems[_currentReadIndex++];
        }

        private string GetCurrentLexem()
        {
            return _currentLexem;
        }

        private void CheckNextLexem(string waitingLexem)
        {
            MoveLexem();
            if ( GetCurrentLexem() != waitingLexem )
            {
                throw new ApplicationException( $"'{waitingLexem}' does not exists in" );
            }
        }

        private void CheckReadOrWrite()
        {
            CheckNextLexem( "(" );

            CheckIdList();

            CheckNextLexem( ")" );
            CheckNextLexem( ";" );
        }

        public void Check()
        {
            CheckProg();
        }

        private void CheckVar()
        {
            CheckNextLexem("VAR");
            CheckIdList();
            CheckNextLexem(":");
            CheckType();
        }

        private void CheckProg()
        {
            CheckNextLexem("PROG");
            CheckNextLexem("id");
            CheckVar();
            CheckNextLexem("begin");
            CheckListSt();
            if (!GetCurrentLexem().Equals("end"))
            {
                throw new ApplicationException("end expected");
            }
            bool end = false;
            try
            {
                MoveLexem();
            }
            catch (EndOfStreamException e)
            {
                end = true;
            }
            if (!end)
            {
                throw new ApplicationException("Symbol after \"end\"");
            }
        }

        private void CheckType()
        {
            MoveLexem();
            string lexem = GetCurrentLexem();
            if (!(lexem.Equals("int") || lexem.Equals("float") || lexem.Equals("bool") || lexem.Equals("string")))
            {
                throw new ApplicationException( "Error on parsing type");
            }
        }

        private void CheckF()
        {
            MoveLexem();
            switch ( GetCurrentLexem() )
            {
                case "-":
                    CheckF();
                    break;
                case "id":
                case "num":
                    break;
                case "(":
                    CheckExp();
                    CheckNextLexem( ")" );
                    break;
                default:
                    throw new ApplicationException( "Incorrect F value" );
                    break;
            }
        }

        private void CheckT()
        {
            CheckF();
            CheckB();
        }

        private void CheckB()
        {
            MoveLexem();
            if (GetCurrentLexem() == "*")
            {
                CheckF();
                CheckB();
            }
        }

        private void CheckExp()
        {
            CheckT();
            CheckN();
        }

        private void CheckN()
        {
            if ( GetCurrentLexem() == "+" )
            {
                CheckT();
                CheckN();
            }
        }

        private void CheckAssign()
        {
            CheckNextLexem( "id" );
            CheckNextLexem( ":" );
            CheckNextLexem( "=" );
            CheckExp();
            CheckNextLexem( ";" );
        }
    }
}
