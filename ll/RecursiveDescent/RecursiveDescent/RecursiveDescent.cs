using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveDescent
{
    public class RecursiveDescent
    {

        // GetLexem() -> string


        string GetLexem()
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
                bool resultFlag = RecursIDLIST(GetLexem());
                if(!resultFlag)
                {
                    throw new ArgumentException("id list Error: waited id list");
                }
            }
            catch (ArgumentException e)
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
                    resultFlag = RecursIDLIST(GetLexem());
                    break;
                case "id":
                    return true;
                default:
                    throw new ArgumentException("id Error: waited id");
            }

            return resultFlag;
        }


        void CheckLISTST()
        {
            try
            {
                RecursLISTST();
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        void RecursLISTST()
        {
            try
            {
                CheckST();
                RecursLISTST();
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        void CheckST()
        {
            string str = GetLexem();
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
                    default:
                        throw new ArgumentException("ST Error");
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            

        }  

    }
}
