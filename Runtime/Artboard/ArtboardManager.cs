using Sirenix.OdinInspector;
using UnityEngine;

public class ArtboardManager : MonoBehaviour
{
    [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
    public void ActivateArtboard(bool active)
    {
        var artboards = GetComponentsInChildren<Artboard>(true);
        foreach (var artboard in artboards)
        {
            artboard.gameObject.SetActive(active);
        }
    }
}
