using UnityEngine;

namespace Screens
{
    public class TransitionToScreenWithIDComponent: MonoBehaviour
    {
        [SerializeField] private string screenID;

        public void TransitionToScreen()
        {
            if (string.IsNullOrEmpty(screenID))
            {
                Debug.LogError($"Missing Screen ID: {gameObject.name}");
                return;
            }
            
            ScreenManager.TransitionToScreenWithID?.Invoke(screenID);
        }
    }
}