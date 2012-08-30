﻿using System;
using System.Collections.Generic;

namespace MVC {

    public abstract class AModelItem : AModelItem<StringID> {
        protected AModelItem(String id)
            : base(new StringID(id)) {
        }
    }

    public abstract class AModelItem<I> : ANotifyingObject, IComparable<AModelItem<I>>, IModelItem where I : AIdentifier {
        public I id {
            get;
            protected set;
        }

        public virtual string ToolTip {
            get;
            protected set;
        }

        protected List<IComparable> comparisons = new List<IComparable>();

        protected AModelItem() {
        }

        protected AModelItem(I id) {
            this.id = id;
        }


        public virtual int CompareTo(AModelItem<I> comparable) {
            return this.id.CompareTo(comparable.id);
        }


        protected static int compare(IComparable a, IComparable b) {
            if (a == null) {
                if (b == null)
                    return 0;
                else
                    return -1;
            } else {
                return a.CompareTo(b);
            }
        }


        private AModelItem<I> _parent;
        public AModelItem<I> Parent {
            get { return _parent; }
            set { _parent = value; }
        }

        private bool _isSelected = false;
        public bool IsSelected {
            get { return _isSelected; }
            set {
                if (value != _isSelected) {
                    _isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }
        }
        private bool _isExpanded = false;
        public bool IsExpanded {
            get { return _isExpanded; }
            set {
                if (value != _isExpanded) {
                    _isExpanded = value;
                    this.NotifyPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                //if (_isExpanded && _parent != null)
                //_parent.IsExpanded = true;
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

        public System.Drawing.Color BackgroundColor {
            get {
                return System.Drawing.Color.White;
            }
        }

        public System.Drawing.Color SelectedColor {
            get {
                return System.Drawing.Color.Blue;
            }
        }


    }
}
