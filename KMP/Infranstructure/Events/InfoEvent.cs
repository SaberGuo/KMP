using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infranstructure.Events
{
    public enum ExceptionType
    {
        INFO = 1,
        WARNING = 2,
        ERROR = 3,
    }
    public class MyException : Exception
    {
        private ExceptionType _infoType;

        public ExceptionType InfoType
        {
            get { return _infoType; }
            set { _infoType = value; }
        }

        public MyException(string info, ExceptionType infoType)
            : base(info)
        {
            this._infoType = infoType;
        }
    }

    public class InfoEvent:CompositePresentationEvent<Exception>
    {
    }
}
