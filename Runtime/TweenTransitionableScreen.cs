using System;
using Nimlok.Tweens;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nimlok.Screens
{
    public class TweenTransitionableScreen: TransitionableScreen
    {
        [BoxGroup("Transition In"), ShowIf("transitionIn"), SerializeField] private TweenSequence tweenTransitionIn;
        
        [BoxGroup("Transition Out")]
        [ShowIf("transitionOut"), SerializeField] private bool separateTransitionOut;
        [BoxGroup("Transition Out"), ShowIf("separateTransitionOut"), SerializeField] private TweenSequence tweenTransitionOut;

        protected override void TransitionOutChanged()
        {
            if (transitionOut)
                return;

            separateTransitionOut = false;
        }

        protected override void PlayTransitionIn(Action onTransitionComplete)
        {
            transitionEvents.onTransitionInStarted?.Invoke();
            if (tweenTransitionIn == null)
            {
                Debug.LogWarning($"Missing Tween Sequence: {gameObject.name}");
                transitionEvents.onTransitionInComplete?.Invoke();
                onTransitionComplete?.Invoke();
                return;
            }
            
            tweenTransitionIn.PlaySequenceForward(onTransitionComplete);
        }

        protected override void PlayTransitionOut(Action onTransitionComplete)
        {
            if (tweenTransitionOut == null)
            {
                TransitionInBackwards(onTransitionComplete);
                return;
            }
            
            tweenTransitionOut.PlaySequenceForward(onTransitionComplete);
        }

        private void TransitionInBackwards(Action onTransitionComplete)
        {
            transitionEvents.onTransitionOutStarted?.Invoke();
            if (tweenTransitionIn == null)
            {
                Debug.LogWarning($"Missing Tween Sequence: {gameObject.name}");
                onTransitionComplete?.Invoke();
                return;
            }
            
            tweenTransitionIn.PlaySequenceBackward(onTransitionComplete);
        }

        protected override void StopTransition()
        {
            tweenTransitionIn.StopSequence();
        }

        public override bool IsPlaying()
        {
            return tweenTransitionIn.GetIsPlaying;
        }
    }
}