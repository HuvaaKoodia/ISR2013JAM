using UnityEngine;
using System.Collections;

public class SoldierMain : MonoBehaviour {
	
	public ChessboardGrid grid;
	public GameObject graphics;
	public TowerMain Target;
	
	public int x,y;
	public bool AllyUnit;
	
	public bool DEAD{get;private set;}
	public bool MOVING{get{return moving;}}
	
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

	
	public void UpdateTurn(){
		
		//check for enemies
		for (int i=0;i<4;i++){
			int xx=0,xy=0;
			if (i==0)
				xx=1;
			if (i==1)
				xx=-1;
			if (i==2)
				xy=1;
			if (i==3)
				xy=-1;
			
			SoldierMain unit=grid.GetUnit(x+xx,y+xy);
			if (unit!=null){
				if (AllyUnit!=unit.AllyUnit){
					//attack
					unit.Hit(attack_power);
					return;
				}
			}
		}
		
		//move
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
			//SetPos(x+mx,y+my);
			Move(mx,my);
		}
	}
	
		
	// Update is called once per frame
	void Update () {
			
		if (Input.GetKeyDown(KeyCode.W)){
			//StartBlinking();
			Move(-1,0);
		}
		
		if (Input.GetKeyDown(KeyCode.S)){
			//StartBlinking();
			Move(1,0);
		}
		
		if (Input.GetKeyDown(KeyCode.A)){
			//StartBlinking();
			Move(0,-1);
		}
		
		if (Input.GetKeyDown(KeyCode.D)){
			//StartBlinking();
			Move(0,1);
		}
		
		if (Input.GetKeyDown(KeyCode.F)){
			//StartBlinking();
			StartBlinking();
		}
		
		
		if (moving){
			float x_speed=0,z_speed=0;
			
			if (move_x<0){
				x_speed=-1;
				if (move_tx>transform.position.x){
					moving=false;
					SetWPos(x,y);
				}
			}
			if (move_x>0){
				x_speed=+1;
				if (move_tx<=transform.position.x){
					moving=false;
					SetWPos(x,y);
				}
			}
			if (move_y<0){
				z_speed=-1;
				if (move_ty>transform.position.z){
					moving=false;
					SetWPos(x,y);
				}
			}
			if (move_y>0){
				z_speed=+1;
				if (move_ty<transform.position.z){
					moving=false;
					SetWPos(x,y);
				}
			}
			
			transform.position=new Vector3(transform.position.x+x_speed*Time.deltaTime,transform.position.y,transform.position.z+z_speed*Time.deltaTime);
		}
	}
	
	public void Hit(int power){
		HP-=power;
		StartBlinking();
	}
	
	float move_tx,move_ty,move_x,move_y;
	bool moving=false;
	
	public void Move(int rx,int ry){
		if (moving||grid.GetPos(x+rx,y+ry)) return;
		StartCoroutine(MoveAnimation());
		moving=true;
		move_x=rx;
		move_y=ry;
	 	move_tx=x+rx+0.5f;
		move_ty=y+ry+0.5f;
		
		SetGPos(x+rx,y+ry);
	}
	
	IEnumerator MoveAnimation(){
		yield return new WaitForSeconds(Random.Range(0,250)*0.001f);
		graphics.animation.Play ();
	}
	
	public void SetPos(int x,int y){
		SetGPos(x,y);
		SetWPos(x,y);
	}
	
	public void SetGPos(int x,int y){
		grid.ClearPos(this.x,this.y);
		
		grid.SetPos(x,y,true);
		grid.SetUnit(x,y,this);
		this.x=x;this.y=y;
		
	}
	
	public void SetWPos(int x,int y){
		transform.position=new Vector3(x+0.5f,0,y+0.5f);
	}
	
	Color _color;
	
	public void setColor(Color color){
		graphics.renderer.material.color=color;
	
		_color=color;
	}
	
	void StartBlinking(){
		StopCoroutine("Blink");
		graphics.renderer.material.color=_color;
		StartCoroutine("Blink");
	}
	
	IEnumerator Blink(){
		for (int i=0;i<4;i++){
			Color c=_color;
			if (graphics.renderer.material.color==_color)
				c=new Color(1f,0,0);
			
			graphics.renderer.material.color=c;
			
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void DIE ()
	{
		grid.ClearPos(x,y);
		Destroy(gameObject);
	}
}
