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

        private void ParseVar()
        {
            if (GetLexem().Equals("VAR"))
            {
                ParseIdlist();
                if (GetLexem().Equals(":"))
                {
                    ParseType();
                    return;
                }
            }
            throw new ApplicationException( "Error on parsing VAR");
        }

        public void CheckProg()
        {
            if ( GetLexem().Equals( "PROG" ) )
            {
                if ( GetLexem().Equals( "id" ) )
                {
                    ParseVar();
                    if ( GetLexem().Equals( "begin" ) )
                    {
                        CheckLISTST();
                        if ( GetLexem().Equals( "end" ) )
                        {
                            bool end = false;
                            try
                            {
                                GetLexem();
                            }
                            catch ( EndOfStreamException e )
                            {
                                end = true;
                            }
                            if ( !end )
                            {
                                throw new ApplicationException( "Symbol after \"end\"" );
                            }
                            return;
                        }
                    }
                }
            }
            throw new ApplicationException( "Error on parsing program structure" );
        }

        private void ParseVar()
        {
            if (GetLexem().Equals("VAR"))
            {
                CheckIDLIST();
                if (GetLexem().Equals(":"))
                {
                    ParseType();
                    return;
                }
            }
            throw new ApplicationException("Error on parsing VAR");
        }

        private void ParseType()
        {
            string lexem = GetLexem();
            if (!(lexem.Equals("int") || lexem.Equals("float") || lexem.Equals("bool") || lexem.Equals("string")))
            {
                throw new ApplicationException( "Error on parsing type");
            }
        }
    }
}
