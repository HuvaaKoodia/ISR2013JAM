using UnityEngine;
using System.Collections;

public class AnswerButtonMain : MonoBehaviour {
	
	public SpeechBubbleMain Base;
	public DialogueData Data;
	public ButtonPressedScr bps;
	
	public float y_size;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetData(DialogueData data){
		Data=data;
		
		Base.text_label.text=Subs.autofit_text(data.Text,300,Base.text_label.font);
		var size=Base.text_label.font.CalculatePrintedSize(Base.text_label.text,false,UIFont.SymbolStyle.Uncolored);
		
		
		y_size=size.y*40;
		Base.sprite.transform.localScale=new Vector3(300,y_size,0);
		
		
	}
	
	
}
