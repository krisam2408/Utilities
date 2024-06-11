namespace MVVMCore;

public abstract class BaseViewModel : ObservableProperty
{
    private bool m_enabled;
    public bool Enabled { get { return m_enabled; } set { SetValue(ref m_enabled, value); } }

    protected BaseViewModel() { }
}
