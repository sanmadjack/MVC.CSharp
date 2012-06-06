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
        }

        void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            throw new NotImplementedException();
        }

        private Dictionary<string, object> filters = new Dictionary<string,object>();

        public void AddFilter(string property_name, object value) {
            filters.Add(property_name,value);
            model.refresh();
        }

        private bool matchesFilters(T item) {
            foreach (string property in filters.Keys) {
                object value = item.GetType().GetProperty(property).GetValue(item, null);
                if (value != filters[property]) {
                    return false;
                }
            }
            return true;
            ;
        }

        public new IList<T> Items {
            get {
                List<T> items = new List<T>();
                foreach (T item in model.Items) {
                    if (matchesFilters(item)) {
                        items.Add(item);
                    }
                }
                return items;
            }
        }

        public int Count {
            get {
                return Items.Count;
            }
        }

    }
}
