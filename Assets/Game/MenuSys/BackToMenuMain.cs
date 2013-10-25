using UnityEngine;
using System.Collections;
using System;

public class BackToMenuMain : MonoBehaviour {
	
	public GameObject Menu;
	
	public Action OnChange;
	
	public bool INMENU{get{return Menu.activeSelf;}}
	
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
		Menu.SetActive(!Menu.activeSelf);
		if (Menu.activeSelf){
			Time.timeScale=0.00000001f;
		}
		else{
						Time.timeScale=1;
		}
		if (OnChange!=null)
			OnChange();
	}
	
	public bool IsOn(){
		return Menu.activeSelf;
	}
}
