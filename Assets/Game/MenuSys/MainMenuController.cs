using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	
	public GameObject CreditsPanel,HelpPanel,OptionsPanel;
	public GameObject ExitButton;

	void Start(){
		DisableAllPanels();

#if UNITY_WEBPLAYER
		ExitButton.SetActive(false);
#endif
	}

	void PlayPressed(){
		Application.LoadLevel("GameScene");
	}
	
	void ExitPressed(){
		Application.Quit();
	}

	void CreditsPressed(){
		TogglePanel(CreditsPanel);
	}
	
	void ControlsPressed(){
		TogglePanel(HelpPanel);
		
	}

	void OptionsPressed(){
		TogglePanel(OptionsPanel);
		
	}

	void TogglePanel(GameObject go){
		if (go.activeSelf)
			go.SetActive(false);
		else{
			DisableAllPanels();

			go.SetActive(true);
		}
	}

	void DisableAllPanels(){
		HelpPanel.SetActive(false);
		OptionsPanel.SetActive(false);
		CreditsPanel.SetActive(false);
	}
}
