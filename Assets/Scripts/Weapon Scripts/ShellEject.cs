﻿using UnityEngine;

public class ShellEject : MonoBehaviour {
	
[SerializeField] private Rigidbody bulletCasing;
public int ejectSpeed = 20;
[SerializeField] private float fireRate = 0.1f;
private float nextFire = 0.0f;


private void Update () {
 
if (Input.GetMouseButton(0) && Time.time > nextFire) {
nextFire =  Time.time + fireRate;

Rigidbody clone;
 
clone = Instantiate(bulletCasing, transform.position, transform.rotation);
clone.velocity = transform.TransformDirection(-3,Random.Range(4,6),Random.Range(0,-2));
}
}
}
