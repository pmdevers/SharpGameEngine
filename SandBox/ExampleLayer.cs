using OpenGL;
using SharpEngine;
using SharpEngine.Events;
using SharpEngine.GUI;
using SharpEngine.GUI.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox
{
    public class ExampleLayer : Layer
    {
        public ExampleLayer() : base("ExampleLayer")
        {

        }
        public override void OnEvent(Event @event)
        {
            // EntryPoint.CoreLogger.Trace(@event);

            
        }

        public override void OnUpdate()
        {

            if (Input.IsKeyPressed(97))
            {
                EntryPoint.ClientLogger.Info("Key pressed");
            }

            //EntryPoint.ClientLogger.Info("ExampleLayer OnUpdate");
        }
    }
}
