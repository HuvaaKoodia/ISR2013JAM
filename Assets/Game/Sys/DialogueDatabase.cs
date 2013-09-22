using UnityEngine;
using System.Collections;

public class DialogueDatabase : MonoBehaviour {
	
	public DialogueData King1StartDialogue,EndDialogueData;
	
	// Use this for initialization
	void Start () {
		
		DialogueData ans1,ans2,ans3,ans4,ka1;
		
		//start dialogue 1
		King1StartDialogue=new DialogueData("The time has come!\n\nSlay this vile king and his army.\nThis is thy destiny!");
		
		ans1=new DialogueData("Will do.");
		ans2=new DialogueData("Can we postpone this.");
		ans3=new DialogueData("I.. I..");
		
		King1StartDialogue.AddAnswer(ans1);
		King1StartDialogue.AddAnswer(ans2);
		King1StartDialogue.AddAnswer(ans3);
		
		ka1=new DialogueData("Splendid!");
		ans1.AddAnswer(ka1);
		ka1=new DialogueData("Absolutely not!\n\nHave at them!");
		ans2.AddAnswer(ka1);
		ka1=new DialogueData("What is it!\nSpit it out!!");
		ans3.AddAnswer(ka1);
		ka1.AddAnswer(new DialogueData("Nothing...","ENDL"));
		
		//end message
		
		EndDialogueData=new DialogueData("");
		EndDialogueData.AddAnswer(new DialogueData("<End Conversation>","ENDL"));
		
		//
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
