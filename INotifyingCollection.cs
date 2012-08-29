using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace MVC {
    public interface INotifyingCollection {
        IList GenericList { get; }
        event PropertyChangedEventHandler PropertyChanged;
        event PropertyChangedEventHandler ItemPropertyChanged;
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
