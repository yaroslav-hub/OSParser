using System;
namespace LL1
{
    public class TableElem
    {

        public string Char { get; set; } = string.Empty;
        public string PtrCharSet { get; set; } = string.Empty;

        public bool IsShift { get; set; } = false;
        public bool IsError { get; set; } = false;
        public bool IsNeedToAddToStack { get; set; } = false;
        public bool IsEndOfAction { get; set; } = false;
        public TableElem NextElem { get; set; } = null;

        public TableElem(string simbol, string ptrSimbolSet, bool isShift, bool isError, bool isNeedToAddToSteck, bool isEndOfAction, TableElem next = null)
        {
            Char = simbol;
            PtrCharSet = ptrSimbolSet;

            IsShift = isShift;
            IsError = isError;
            IsNeedToAddToStack = isNeedToAddToSteck;
            IsEndOfAction = isEndOfAction;

            NextElem = next;
        }
    }
}

