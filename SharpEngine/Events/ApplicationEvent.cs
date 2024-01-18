using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Events
{
    public class WindowResizeEvent : Event
    {
        public WindowResizeEvent(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public override string ToString()
        {
            return $"{Name}: {Width}, {Height}";
        }

        public override EventCategory GategoryFlags => EventCategory.Application;
    }

    public class WindowCloseEvent : Event
    {
        public WindowCloseEvent()
        {

        }
    }

    public class WindowMinimizedEvent : Event
    {
        public WindowMinimizedEvent() { }
    }

    public class AppTickEvent : Event
    {
        public override EventCategory GategoryFlags => EventCategory.Application;
    }

    public class AppUpdateEvent : Event
    {
        public override EventCategory GategoryFlags => EventCategory.Application;
    }

    public class AppRenderEvent: Event
    {
        public override EventCategory GategoryFlags => EventCategory.Application;
    }
}
