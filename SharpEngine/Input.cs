using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine
{
    public abstract class Input
    {
        internal static Input _instance;

        public static bool IsKeyPressed(int keyCode)
        {
            return _instance.IsKeyPressedImpl(keyCode);
        }

        public static bool IsMouseButtonPressed(int button)
        {
            return _instance.IsMousebButtonPressedImpl(button);
        }

        public static Point GetMousePosition()
        {
            return _instance.GetMousePositionImpl();
        }
        public static bool GetMouseX()
        {
            return _instance.GetMouseXImpl();
        }
        public static bool GetMouseY()
        {
            return _instance.GetMouseYImpl();
        }

        
        protected abstract bool IsKeyPressedImpl(int keyCode);
        protected abstract Point GetMousePositionImpl();
        protected abstract bool IsMousebButtonPressedImpl(int button);
        protected abstract bool GetMouseXImpl();
        protected abstract bool GetMouseYImpl();


    }
}
