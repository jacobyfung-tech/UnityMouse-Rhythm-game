using UnityEngine;

public class HideMesh : MonoBehaviour
{
    void Start()
    {
        // Find all MeshRenderers in the current scene
        MeshRenderer[] allRenderers = Object.FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None);
        LineRenderer[] allLineRenderers = Object.FindObjectsByType<LineRenderer>(FindObjectsSortMode.None);

        // Loop through and disable each one
        foreach (MeshRenderer renderer in allRenderers)
        {
            renderer.enabled = false;
        }
        foreach (LineRenderer lR in allLineRenderers)
        {
            lR.numCornerVertices = VectorUtils.corners;
        }

        Debug.Log($"Disabled {allRenderers.Length} Mesh Renderers.");
    }
}
