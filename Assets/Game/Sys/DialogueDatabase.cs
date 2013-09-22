using UnityEngine;
using System.Collections;

public class DialogueDatabase : MonoBehaviour {
	
	public DialogueData King1StartDialogue,EndDialogueData,King1InTower,EnemiesFlee,EnemiesPlague;
	
	// Use this for initialization
	void Start () {
		
		DialogueData ans1,ans2,ans3,ka1,ka2,ka3;
		
		//start dialogue 1
		King1StartDialogue=new DialogueData("The time has come!\n\nSlay this vile king and his army.\nThis is thy destiny!");
		
		ans1=new DialogueData("Will do.");
		King1StartDialogue.AddAnswer(ans1);
		ans2=new DialogueData("Can we postpone this?");
		King1StartDialogue.AddAnswer(ans2);
		ans3=new DialogueData("I.. I..");	
		King1StartDialogue.AddAnswer(ans3);
		
		ka1=new DialogueData("Splendid!");
		ans1.AddAnswer(ka1);
		ka1=new DialogueData("Absolutely not!\n\nHave at them!");
		ans2.AddAnswer(ka1);
		ka1=new DialogueData("What is it!\nSpit it out!!");
		ans3.AddAnswer(ka1);
		
		ka1.AddAnswer(new DialogueData("Nothing...","ENDL"));
		ans1=new DialogueData("I cannot do this what you ask of me.");
		ka1.AddAnswer(ans1);
		
		ka1=new DialogueData("What is this! Do you dare to defy your king!");
		ans1.AddAnswer(ka1);
		
		ans1=new DialogueData("Yes. That is it precisely,");
		ka1.AddAnswer(ans1);
		ans2=new DialogueData("I changed my mind. Forgive me for questioning your regal reasons.");
		ka1.AddAnswer(ans2);
		
		ka1=new DialogueData("I have no need for traitorous heretics!\n\nBegone foul shade!");
		ans1.AddAnswer(ka1);
		
		ans1=new DialogueData("So be it.","GAMEOVER_GAVEUP");
		ka1.AddAnswer(ans1);
		
		ka2=new DialogueData("Good, now make up for this unfortunate slip and claim the land.");
		ans2.AddAnswer(ka2);
		
		
		
		//end message
		
		EndDialogueData=new DialogueData("");
		EndDialogueData.AddAnswer(new DialogueData("<End Conversation>","ENDL"));
		
		//temp
		King1InTower=new DialogueData("What is this!! You smahed my gate!");
		
		ans1=new DialogueData("I will go now","LEAVE_PLAYERTOWER");
		King1InTower.AddAnswer(ans1);
		
		EnemiesPlague=new DialogueData("What kind of dark magic is this?");
		
		EnemiesFlee=new DialogueData("They flee cut them down");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
