using OpenGL;
using SharpEngine.Events;
using SharpEngine.Platforms.Windows;
using System;

namespace SharpEngine
{
    public class Application
    {
        private static Application _instance;
        private readonly Window _window;
        private readonly LayerStack _layerStack;

        public Application()
        {
            _instance = this;
            _layerStack = new LayerStack();
            _window = SDLWIndow.Create(new WindowProperties());
            
            _window.SetEventCallBack(OnEvent);
        }

        public static Application Get()
        {
            return _instance;
        }
        public Window Window { get { return _window; } }

        private bool _isMinimized = false;
        private bool _isRunnig = true;
        public void Run()
        {

            while (_isRunnig)
            {
                
                foreach (var layer in _layerStack)
                {
                    layer.OnUpdate();
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

            EntryPoint.CoreLogger.Trace(e);

            for(var i = _layerStack.End(); i != _layerStack.Begin(); i--)
            {
                _layerStack[i].OnEvent(e);
                if (e.Handled)
                    break;
            }
        }

        private bool OnWindowResize(WindowResizeEvent e)
        {
            if(e.Width == 0 || e.Height == 0)
            {
                _isMinimized = true;
                return false;
            }

            _isMinimized = false;

            return false;
        }

        private bool OnWindowClosed(WindowCloseEvent e)
        {
            _isRunnig = false;
            return true;
        }
    }
}
