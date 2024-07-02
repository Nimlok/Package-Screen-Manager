using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Screens
{
    public class ScreenInactiveManager : MonoBehaviour
    {
        [BoxGroup, ReadOnly] public bool active;
        [BoxGroup, ShowInInspector, ReadOnly] private float currentTime;
    
        [Space, SerializeField] private float idleTime;
        [Space, SerializeField] private UnityEvent onIdle;
        [Space, SerializeField] private UnityEvent onAnyKeyPressed;
    
        private void Awake()
        {
            Reset();
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                onAnyKeyPressed?.Invoke();
                Reset();
            }
        
            if (!active)
                return;

            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Stop();
                onIdle?.Invoke();
            
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

