using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Screens
{
    public class ScreenManager: MonoBehaviour
    {
        [SerializeField] private TransitionableScreen initialScreen;
        [SerializeField] private List<TransitionableScreen> screens;
        
        private TransitionableScreen currentScreen;
        public List<TransitionableScreen> GetAllScreens => screens;
        public TransitionableScreen GetCurrentScreen => currentScreen;
        
        private void Awake()
        {
           GetScreensFromChildren();
           SetAllScreenActive(false);
        }

        private void Start()
        {
            if(initialScreen != null)
                LoadScreen(initialScreen);
        }

        //TODO: DS 23/04/24 Remove if not required
        public void TransitionToScreen(string id)
        {
            if (currentScreen != null && string.CompareOrdinal(currentScreen.GetID, id) == 0)
            {
                Debug.LogWarning($"Trying to load current screen {currentScreen.GetID}");
                return;
            }
            
            UnloadScreen(currentScreen, () =>
            {
                LoadScreenById(id);
            });
        }

        public void TransitionToScreen(TransitionableScreen transitionableScreen)
        {
            if (transitionableScreen == null)
            {
                Debug.LogError("Missing screen from event");
                return;
            }
            
            UnloadScreen(currentScreen, () =>
            {
                LoadScreen(transitionableScreen);
            });
        }

        //TODO: DS 23/04/24 Remove if not required
        public void UnloadScreen(string id)
        {
            var screen = FindScreen(id);
            if (screen == null)
                return;
            UnloadScreen(screen);
        }

        //TODO: DS 23/04/24 Remove if not required
        public void LoadScreenById(string id)
        {
            var screen = FindScreen(id);
            if (screen == null)
                return;
            LoadScreen(screen);
        }
        
        private void LoadScreen(TransitionableScreen screen  ,Action OnLoadComplete = null)
        {
            if (screen == null)
            {
                Debug.LogWarning($"Failed to load screen: screen was null");
                return;
            }
            
            screen.gameObject.SetActive(true);
            currentScreen = screen;
            screen.LoadScreen(OnLoadComplete);
        }

        private void UnloadScreen(TransitionableScreen screen, Action OnUnloadComplete = null)
        {
            if (screen == null)
            {
                Debug.LogWarning($"Failed to unload screen: screen was null");
                return;
            }
            
            screen.UnloadScreen(() =>
            {
                screen.gameObject.SetActive(false);
                OnUnloadComplete?.Invoke();
            });
        }
        
        private TransitionableScreen FindScreen(string id)
        {
            if (screens == null || screens.Count == 0)
            {
                Debug.LogWarning($"No Displays added to screen manager");
                return null;
            }
            
            var screen = screens.Find(x => string.CompareOrdinal(x.GetID, id) == 0);
            if (screen == null)
            {
                Debug.LogWarning($"No Screen found for {id}");
            }

            return screen;
        }
        
        private void GetScreensFromChildren()
        {
            if (screens != null && screens.Count != 0)
                return;

            screens = GetComponentsInChildren<TransitionableScreen>(true).ToList();
        }

        //TODO: DS 29.04.24 Possibly rename
        private void SetAllScreenActive(bool active)
        {
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(active);
            }
        }
    }
}