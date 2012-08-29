using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
namespace MVC {
    public class Model<I, T> : SimpleModel<T>
        where T : AModelItem<I>
        where I : AIdentifier {


        public Boolean containsId(I id) {
            lock (this) {
                foreach (T item in this) {
                    if (item.id.Equals(id))
                        return true;
                }
            }
            return false;
        }

        public T get(I id) {
            lock (this) {
                foreach (T item in this) {
                    if (item.id.Equals(id))
                        return item;
                }
            }
            return null;
        }

        public void AddWithSort(T add_me) {
            add_me.PropertyChanged += new PropertyChangedEventHandler(NotifyItemPropertyItemChanged);
            base.AddWithSort(add_me);
        }

        public List<T> SelectedItems {
            get {
                List<T> return_me = new List<T>();
                lock (this) {
                    foreach (T item in this) {
                        if (item.IsSelected)
                            return_me.Add(item);
                    }
                }
                return return_me;
            }
        }

    }

    public class Model<T> : Model<StringID, T> where T : AModelItem<StringID> {
    }


}
