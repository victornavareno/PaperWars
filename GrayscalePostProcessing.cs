using UnityEngine;

[ExecuteInEditMode]
public class GrayscalePostProcessing : MonoBehaviour
{
    public Material grayscaleMaterial; // Reference to the grayscale material

    // Called after the camera has rendered the scene
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (grayscaleMaterial != null)
        {
            // Apply the grayscale material as a post-processing effect
            Graphics.Blit(src, dest, grayscaleMaterial);
        }
        else
        {
            // If no material is set, just copy the original image
            Graphics.Blit(src, dest);
        }
    }
}
