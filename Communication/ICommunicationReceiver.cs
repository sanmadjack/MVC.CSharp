using System.Threading;

namespace MVC.Communication {
    public interface ICommunicationReceiver {

		IThreadBridge ThreadBridge { get; }

        bool Available { get; }
        ResponseType sendMessage(MessageEventArgs e);
        void requestInformation(RequestEventArgs e);
        void updateProgress(ProgressUpdatedEventArgs e);
        void disableInterface();
        void enableInterface();
        void hideInterface();
        void showInterface();
        void closeInterface();

        bool isSameContext();
    }
}
