using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum SoldierState{Ally,Enemy,Sick}
public enum MovementType{Linear,Relative}

public delegate void HPchangedEvent(int hp_change);

public class SoldierMain : MonoBehaviour {
	
	public ChessboardGrid grid;
	public GameObject graphics,graphics_offset;
	public TowerMain EnemyTower,AllyTower,Target;
	
	public int x,y;
	public SoldierState State;
	
	public bool DEAD{get;private set;}
	public bool MOVING{get{return moving;}}
	public bool AI{get;set;}
	
	public bool Fleeing{get;private set;}
	
	MovementType movement_type=MovementType.Linear;
	
	public int attack_power=10;
	
	public HPchangedEvent HPchanged;
	int hp,hp_max;
	public int HP{
		get{
			return hp;
		}
		set{
			int change=value-hp;
			hp=value;
			if (hp<=0){
				hp=0;
				DEAD=true;
			}
			if (HPchanged!=null)
				HPchanged(change);
		}
	}
	
	public int HPMAX{
		get{
			return hp_max;
		}
		set{
			hp_max=value;
			hp=value;
		}
	}
	
	public void DestroyGraphics(){
		Destroy(graphics);
	}
	
	public void Flee(){
		Fleeing=true;
		Target=AllyTower;
	}
	
	//Use this for initialization
	void Start () {
		HPMAX=100;
		Fleeing=false;
		
	graphics_start_scale_y=graphics_offset.transform.localScale.y;
	}
	
	public bool updated_already_this_turn=false;
	bool in_an_update_ask_loop=false;

	public void UpdateTurn(List<SoldierMain> all_soldiers){
		if (!AI||DEAD||updated_already_this_turn||in_an_update_ask_loop) return;
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
			
			//attack
			SoldierMain unit=grid.GetUnit(x+xx,y+xy);
			if (unit!=null){
				bool attack=true;
				if (Fleeing){
					if (Subs.RandomPercent()>25){
						attack=false;
					}
				}
				if (attack&&State!=unit.State){
					//attack
					Attack (unit);
					return;
				}
			}
		}
		
		//move
		int mx=0,my=0;
		if (State==SoldierState.Sick){
			float min=200202002;
			SoldierMain closest=null;
			foreach(var s in all_soldiers){
				if (s==this||s.State==SoldierState.Sick) continue;
				float dis=Vector2.Distance(new Vector2(x,y),new Vector2(s.x,s.y));
				if (dis<min){
					min=dis;
					closest=s;
				}
			}
			
			if (closest==null){
				//wander aimlessly
				if (Subs.RandomPercent()<40){
					//do nothing
				}
				else{
					if (Subs.RandomPercent()<40){
						if (Subs.RandomBool())
							mx=-1;
						else
							mx=1;
					}
					else{
						if (Subs.RandomBool())
							my=-1;
						else
							my=1;
					}
				}
			}
			else{
				//move towards non infected.
				int tx=closest.x-x;
				int ty=closest.y-y;
				
				int ty_a=Mathf.Abs(ty);
				int tx_a=Mathf.Abs(tx);
				
				int ty_s=(int)Mathf.Sign(ty);
				int tx_s=(int)Mathf.Sign(tx);
				
				if (tx_a>ty_a){
					mx=tx_s;
				}
				else{
					my=ty_s;
				}
			}
		}
		else{
			
			int tx=Target.x-x;
			int ty=Target.y-y;
			
			int ty_a=Mathf.Abs(ty);
			int tx_a=Mathf.Abs(tx);
			
			int ty_s=(int)Mathf.Sign(ty);
			int tx_s=(int)Mathf.Sign(tx);
			
			
			{
				//move towards target.
				if (ty_a<8){
					
					if (ty_a==1){
						//move only x
						mx=tx_s;
					}
					else{
						//move x
						if (Subs.RandomPercent()<50){
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
			}
		}
		
		//move
		if (grid.GetPos(x+mx,y+my)){
			SoldierMain unit=grid.GetUnit(x+mx,y+my);
			if (unit!=null&&unit.State==State){
				in_an_update_ask_loop=true;
				unit.UpdateTurn(all_soldiers);
			}
		}
		
		if (!grid.GetPos(x+mx,y+my)){
			//Move 
			//SetPos(x+mx,y+my);
			Move(mx,my);
		}
		
		updated_already_this_turn=true;
		in_an_update_ask_loop=false;
	}
	
		
	// Update is called once per frame
	void Update () {
		
		/*
		if (Input.GetKeyDown(KeyCode.W)){
			Move(-1,0);
		}
		
		if (Input.GetKeyDown(KeyCode.S)){
			Move(1,0);
		}
		
		if (Input.GetKeyDown(KeyCode.A)){
			Move(0,-1);
		}
		
		if (Input.GetKeyDown(KeyCode.D)){
			Move(0,1);
		}
		
		if (Input.GetKeyDown(KeyCode.F)){
			//StartBlinking();
			StartBlinking();
		}*/
	}
	
	void FixedUpdate(){
		
		if (moving){
			
			float speed=1;
			if (movement_type==MovementType.Relative){
				speed=_dis_per_step;
			}
			
			var mv=move_v.normalized*speed*Time.deltaTime;
			
			transform.position=new Vector3(transform.position.x+mv.x,transform.position.y,transform.position.z+mv.y);
			
			move_length-=mv.magnitude;
			
			if (move_length<=0){
				moving=false;
				SetWPos(x,y);
			}
		}
	}
	
	public void Attack(SoldierMain unit){
		unit.Hit(attack_power,this);
		graphics_offset.transform.rotation=Quaternion.LookRotation(new Vector3(unit.x-x,0,unit.y-y),Vector3.up)*Quaternion.AngleAxis(-90,Vector3.up);
		StartCoroutine(AttackAnimation());
	}
	
	public void Hit(int power,SoldierMain attacker){
		HP-=power;
		
		if (DEAD) return;
		
		if (attacker.State==SoldierState.Sick){
			if (Subs.RandomPercent()<25){
				SetState(SoldierState.Sick);
			}
		}
		
		StartBlinking();
	}
	
	float move_tx,move_ty,move_length;
	bool moving=false;
	Vector2 move_v;
	float _anim_length;
	float _dis_per_step;
	
	
	public void MoveTo(int wx,int wy){
		Move (wx-x,wy-y);
	}
	
	public void Move(int rx,int ry){
		if (moving) return;
		StartCoroutine(MoveAnimation());
		moving=true;
	 	move_tx=x+rx+0.5f;
		move_ty=y+ry+0.5f;
		
		move_v=new Vector2(rx,ry);
		move_length=move_v.magnitude;
		
		SetGPos(x+rx,y+ry);
		
		_anim_length=graphics.animation.clip.length;
		_dis_per_step=move_length/(_anim_length);
	}
	
	IEnumerator AttackAnimation(){
		
		yield return new WaitForSeconds(Random.Range(0,250)*0.001f);
		graphics.animation.Play(attack_animation);
	}
	
	IEnumerator MoveAnimation(){
		if (movement_type==MovementType.Linear)
			yield return new WaitForSeconds(Random.Range(0,250)*0.001f);
		graphics.animation.Play(move_animation);
	}
	
	public string move_animation="Soldier_move",attack_animation="Soldier_attack";
	
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
		if (grid.GetUnit(x,y)==this) 
			grid.ClearPos(x,y);
		Destroy(gameObject);
	}
	
	
	public void SetState(SoldierState state){
		State=state;
		
		if (state==SoldierState.Ally){
			setColor(new Color(0.65f,0.65f,0.65f));
		}
		else
		if (state==SoldierState.Enemy){
			setColor(new Color(0.1f,0.1f,0.1f));
		}
		else
		if (state==SoldierState.Sick){
			setColor(GameController.ZombieColor);
			Fleeing=false;
		}
		
	}
	
	public void SetMovemenType(MovementType type){
		movement_type=type;
	}
	
	float graphics_start_scale_y;
	public void SquishGraphics (float dif)
	{
		graphics_offset.transform.localScale=new Vector3(graphics.transform.localScale.x,dif/graphics_start_scale_y,graphics.transform.localScale.z);
	}
}
