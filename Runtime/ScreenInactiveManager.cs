using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Nimlok.Screens
{
    public class ScreenInactiveManager : MonoBehaviour
    {
        [BoxGroup, ReadOnly] public bool active;
        [BoxGroup, ShowInInspector, ReadOnly] private float currentTime;
    
        [Space, SerializeField] private float idleTime;
        [Space] public UnityEvent onIdle;
        [Space] public UnityEvent onAnyKeyPressed;
    
        public static Action RestartIdle;

        private void OnEnable()
        {
            RestartIdle += Reset;
        }

        private void OnDisable()
        {
            RestartIdle -= Reset;
        }
        
        private void Awake()
        {
            Reset();
        }

        private void Update()
        {
            if (!active)
                return;

            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Stop();
                onIdle?.Invoke();
            }
            
            if (Input.anyKey)
            {
                Stop();
                onAnyKeyPressed?.Invoke();
            }
        }

        public void StartIdle()
        {
            active = true;
        }

        public void Stop()
        {
            active = false;
            Reset();
        }
    
        private void Reset()
        {
            currentTime = idleTime;
        }
    }
}

