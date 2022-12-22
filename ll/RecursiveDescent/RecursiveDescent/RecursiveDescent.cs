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
                bool isFasedStatmentList = false;
                CheckSt(ref isFasedStatmentList);
                RecursCheckListSt();
                if (!isFasedStatmentList)
                {
                    throw new ApplicationException("statement list Error: waited statement list");
                }
            }
            catch (ApplicationException e)
            {
                throw e;
            }
        }

        private void CheckSt(ref bool isFasedStatmentListptr)
        {
            MoveLexem();
            string str = GetCurrentLexem();
            try
            {
                switch (str)
                {
                    case "READ":
                        CheckReadOrWrite();
                        isFasedStatmentListptr = true;
                        break;
                    case "WRITE":
                        CheckReadOrWrite();
                        isFasedStatmentListptr = true;
                        break;
                    case "id":
                        CheckAssign();
                        isFasedStatmentListptr = true;
                        break;
                    case "end":
                        if(!isFasedStatmentListptr)
                        {
                            throw new ApplicationException("ST Error: waited READ/WRITE/Assign(id := ...)");
                        }
                        break;
                    default:
                        if (!isFasedStatmentListptr)
                        {
                            throw new ApplicationException("ST Error: waited READ/WRITE/Assign(id := ...)");
                        }
                        else
                        {
                            throw new ApplicationException("Error: Waited end");
                        }
                        
                }
            }
            catch (ApplicationException e)
            {
                throw e;
            }
            

        }  

    }
}
