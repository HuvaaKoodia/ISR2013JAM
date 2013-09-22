using UnityEngine;
using System.Collections;

public class ButtonPressedScr1 : MonoBehaviour {
	
	public GameObject controller;
	public string EventName;
	
	void OnClick(){
		controller.SendMessage(EventName);
	}
}
