using System.ComponentModel;
namespace MVC {
    public class ANotifyingObject : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string prop) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }


    }
}
