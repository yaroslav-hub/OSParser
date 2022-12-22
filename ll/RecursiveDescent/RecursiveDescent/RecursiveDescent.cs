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

        private void CheckIdList()
        {
            try
            {
                MoveLexem();
                bool resultFlag = RecursCheckIdList(GetCurrentLexem());
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

        private bool RecursCheckIdList(string str)
        {
            bool resultFlag = false;

            switch (str)
            {
                case "id,":
                    MoveLexem();
                    resultFlag = RecursCheckIdList(GetCurrentLexem());
                    break;
                case "id":
                    return true;
                default:
                    throw new ApplicationException("id Error: waited id");
            }

            return resultFlag;
        }


        private void CheckListSt()
        {
            try
            {
                RecursCheckListSt();
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        private void RecursCheckListSt()// съедает end -> getCurrentLExem() хранит end
        {
            try
            {
                CheckSt();
                RecursCheckListSt();
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        private void CheckSt()
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
