using SharpEngine;
using SharpEngine.Events;
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
            EntryPoint.ClientLogger.Trace(@event);
        }

        public override void OnUpdate()
        {
            //EntryPoint.ClientLogger.Info("ExampleLayer OnUpdate");
        }
    }
}
