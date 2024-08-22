using System;
using UnityEngine.Events;

namespace Nimlok.Screens
{
    [Serializable]
    public class TransitionEvents
    {
        public UnityEvent onTransitionInStarted;
        public UnityEvent onTransitionInComplete;
        
        public UnityEvent onTransitionOutStarted;
        public UnityEvent onTransitionOutComplete;
    }
}