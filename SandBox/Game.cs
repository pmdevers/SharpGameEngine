using SharpEngine;
using SharpEngine.Debug;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox
{
    public class Game : Application
    {
        public Game()
        {
            this.PushLayer(new ExampleLayer());
            this.PushOverlay(new ImGuiLayer());
        }
    }
}
