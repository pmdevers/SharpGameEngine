using OpenTK.Graphics.OpenGL4;
using SharpEngine.Renderer;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SharpEngine.Platform.OpenGL;

public unsafe class OpenGLVertexBuffer<T> : IVertexBuffer<T>
    where T : struct
{
    private int _id;

    public OpenGLVertexBuffer(int size)
    {
        _id = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
        GL.BufferData(BufferTarget.ArrayBuffer, size, 0, BufferUsageHint.DynamicDraw);
    }

    public OpenGLVertexBuffer(float[] data)
    {
        _id = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
        GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
    }

    ~OpenGLVertexBuffer()
    {
        Dispose();
    }

    public BufferLayout BufferLayout { get; set; } = new BufferLayout([]);

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
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

    public void SetData(T[] data, uint size)
    {
        GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

        try
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
            //Gl.BufferSubData(BufferTargetARB.ArrayBuffer, 0, (int)size, handle.AddrOfPinnedObject());
        }
        finally
        {
            handle.Free();
        }
    }

    public void UnBind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
}
