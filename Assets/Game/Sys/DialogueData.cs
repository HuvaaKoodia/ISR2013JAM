using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueData{

	public string Text,Type="";
	
	public List<DialogueData> answers=new List<DialogueData>();

	public DialogueData(string text){
		Text=text;
	}
	
	public DialogueData(string text,string type){
		Text=text;
		Type=type;
	}
	
	public bool hasAnswers ()
	{
		return answers.Count>0;
	}

	public void AddAnswer (DialogueData ka1)
	{
		answers.Add(ka1);
	}
}
