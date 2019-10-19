using SharpEngine.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SharpEngine
{
    public abstract class Window
    {
        public string Title { get; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public bool FullScreen { get; protected set; }
        public int MainThreadID { get; private set; }

        public abstract IntPtr Handle { get; }
        public abstract bool IsVSync { get; set; }
        public Window(string title = "SharpEngine", int width = 1200, int height = 720)
        {
            Title = title;
            Width = width;
            Height = height;
            FullScreen = false;
            MainThreadID = Thread.CurrentThread.ManagedThreadId;

        }
        public abstract void OnUpdate();
        public abstract void SetEventCallBack(Action<Event> eventCallBack);
    }

    public class WindowProperties
    {
        public string Title { get; set; } = "SharpEngine";
        public int Width { get; set; } = 1200;
        public int Height { get; set; } = 780;
    }

}
