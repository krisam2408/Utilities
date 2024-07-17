using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMCore;

public abstract class ObservableModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if(EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        NotifyPropertyChanged(propertyName);
        return true;
    }
}
