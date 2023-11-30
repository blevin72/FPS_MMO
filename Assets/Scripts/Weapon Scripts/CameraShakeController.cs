using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
	[SerializeField] private float shakePower;
    [SerializeField] private float shakeDuration;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float slowdownAmount;
	[HideInInspector]
    public bool canShake = false;

    private Vector3 startPosition;
    private float initialDuration;

    private void Start()
    {
	    mainCamera = Camera.main.transform;
	    startPosition = mainCamera.localPosition;
	    initialDuration = shakeDuration;
    }

    private void Update()
    {
        RotateCameraAndShake();       
    }

    private void RotateCameraAndShake()
    {
   
		
		 GameObject[] explosion = GameObject.FindGameObjectsWithTag("Explosion");
	     foreach(GameObject target in explosion) {
         float distance = Vector3.Distance(target.transform.position, transform.position);
         if(distance < 25) {
             canShake = true;  
         }
     }

     if(canShake)
	{
		if(shakeDuration > 0)
		{
			mainCamera.localPosition = startPosition + Random.insideUnitSphere * shakePower;
			shakeDuration -= Time.deltaTime * slowdownAmount;
		}
		else
		{
			canShake = false;
			shakeDuration = initialDuration;
			mainCamera.localPosition = startPosition;
		}
	}
	}	
}
