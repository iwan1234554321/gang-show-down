using System.Collections.Generic;
using UnityEngine;

namespace Notteam.GangShowDown.Logic
{
    public class TriggerSystem : MonoBehaviour
    {
        private readonly List<TriggerPoint> _points = new();

        public List<TriggerPoint> Points => _points;
        
        public void AddTriggerPoint(TriggerPoint point)
        {
            if (!_points.Contains(point))
                _points.Add(point);
        }
        
        public void RemoveTriggerPoint(TriggerPoint point)
        {
            if (_points.Contains(point))
                _points.Remove(point);
        }
    }
}
