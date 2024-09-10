using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nimlok.Screens
{
    public abstract class TransitionableScreen: MonoBehaviour
    {
        //TODO: DS 24.04.24 Remove ID if no longer required 
        [SerializeField, PropertyOrder(-10)] private int id;
        
        [BoxGroup("Loop Properties"), SerializeField] private bool loopingScreen;
        [SerializeField, ShowIf("loopingScreen")] private LoopScreenProperties loopingScreenProperties;
        
        [BoxGroup("Transition In"), SerializeField]
        private bool transitionIn = true;
        
        [BoxGroup("Transition Out"), OnValueChanged("TransitionOutChanged"), SerializeField] 
        protected bool transitionOut = true;
        
        [Space, PropertyOrder(1)]
        [SerializeField] protected TransitionEvents transitionEvents;
        
        public int GetID => id;
        public LoopScreenProperties GetLoopProperties => loopingScreenProperties;
        public bool LoopingScreen => loopingScreen;

        protected virtual void TransitionOutChanged()
        {
            
        }
        
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