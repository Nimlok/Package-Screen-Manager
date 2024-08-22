using System;
using UnityEngine;

namespace Nimlok.Screens
{
    public abstract class TransitionableScreen: MonoBehaviour
    {
        //TODO: DS 24.04.24 Remove ID if no longer required 
        [SerializeField] private string id;
        [SerializeField] private bool transitionIn = true;
        [SerializeField] private bool transitionOut = true;
        [SerializeField] private LoopScreenProperties loopingScreen;
        
        [Space]
        [SerializeField] protected TransitionEvents transitionEvents;
        
        public string GetID => id;
        public LoopScreenProperties GetLoopProperties => loopingScreen;

        public void LoadScreen(Action OnLoadComplete)
        {
            ScreenInactiveManager.RestartIdle?.Invoke();
            
            //TODO: DS 19/04/24 Check to see if transition is playing
            if (!transitionIn)
            {
                OnLoadComplete?.Invoke();
                transitionEvents.onTransitionInComplete?.Invoke();
                return;
            } 
            
            PlayTransitionIn(() =>
            {
                transitionEvents.onTransitionInComplete?.Invoke();
                OnLoadComplete?.Invoke();
            });
        }

        public void UnloadScreen(Action onUnloadComplete)
        {
            //TODO: DS 19/04/24 Check to see if transition is playing
            if (!transitionOut)
            {
                onUnloadComplete?.Invoke();
                transitionEvents.onTransitionOutComplete?.Invoke();
                return;
            }
            
            PlayTransitionOut(() =>
            {
                transitionEvents.onTransitionOutComplete?.Invoke();
                onUnloadComplete?.Invoke();
            });
        }
        
        protected abstract void PlayTransitionIn(Action onTransitionComplete);

        protected abstract void PlayTransitionOut(Action onTransitionComplete);
        
        protected abstract void StopTransition();

        public abstract bool IsPlaying();
    }
}