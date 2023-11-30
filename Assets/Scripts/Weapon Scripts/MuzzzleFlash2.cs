using System.Collections;
using UnityEngine;

public class MuzzzleFlash2 : MonoBehaviour {
	
[SerializeField] private float minWaitTime;
[SerializeField] private float maxWaitTime;
private Renderer muzzleflash;

	
private void Start () {
	muzzleflash = GetComponent<Renderer>();
	gameObject.SetActive (false);	
	}
	
	
private void Update () {	
muzzleflash = GetComponent<Renderer>();
muzzleflash.enabled = true;
transform.Rotate(0,Random.Range (359, 0),0);
StartCoroutine(Flashing());
}
private IEnumerator Flashing ()
	{
  while (true)
		{
			yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime));
			muzzleflash.enabled = !muzzleflash.enabled;

		}
	}
}
