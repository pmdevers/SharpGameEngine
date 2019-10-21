using OpenGL;
using SDL2;
using SharpEngine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Platforms.Windows
{
    public class SDLInput : Input
    {
        private List<int> _keys = new List<int>();
        private List<int> _mouseButtons = new List<int>();

        internal void SetKeys(List<int> keys)
        {
            _keys = keys;
        }

        internal void MouseClick(int button)
        {
            _mouseButtons.Add(button);
        }

        internal void MouseRelease(int button)
        {
            _mouseButtons.Remove(button);
        }

        protected override bool IsMousebButtonPressedImpl(int button)
        {
            return _mouseButtons.Contains(button);
        }

        protected override bool GetMouseXImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool GetMouseYImpl()
        {
            throw new NotImplementedException();
        }

        protected override Point GetMousePositionImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsKeyPressedImpl(int keyCode)
        {
            return _keys.Contains(keyCode);
        }
    }
}
