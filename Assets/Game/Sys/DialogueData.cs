using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DialogueData{

	public string Text,Type="";
	
	public List<DialogueLink> Answers{get;private set;}

	public DialogueData(string text){
		Text=text;
		Answers=new List<DialogueLink>();
	}
	
	public DialogueData(string text,string type){
		Text=text;
		Type=type;
		Answers=new List<DialogueLink>();
	}
	
	public bool hasAnswers()
	{
		return Answers.Count>0;
	}

	public void AddAnswer (DialogueData ka1)
	{
		Answers.Add(new DialogueLink(ka1));
	}
	public void AddAnswer (DialogueData ka1,float chance)
	{
		Answers.Add(new DialogueLink(ka1,chance));
	}
	
	public void AddLink (DialogueLink link)
	{
		Answers.Add(link);
	}

	
	public DialogueData GetRandom()
	{
		List<DialogueLink> links=new List<DialogueLink>();
		bool even_random=true;
		foreach(var l in Answers){
			links.Add(l);
			if(l.RandomChance!=0){
				even_random=false;
			}
		}
		if (even_random){
			return links[Random.Range(0,links.Count)].Data;
		}
		else{
			links.OrderByDescending(l=>l.RandomChance);
			
			foreach(var l in links){
				if (Subs.RandomPercent()<l.RandomChance){
					return l.Data;
				}
			}
			return links.Last().Data;
		}
	}
}
	
public class DialogueLink{
	public DialogueData Data{get;set;}
	public float RandomChance{get;private set;}
	public string Link{get;private set;}
	
	public DialogueLink(DialogueData data){
		Data=data;
	}
	public DialogueLink(DialogueData data,float chance){
		Data=data;
		RandomChance=chance;
	}
	
	public DialogueLink(string link){
		Link=link;
	}
	public DialogueLink(string link,float chance){
		Link=link;
		RandomChance=chance;
	}
}	

