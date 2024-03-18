namespace SpiegelHase.DataTransfer;

public class BackParameter
{
    public string BackController { get; private set; }
    public string BackAction { get; private set; }

    private string? m_backId;
    public string? BackId 
    {
        get { return m_backId; }
        private set 
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                m_backId = null;
                return;
            }
            m_backId = value;
        }
    }

    public BackParameter(string controller, string action = "index", string back = "")
    {
        BackController = controller;
        BackAction = action;
        BackAction = back;
    }
}
