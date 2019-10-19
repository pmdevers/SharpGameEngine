using OpenGL;
using SharpEngine.Events;
using SharpEngine.GUI;
using SharpEngine.GUI.Controls;

namespace SharpEngine.Debug
{
    public class ImGuiLayer : Layer
    {
        int _vertexBufferObject;

        public ImGuiLayer() : base("ImGuiLayer")
        {

        }

        public override void OnAttach()
        {
            UI.InitUI(Application.Get().Window.Width, Application.Get().Window.Height);

            Gl.Enable(EnableCap.DepthTest);
            Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


            UI.Visible = true;
            var helloWorld = new Text(Text.FontSize._24pt, "Hello WOrld", BMFont.Justification.Center);
            helloWorld.RelativeTo = Corner.Center;

            UI.AddElement(helloWorld);

        }

        public override void OnDetach()
        {
            

        }

        public override void OnUpdate()
        {
            // set up the OpenGL viewport and clear both the color and depth bits
            Gl.Viewport(0, 0, Application.Get().Window.Width, Application.Get().Window.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            UI.Draw();

        }

        public override void OnEvent(Event @event)
        {

        }
    }
}
