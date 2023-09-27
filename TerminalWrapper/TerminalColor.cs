namespace TerminalWrapper;

public struct TerminalColor
{
    private float m_red;
    public float Red
    {
        get { return m_red; }
        set { m_red = Math.Clamp(value, 0f, 1f); }
    }

    private float m_green;
    public float Green
    {
        get { return m_green; }
        set { m_green = Math.Clamp(value, 0f, 1f); }
    }

    private float m_blue;
    public float Blue
    {
        get { return m_blue; }
        set { m_blue = Math.Clamp(value, 0f, 1f); }
    }

    private float m_alpha;
    public float Alpha
    {
        get { return m_alpha; }
        set { m_alpha = Math.Clamp(value, 0f, 1f); }
    }

    public TerminalColor(float r, float g, float b, float a)
    {
        m_red = 1f;
        m_green = 1f;
        m_blue = 1f;
        m_alpha = 1f;

        Red = r;
        Green = g;
        Blue = b;
        Alpha = a;
    }

    public TerminalColor(float r, float g, float b) : this(r, g, b, 1f) { }

    public TerminalColor() : this(1f, 1f, 1f, 1f) { }

    public float[] Channels()
    {
        return new float[] { Red, Green, Blue };
    }

    public static TerminalColor FromHexCode(string hexcode)
    {
        if (hexcode.Length != 6)
            throw new FormatException($"{nameof(hexcode)} must be in FFFFFF format");

        float[] channels = new float[3];
        for (int i = 0; i < 3; i++)
        {
            string channelHex = hexcode.ToLower().Substring(i * 2, 2);
            channels[i] = ((float)Convert.ToByte(channelHex, 16)) / 255f;
        }

        return new TerminalColor(channels[0], channels[1], channels[2]);
    }
}
