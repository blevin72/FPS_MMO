using UnityEngine;

public class DestroyExplosion : MonoBehaviour {
	
	[SerializeField] private int lifetime = 5 ;

private void Update () {
	
        Destroy(gameObject,lifetime);
	
}
}