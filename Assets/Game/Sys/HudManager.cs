using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//temp
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			King1Talk(database.King1StartDialogue);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			King2Talk(database.King1StartDialogue);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			EndDialogue();
		}
	}
	
	void King1Talk(DialogueData data){
		ResetKings();
		DIALOGUE_ON=true;
		speech_bubble_anchor.side=UIAnchor.Side.TopRight;
			
		speech_bubble.setPosition(king1_speech_pos);
		speech_bubble.appear();
		
		speech_bubble.setText(data.Text);
		setAnswers(data);
		
		king1.StartTalking();
		
		player_camera.MoveToCameraPos(Tower1CameraPos);
	}
	
	void King2Talk(DialogueData data){
		ResetKings();
		DIALOGUE_ON=true;
		speech_bubble_anchor.side=UIAnchor.Side.TopLeft;
			
		speech_bubble.setPosition(king2_speech_pos);
		speech_bubble.appear();
		
		speech_bubble.setText(data.Text);
		setAnswers(data);
	
		king2.StartTalking();
		
		player_camera.MoveToCameraPos(Tower2CameraPos);
	}
	
	void ChangeDialogue(DialogueData data){
		speech_bubble.setText(data.Text);
		setAnswers(data);
	}
	
	void ResetKings(){
		DIALOGUE_ON=false;
		speech_bubble.disappear();
		king1.StopTalking();
		king2.StopTalking();
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
			foreach (var d in data.answers){
				
				var go=Instantiate(answer_button_prefab,Vector3.zero,Quaternion.identity) as GameObject;
				var ab=go.GetComponent<AnswerButtonMain>();
				
				go.transform.parent=speech_bubble_answer_buttons_parent.transform;
				go.transform.localPosition=speech_bubble.transform.localPosition+answer_text_offset+Vector3.down*y_off;
				y_off+=64;
				
				ab.SetData(d);
				ab.Base.appear();
				ab.bps.controller=gameObject;
				
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
		
		var a=ans.Data.answers[0];
		if(a!=null){//DEV.random data if many?
			ChangeDialogue(a);
			if (!a.hasAnswers()){
				setAnswers(database.EndDialogueData);
			}
		}
		else{
			setAnswers(database.EndDialogueData);
		}
	}
	
	void EndDialogue(){
		ResetKings();
		player_camera.MoveToPlayerPos();
	}
}
