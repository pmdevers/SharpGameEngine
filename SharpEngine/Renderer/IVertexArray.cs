using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;

namespace SharpEngine.Renderer;

public interface IVertexArray
{
    void Bind();
    void UnBind();

    IIndexBuffer IndexBuffer { get; set; }
    void AddVertexBuffer(IVertexBuffer vertexBuffer);

    public static abstract IVertexArray Create();
}


public enum ShaderDataType
{
    Float = 0,
    Float2,
    Float3,
    Float4,
    Matrix3,
    Matrix4,
    Int,
    Int2,
    Int3,
    Int4,
    Bool,
};

public struct BufferElement(ShaderDataType type, string name, bool normalize = false)
{
    public string Name = name;
    public ShaderDataType Type = type;
    public int GetSize() {
        return Type switch
        {
            ShaderDataType.Float => 4,
            ShaderDataType.Float2 => 4 * 2,
            ShaderDataType.Float3 => 4 * 3,
            ShaderDataType.Float4 => 4 * 4,
            ShaderDataType.Matrix3 => 4 * 3 * 3,
            ShaderDataType.Matrix4 => 4 * 4 * 4,
            ShaderDataType.Int => 4,
            ShaderDataType.Int2 => 4 * 2,
            ShaderDataType.Int3 => 4 * 3,
            ShaderDataType.Int4 => 4 * 4,
            ShaderDataType.Bool => 1,
            _ => 0,
        };
    }
    public int Offset = 0;
    public bool Normalized = normalize;
    public int GetComponentCount() {
        return Type switch
        {
            ShaderDataType.Float => 1,
            ShaderDataType.Float2 => 2,
            ShaderDataType.Float3 => 3,
            ShaderDataType.Float4 => 4,
            ShaderDataType.Matrix3 => 3,
            ShaderDataType.Matrix4 => 4,
            ShaderDataType.Int => 1,
            ShaderDataType.Int2 => 2,
            ShaderDataType.Int3 => 3,
            ShaderDataType.Int4 => 4,
            ShaderDataType.Bool => 1,
            _ => 0,
        };
    }
    
}

public class BufferLayout : IEnumerable<BufferElement>
{
    private readonly List<BufferElement> _elements;

    public BufferLayout(List<BufferElement> elements)
    {
        _elements = elements;
        CalculateOffsetsAndStride();
    }

    public int Stride { get; private set; }

    public IEnumerator<BufferElement> GetEnumerator()
        => _elements.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => _elements.GetEnumerator();

    private void CalculateOffsetsAndStride()
    {
        int offset = 0;
        Stride = 0;
        _elements.ForEach(x =>
        {
            x.Offset = offset;
            offset += x.GetSize();
            Stride += x.GetSize();
        });
    }
}

public interface IVertexBuffer : IDisposable
{
    BufferLayout BufferLayout { get; set; }
    void Bind();
    void UnBind();
}

public interface IVertexBuffer<T> : IVertexBuffer
    where T : struct
{
    void SetData(T[] data, uint size);
}

public interface IIndexBuffer : IDisposable
{
    int Count { get; }
    void Bind();
    void UnBind();
}
