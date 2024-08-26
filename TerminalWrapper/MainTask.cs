namespace TerminalWrapper;

public abstract class MainTask
{
    private Terminal? m_terminal;
    public Terminal Terminal
    {
        get
        {
            if (m_terminal is null)
                throw new NullReferenceException("Terminal was not correctly set");
            return m_terminal;
        }
        internal set
        {
            if(value is not null)
                m_terminal = value;
        }
    }

    public abstract string TaskName { get; }
    public abstract Task ExecuteAsync(CancellationToken cancelToken);
}
