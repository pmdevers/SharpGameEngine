using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Events
{
    public enum EventType
    {
        None = 0,
        WindowClose, WindowResize, WindowFocus, WindowLostFocus, WindowMoved,
        AppTick, AppUpdate, AppRender,
        KeyPressed, KeyReleased,
        MouseButtonPressed, MouseButtonReleased, MouseMoved, MouseScrolled
    }
    [Flags]
    public enum EventCategory
    {
        None        = 0,
        Application = 1,
        Input       = 2,
        Keyboard    = 4,
        Mouse       = 8,
        MouseButton = 16
    }
    public class Event
    {
        internal bool Handled = false;
        public virtual EventType EventType { get; }
        
        public virtual string Name { get { return GetType().Name; } }
        public virtual EventCategory GategoryFlags { get; }

        public override string ToString()
        {
            return Name;
        }

        public bool IsInCategory(EventCategory category)
        {
            return GategoryFlags == category;
        }

    }

    public class EventDispatcher
    {
        private readonly Event _event;

        public EventDispatcher(Event @event)
        {
            _event = @event;
        }

        public bool Dispatch<T>(Func<T, bool> func)
            where T : Event
        {
            if(_event.GetType() == typeof(T))
            {
                _event.Handled = func((T)_event);
                return true;
            }
            return false;
        }
    }
}
