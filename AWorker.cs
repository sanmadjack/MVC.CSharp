using System.ComponentModel;
namespace MVC {
    public abstract class AWorker : ANotifyingObject, ICancellable {
        public BackgroundWorker worker;

        protected AWorker(RunWorkerCompletedEventHandler when_done) {
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);


            if (when_done != null)
                this.Completed += when_done;

            worker.DoWork +=new DoWorkEventHandler(preWork);

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
        }

        private bool cancelled = false;

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            RunWorkerCompletedEventArgs ne = new RunWorkerCompletedEventArgs(e.Result,e.Error,cancelled); 


            if(Completed!=null)
                Completed(this, ne);
        }

        protected void preWork(object sender, DoWorkEventArgs e) {
            cancelled = false;
        }

        protected abstract void worker_DoWork(object sender, DoWorkEventArgs e);

        public event RunWorkerCompletedEventHandler Completed;

        public void Cancel() {
            this.cancelled = true;
            if (worker != null)
                worker.CancelAsync();
        }
    }
}
