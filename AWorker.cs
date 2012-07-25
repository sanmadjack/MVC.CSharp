using System.ComponentModel;
namespace MVC {
    public abstract class AWorker : ANotifyingObject, ICancellable {
        public BackgroundWorker worker;

        protected AWorker(RunWorkerCompletedEventHandler when_done) {
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            if (when_done != null)
                worker.RunWorkerCompleted += when_done;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
        }

        protected abstract void worker_DoWork(object sender, DoWorkEventArgs e);


        public event RunWorkerCompletedEventHandler Completed {
            add { worker.RunWorkerCompleted += value; }
            remove { worker.RunWorkerCompleted -= value; }
        }


        public void Cancel() {
            if (worker != null)
                worker.CancelAsync();
        }
    }
}
