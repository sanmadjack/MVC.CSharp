using System.Collections.Generic;
using System.Threading;
namespace MVC.Communication {
    #region Progress Definitions
    public delegate void ProgressChangedEventHandler(ProgressUpdatedEventArgs e);
    #endregion

    public class ProgressHandler : CommunicationHandler {

        private static Stack<string> saved_messages = new Stack<string>();
        private static string _message = null;
        public static string message {
            get {
                return _message;
            }
            set {
                if (!suppress_communication)
                    _message = value;
                setProgress();
            }
        }
        public static void saveMessage() {
            if (_message != null)
                saved_messages.Push(_message);
        }
        public static void restoreMessage() {
            if (saved_messages.Count > 0)
                message = saved_messages.Pop();
        }
        public static void clearMessage() {
            message = null;
            saved_messages.Clear();
        }

        private static int _value = 0;
        public static int value {
            set {
                _value = value;
                _state = ProgressState.Normal;
                setProgress();
            }
            get {
                return _value;
            }
        }
        private static int _max = 0;
        public static int max {
            set {
                _max = value;
                setProgress();
            }
            get {
                return _max;
            }
        }
        private static ProgressState _state = ProgressState.Normal;
        public static ProgressState state {
            set {
                _state = value;
                setProgress();
            }
            get {
                return _state;
            }
        }
        private static int _sub_value = 0;
        public static int sub_value {
            set {
                _sub_value = value;
                setSubProgress();
            }
            get {
                return _sub_value;
            }
        }
        private static int _sub_max = 0;
        public static int sub_max {
            set {
                _sub_max = value;
                setSubProgress();
            }
            get {
                return _sub_max;
            }
        }
        private static ProgressState _sub_state = ProgressState.Normal;
        public static ProgressState sub_state {
            set {
                _sub_state = value;
                setSubProgress();
            }
            get {
                return _sub_state;
            }
        }


        //protected static event ProgressChangedEventHandler ProgressChanged;
        //protected static event ProgressChangedEventHandler SubProgressChanged;

        private static void setProgress() {
            setProgress(_value, _max, _message, _state);
        }

        private static void setProgress(int value, int max, string message, ProgressState progstate) {
            ProgressUpdatedEventArgs e = new ProgressUpdatedEventArgs();
            e.max = max;
            e.message = message;
            e.value = value;
            e.state = progstate;
            ICommunicationReceiver receiver = getReceiver();
            if (receiver == null)
                return;

            if (receiver.context != null) {
                receiver.context.Post(new SendOrPostCallback(delegate(object state) {
                    ProgressChangedEventHandler handler = receiver.updateProgress;
                    if (handler != null) {
                        handler(e);
                    }
                }), null);
            } else {
                receiver.updateProgress(e);
            }
        }

        private static void setSubProgress() {
            setSubProgress(_sub_value, _sub_max, _sub_state);
        }

        private static void setSubProgress(int value, int max, ProgressState progstate) {
            ProgressUpdatedEventArgs e = new ProgressUpdatedEventArgs();
            e.max = max;
            e.message = null;
            e.value = value;
            e.state = progstate;

            ICommunicationReceiver receiver = getReceiver();
            if (receiver == null)
                return;

            if (receiver.context != null) {
                receiver.context.Post(new SendOrPostCallback(delegate(object state) {
                    ProgressChangedEventHandler handler = receiver.updateProgress;
                    if (handler != null) {
                        handler(e);
                    }
                }), null);
            } else {
                receiver.updateProgress(e);
            }
        }

    }
}
