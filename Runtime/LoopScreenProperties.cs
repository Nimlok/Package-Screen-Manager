using System;

namespace Nimlok.Screens
{
    [Serializable]
    public class LoopScreenProperties
    {
        public bool loopingScreen;
        public float loopTime = 5;
        public TransitionableScreen nextScreen;
    }
}