using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using System.ComponentModel;
namespace MVC {
    public class SimpleModel<T> : ObservableCollection<T>, INotifyingCollection  {

        public new IList<T> Items {
            get {
                return base.Items;
            }
        }

        public IList GenericList {
            get {
                ArrayList items = new ArrayList();
                foreach (object item in base.Items) {
                    items.Add(item);
                }
                return items;
            }
        }

        protected bool _cancelling = false;
        public void cancel() { _cancelling = true; }


        public void AddRange(ICollection<T> us) {
            foreach (T me in us) {
                AddWithSort(me);
            }

        }

        public new event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string prop) {
            OnPropertyChanged(new PropertyChangedEventArgs(prop));
        }

        public virtual void AddWithSort(T add_me) {
            if (this.Count == 0) {
                this.InsertItem(0, add_me);
            } else {
                for (int i = 0; i < this.Count; i++) {
                    T compare = this[i];
                    int result = 0;
                    if (compare is IComparable) {
                        IComparable comparer = compare as IComparable;
                        result = comparer.CompareTo(add_me);
                    } else if (compare is IComparable<T>) {
                        IComparable<T> comparerr = compare as IComparable<T>;
                        result = comparerr.CompareTo(add_me);
                    } else {
                        compare.ToString().CompareTo(add_me.ToString());
                    }
                    if (result > 0) {
                        this.InsertItem(i, add_me);
                        return;
                    }
                }
                this.InsertItem(this.Count, add_me);
            }
        }

        public event PropertyChangedEventHandler ItemPropertyChanged;
        protected void NotifyItemPropertyItemChanged(object sender, PropertyChangedEventArgs e) {
            if (ItemPropertyChanged != null) {
                ItemPropertyChanged(sender, e);
            }
        }


        private bool _isEnabled = true;
        public bool IsEnabled {
            get { return _isEnabled; }
            set {
                if (value != _isEnabled) {
                    _isEnabled = value;
                    this.NotifyPropertyChanged("IsEnabled");
                }

            }
        }

        public SimpleModel() {
        }

        public virtual void Refresh() {
            NotifyCollectionChangedEventArgs e =
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            try {
                this.OnCollectionChanged(e);
            } catch (Exception ex) {
                throw new Exception("Error While Refreshing List", ex);
            }
        }

        public override event NotifyCollectionChangedEventHandler CollectionChanged;
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            var eh = CollectionChanged;
            if (eh != null) {
                Dispatcher dispatcher = (from NotifyCollectionChangedEventHandler nh in eh.GetInvocationList()
                                         let dpo = nh.Target as DispatcherObject
                                         where dpo != null
                                         select dpo.Dispatcher).FirstOrDefault();

                if (dispatcher != null && dispatcher.CheckAccess() == false) {
                    dispatcher.Invoke(DispatcherPriority.DataBind, (Action)(() => OnCollectionChanged(e)));
                } else {
                    foreach (NotifyCollectionChangedEventHandler nh in eh.GetInvocationList())
                        nh.Invoke(this, e);
                }
            }
        }



    }
}
