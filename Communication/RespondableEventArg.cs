using System;

namespace MVC.Communication {
    public class RespondableEventArg : EventArgs {
        public string title, message;
        public ResponseType response = ResponseType.None;
    }
}
