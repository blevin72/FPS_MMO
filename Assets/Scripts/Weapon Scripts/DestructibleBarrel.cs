﻿using UnityEngine;

public class DestructibleObject : MonoBehaviour {
	
public GameObject destroyedObject;
[SerializeField] private float health = 100f;

public void TakeDamage (float amount)
{
	health -= amount;
	if (health <= 0f)
	{
		Destroy();
	}
}

public void Destroy()
{
	Instantiate(destroyedObject, transform.position, transform.rotation);
	gameObject.SetActive(false);
}
}