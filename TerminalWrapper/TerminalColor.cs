namespace TerminalWrapper;

public struct TerminalColor
{
    public string? Name { get; set; } 

    private float m_r;
    public float R
    {
        get { return m_r; }
        set { m_r = Math.Clamp(value, 0f, 1f); }
    }

    private float m_g;
    public float G
    {
        get { return m_g; }
        set { m_g = Math.Clamp(value, 0f, 1f); }
    }

    private float m_b;
    public float B
    {
        get { return m_b; }
        set { m_b = Math.Clamp(value, 0f, 1f); }
    }

    private float m_alpha;
    public float Alpha
    {
        get { return m_alpha; }
        set { m_alpha = Math.Clamp(value, 0f, 1f); }
    }

    public TerminalColor(string name, float r, float g, float b, float a)
    {
        Name = name;
        R = r;
        G = g;
        B = b;
        Alpha = a;
    }

    public TerminalColor(float r, float g, float b, float a) : this("SomeColor", r, g, b, a) { }

    public TerminalColor(float r, float g, float b) : this(r, g, b, 1f) { }

    public TerminalColor() : this(1f, 1f, 1f, 1f) { }

    public float[] Channels()
    {
        return new float[] { R, G, B };
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

        return new TerminalColor(hexcode, channels[0], channels[1], channels[2], 1f);
    }

    public static TerminalColor White => new("White", 1f, 1f, 1f, 1f);
    public static TerminalColor Black => new("Black", 0f, 0f, 0f, 1f);
    public static TerminalColor Red => new("Red", 1f, 0f, 0f, 1f);
    public static TerminalColor Yellow => new("Yellow", 1f, 1f, 0f, 1f);
    public static TerminalColor Green => new("Green", 0f, 1f, 0f, 1f);
    public static TerminalColor Cyan => new("Cyan", 0f, 1f, 1f, 1f);
    public static TerminalColor Blue => new("Blue", 0f, 0f, 1f, 1f);
    public static TerminalColor Magenta => new("Magenta", 1f, 0f, 1f, 1f);
}
