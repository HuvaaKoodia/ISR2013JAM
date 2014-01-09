using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OnDialogueToggle(bool b);

public class HudManager : MonoBehaviour {
	
	public DialogueDatabase database;
	public SpeechBubbleMain speech_bubble;
	public UIAnchor speech_bubble_anchor;
	public UIPanel speech_bubble_panel;
	public GameObject speech_bubble_answer_buttons_parent;
	
	public Vector3 king1_speech_pos,king2_speech_pos,answer_text_offset;
	public GameObject Tower1CameraPos,Tower2CameraPos;
	public KingMain king1,king2;
	
	public GameController game_controller;
	public CameraMain player_camera;
	
	public GameObject answer_button_prefab;
	
	public GameOverHud gameover_hud;
	
	public OnDialogueToggle OnDialogueToggleEvent;

	void Update () {	
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.F2)){
			King1Talk(database.ZOMBIE_MeetingTheReverendKing);
		}
		
		if (Input.GetKeyDown(KeyCode.F3)){
			King2Talk(database.ZOMBIE_MeetingThePlagueKing);
		}
		
		if (Input.GetKeyDown(KeyCode.F4)){
			EndDialogue();
		}
		
		if (Input.GetKeyDown(KeyCode.F5)){
			king1.DIE();
		}
		if (Input.GetKeyDown(KeyCode.F6)){
			king2.DIE();
		}
#endif
		
		if (DIALOGUE_ON){
			if (Input.GetKeyDown(KeyCode.Alpha1)){
				if (answer_buttons.Count>0){
					AnswerButtonPressed(answer_buttons[0]);
				}
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha2)){
				if (answer_buttons.Count>1){
					AnswerButtonPressed(answer_buttons[1]);
				}
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha3)){
				if (answer_buttons.Count>2){
					AnswerButtonPressed(answer_buttons[2]);
				}
			}
		}
		
	}
	
	public void SoldierTalk(SoldierMain soldier,DialogueData data){
		DIALOGUE_ON=true;
		
		speech_bubble_anchor.side=UIAnchor.Side.TopRight;
		speech_bubble.setPosition(king1_speech_pos);
		speech_bubble.appear();
		
		ChangeDialogue(data);
		
		king1.StartTalking();
		
		var pos=soldier.transform.position+new Vector3(2,2.5f);
		player_camera.MoveToCameraPos(pos,Quaternion.LookRotation(soldier.transform.position-pos,Vector3.up));
	}
	
	public void King1Talk(DialogueData data){
		ResetKings();
		DIALOGUE_ON=true;
		
		speech_bubble_anchor.side=UIAnchor.Side.TopRight;
		speech_bubble.setPosition(king1_speech_pos);
		speech_bubble.appear();
		
		ChangeDialogue(data);
		
		king1.StartTalking();
		
		player_camera.MoveToCameraPos(Tower1CameraPos);
	}
	
	public void King2Talk(DialogueData data){
		ResetKings();
		DIALOGUE_ON=true;
		
		speech_bubble_anchor.side=UIAnchor.Side.TopLeft;
		speech_bubble.setPosition(king2_speech_pos);
		speech_bubble.appear();
		
		ChangeDialogue(data);
	
		king2.StartTalking();
		
		player_camera.MoveToCameraPos(Tower2CameraPos);
	}
	
	void ChangeDialogue(DialogueData data){
		if (data.Type=="RANDOM"){
			if (!data.hasAnswers()){
				Debug.LogError("No anwers in "+data.Text+". type: RANDOM.");
				return;
			}
			//data=data.answers[Random.Range(0,data.answers.Count)];
			data=data.GetRandom();
		}
		
		
		speech_bubble.setText(data.Text);
		
		if (data.hasAnswers()){
			setAnswers(data);
		}
		else{
			setAnswers(database.EndDialogueData);
		}
		
		if (OnDialogueToggleEvent!=null)
			OnDialogueToggleEvent(true);
	}
	
	void ResetKings(){
		DIALOGUE_ON=false;
		speech_bubble.disappear();
		king1.StopTalking();
		king2.StopTalking();
		
		if (OnDialogueToggleEvent!=null)
			OnDialogueToggleEvent(false);
	}
	
	public bool DIALOGUE_ON{get;private set;}
	
	List<AnswerButtonMain> answer_buttons=new List<AnswerButtonMain>();
	
	void ClearAnswers(){
		answer_buttons.Clear();
		
		int c=speech_bubble_answer_buttons_parent.transform.childCount;
		for (int i=0;i<c;i++){
			NGUITools.Destroy(speech_bubble_answer_buttons_parent.transform.GetChild(0).gameObject);
		}
	}
	
	void setAnswers(DialogueData data){
		ClearAnswers();
		
		if (data==null) return;
		
		if (data.hasAnswers()){
			int y_off=0;
			foreach (var d in data.Answers){
				
				var go=Instantiate(answer_button_prefab,Vector3.zero,Quaternion.identity) as GameObject;
				var ab=go.GetComponent<AnswerButtonMain>();
				
				go.transform.parent=speech_bubble_answer_buttons_parent.transform;
				go.transform.localPosition=speech_bubble.transform.localPosition+answer_text_offset+Vector3.down*y_off;
				
				if (d.Data.Type=="RANDOM"){
					ab.SetData(d.Data.GetRandom());
				}
				else{
					ab.SetData(d.Data);
				}
				ab.Base.appear();
				ab.bps.controller=gameObject;
				
				y_off+=(int)ab.y_size+16;
				
				answer_buttons.Add(ab);
			}	
		}
	}
	
	void AnswerButtonPressed(AnswerButtonMain ans){
		
		if (ans.Data.Type=="ENDL"){
			EndDialogue();
			ClearAnswers();
			return;
		}
		
		if (checkGameEvent(ans.Data.Type)){
			ResetKings();
			ClearAnswers();
			return;
		}
		
		if (!ans.Data.hasAnswers()){
			Debug.LogError("No further anwers in "+ans.Data.Text);
			return;
		}
		
		var a=ans.Data.Answers[0].Data;
		ChangeDialogue(a);
		
		/*if(a!=null){//DEV.random data if many?
			
			if (!a.hasAnswers()){
				setAnswers(database.EndDialogueData);
			}
		}
		else{
			setAnswers(database.EndDialogueData);
		}*/
	}
	
	void EndDialogue(){
		ResetKings();
		ClearAnswers();
		player_camera.MoveToPlayerPos();
	}
	
	public bool pking_fooled=false;
	
	bool checkGameEvent(string type){
		//gameover events
		if (type=="GAMEOVER_DEAD"){
			
			game_controller.GAMEOVER=true;
			gameover_hud.GAMEOVER("You are dead!");
			return true;
		}
		
		if (type=="GAMEOVER_GAVEUP"){
			game_controller.GAMEOVER=true;
			gameover_hud.GAMEOVER("You gave up!");
			EndDialogue();
			return true;
		}
		
		if (type== "GAMEOVER_PLAGUEKINGSURRENDER"){
			game_controller.GAMEOVER=true;
			gameover_hud.GAMEOVER("The Plague King surrenders!");
			
			return true;
		}
		
		if (type== "GAMEOVER_PLAGUEKINGDEAD"){
			king2.DIE();
			game_controller.GAMEOVER=true;
			gameover_hud.GAMEOVER("The Plague King Is Defeated!");
			return true;
		}
		
		if (type== "GAMEOVER_KINGDEAD"){
			king1.DIE();
			game_controller.GAMEOVER=true;
			gameover_hud.GAMEOVER("You dethroned the king!");
			return true;
		}
		
		if (type== "GAMEOVER_EVILALLIANCE"){
			game_controller.GAMEOVER=true;
			gameover_hud.GAMEOVER("An evil alliance is formed!");

			return true;
		}
		
		if (type== "GAMEOVER_ZOMBIEKING"){
			king1.ZOMBIFY();
			game_controller.GAMEOVER=true;
			gameover_hud.GAMEOVER("Regal brains are tasty!");
			
			return true;
		}
		
		if (type== "GAMEOVER_ZOMBIEPLAGUEKING"){
			king2.ZOMBIFY();
			game_controller.GAMEOVER=true;
			gameover_hud.GAMEOVER("The Plague cannot be stopped!");//DEV.temp
			return true;
		}
		
		//neutral events
		if (type=="LEAVE_PLAYERTOWER"){
			
			game_controller.GOTO_PLAYERTOWER_BASE();
			EndDialogue();
			return true;
		}
		if (type=="LEAVE_ENEMYTOWER"){
			
			game_controller.GOTO_ENEMYTOWER_BASE();
			EndDialogue();
			return true;
		}
		
		if (type=="OPENPLAGUEGATE"){
			game_controller.EnemyTower.OpenGate();
			EndDialogue();
			return true;
		}
		
		return false;
	}
}
