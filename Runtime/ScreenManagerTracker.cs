using System.Collections.Generic;
using UnityEngine;

namespace Screens
{
    [RequireComponent(typeof(ScreenManager))]
    public class ScreenManagerTracker: MonoBehaviour
    {
        [SerializeField] private TransitionableScreen screenToClearHistory;
        
        private ScreenManager screenManager;
        private Stack<TransitionableScreen> screenHistory = new Stack<TransitionableScreen>();
        private TransitionableScreen currentScreen;
        
        private void Awake()
        {
            screenManager = GetComponent<ScreenManager>();
            screenManager.OnScreenTransitionTriggered += AddScreen;
        }

        private void OnDisable()
        {
            screenManager.OnScreenTransitionTriggered -= AddScreen;
        }

        public void Back()
        {
            if (screenHistory.Count <= 0)
                return;

            currentScreen = screenManager.GetCurrentScreen;
            var backScreen = screenHistory.Pop();
            screenManager.TransitionToScreen(backScreen);
        }

        public void ClearHistory()
        {
            screenHistory.Clear();
        }
        
        private void AddScreen(TransitionableScreen nextScreen)
        {
            if (screenToClearHistory != null && nextScreen == screenToClearHistory)
            {
                screenHistory.Clear();
                return;
            }
            
            if (currentScreen == screenManager.GetCurrentScreen)
            {
                currentScreen = null;
                return;
            }
            
            screenHistory.Push(screenManager.GetCurrentScreen);
        }
    }
}