using System;

namespace Screens
{
    public class AnimatorTransitionableScreen: TransitionableScreen
    {
        protected override void PlayTransitionIn(Action onTransitionComplete)
        {
            throw new NotImplementedException();
        }

        protected override void PlayTransitionOut(Action onTransitionComplete)
        {
            throw new NotImplementedException();
        }

        protected override void StopTransition()
        {
            throw new NotImplementedException();
        }

        public override bool IsPlaying()
        {
            throw new NotImplementedException();
        }
    }
}