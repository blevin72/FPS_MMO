using UnityEngine;

public class FadeAway : MonoBehaviour {
	
private Material mat;
 
   private  void Start () {
         mat = gameObject.GetComponent<MeshRenderer>().material;
     }
     
   private void Update () {
		 
             Color newColor = mat.color;
             newColor.a -= Time.deltaTime / 10;
             mat.color = newColor;
			 Destroy (gameObject, 10);

     }
 }