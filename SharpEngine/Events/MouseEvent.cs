using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Events
{
    public class MouseMovedEvent : Event
    {

        public MouseMovedEvent(float mousex, float mousey)
        {
            MouseX = mousex;
            MouseY = mousey;
        }

        public float MouseX { get; }
        public float MouseY { get; }

        public override EventCategory GategoryFlags => EventCategory.Input | EventCategory.Mouse;

        public override string ToString()
        {
            return $"{Name}: {MouseX}, {MouseY}";
        }
    }

    public class MouseScrollEvent : Event
    {
        public MouseScrollEvent(float xOffset, float yOffset)
        {
            XOffset = xOffset;
            YOffset = yOffset;
        }

        public float XOffset { get; }
        public float YOffset { get; }

        public override string ToString()
        {
            return $"{Name}: {XOffset}, {YOffset}";
        }

        public override EventCategory GategoryFlags => EventCategory.Input | EventCategory.Mouse;
    }

    public class MouseButtonEvent : Event
    {
        protected MouseButtonEvent(int button)
        {
            MouseButton = button;
        }

        public int MouseButton { get; }

        public override EventCategory GategoryFlags => EventCategory.Input | EventCategory.MouseButton;
    }

    public class MouseButtonPressedEvent : MouseButtonEvent
    {
        public MouseButtonPressedEvent(int button) : base(button)
        {
        }

        public override string ToString()
        {
            return $"{Name}: {MouseButton}";
        }

        public override EventCategory GategoryFlags => EventCategory.Input | EventCategory.MouseButton;
    }

    public class MouseButtonReleasedEvent : MouseButtonEvent
    {
        public MouseButtonReleasedEvent(int button) : base(button)
        {
        }

        public override string ToString()
        {
            return $"{Name}: {MouseButton}";
        }

        public override EventCategory GategoryFlags => EventCategory.Input | EventCategory.MouseButton;
    }
}
