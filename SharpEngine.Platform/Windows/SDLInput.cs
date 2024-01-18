using SharpEngine.Core;
using Point = SharpEngine.Core.Point;

namespace SharpEngine.Platform.Windows
{
    public class SDLInput : IInput
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

        public bool IsMouseButtonPressed(int button)
        {
            return _mouseButtons.Contains(button);
        }

        public bool GetMouseX()
        {
            throw new NotImplementedException();
        }

        public bool GetMouseY()
        {
            throw new NotImplementedException();
        }

        public Point GetMousePosition()
        {
            throw new NotImplementedException();
        }

        public bool IsKeyPressed(int keyCode)
        {
            return _keys.Contains(keyCode);
        }
    }
}
