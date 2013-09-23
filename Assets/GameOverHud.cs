using UnityEngine;
using System.Collections;

public class GameOverHud : MonoBehaviour {
	
	public UISprite sprite;
	public UILabel gameover_label,desc_label,other_label;
	
	// Use this for initialization
	void Start (){
		sprite.alpha=0;
		gameover_label.alpha=0;
		desc_label.alpha=0;
		other_label.alpha=0;
		//sprite.transform.localScale=new Vector3(Screen.width+64,Screen.height+64,0);
		sprite.transform.localScale=new Vector3(Screen.width*2+64,Screen.height*2+64,0);
	}
	
	// Update is called once per frame
	void Update () {}
	
	public void GAMEOVER(string description){
		StartCoroutine(FadeAlpha());
		desc_label.text=description;
	}
	
	IEnumerator FadeAlpha(){
		
		while(true){
			if (sprite.alpha>=1){
				sprite.alpha=gameover_label.alpha=desc_label.alpha=other_label.alpha=1;
				break;	
			}
			
			yield return new WaitForSeconds(Time.deltaTime);
		
			sprite.alpha+=0.01f;
			gameover_label.alpha=desc_label.alpha=other_label.alpha=sprite.alpha;
		}
	}
}
