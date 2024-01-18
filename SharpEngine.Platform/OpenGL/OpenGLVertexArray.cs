using OpenTK.Graphics.OpenGL4;
using SharpEngine.Renderer;

namespace SharpEngine.Platform.OpenGL;

public class OpenGLVertexArray : IVertexArray
{
    private int _id;
    private IIndexBuffer? _indexBuffer;
    private int _vertexBufferIndex = 0;
    //private List<IVertexBuffer> _vertexBuffers = [];

    private OpenGLVertexArray()
    {
        _id = GL.GenVertexArray();
        GL.BindVertexArray(_id);
    }

    ~OpenGLVertexArray()
    {
        GL.DeleteVertexArrays(1, [_id]);
    }

    public unsafe void AddVertexBuffer(IVertexBuffer vertexBuffer)
    {
        GL.BindVertexArray(_id);
        vertexBuffer.Bind();

        //var Gl = OpenGLRendererApi.Gl;

        //Gl.EnableVertexAttribArray(0);
        //Gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, null);

        foreach (var element in vertexBuffer.BufferLayout)
        {
            switch(element.Type)
            {
                case ShaderDataType.Float:
                case ShaderDataType.Float2:
                case ShaderDataType.Float3:
                case ShaderDataType.Float4:
                    GL.EnableVertexAttribArray(_vertexBufferIndex);
                    GL.VertexAttribPointer(
                        _vertexBufferIndex, 
                        element.GetComponentCount(), 
                        GetEnumType(element.Type),
                        element.Normalized,
                        vertexBuffer.BufferLayout.Stride,
                        0);
                    _vertexBufferIndex++;
                    break;

                case ShaderDataType.Int:
                case ShaderDataType.Int2:
                case ShaderDataType.Int3:
                case ShaderDataType.Int4:
                case ShaderDataType.Bool:
                    GL.EnableVertexAttribArray(_vertexBufferIndex);
                    GL.VertexAttribIPointer(
                        _vertexBufferIndex,
                        element.GetComponentCount(),
                        GetEnumIntType(element.Type),
                        vertexBuffer.BufferLayout.Stride,
                        0);
                    _vertexBufferIndex++;
                    break;

                case ShaderDataType.Matrix3:
                case ShaderDataType.Matrix4:
                    var count = element.GetComponentCount();
                    for (int i = 0; i < count; i++)
                    {
                        GL.EnableVertexAttribArray(_vertexBufferIndex);
                        GL.VertexAttribPointer(_vertexBufferIndex,
                            count,
                            GetEnumType(element.Type),
                            element.Normalized,
                            vertexBuffer.BufferLayout.Stride,
                            0);
                        GL.VertexAttribDivisor(_vertexBufferIndex, 1);
                        _vertexBufferIndex++;
                    }
                    break;


                default: throw new NotSupportedException();
            }
        }
    }

    private VertexAttribPointerType GetEnumType(ShaderDataType type)
    {
        return type switch
        {
            ShaderDataType.Float => VertexAttribPointerType.Float,
            ShaderDataType.Float2 => VertexAttribPointerType.Float,
            ShaderDataType.Float3 => VertexAttribPointerType.Float,
            ShaderDataType.Float4 => VertexAttribPointerType.Float,
            ShaderDataType.Matrix3 => VertexAttribPointerType.Float,
            ShaderDataType.Matrix4 => VertexAttribPointerType.Float,
            _ => 0,
        };
    }

    private VertexAttribIntegerType GetEnumIntType(ShaderDataType type)
    {
        return type switch
        {
            ShaderDataType.Int => VertexAttribIntegerType.Int,
            ShaderDataType.Int2 => VertexAttribIntegerType.Int,
            ShaderDataType.Int3 => VertexAttribIntegerType.Int,
            ShaderDataType.Int4 => VertexAttribIntegerType.Int,
            ShaderDataType.Bool => VertexAttribIntegerType.Byte,
            _ => 0,
        };
    }

    public IIndexBuffer? IndexBuffer 
    { 
        get => _indexBuffer; 
        set {
            //Bind();
            _indexBuffer = value;
        }
    }

    public static IVertexArray Create()
    {
        return new OpenGLVertexArray();
    }

    public void Bind()
    {
        GL.BindVertexArray(_id);
    }

    public void UnBind()
    {
        GL.BindVertexArray(0);
    }
}
