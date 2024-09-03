using System;
using Sirenix.OdinInspector;

namespace Nimlok.Screens
{
    [Serializable, BoxGroup("Loop Properties")]
    public class LoopScreenProperties
    {
        public float loopTime = 5;
        public TransitionableScreen nextScreen;
    }
}