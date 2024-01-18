using SharpEngine.Core;

namespace SharpEngine.Renderer;

public static class Renderer
{
    public static IRendererApi Instance { get; set; }

    public static void Clear()
    {
        Instance.Clear();
    }

    public static void DrawIndexed(IVertexArray vertexArray)
    {
        Instance.DrawIndexed(vertexArray);
    }

    public static void DrawLines(IVertexArray vertexArray, int indexCount)
    {
        Instance.DrawLines(vertexArray, indexCount);
    }

    public static void SetClearColor(Color color)
    {
        Instance.SetClearColor(color);
    }

    public static void SetLineWidth(float width)
    {
        Instance.SetLineWidth(width);
    }

    public static void SetViewPort(int x, int y, int width, int height)
    {
        Instance.SetViewPort(x, y, width, height);
    }
}

public interface IRendererApi
{
    void SetViewPort(int x, int y, int width, int height);
    
    void SetClearColor(Color color);

    void Clear();

    void DrawIndexed(IVertexArray vertexArray);
    void DrawLines(IVertexArray vertexArray, int indexCount);
    void SetLineWidth(float width);

    IVertexArray CreateVertexArray();
    IVertexBuffer CreateVertexBuffer(float[] data);
    IIndexBuffer CreateIndexBuffer(uint[] indices);
    IShader CreateShader(string name, string vertexSrc, string fragmentSrc);
    IShader CreateShader(string name);
}

public class VertexArray { }