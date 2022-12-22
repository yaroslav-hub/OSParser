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

        public RecursiveDescent( string code )
        {
            _code = code;
            _currentReadIndex = 0;
            _lexems = _code.Split( " " ).ToList();
        }

        private void MoveLexem()
        {
            if ( _currentReadIndex == _lexems.Count )
            {
                throw new EndOfStreamException();
            }

            while ( _lexems[ _currentReadIndex ] == "" )
            {
                _currentReadIndex++;
            }

            _currentLexem = _lexems[ _currentReadIndex++ ];
        }

        private string GetCurrentLexem()
        {
            return _currentLexem.ToUpper();
        }

        private void CheckNextLexem( string waitingLexem )
        {
            MoveLexem();
            if ( GetCurrentLexem() != waitingLexem )
            {
                throw new ApplicationException( $"'{waitingLexem}' does not exists in" );
            }
        }

        private void CheckIdList()
        {
            try
            {
                MoveLexem();
                bool resultFlag = RecursCheckIdList( GetCurrentLexem() );
                if ( !resultFlag )
                {
                    throw new ApplicationException( "id list Error: waited id list" );
                }
            }
            catch ( ApplicationException e )
            {
                throw e;
            }
        }

        private bool RecursCheckIdList( string str )
        {
            bool resultFlag = false;

            switch ( str )
            {
                case "ID,":
                    MoveLexem();
                    resultFlag = RecursCheckIdList( GetCurrentLexem() );
                    break;
                case "ID":
                    return true;
                default:
                    throw new ApplicationException( "id Error: waited id" );
            }

            return resultFlag;
        }


        private void CheckListSt()
        {
            RecursCheckListSt();
        }

        private void RecursCheckListSt()// съедает end -> getCurrentLExem() хранит end
        {
            bool isFasedStatmentList = false;
            CheckSt( ref isFasedStatmentList );
            RecursCheckListSt();
            if ( !isFasedStatmentList )
            {
                throw new ApplicationException( "statement list Error: waited statement list" );
            }
        }

        private void CheckSt( ref bool isFasedStatmentListptr )
        {
            MoveLexem();
            string str = GetCurrentLexem();
            switch ( str )
            {
                case "READ":
                    CheckReadOrWrite();
                    isFasedStatmentListptr = true;
                    break;
                case "WRITE":
                    CheckReadOrWrite();
                    isFasedStatmentListptr = true;
                    break;
                case "ID":
                    CheckAssign();
                    isFasedStatmentListptr = true;
                    break;
                case "END":
                    if ( !isFasedStatmentListptr )
                    {
                        throw new ApplicationException( "ST Error: waited READ/WRITE/Assign(id := ...)" );
                    }
                    break;
                default:
                    if ( !isFasedStatmentListptr )
                    {
                        throw new ApplicationException( "ST Error: waited READ/WRITE/Assign(id := ...)" );
                    }
                    else
                    {
                        throw new ApplicationException( "Error: Waited end" );
                    }
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
            CheckNextLexem( "VAR" );
            CheckIdList();
            CheckNextLexem( ":" );
            CheckType();
        }

        private void CheckProg()
        {
            CheckNextLexem( "PROG" );
            CheckNextLexem( "ID" );
            CheckVar();
            CheckNextLexem( "BEGIN" );
            CheckListSt();
            if ( !GetCurrentLexem().Equals( "END" ) )
            {
                throw new ApplicationException( "'end' expected" );
            }
            bool end = false;
            try
            {
                MoveLexem();
            }
            catch ( EndOfStreamException )
            {
                end = true;
            }
            if ( !end )
            {
                throw new ApplicationException( "Symbol after 'end'" );
            }
        }

        private void CheckType()
        {
            MoveLexem();
            string lexem = GetCurrentLexem();
            if ( !( lexem.Equals( "INT" ) || lexem.Equals( "FLOAT" ) || lexem.Equals( "BOOL" ) || lexem.Equals( "STRING" ) ) )
            {
                throw new ApplicationException( "Error on parsing type" );
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
                case "ID":
                case "NUM":
                    break;
                case "(":
                    CheckExp();
                    CheckNextLexem( ")" );
                    break;
                default:
                    throw new ApplicationException( "Incorrect F value" );
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
            if ( GetCurrentLexem() == "*" )
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
            CheckNextLexem( "ID" );
            CheckNextLexem( ":" );
            CheckNextLexem( "=" );
            CheckExp();
            CheckNextLexem( ";" );
        }
    }
}
