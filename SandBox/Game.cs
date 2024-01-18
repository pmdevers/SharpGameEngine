using SharpEngine.Core;
using SharpEngine.Platform.OpenGL;
using SharpEngine.Platform.Windows;

namespace SandBox
{
    public class Game : Application<OpenTKWindow>
    {
        public Game() : base(new WindowProperties() {  Title ="Hello World!" })
        {
            
            PushLayer(new ExampleLayer());
            //PushOverlay(new DebugLayer());
        }       
    }
}
