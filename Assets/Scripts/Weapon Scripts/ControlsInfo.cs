using UnityEngine;

public class ControlsInfo : MonoBehaviour {

[SerializeField] private GameObject Text01;
[SerializeField] private GameObject Text02;
private bool ShowHide;

private void Update () {

if(Input.GetKeyDown(KeyCode.Tab)){ 
	if(ShowHide){
	ShowHide = false;	
	Text01.SetActive (true);
	if(Text02 == null){
		Text02 = null;
		}
	else{
	Text02.SetActive (true);
	}
	}
	else{
	ShowHide = true;	
	Text01.SetActive (false);
	Text02.SetActive (false);
	}
	}
}
}