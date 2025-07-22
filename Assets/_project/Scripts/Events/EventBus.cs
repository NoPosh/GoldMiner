using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace MyGame.EventBus
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> listeners = new();

        public static void Subscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (!listeners.ContainsKey(type))
            {
                listeners[type] = new List<Delegate>();
            }

            listeners[type].Add(callback);
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (listeners.TryGetValue(type, out var list))
            {
                list.Remove(callback);
            }
        }

        public static void Raise<T>(T eventData)
        {
            var type = typeof(T);
            if (listeners.TryGetValue(type,out var list))
            {
                foreach (var listener in list.OfType<Action<T>>())
                    listener.Invoke(eventData);
            }
        }
    }
}
