using OpenTK.Graphics.OpenGL4;
using SharpEngine.Core;
using SharpEngine.Events;
using SharpEngine.Platform.OpenGL;
using SharpEngine.Renderer;

namespace SandBox;

public unsafe class ExampleLayer : Layer
{
    private readonly CameraController _cameraController = new(1280f / 720f);
    private readonly IShader Default;
    float r = 0.1f;
    float increment = 1f;
    private IVertexArray va;
    private IVertexBuffer vbo;

    public ExampleLayer() : base("ExampleLayer")
    {
        //Default = Renderer.Instance.CreateShader("Default");
        va = Renderer.Instance.CreateVertexArray();
        float[] positions = [
            -0.5f, -0.5f,
             0.5f, -0.5f,
             0.5f,  0.5f,
        ];

        uint[] indices = [
            0, 1, 2,
            2, 3, 0,
        ];

        
        




        vbo = Renderer.Instance.CreateVertexBuffer(positions);
        vbo.BufferLayout = new BufferLayout([
            new BufferElement(ShaderDataType.Float2, "indices"),
        ]);

        var ibo = Renderer.Instance.CreateIndexBuffer(indices);
        va.IndexBuffer = ibo;       
    }
    public override void OnEvent(Event @event)
    {
        // EntryPoint.CoreLogger.Trace(@event);

        _cameraController.OnEvent(@event);
    }

    public override void OnUpdate(long ticks)
    {
        _cameraController.OnUpdate(ticks);

        //Renderer.SetClearColor(new Color(100, 149, 237));
        //Renderer.Clear();

        //if (r > 1.0f)
        //{
        //    increment = -0.01f;
        //}
        //else if (r < 0.0f)
        //{
        //    increment = 1f;
        //}

        //r += increment;

        ////Default.SetFloat4("u_Color", new(r, 0.0f, 0.0f, 1.0f));
        ////Default.Bind();

        //va.AddVertexBuffer(vbo);
        //va.Bind();

        //Renderer.SetLineWidth(8f);
        //Renderer.DrawIndexed(va);

        float[] positions = [
           -0.5f,
            -0.5f,
            0.5f,
            -0.5f,
            0.5f,
            0.5f,
        ];

        int vertexBuffer = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * 6, positions, BufferUsageHint.StaticDraw);

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.UnsignedInt, false, 0, 0);

        //Default.SetFloat4("u_Color", new(r, 0.0f, 0.0f, 1.0f));
        //Default.Bind();

        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DisableVertexAttribArray(0);
     }
}
