using UnityEngine;
using System.Collections;

public class BackToMenuMain : MonoBehaviour {
	
	public GameObject Menu;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnYes(){
		Time.timeScale=1;
		Application.LoadLevel("MainMenuScene");
	}
	
	void OnNo(){
		ToggleMenu();
	}
	
	public void ToggleMenu(){
		if (Menu.activeSelf){
			Menu.SetActive(false);
			Time.timeScale=1;
			return;
		}
		Menu.SetActive(true);
		Time.timeScale=0.00000001f;
	}
	
	public bool IsOn(){
		return Menu.activeSelf;
	}
}
