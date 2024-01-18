using OpenTK.Graphics.OpenGL4;
using SharpEngine.Platform.OpenGL;
using SharpEngine.Renderer;

namespace SharpEngine.Platform.OpenGL;

internal class OpenGLIndexBuffer : IIndexBuffer
{
    private int _id = 0;
    public int Count { get; }

    public OpenGLIndexBuffer(uint[] indeces)
    {
        _id = GL.GenBuffer();
        Count = indeces.Length;
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _id);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indeces.Length, indeces, BufferUsageHint.StaticDraw);
    }

    ~OpenGLIndexBuffer() 
    { 
        Dispose();    
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _id);
    }

    public void Dispose()
    {
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_id != 0)
        {
            GL.DeleteBuffers(1, [ _id ]);
            _id = 0;
        }
    }

    public void UnBind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }
}
