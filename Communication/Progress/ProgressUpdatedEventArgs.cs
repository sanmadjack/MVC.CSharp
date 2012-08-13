using System;

namespace MVC.Communication {
    public class ProgressUpdatedEventArgs : EventArgs {
        public int value;
        public int max;
        public float progress_percentage {
            get {
                return (float)value / (float)max;
            }
        }
        public string message;
        public ProgressState state;
        public ProgressUpdatedEventArgs() : base() { }

    }
}
