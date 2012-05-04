using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MVC
{
    public class CheckedTreeViewModel: INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
          PropertyChangedEventHandler handler = PropertyChanged;
          if (handler != null)
          {
              handler(this, new PropertyChangedEventArgs(name));
          }
        }


    }
}
