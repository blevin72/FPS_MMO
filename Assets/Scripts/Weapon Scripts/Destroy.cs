using UnityEngine;

public class Destroy : MonoBehaviour {
	
	[SerializeField] private int lifetime = 2 ;


private void Update () {
	
		transform.Rotate(Random.Range(0, 10),Random.Range(0, 15),Random.Range (0, 22));	
        Destroy(gameObject,lifetime);
	
}
}