using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Modles
{
    public class MES_RETURN_UI
    {
        private string _TYPE;

        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        private string _MESSAGE;

        public string MESSAGE
        {
            get { return _MESSAGE; }
            set { _MESSAGE = value; }
        }

        private int _TID;

        public int TID
        {
            get { return _TID; }
            set { _TID = value; }
        }

        private string _GC;

        public string GC
        {
            get { return _GC; }
            set { _GC = value; }
        }

        private string _TM;

        public string TM
        {
            get { return _TM; }
            set { _TM = value; }
        }

        private int _TMSX;

        public int TMSX
        {
            get { return _TMSX; }
            set { _TMSX = value; }
        }
        private string _BH;

        public string BH
        {
            get { return _BH; }
            set { _BH = value; }
        }

    }
}
