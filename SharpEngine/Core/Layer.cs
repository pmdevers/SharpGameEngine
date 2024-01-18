using SharpEngine.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Core
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
        public virtual void OnUpdate(long ticks) { }

        public virtual void OnEvent(Event @event) { }
    }
}
