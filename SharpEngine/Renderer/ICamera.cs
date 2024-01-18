using System.Numerics;

namespace SharpEngine.Renderer;

public interface ICamera
{
    Matrix4x4 ViewProjectionMatrix { get; }
    float Rotation { get; set; }
}

public class DefaultCamera : ICamera
{
    public float Left { get;set;}
    public float Right { get;set; }
    public float Top { get;set;}
    public float Bottom { get;set;}

    public Vector3 Position { get; set; } = Vector3.Zero;
    public float Rotation { get; set; } = 0f;

    public Matrix4x4 ViewProjectionMatrix =>
        Matrix4x4.CreateOrthographicOffCenter(Left, Right, Bottom, Top, -1f, 1f) *
        Matrix4x4.Identity;
}