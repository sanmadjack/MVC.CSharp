using System.ComponentModel;
namespace MVC {
    public interface IModelItem {
        bool IsSelected { get; set; }
        string ToolTip { get; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
