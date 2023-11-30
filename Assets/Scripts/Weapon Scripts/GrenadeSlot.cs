using UnityEngine;

public class GrenadeSlot : MonoBehaviour {

public int grenadeQuantity;
[SerializeField] private GUIStyle mystyle;

	// Use this for initialization
	void Start () {	
	mystyle.fontSize = 20;
    mystyle.normal.textColor = Color.white;
}
private void OnGUI () {
GUI.Label (new Rect (45,Screen.height - 95,100,50), grenadeQuantity+"",mystyle);
	}
}
