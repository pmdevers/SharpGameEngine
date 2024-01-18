using OpenTK.Windowing.Desktop;
using SharpEngine.Core;
using SharpEngine.Events;
using SharpEngine.Platform.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenTK.Compute.OpenCL.CL;

namespace SharpEngine.Platform.Windows
{
    public class OpenTKWindow : GameWindow, IWindow
    {
        private Action<Event> _eventCallBack;
        public OpenTKWindow(NativeWindowSettings nativeWindowSettings) : base(new(), nativeWindowSettings)
        {
            Renderer.Renderer.Instance = new OpenGLRendererApi();
            Input.Instance = new SDLInput();
        }

        public int Width => this.ClientSize.X;

        public int Height => this.ClientSize.Y;

        public bool FullScreen => this.IsFullscreen;

        public int MainThreadID => this.Handle.ToInt32();

        public nint Handle { get; }

        public bool IsVSync { get;set; }

        public static IWindow Create(WindowProperties properties)
        {
            var set = new NativeWindowSettings()
            {
                ClientSize = new OpenTK.Mathematics.Vector2i(properties.Width, properties.Height),
            };

            return new OpenTKWindow(set);
        }

        public void OnUpdate()
        {
            
        }

        public void SetEventCallBack(Action<Event> eventCallBack)
        {
            _eventCallBack = eventCallBack;
        }
    }
}
