using SharpEngine.Core;
using SharpEngine.Events;
using SharpEngine.GUI;
using SharpEngine.GUI.Controls;

namespace SharpEngine.Debug
{
    public class DebugLayer : Layer
    {
        private readonly int _vertexBufferObject;

        public DebugLayer() : base("DebugLayer")
        {

        }

        public override void OnAttach()
        {
            UI.InitUI(Application.Instance.Window.Width, Application.Instance.Window.Height);

            Gl.Enable(EnableCap.DepthTest);
            Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


            UI.Visible = true;
            var helloWorld = new Text(Text.FontSize._24pt, "Hello World", BMFont.Justification.Center);
            helloWorld.Name = "txt1";
            helloWorld.RelativeTo = Corner.Center;

            Text coloredText = new Text(Text.FontSize._24pt, "using C#", BMFont.Justification.Center);
            coloredText.Position = new Point(0, -30);
            coloredText.Color = new Vector3(0.2f, 0.3f, 1f);
            coloredText.RelativeTo = Corner.Center;

            Button button = new Button(120, 40);
            button.Position = new Point(0, -80);
            button.RelativeTo = Corner.Center;
            button.Text = "OK";
            button.BackgroundColor = new Vector4(0.2f, 0.3f, 1f, 1f);


            UI.AddElement(helloWorld);
            UI.AddElement(coloredText);
            UI.AddElement(button);
            

        }

        public override void OnDetach()
        {
            UI.Dispose();
        }

        public override void OnUpdate(long ticks)
        {
            // set up the OpenGL viewport and clear both the color and depth bits
            Gl.Viewport(0, 0, Application.Get().Window.Width, Application.Get().Window.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var element = (Text)UI.GetElement("txt1");

            if(Input.IsKeyPressed(97))
                element.Color = new Vector3(1f, 0.3f, 1f);
            else
                element.Color = new Vector3(0.2f, 0.3f, 1f);

            UI.Draw();

        }

        public override void OnEvent(Event @event)
        {
            new EventDispatcher(@event).Dispatch<WindowResizeEvent>(x => { UI.OnResize(x.Width, x.Height); return false; });
            new EventDispatcher(@event).Dispatch<MouseMovedEvent>(x => UI.OnMouseMove(x));
            new EventDispatcher(@event).Dispatch<MouseButtonPressedEvent>(x => UI.OnMouseClick(x));
            new EventDispatcher(@event).Dispatch<MouseButtonReleasedEvent>(x => UI.OnMouseClick(x));
        }

    }
}
