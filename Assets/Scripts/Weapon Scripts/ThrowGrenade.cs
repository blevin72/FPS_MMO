using UnityEngine;

public class ThrowGrenade : MonoBehaviour {
	
	[SerializeField] private GameObject grenade;
	[SerializeField] private Transform startPoint;
	[SerializeField] private float throwForce = 15f;
	
	
private	void Update () {
			GameObject gren = Instantiate (grenade, startPoint.position, startPoint.rotation) as GameObject;
			gren.GetComponent <Rigidbody> ().AddForce(startPoint.forward * throwForce, ForceMode.Impulse);
		
	}
}