namespace SharpEngine.Core;

public record struct Point(float X, float Y);

public interface IInput
{
    bool IsKeyPressed(int keyCode); 
    bool IsMouseButtonPressed(int button);
    Point GetMousePosition();
    bool GetMouseX();
    bool GetMouseY();
}

public static class Input
{
    public static IInput Instance;

    public static bool IsKeyPressed(int keyCode)
    {
        return Instance.IsKeyPressed(keyCode);
    }

    public static bool IsMouseButtonPressed(int button)
    {
        return Instance.IsMouseButtonPressed(button);
    }

    public static Point GetMousePosition()
    {
        return Instance.GetMousePosition();
    }
    public static bool GetMouseX()
    {
        return Instance.GetMouseX();
    }
    public static bool GetMouseY()
    {
        return Instance.GetMouseY();
    }
}


public static class KeyCode
{
    public static int A = 97;
    public static int B = 98;
    public static int C = 99;
}