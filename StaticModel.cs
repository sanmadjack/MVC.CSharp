using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.Generic;
namespace MVC {
    public abstract class StaticModel<I, T> : ANotifyingObject, INotifyCollectionChanged
        where T : AModelItem<I>
        where I : AIdentifier {
        protected static Model<I, T> model = new Model<I, T>();

        public StaticModel() {
            model.PropertyChanged += new PropertyChangedEventHandler(model_PropertyChanged);
            model.CollectionChanged += new NotifyCollectionChangedEventHandler(model_CollectionChanged);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        void model_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (CollectionChanged != null) {
                CollectionChanged(this, e);
            }
        }

        public static IList<T> SelectedItems {
            get {
                List<T> items = new List<T>();
                lock (model) {
                    foreach (T item in model.Items) {
                        if (item.IsSelected)
                            items.Add(item);
                    }
                }
                return items;
            }
        }

        public static bool IsEnabled {
            protected set {
                model.IsEnabled = value;
            }
            get {
                return model.IsEnabled;
            }
        }

        void model_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            NotifyPropertyChanged(e.PropertyName);
        }

        protected static void StaticNotifyPropertyChanged(string name) {
            model.NotifyPropertyChanged(name);
        }

        public static void Clear() {
            model.Clear();
        }

        public static void Refresh() {
            model.Refresh();
        }

        public static T Get(I id) {
            return model.get(id);
        }

        public static bool Contains(I id) {
            return model.containsId(id);
        }

        public static void Add(T item) {
            model.Add(item);
        }

        public static INotifyPropertyChanged DataContext {
            get {
                return model;
            }
        }

        public static IList<T> Items {
            get {
                return model.Items;
            }
        }
    }
}
