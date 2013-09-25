using UnityEngine;
using System.Collections;

public class TowerMain : MonoBehaviour {
	public int x,y,gate_x,gate_y;
	public ChessboardGrid grid;
	public Torningrafiikka graphics;
	
	public GameObject player_pos;
	// Use this for initialization
	
	public SoldierState State;
	
	
	public bool DEAD;
	int hp;
	public int HP{
		
		get{
			return hp;
		}
		set{
			hp=value;
			
			if (hp<=0){
				hp=0;
				DEAD=true;
				grid.SetPos(gate_x,gate_y,false);
				graphics.SmashPortti();
			}
			else{
				StartBlinking();
			}
		}
	}
	
	Color _color;
	
	void Start (){
		_color=new Color(0.9f,0.9f,0.9f);
		graphics.SetColor(_color);
		hp=100;
	}
	
	// Update is called once per frame
	void Update (){
		//temppikoodia
		/*if (Input.GetKeyDown (KeyCode.UpArrow)) {
			OpenGate();
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			CloseGate();	
		}*/
	}
	
	public void SetPos(int x,int y,bool face_up){
		grid.SetPos(x,y,true);
		transform.position=new Vector3(x+0.5f,0,y+0.5f);
		this.x=x;this.y=y;
		
		int y_off=0;
		if (face_up)
			y_off=-2;
		
		for (int i=0;i<3;i++){
			for (int j=0;j<3;j++){
				int xx,yy;
				
				xx=x-1+i;
				yy=y+y_off+j;
				
				grid.SetPos(xx,yy,true);
				grid.SetPos(xx,yy,true);
			}
		}
		gate_x=x;gate_y=y;
	}
	
	public void OpenGate(){
		grid.SetPos(gate_x,gate_y,false);
		graphics.OpenPortti();
	}
	public void CloseGate(){
		grid.SetPos(gate_x,gate_y,true);
		graphics.ClosePortti();
	}
	
	void StartBlinking(){
		StopCoroutine("Blink");
		graphics.SetColor(_color);
		on_point=true;
		StartCoroutine("Blink");
	}
	
	bool on_point=false;
	
	IEnumerator Blink(){
		for (int i=0;i<4;i++){
			Color c=_color;
			if (on_point)
				c=new Color(1f,0,0);
			on_point=!on_point;
			
			graphics.SetColor(c);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public bool IsAlly ()
	{
		return State==SoldierState.Ally;
	}
}
