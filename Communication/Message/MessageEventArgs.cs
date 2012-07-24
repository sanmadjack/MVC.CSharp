using System;

namespace MVC.Communication {
    public class MessageEventArgs : RespondableEventArg {
        public MessageTypes type;
        public bool acknowledged = false;
        public Exception exception = null;
    }
}
