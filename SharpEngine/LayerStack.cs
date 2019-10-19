using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine
{
    public class LayerStack : IEnumerable<Layer>
    {
        private int _layerIndex = 0;
        private readonly List<Layer> _layers = new List<Layer>();

        public Layer this[int index]
        {
            get { return _layers[index]; }
        }

        public int Begin()
        {
            return 0;
        }

        public int End()
        {
            return _layers.Count - 1;
        }

        public void PushLayer(Layer layer)
        {
            _layers.Insert(_layerIndex, layer);
            _layerIndex++;
        }

        public void PushOverlay(Layer overlay)
        {
            _layers.Add(overlay);
        }

        public void PopLayer(Layer layer)
        {
            _layers.Remove(layer);
            _layerIndex--;
        }

        public void PopOverlay(Layer overlay)
        {
            _layers.Remove(overlay);
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return _layers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
