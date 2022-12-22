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
        public RecursiveDescent(string code)
        {
            _code = code;
            _currentReadIndex = 0;
            _lexems = _code.Split(" ").ToList();
        }

        private string GetLexem()
        {
            if (_currentReadIndex == _lexems.Count())
            {
                throw new EndOfStreamException();
            }

            while (_lexems[_currentReadIndex] == "")
            {
                _currentReadIndex++;
            }

            return _lexems[_currentReadIndex++];
        }

        private void CheckWrite()
        {
            if (GetLexem() != "WRITE" || GetLexem() != "(")
            {
                throw new Exception("'WRITE(' does not exists in");
            }

            try
            {
                CheckIdList();
            }
            catch (Exception)
            {
                throw;
            }

            if (GetLexem() != ")" || GetLexem() != ";")
            {
                throw new Exception("');' does not exists in");
            }
        }

        private void CheckRead()
        {
            if (GetLexem() != "READ" || GetLexem() != "(")
            {
                throw new Exception("'READ(' does not exists in");
            }

            try
            {
                CheckIdList();
            }
            catch (Exception)
            {
                throw;
            }

            if (GetLexem() != ")" || GetLexem() != ";")
            {
                throw new Exception("');' does not exists in");
            }
        }

        public void Check()
        {
            ParseProg();
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
            throw new ApplicationException("Error on parsing VAR");
        }

        public void ParseProg()
        {
            if ( GetLexem().Equals( "PROG" ) )
            {
                if ( GetLexem().Equals( "id" ) )
                {
                    ParseVar();
                    if ( GetLexem().Equals( "begin" ) )
                    {
                        ParseListst();
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

        private void ParseType()
        {
            string lexem = GetLexem();
            if (!(lexem.Equals("int") || lexem.Equals("float") || lexem.Equals("bool") || lexem.Equals("string")))
            {
                throw new ApplicationException("Error on parsing type");
            }
        }
    }
}
