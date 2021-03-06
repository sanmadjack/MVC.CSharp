﻿using System.Collections.Generic;
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
                System.Text.StringBuilder output = new System.Text.StringBuilder(_message);
                if (!string.IsNullOrWhiteSpace(_debug_message))
                {
                    output.Append(" (");
                    output.Append(_debug_message);
                    output.Append(")");
                }
                return output.ToString();
            }
            set {
                if (!suppress_communication)
                {
                    _message = value;
                    _debug_message = null;
                }
                setProgress();
            }
        }
        private static string _debug_message = null;
        public static string DebugMessage
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _debug_message = null;
                }
                else
                {
                    _debug_message = value;
                    setProgress();
                }
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
            _debug_message = null;
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
            setProgress(_value, _max, message, _state);
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

			if (receiver.ThreadBridge != null) {
				receiver.ThreadBridge.Post(delegate() {
                    ProgressChangedEventHandler handler = receiver.updateProgress;
                    if (handler != null) {
                        handler(e);
                    }
                });
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

			if (receiver.ThreadBridge != null) {
				receiver.ThreadBridge.Post(delegate() {
                    ProgressChangedEventHandler handler = receiver.updateProgress;
                    if (handler != null) {
                        handler(e);
                    }
                });
            } else {
                receiver.updateProgress(e);
            }
        }

    }
}
