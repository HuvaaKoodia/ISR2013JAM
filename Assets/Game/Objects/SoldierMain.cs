using UnityEngine;
using System.Collections;

public class SoldierMain : MonoBehaviour {
	
	public ChessboardGrid grid;
	public GameObject graphics;
	public TowerMain Target;
	
	public int x,y;
	public bool AllyUnit;
	
	public bool DEAD{get;private set;}
	
	int attack_power=10;
	
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
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		hp=100;
	}
	
	// Update is called once per frame
	void Update () {
			
		if (Input.GetKeyDown(KeyCode.A)){
			StartBlinking();
		}
	}
	
	public void UpdateTurn(){
		
		int tx=Target.x-x;
		int ty=Target.y-y;
		
		int ty_a=Mathf.Abs(ty);
		int tx_a=Mathf.Abs(tx);
		
		int ty_s=(int)Mathf.Sign(ty);
		int tx_s=(int)Mathf.Sign(tx);
		
		int mx=0,my=0;
		
		if (ty_a<8){
			
			if (ty_a==1){
				//move only x
				mx=tx_s;
			}
			else{
				//move x
				if (Subs.RandomPercent()<30){
					mx=tx_s;
				}
				else{
					my=ty_s;
				}
			}
					
		}
		else{
			my=ty_s;
		}
		
		if (!grid.GetPos(x+mx,y+my)){
			//Move
			SetPos(x+mx,y+my);
		}
		else{
			SoldierMain unit=grid.GetUnit(x+mx,y+my);
			if (unit!=null){
				if (AllyUnit!=unit.AllyUnit){
					//attack
					unit.Hit(attack_power);
				}
			}
			
		}

	}
	
	public void Hit(int power){
		hp-=power;
		
	}
	
	public void SetPos(int x,int y){
		grid.ClearPos(this.x,this.y);
		
		grid.SetPos(x,y,true);
		grid.SetUnit(x,y,this);
		transform.position=new Vector3(x+0.5f,0,y+0.5f);
		
		this.x=x;this.y=y;
		
	}
	
	Color _color;
	
	public void setColor(Color color){
		graphics.renderer.material.color=color;
	
		_color=color;
	}
	
	void StartBlinking(){
		
		StartCoroutine(Blink());
	}
	
	IEnumerator Blink(){
		while(true){
			Debug.Log("asdasdasd!!");
			yield return new WaitForSeconds(100);
		}
	}
	
	IEnumerator Blink2(){
		for (int i=0;i<4;i++){
			Color c=_color;
			if (graphics.renderer.material.color==_color)
				c=new Color(1f,0,0);
			
			graphics.renderer.material.color=c;
			
			yield return new WaitForSeconds(100);
		}
	}
}
