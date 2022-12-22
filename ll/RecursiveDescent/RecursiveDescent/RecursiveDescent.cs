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
            _lexems = _code
                .Split( " " )
                .Where( s => !String.IsNullOrEmpty( s ) )
                .ToList();
        }

        public void Check()
        {
            CheckProg();
        }

        #region Lexem
        private void MoveLexem()
        {
            if ( _currentReadIndex == _lexems.Count )
            {
                throw new EndOfStreamException();
            }

            _currentLexem = _lexems[ _currentReadIndex++ ];
        }

        private string GetCurrentLexem()
        {
            return _currentLexem.ToUpper();
        }

        private void CheckCurrentLexem( string waitingLexem )
        {
            if ( GetCurrentLexem() != waitingLexem )
            {
                throw new ApplicationException( $"'{waitingLexem}' does not exists in" );
            }
        }

        private void CheckNextLexem( string waitingLexem )
        {
            MoveLexem();
            CheckCurrentLexem( waitingLexem );
        }
        #endregion

        #region Blocks Checking
        private void CheckProg()
        {
            CheckNextLexem( "PROG" );
            CheckNextLexem( "ID" );
            CheckVar();
            CheckNextLexem( ";" );
            CheckNextLexem( "BEGIN" );
            CheckStatementList();
            if ( !GetCurrentLexem().Equals( "END" ) )
            {
                throw new ApplicationException( "'end' expected" );
            }

            try
            {
                MoveLexem();
            }
            catch ( EndOfStreamException )
            {
                return;
            }

            throw new ApplicationException( "Symbol after 'end'" );
        }

        private void CheckVar()
        {
            CheckNextLexem( "VAR" );
            CheckIdList();
            CheckNextLexem( ":" );
            CheckType();
        }

        private void CheckIdList()
        {
            MoveLexem();
            if ( !RecoursiveCheckIdList( GetCurrentLexem() ) )
            {
                throw new ApplicationException( "id list Error: waited id list" );
            }
        }

        private bool RecoursiveCheckIdList( string str )
        {
            switch ( str )
            {
                case "ID,":
                    MoveLexem();
                    return RecoursiveCheckIdList( GetCurrentLexem() );
                case "ID":
                    return true;
                default:
                    throw new ApplicationException( "id Error: waited id" );
            }
        }


        private void CheckStatementList()
        {
            bool foundStatmentList = false;
            bool foundEnd = false;
            RecoursiveCheckStatementList( ref foundStatmentList, ref foundEnd );
        }

        // Съедает END -> getCurrentLexem() хранит END
        private void RecoursiveCheckStatementList( ref bool foundStatmentList, ref bool foundEnd )
        {
            if ( !foundEnd )
            {
                CheckStatement( ref foundStatmentList, ref foundEnd );
                RecoursiveCheckStatementList( ref foundStatmentList, ref foundEnd );
                if ( !foundStatmentList )
                {
                    throw new ApplicationException( "statement list Error: waited statement list" );
                }
            }
        }
        #endregion

        #region Operators Checking
        private void CheckReadOrWrite()
        {
            CheckNextLexem( "(" );
            CheckIdList();
            CheckNextLexem( ")" );
            CheckNextLexem( ";" );
        }

        private void CheckAssign()
        {
            CheckNextLexem( ":" );
            CheckNextLexem( "=" );
            CheckCommonExpression();
            CheckCurrentLexem( ";" );
        }
        #endregion

        #region Expression Checking
        private void CheckCommonExpression()
        {
            CheckExpression();
            CheckAddition();
        }

        private void CheckExpression()
        {
            CheckElement();
            CheckMultiplication();
        }

        private void CheckMultiplication()
        {
            MoveLexem();

            if ( GetCurrentLexem().Equals("*") )
            {
                CheckElement();
                CheckMultiplication();
            }
        }

        private void CheckAddition()
        {
            if ( GetCurrentLexem().Equals("+") )
            {
                CheckExpression();
                CheckAddition();
            }
        }
        #endregion

        #region Primary Checking
        private void CheckStatement( ref bool foundStatmentList, ref bool foundEnd )
        {
            MoveLexem();
            string str = GetCurrentLexem();
            switch ( str )
            {
                case "READ":
                case "WRITE":
                    CheckReadOrWrite();
                    foundStatmentList = true;
                    break;
                case "ID":
                    CheckAssign();
                    foundStatmentList = true;
                    break;
                case "END":
                    if ( !foundStatmentList )
                    {
                        throw new ApplicationException( "ST Error: waited READ/WRITE/Assign(id := ...)" );
                    }
                    foundEnd = true;
                    break;
                default:
                    if ( !foundStatmentList )
                    {
                        throw new ApplicationException( "ST Error: waited READ/WRITE/Assign(id := ...)" );
                    }
                    else
                    {
                        throw new ApplicationException( "Error: Waited end" );
                    }
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

        private void CheckElement()
        {
            MoveLexem();

            switch ( GetCurrentLexem() )
            {
                case "-":
                    CheckElement();
                    break;
                case "ID":
                case "NUM":
                    break;
                case "(":
                    CheckCommonExpression();
                    CheckCurrentLexem( ")" );
                    break;
                default:
                    throw new ApplicationException( "Incorrect F value" );
            }
        }
        #endregion
    }
}
