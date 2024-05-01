using System;
using System.Collections;
using UnityEngine;

namespace Screens
{
    [RequireComponent(typeof(ScreenManager))]
    public class LoopScreenManager: MonoBehaviour
    {
        private TransitionableScreen[] loopScreens;
        private ScreenManager screenManager;
        private Coroutine currentCoroutine;
        private int index;

        public static Action TriggerLoopingScreens;
        public static Action StopLoopScreens;

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
        }

        private void Start()
        {
            GetLoopableScreens();
        }
        
        public void StartLoop()
        {
            CheckCurrentScreen();
            var currentScreen = loopScreens[index];
            currentCoroutine = StartCoroutine(LoopScreen(currentScreen));
        }

        public void StopLoop()
        {
            if (currentCoroutine == null)
                return;
            
            StopCoroutine(currentCoroutine);
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
            UpdateIndex();
            StartLoop();
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
            if (string.CompareOrdinal(screenManager.GetCurrentScreen.GetID, currentScreen.GetID) == 0)
            {
                UpdateIndex();
            }
        }
    }
}