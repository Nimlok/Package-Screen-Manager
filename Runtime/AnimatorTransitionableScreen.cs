using System;
using System.Collections;
using UnityEngine;

namespace Nimlok.Screens
{
    public class AnimatorTransitionableScreen: TransitionableScreen
    {
        [Space]
        [SerializeField] private string TransitionInTrigger;
        [SerializeField] private string TransitionOutTrigger;
        [Space]
        [SerializeField] private string TransitionInClipName;
        [SerializeField] private string TransitionOutClipName;
        [Space]
        [SerializeField] private Animator animator;

        private string currentClipName;
        
        protected override void PlayTransitionIn(Action onTransitionComplete)
        {
            transitionEvents.onTransitionInStarted?.Invoke();
            animator.SetTrigger(TransitionInTrigger);
            currentClipName = TransitionInClipName;
            StartCoroutine(WaitForClipToFinish(onTransitionComplete));
        }

        protected override void PlayTransitionOut(Action onTransitionComplete)
        {
            transitionEvents.onTransitionOutStarted?.Invoke();
            animator.SetTrigger(TransitionOutTrigger);
            currentClipName = TransitionOutClipName;
            StartCoroutine(WaitForClipToFinish(onTransitionComplete));
        }

        protected override void StopTransition()
        {
            
        }

        private IEnumerator WaitForClipToFinish(Action animationComplete)
        {
            yield return new WaitUntil(IsPlaying);
            animationComplete?.Invoke();
        }
        
        public override bool IsPlaying()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(currentClipName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
        }
    }
}