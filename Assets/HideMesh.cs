using UnityEngine;

public class HideMesh : MonoBehaviour
{
    void Start()
    {
        // Find all MeshRenderers in the current scene
        MeshRenderer[] allRenderers = Object.FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None);

        // Loop through and disable each one
        foreach (MeshRenderer renderer in allRenderers)
        {
            renderer.enabled = false;
        }

        Debug.Log($"Disabled {allRenderers.Length} Mesh Renderers.");
    }
}
