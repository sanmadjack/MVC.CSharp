using System.Collections.Specialized;
using System.ComponentModel;
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


        void model_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            NotifyPropertyChanged(e.PropertyName);
        }

        public static T Get(I id) {
            return model.get(id);
        }

        public static void Add(T item) {
            model.Add(item);
        }

    }
}
