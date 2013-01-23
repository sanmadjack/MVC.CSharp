using System.Threading;

namespace MVC.Communication.Interface {
    delegate void GenericInterfaceEventHandler();

    public class InterfaceHandler : CommunicationHandler {
        public static void closeInterface() {
            ICommunicationReceiver receiver = getReceiver();
            if (receiver == null)
                return;

            if (receiver.ThreadBridge != null) {
				receiver.ThreadBridge.Send(delegate() {
                    GenericInterfaceEventHandler handler = receiver.closeInterface;
                    if (handler != null) {
                        handler();
                    }
                });
            } else {
                receiver.closeInterface();
            }
        }
        public static void disableInterface() {
            ICommunicationReceiver receiver = getReceiver();
            if (receiver == null)
                return;

			if (receiver.ThreadBridge != null) {
				receiver.ThreadBridge.Send(delegate() {
                    GenericInterfaceEventHandler handler = receiver.disableInterface;
                    if (handler != null) {
                        handler();
                    }
                });
            } else {
                receiver.disableInterface();
            }
        }
        public static void enableInterface() {
            ICommunicationReceiver receiver = getReceiver();
            if (receiver == null)
                return;

			if (receiver.ThreadBridge != null) {
				receiver.ThreadBridge.Send(delegate() {
                    GenericInterfaceEventHandler handler = receiver.enableInterface;
                    if (handler != null) {
                        handler();
                    }
                });
            } else {
                receiver.enableInterface();
            }

        }
        public static void hideInterface() {
            ICommunicationReceiver receiver = getReceiver();
            if (receiver == null)
                return;

			if (receiver.ThreadBridge != null) {
				receiver.ThreadBridge.Send(delegate() {
                    GenericInterfaceEventHandler handler = receiver.hideInterface;
                    if (handler != null) {
                        handler();
                    }
                });
            } else {
                receiver.hideInterface();
            }

        }
        public static void showInterface() {
            ICommunicationReceiver receiver = getReceiver();
            if (receiver == null)
                return;

			if (receiver.ThreadBridge != null) {
				receiver.ThreadBridge.Send(delegate() {
                    GenericInterfaceEventHandler handler = receiver.showInterface;
                    if (handler != null) {
                        handler();
                    }
                });
            } else {
                receiver.showInterface();
            }

        }
    }
}
