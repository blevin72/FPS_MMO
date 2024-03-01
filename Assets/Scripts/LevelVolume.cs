using UnityEngine;

public class LightingAdjustment : MonoBehaviour
{
    // Define the ambient light settings for outside and inside the level
    public Color outsideAmbientColor = new Color(0.3f, 0.3f, 0.3f);
    public float outsideAmbientIntensity = 1f;

    // Define the transition duration for global lighting
    public float globalTransitionDuration = 2f;

    private bool isInsideGlobalVolume = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player entered the global level volume
            if (!isInsideGlobalVolume)
            {
                StartCoroutine(AdjustLightingOverTime(outsideAmbientColor, outsideAmbientIntensity, globalTransitionDuration));
                isInsideGlobalVolume = true;
            }
        }

        // Add additional checks for other specific volumes (building volumes) as needed
        // For example, check for a "Building" tag or another identifier for building volumes
        if (other.CompareTag("Building"))
        {
            // Player entered a building, adjust lighting for the building
            AdjustBuildingLighting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player exited the global level volume
            isInsideGlobalVolume = false;
        }

        // Add additional checks for other specific volumes (building volumes) as needed
        if (other.CompareTag("Building"))
        {
            // Player exited a building, reset lighting to global settings
            StartCoroutine(AdjustLightingOverTime(outsideAmbientColor, outsideAmbientIntensity, globalTransitionDuration));
        }
    }

    private System.Collections.IEnumerator AdjustLightingOverTime(Color targetColor, float targetIntensity, float duration)
    {
        Color startColor = RenderSettings.ambientLight;
        float startIntensity = RenderSettings.ambientIntensity;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            RenderSettings.ambientLight = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            RenderSettings.ambientIntensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure a smooth final transition
        RenderSettings.ambientLight = targetColor;
        RenderSettings.ambientIntensity = targetIntensity;
    }

    private void AdjustBuildingLighting()
    {
        // Implement logic to adjust lighting specifically for the building
        // You can use a similar approach as the global lighting adjustment
        // with a different set of ambient color and intensity values.
        // For example:
        // StartCoroutine(AdjustLightingOverTime(buildingAmbientColor, buildingAmbientIntensity, buildingTransitionDuration));
    }
}
