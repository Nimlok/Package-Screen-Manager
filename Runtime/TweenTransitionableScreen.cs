using System;
using DG.Tweening.Core;
using Tweens;
using UnityEngine;

namespace Screens
{
    public class TweenTransitionableScreen: TransitionableScreen
    {
        [SerializeField] private TweenSequence tweenSequence;
        
        //TODO: DS 24.4.24 Tidy Up
        protected override void PlayTransitionIn(Action onTransitionComplete)
        {
            transitionEvents.onTransitionInStarted?.Invoke();
            if (tweenSequence == null)
            {
                Debug.LogWarning($"Missing Tween Sequence: {gameObject.name}");
                transitionEvents.onTransitionInComplete?.Invoke();
                onTransitionComplete?.Invoke();
                return;
            }
            
            tweenSequence.PlaySequenceForward(onTransitionComplete);
        }

        protected override void PlayTransitionOut(Action onTransitionComplete)
        {
            transitionEvents.onTransitionOutStarted?.Invoke();
            if (tweenSequence == null)
            {
                Debug.LogWarning($"Missing Tween Sequence: {gameObject.name}");
                onTransitionComplete?.Invoke();
                return;
            }
            
            tweenSequence.PlaySequenceBackward(onTransitionComplete);
        }

        protected override void StopTransition()
        {
            tweenSequence.StopSequence();
        }

        public override bool IsPlaying()
        {
            return tweenSequence.GetIsPlaying;
        }
    }
}