using System.ComponentModel;
namespace MVC {
    public interface ICancellable {
        void Cancel();
        event RunWorkerCompletedEventHandler Completed;
    }
}
