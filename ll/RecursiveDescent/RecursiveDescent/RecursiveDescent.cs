using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveDescent
{
    public class RecursiveDescent
    {

        // 
        // moveLexem() -> string; new Lexem for CurrentLexem
        // getCurrentLExem() -> string;

        void MoveLexem()
        { }
        string GetCurrentLexem()
        { return ""; }

        void CheckRead()
        { }
        void CheckWrite()
        { }
        void CheckAssign()
        { }

        void CheckIDLIST()
        {
            try
            {
                MoveLexem();
                bool resultFlag = RecursIDLIST(GetCurrentLexem());
                if(!resultFlag)
                {
                    throw new ApplicationException("id list Error: waited id list");
                }
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        bool RecursIDLIST(string str)
        {
            bool resultFlag = false;

            switch (str)
            {
                case "id,":
                    MoveLexem();
                    resultFlag = RecursIDLIST(GetCurrentLexem());
                    break;
                case "id":
                    return true;
                default:
                    throw new ApplicationException("id Error: waited id");
            }

            return resultFlag;
        }


        void CheckLISTST()
        {
            try
            {
                RecursLISTST();
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        void RecursLISTST()// съедает end -> getCurrentLExem() хранит end
        {
            try
            {
                CheckST();
                RecursLISTST();
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        void CheckST()
        {
            MoveLexem();
            string str = GetCurrentLexem();
            try
            {
                switch (str)
                {
                    case "READ":
                        CheckRead();
                        break;
                    case "WRITE":
                        CheckWrite();
                        break;
                    case "id":
                        CheckAssign();
                        break;
                    case "end":
                        break;
                    default:
                        throw new ApplicationException("ST Error: waited READ/WRITE/Assign(id := ...)");
                }
            }
            catch (ApplicationException e)
            {
                throw e;
            }
            

        }  

    }
}
