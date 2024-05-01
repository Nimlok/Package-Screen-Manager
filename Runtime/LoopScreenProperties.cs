using System;
using UnityEngine;

namespace Screens
{
    [Serializable]
    public class LoopScreenProperties
    {
        public bool loopingScreen;
        public float loopTime = 5;
        public TransitionableScreen nextScreen;
    }
}