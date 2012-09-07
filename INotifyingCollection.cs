using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MVC {
    public interface INotifyingCollection {
        IList GenericList { get; }

        event PropertyChangedEventHandler PropertyChanged;
        event PropertyChangedEventHandler ItemPropertyChanged;
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
