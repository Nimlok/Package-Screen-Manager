using System;

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