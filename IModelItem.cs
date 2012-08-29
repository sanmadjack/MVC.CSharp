﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace MVC {
    public interface IModelItem {
        bool IsSelected { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
