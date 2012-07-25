using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
namespace MVC {
    public class FilteredModel<I, T> : Model<I, T>
        where T : AModelItem<I>
        where I : AIdentifier {

        private Model<I,T> model;

        public FilteredModel(Model<I, T> data_source) {
            model = data_source;

            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(model_PropertyChanged);
            model.CollectionChanged += new NotifyCollectionChangedEventHandler(model_CollectionChanged);
            model.ItemPropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(model_ItemPropertyChanged);
            Refresh();
        }

        void model_ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (matchesFilters(sender as T)) {
                if (!this.Contains(sender)) {
                    this.AddWithSort(sender as T);
                }
            } else {
                if (this.Contains(sender)) {
                    this.Remove(sender as T);
                }
            }
        }

        void model_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Reset:
                    this.Refresh();
                    break;
                case NotifyCollectionChangedAction.Add:
                    foreach(T item in e.NewItems) {
                        if (matchesFilters(item)) {
                            this.Add(item);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems) {
                        if (this.Contains(item))
                            this.Remove(item);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void Refresh() {
            this.Clear();
            foreach (T item in model.Items) {
                if (matchesFilters(item)) {
                    this.AddWithSort(item);
                }
            }
            base.Refresh();
        }

        void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            throw new NotImplementedException();
        }

        private Dictionary<string, object> filters = new Dictionary<string,object>();

        public void AddFilter(string property_name, object value) {
            filters.Add(property_name,value);
            model.Refresh();
        }

        private bool matchesFilters(T item) {
            foreach (string property in filters.Keys) {
                object value = item.GetType().GetProperty(property).GetValue(item, null);
                if (!value.Equals(filters[property])) {
                    return false;
                }
            }
            return true;
        }

    }
}
