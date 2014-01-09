using UnityEngine;
using System.Collections;

public class OptionsPanel : MonoBehaviour {

	public UICheckbox Freeform,Chesslike;
	GameOptionsDatabase GODB;

	void Start () {
		GODB=GameObject.FindGameObjectWithTag("GameOptions").GetComponent<GameOptionsDatabase>();

		if (GODB.KnightMovement==GameOptionsDatabase.KnightMovementType.Freeform){
			Freeform.isChecked=true;
			Chesslike.isChecked=false;
		}
		else{
			Freeform.isChecked=false;
			Chesslike.isChecked=true;
		}

	}

	void FreeformPressed(){
		if (!Freeform.isChecked) {
			Freeform.isChecked=true;
			return;
		}
		Chesslike.isChecked=false;
		GODB.KnightMovement=GameOptionsDatabase.KnightMovementType.Freeform;
	}

	void ChesslikePressed(){
		if (!Chesslike.isChecked) {
			Chesslike.isChecked=true;
			return;
		}
		Freeform.isChecked=false;
		GODB.KnightMovement=GameOptionsDatabase.KnightMovementType.Chesslike;
	}

	void FullscreenPressed(){
		Screen.fullScreen=!Screen.fullScreen;
	}

}
