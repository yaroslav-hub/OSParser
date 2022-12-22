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

        private void MoveLexem()
        { }
        private string GetCurrentLexem()
        { return ""; }

        private void CheckReadOrWrite()
        { }
        private void CheckAssign()
        { }

        private void CheckIDLIST()
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

        private bool RecursIDLIST(string str)
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


        private void CheckLISTST()
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

        private void RecursLISTST()// съедает end -> getCurrentLExem() хранит end
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

        private void CheckST()
        {
            MoveLexem();
            string str = GetCurrentLexem();
            try
            {
                switch (str)
                {
                    case "READ":
                        CheckReadOrWrite();
                        break;
                    case "WRITE":
                        CheckReadOrWrite();
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
