using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SharpEngine.Core;
using SharpEngine.Renderer;
using System.Runtime.InteropServices;

namespace SharpEngine.Platform.OpenGL;

public unsafe class OpenGLRendererApi : IRendererApi
{

    public OpenGLRendererApi()
    {
        var version = GL.GetString(StringName.Version);
        EntryPoint.CoreLogger.Debug("OpenGL Version: " + version);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.LineSmooth);
        
        GL.DebugMessageCallback((source, type, id, severity, length, message, userParam) =>
        {
            EntryPoint.CoreLogger.Debug((DebugSource)source, (DebugType)type, id, (DebugSeverity)severity,
                Marshal.PtrToStringAnsi(message, length));
        }, 0);
    }

    public void Clear()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    public IIndexBuffer CreateIndexBuffer(uint[] indices)
    {
        return new OpenGLIndexBuffer(indices);
    }

    public IShader CreateShader(string name, string vertexSrc, string fragmentSrc)
    {
        return new OpenGLShader(name, vertexSrc, fragmentSrc);
    }

    public IShader CreateShader(string name)
    {
        var src = Application.ResourceManager.GetShaderSource(name);

        return new OpenGLShader(name, src.VertexShaderSource, src.FragmentShaderSource);
    }

    public IVertexArray CreateVertexArray()
    {
        return OpenGLVertexArray.Create();
    }

    public IVertexBuffer CreateVertexBuffer(float[] data)
    {
        return new OpenGLVertexBuffer<float>(data);
    }

    public void DrawIndexed(IVertexArray vertexArray)
    {
        var count = vertexArray.IndexBuffer.Count;
        GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, nint.Zero);
    }

    public void DrawLines(IVertexArray vertexArray, int vertexCount)
    {
        vertexArray.Bind();
        GL.DrawArrays(PrimitiveType.Lines, 0, vertexCount);
    }

    public void SetClearColor(Color color)
    {
        GL.ClearColor(color.Red / 255f, color.Green / 255f, color.Blue / 255f, color.Alpha / 100f);
    }

    public void SetLineWidth(float width)
    {
        GL.LineWidth(width);
    }

    public void SetViewPort(int x, int y, int width, int height)
    {
        GL.Viewport(x, y, width, height);
    }
}
