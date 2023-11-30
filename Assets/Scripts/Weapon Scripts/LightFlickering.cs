using System.Collections;
using UnityEngine;

public class LightFlickering : MonoBehaviour {

private Light MuzzleFlashLight;

private void Start()
{
MuzzleFlashLight = GetComponent<Light>();
}

private void Update () {
StartCoroutine(Flashing());
}

private IEnumerator Flashing ()
	{
  
  while (true)
		{
			yield return new WaitForSeconds(Random.Range(0,0));
			MuzzleFlashLight.enabled = !MuzzleFlashLight.enabled;

		}

	
}
}
