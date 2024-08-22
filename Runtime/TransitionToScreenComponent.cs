using UnityEngine;

namespace Nimlok.Screens
{
    public class TransitionToScreenComponent: MonoBehaviour
    {
        [SerializeField] private TransitionableScreen screen;

        public void TransitionToScreen()
        {
            if (screen == null)
            {
                Debug.LogError($"Missing Screen ID: {gameObject.name}");
                return;
            }
            
            ScreenManager.TransitionToScreenWithScreen?.Invoke(screen);
        }
    }
}