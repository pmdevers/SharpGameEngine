using SharpEngine;
using SharpEngine.Debug;

namespace SandBox
{
    public class Game : Application
    {
        public Game()
        {
            this.PushLayer(new ExampleLayer());
            this.PushOverlay(new DebugLayer());
        }
    }
}
