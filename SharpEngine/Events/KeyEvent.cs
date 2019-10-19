using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Events
{
    public class KeyEvent : Event
    {

        protected KeyEvent(int keyCode)
        {
            KeyCode = keyCode;
        }
        public int KeyCode { get; }
        public override EventCategory GategoryFlags
        {
            get
            {
                return EventCategory.Keyboard | EventCategory.Input;
            }
        }
    }

    public class KeyPressedEvent : KeyEvent
    {
        public KeyPressedEvent(int keyCode, int repeatCount) : base(keyCode)
        {
            RepeatCount = repeatCount;
        }

        public int RepeatCount { get; }

        public override string ToString()
        {
            return $"{Name}: {KeyCode} ({RepeatCount} repeats)";
        }
    }

    public class KeyReleaseEvent : KeyEvent
    {
        public KeyReleaseEvent(int keyCode) : base(keyCode)
        {
        }

        public override string ToString()
        {

            return $"{Name}: {KeyCode}";
        }

    }
}
