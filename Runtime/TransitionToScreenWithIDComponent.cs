using UnityEngine;

namespace Nimlok.Screens
{
    public class TransitionToScreenWithIDComponent: MonoBehaviour
    {
        [SerializeField] private int screenID;

        public void TransitionToScreen()
        {
            ScreenManager.TransitionToScreenWithID?.Invoke(screenID);
        }
    }
}