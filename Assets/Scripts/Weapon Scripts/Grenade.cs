using UnityEngine;

public class Grenade : MonoBehaviour {
	
	[SerializeField] private float delay = 3f;
	[SerializeField] private float radius = 5f;
	[SerializeField] private float force = 500f;
	
	[SerializeField] private GameObject explosionEffect;
	
	private float countdown;
	private bool hasExploded = false;
	
	private void Start (){
		countdown = delay;
	}
		
	private void Update () {
			countdown -= Time.deltaTime;
			if (countdown <= 0f && !hasExploded)
			{
				Explode();
				hasExploded = true;
			}
		}
		
	private void Explode ()
		{
			Instantiate(explosionEffect, transform.position, transform.rotation);
			Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
			foreach (Collider nearbyObject in collidersToDestroy)
			{
     			DestructibleObject dest = nearbyObject.GetComponent<DestructibleObject>();
				if (dest != null)
				{
					dest.Destroy();
				}
			
			}
			Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
			foreach (Collider nearbyObject in collidersToMove)
			{
			    Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
				if (rb != null)
				{
					rb.AddExplosionForce(force, transform.position, radius);
				}
				
			}
			
			Destroy(gameObject);
			
		}
	}
	
		 	