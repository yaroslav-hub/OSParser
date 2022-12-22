using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveDescent
{
    public class RecursiveDescent
    {
        public void Check()
        {
            ParseProg();
        }

        public void ParseProg()
        {
            if (GetLexem().Equals("PROG"))
            {
                if (GetLexem.Equals("id"))
                {
                    ParseVar();                    
                    if (GetLexem.Equals("begin"))
                    {
                        ParseListst();
                        if (GetLexem.Equals("end"))
                        {
                            bool end = false;
                            try
                            {
                                GetLexem();
                            }
                            catch (EndOfStreamException e)
                            {
                                end = true;
                            }
                            if (!end)
                            {
                                throw new ApplicationException("Symbol after \"end\"");
                            }
                            return;
                        }
                    }
                }
            }
            throw new ApplicationException("Error on parsing program structure");
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
