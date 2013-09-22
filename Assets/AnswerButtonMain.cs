using UnityEngine;
using System.Collections;

public class AnswerButtonMain : MonoBehaviour {
	
	public SpeechBubbleMain Base;
	public DialogueData Data;
	public ButtonPressedScr bps;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetData(DialogueData data){
		Data=data;
		
		Base.text_label.text=data.Text;
		float size=Base.text_label.font.CalculatePrintedSize(data.Text,false,UIFont.SymbolStyle.Uncolored).x*Base.text_label.font.size;
		Base.sprite.transform.localScale=new Vector3(Mathf.Max(300,size),Base.sprite.transform.localScale.y,0);
	}
	
	
}
