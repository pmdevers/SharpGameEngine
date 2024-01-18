using SharpEngine.Core;
using SharpEngine.Events;
using System;
using System.Numerics;

namespace SharpEngine.Renderer;

public class CameraController(float aspectRatio, bool rotation = false)
{
    private Vector3 CameraPosition = new(0.0f);
    private float TranslationSpeed = 1.0f;
    public ICamera Camera { get; } = new DefaultCamera();
    public float ZoomLevel { get; set; }

    public void OnUpdate(long ticks)
    {
        if(Input.IsKeyPressed(KeyCode.A))
        {
            CameraPosition.X -= MathF.Cos(Camera.Rotation * TranslationSpeed * ticks);
            CameraPosition.Y -= MathF.Sin(Camera.Rotation * TranslationSpeed * ticks);
        }
    }

    public void OnEvent(Event e)
    {

    }

    public void OnResize(float width, float height)
    {

    }

}
