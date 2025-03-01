﻿using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;
using System.Linq;

namespace UnityEventReferenceViewer
{
    public class EventReferenceInfo
    {
        public MonoBehaviour Owner { get; set; }
        public List<MonoBehaviour> Listeners { get; set; } = new List<MonoBehaviour>();
        public List<string> MethodNames { get; set; } = new List<string>();
    }

    public class UnityEventReferenceFinder : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        [ContextMenu("FindReferences")]
        public void FindReferences()
        {
            FindAllUnityEventsReferences();
        }

        public static List<EventReferenceInfo> FindAllUnityEventsReferences()
        {
            var behaviours = Resources.FindObjectsOfTypeAll<MonoBehaviour>();
            var events = new Dictionary<MonoBehaviour, List<UnityEventBase>>();

            foreach (var b in behaviours)
            {
                var info = b.GetType().GetTypeInfo();
                var evnts = info.DeclaredFields.Where(f => f.FieldType.IsSubclassOf(typeof(UnityEventBase))).ToList();
                foreach (var e in evnts)
                {
                    var unityEvent = e.GetValue(b) as UnityEventBase;

                    if (!events.ContainsKey(b))
                    {
                        events[b] = new List<UnityEventBase>();
                    }

                    events[b].Add(unityEvent);
                }
            }

            var infos = new List<EventReferenceInfo>();

            foreach (var e in events)
            {
                foreach (var unityEvent in e.Value)
                {
                    int count = unityEvent.GetPersistentEventCount();
                    var info = new EventReferenceInfo();
                    info.Owner = e.Key;

                    for (int i = 0; i < count; i++)
                    {
                        var obj = unityEvent.GetPersistentTarget(i);
                        var method = unityEvent.GetPersistentMethodName(i);

                        info.Listeners.Add(obj as MonoBehaviour);
                        info.MethodNames.Add(obj.GetType().Name.ToString() + "." + method);
                    }

                    infos.Add(info);
                }
            }

            return infos;
        }
    }
}