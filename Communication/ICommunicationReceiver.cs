using System.Threading;

namespace MVC.Communication {
    public interface ICommunicationReceiver {
        SynchronizationContext context { get; }
        bool Available { get; }
        void sendMessage(MessageEventArgs e);
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
