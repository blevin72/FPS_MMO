using UnityEngine;

public class TechBuildingVolume : MonoBehaviour
{
    // Define the ambient light settings for outside and inside the building
    public Color outsideAmbientColor = new Color(0.3f, 0.3f, 0.3f);
    public float outsideAmbientIntensity = 1f;

    public Color insideAmbientColor = new Color(0.1f, 0.1f, 0.1f);
    public float insideAmbientIntensity = 0.5f;

    // Define the transition duration
    public float transitionDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered the building, start gradual transition
            StartCoroutine(AdjustLightingOverTime(insideAmbientColor, insideAmbientIntensity));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player exited the building, start gradual transition
            StartCoroutine(AdjustLightingOverTime(outsideAmbientColor, outsideAmbientIntensity));
        }
    }

    private System.Collections.IEnumerator AdjustLightingOverTime(Color targetColor, float targetIntensity)
    {
        Color startColor = RenderSettings.ambientLight;
        float startIntensity = RenderSettings.ambientIntensity;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            RenderSettings.ambientLight = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            RenderSettings.ambientIntensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / transitionDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure a smooth final transition
        RenderSettings.ambientLight = targetColor;
        RenderSettings.ambientIntensity = targetIntensity;
    }
}
