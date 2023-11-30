﻿using UnityEngine;

public class ProjectileDestoy : MonoBehaviour {
	
	[SerializeField] private float delay = 2f;
	[SerializeField] private float radius = 5f;
	[SerializeField] private float force = 500f;
	
	[SerializeField] private GameObject explosionEffect;
	[SerializeField] private GameObject rocket;
	
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
		
	private	void OnCollisionEnter (Collision col)
		{
			if (col.gameObject.tag == "Target" || col.gameObject.tag == "Concrete" || col.gameObject.tag == "Metal")
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
			
			Destroy(rocket);
			Destroy(gameObject,5);
			
			
		}
	}