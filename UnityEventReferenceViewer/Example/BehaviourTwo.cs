using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEventReferenceViewer.Example
{
    [System.Serializable]
    public class GenericEvent : UnityEvent<int> { }

    public class BehaviourTwo : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private GenericEvent onExampleGenericEvent;

        #endregion

        #region Properties

        #endregion

        public void Spam()
        {

        }

        public void Eggs()
        {

        }

        public void IntEggs(float input)
        {
        }
    }
}
