using System;

namespace SharpEngine.Renderer;

public interface IShader : IDisposable
{
    void SetInt(string name, int value);
    void SetIntArray(string name, int[] values);
    void SetFloat(string name, float value);
    //void SetFloat2(string name, object value);
    //void SetFloat3(string name, object value);
    //void SetFloat4(string name, object value);
    //void SetMatrix4(string name, object value);

    string Name { get; }

    void Bind();
    void Unbind();
}
