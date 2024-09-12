namespace TerminalWrapper.CommonTasks;

public class ClearTerminalTask : MainTask
{
    private const string m_defaultName = "Clear Terminal";
    private readonly string? m_taskName;
    public override string TaskName 
    {
        get
        {
            if(string.IsNullOrWhiteSpace(m_taskName))
                return m_defaultName;
            return m_taskName;
        }
    }

    public ClearTerminalTask()
    {
        m_taskName = null;
    }

    public ClearTerminalTask(string taskName)
    {
        m_taskName = taskName;
    }

    public override async Task ExecuteAsync(CancellationToken cancelToken)
    {
        await Terminal.ClearAsync();
    }
}
