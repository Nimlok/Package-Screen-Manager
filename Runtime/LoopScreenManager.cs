using System;
using System.Collections;
using UnityEngine;

namespace Nimlok.Screens
{
    [RequireComponent(typeof(ScreenManager))]
    [RequireComponent(typeof(ScreenInactiveManager))]
    public class LoopScreenManager: MonoBehaviour
    {
        private ScreenInactiveManager screenInactiveManager;
        private TransitionableScreen[] loopScreens;
        private ScreenManager screenManager;
        private Coroutine currentCoroutine;
        private int index;

        public static Action TriggerLoopingScreens;
        public static Action StopLoopScreens;

        private bool looping;

        #region  Unity Events
        private void OnEnable()
        {
            TriggerLoopingScreens += StartLoop;
            StopLoopScreens += StopLoop;
        }

        private void OnDisable()
        {
            TriggerLoopingScreens -= StartLoop;
            StopLoopScreens -= StopLoop;
        }

        private void Awake()
        {
            screenManager = GetComponent<ScreenManager>();
            screenInactiveManager = GetComponent<ScreenInactiveManager>();
            screenManager.OnScreenTransitionTriggered += OnScreenTransitioned;
            
            screenInactiveManager.onIdle.AddListener(() =>
            {
                StartLoop();
                screenInactiveManager.Stop();
            });
            
            screenInactiveManager.onAnyKeyPressed.AddListener(() =>
            {
                StopLoop();
                screenInactiveManager.Stop();
            });
            
            if (screenManager.HasInitialScreen && screenManager.GetInitialScreen.GetLoopProperties.loopingScreen)
            {
                screenInactiveManager.StartIdle();
            }
        }

        private void OnScreenTransitioned(TransitionableScreen screen)
        {
            if (!screen.GetLoopProperties.loopingScreen)
            {
                screenInactiveManager.Stop();
                
                if(looping)
                    StopLoop();
                
                return;
            }

            if (looping)
                return;
            
            screenInactiveManager.StartIdle();
        }
        
        private void Start()
        {
            GetLoopableScreens();
        }
        #endregion

        public void StartFromIdle()
        {
            if (looping)
                return;

            if (screenManager.HasInitialScreen)
            {
                StartLoop();
            }
            else
            {
                screenManager.ReturnToInitialScreen();
            }
        }
        
        public void StartLoop()
        {
            if (looping)
                return;
            
            looping = true;
            NextLoopScreen();
        }

        public void StopLoop()
        {
            looping = false;
            if(currentCoroutine != null)
                StopCoroutine(currentCoroutine);
        }

        private void NextLoopScreen()
        {
            CheckCurrentScreen();
            var currentScreen = loopScreens[index];
            currentCoroutine = StartCoroutine(LoopScreen(currentScreen));
        }
        
        private void GetLoopableScreens()
        {
            var allScreens = screenManager.GetAllScreens;
            loopScreens = allScreens.FindAll(x => x.GetLoopProperties.loopingScreen).ToArray();
        }
        
        private IEnumerator LoopScreen(TransitionableScreen screen)
        {
            screenManager.TransitionToScreen(screen);
            var loopProperties = screen.GetLoopProperties;
            var currentScene = screenManager.GetCurrentScreen;
            while(currentScene.IsPlaying())  
            {
                yield return null;
            }
            
            yield return new WaitForSeconds(loopProperties.loopTime);
            NextLoopScreen();
        }

        private void UpdateIndex()
        {
            index++;
            if (index > loopScreens.Length-1)
            {
                index = 0;
            }
        }

        private void CheckCurrentScreen()
        {
            var currentScreen = loopScreens[index];
            if (currentScreen == screenManager.GetCurrentScreen)
            {
                UpdateIndex();
            }
        }
    }
}