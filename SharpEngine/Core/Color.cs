namespace SharpEngine.Core;

public class Color(int red, int green, int blue, int alpha = 100)
{
    public int Red { get;set; } = red;
    public int Green { get;set; } = green;
    public int Blue { get;set; } = blue;
    public int Alpha { get;set; } = alpha;
}
