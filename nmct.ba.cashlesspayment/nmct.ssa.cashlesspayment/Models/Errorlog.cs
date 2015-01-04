using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlesspayment.Models
{
    public class Errorlog
    {
        private Kassa registerid;
        private DateTime timestamp;
        private string message;
        private string stacktrace;
        private Organisatie organisatie;
        public Kassa RegisterID
        {
            get { return registerid; }
            set { registerid = value; }
        }
        public DateTime TimeStamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string Stacktrace
        {
            get { return stacktrace; }
            set { stacktrace = value; }
        }
        public Organisatie Organisatie
        {
            get { return organisatie; }
            set { organisatie = value; }
        }
    }
}