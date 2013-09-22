using UnityEngine;
using System.Collections;

public class ButtonPressedScr : MonoBehaviour {
	
	public GameObject controller;
	public string EventName;
	
	public AnswerButtonMain ans;
	
	void OnClick(){
		controller.SendMessage(EventName,ans);
	}
}
