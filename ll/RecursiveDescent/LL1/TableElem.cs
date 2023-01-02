using System;

namespace LLREcursion
{
    public class TableElem
    {
        public TableElem(string simbol, string ptrSimbolSet, bool isShift, bool isError, bool isNeedToAddToSteck, bool isEndOfAction)
        {
            _char = simbol;
            _ptrCharSet = ptrSimbolSet;

            _isShift = isShift;
            _isError = isError;
            _isNeedToAddToSteck = isNeedToAddToSteck;
            _isEndOfAction = isEndOfAction;
        }

        public TableElem(string simbol, string ptrSimbolSet, bool isShift, bool isError, bool isNeedToAddToSteck, bool isEndOfAction, TableElem next)
        {
            _char = simbol;
            _ptrCharSet = ptrSimbolSet;

            _isShift = isShift;
            _isError = isError;
            _isNeedToAddToSteck = isNeedToAddToSteck;
            _isEndOfAction = isEndOfAction;

            _nextElem = next;
        }

        public void SetNextElem(TableElem next)
        {
            _nextElem = next;
        }


        public string _char = "";
        public string _ptrCharSet = "";

        public bool _isShift = false;
        public bool _isError = false;
        public bool _isNeedToAddToSteck = false;
        public bool _isEndOfAction = false;
        public TableElem _nextElem = null;
    }
}

