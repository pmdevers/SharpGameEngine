using SharpEngine.Events;
using System.Diagnostics;

namespace SharpEngine.Core;

public interface IApplication
{
    IWindow Window { get; }
    ResourceManager Resources { get; }


    void Run();
}

public class Application
{
    public static IApplication Instance { get; set; }

    public static IWindow Window => Instance.Window;
    public static ResourceManager ResourceManager => Instance.Resources;
}
public class Application<TWindow> : IApplication
    where TWindow : IWindow
{
    private readonly IWindow _window;
    private readonly LayerStack _layerStack;
    

    public Application(WindowProperties windowProperties)
    {
        Application.Instance = this;

        Resources = new ResourceManager(GetType().Assembly);

        _layerStack = new LayerStack();
        _window = TWindow.Create(windowProperties);

        _window.SetEventCallBack(OnEvent);
    }

    public IWindow Window { get { return _window; } }
    public ResourceManager Resources { get; }

    private bool _isMinimized = false;
    private bool _isRunnig = true;
    public void Run()
    {
        var time = Stopwatch.GetTimestamp();
        while (_isRunnig)
        {
            time = Stopwatch.GetElapsedTime(time).Ticks;

            if (!_isMinimized)
            {

                foreach (var layer in _layerStack)
                {
                    layer.OnUpdate(time);
                }
            }

            _window.OnUpdate();
        }
    }

    public void PushLayer(Layer layer)
    {
        _layerStack.PushLayer(layer);
        layer.OnAttach();
    }

    public void PushOverlay(Layer overlay)
    {
        _layerStack.PushOverlay(overlay);
        overlay.OnAttach();
    }
    public void OnEvent(Event e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowClosed);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResize);
        dispatcher.Dispatch<WindowMinimizedEvent>(OnWindowMinimized);

        EntryPoint.CoreLogger.Trace(e);

        for (var i = _layerStack.End(); i != _layerStack.Begin(); i--)
        {
            _layerStack[i].OnEvent(e);
            if (e.Handled)
                break;
        }
    }

    private bool OnWindowMinimized(WindowMinimizedEvent @event)
    {
        return false;
    }

    private bool OnWindowResize(WindowResizeEvent e)
    {
        if (e.Width == 0 || e.Height == 0)
        {
            _isMinimized = true;
            return false;
        }

        _isMinimized = false;

        Renderer.Renderer.SetViewPort(0, 0, e.Width, e.Height);

        return false;
    }

    private bool OnWindowClosed(WindowCloseEvent e)
    {
        _isRunnig = false;
        return true;
    }
}
