using SharpEngine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine
{
    public abstract class Layer
    {
        protected Layer(string name = "Layer")
        {
            Name = name;
        }

        public string Name { get; }

        public virtual void OnAttach() { }
        public virtual void OnDetach() { }
        public virtual void OnUpdate() {  }

        public virtual void OnEvent(Event @event) { }
    }
}
