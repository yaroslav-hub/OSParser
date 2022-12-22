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
        public bool Check()
        {
            return ParseProg();
        }

        public bool ParseProg()
        {
            if (GetLexem().Equals("PROG"))
            {
                if (GetLexem.Equals("id"))
                {
                    if (ParseVar())
                    {
                        if (GetLexem.Equals("begin"))
                        {
                            if (ParseListst())
                            {
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
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool ParseVar()
        {
            if (GetLexem().Equals("VAR"))
            {
                if (ParseIdlist())
                {
                    if (GetLexem().Equals(":"))
                    {
                        if (ParseType())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ParseType()
        {
            string lexem = GetLexem();
            return (lexem.Equals("int") || lexem.Equals("float") || lexem.Equals("bool") || lexem.Equals("string"));
        }
    }
}
