using UnityEngine;
using System.Collections;

public class KnightMain : MonoBehaviour {
	
	public SoldierMain Base;
	GameOptionsDatabase.KnightMovementType MovementType;

	// Use this for initialization
	void Start () {
		Base.SetMovemenType(SoldierMain.MovementType.Relative);
		Base.HPMAX=500;
		Base.attack_power=25;
		
		Base.move_animation="Knight_move";
		
		is_sick_tick=is_sick_tick_max;

		var GODB=GameObject.FindGameObjectWithTag("GameOptions").GetComponent<GameOptionsDatabase>();
		MovementType=GODB.KnightMovement;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void TurnEndUpdate(){
		if (IsSick()){
			is_sick_tick--;
			if(is_sick_tick<=0){
				Base.HP-=sickness_hp_dec;
				Base.StartBlinking(Color.green);
				is_sick_tick=is_sick_tick_max;
			}
		}
	}
	
	int sickness_hp_dec=5,is_sick_tick=0,is_sick_tick_max=2;
	
	public void RotateGraphics(int rx,int ry){
		Base.graphics_offset.transform.rotation=Quaternion.LookRotation(new Vector3(rx,0,ry),Vector3.up);
	}
	
	public void RotateGraphics(int angle){
		Base.graphics_offset.transform.rotation=Quaternion.AngleAxis(angle,Vector3.up);
	}
	
	public bool Move(int rx,int ry){
		if (!Base.grid.GetPos(Base.x+rx,Base.y+ry)&&legitMovePosition(Base.x+rx,Base.y+ry)){
			//move
			Base.Move(rx,ry);
			RotateGraphics(rx,ry);
			return true;
		}
		return false;
	}

	public bool AttackTo(int wx,int wy){
		if (Base.grid.GetPos(wx,wy)&&legitMovePosition(wx,wy)){
			//dmg and knockback DEV.IMP
			SoldierMain unit=Base.grid.GetUnit(wx,wy);
		
			if (unit==null||unit==Base) return false;
			unit.Hit(1000,Base);
			
			//move
			RotateGraphics(wx-Base.x,wy-Base.y);
			Base.MoveTo(wx,wy);
			return true;
		}
		return false;
	}
	
	public bool MoveTo(int wx,int wy){
		return Move(wx-Base.x,wy-Base.y);
	}
	public bool Attack(int rx,int ry){
		return AttackTo(Base.x+rx,Base.y+ry);
	}

	public bool legitInteractPosition(int wx,int wy){
		float dis= Vector2.Distance(new Vector2(Base.x,Base.y),new Vector2(wx,wy));
		dis=Mathf.Floor(dis);
		//Debug.Log(dis);
		return dis<=2;
	}

	public bool legitMovePosition(int wx,int wy){
		if (MovementType==GameOptionsDatabase.KnightMovementType.Freeform){
			return legitInteractPosition(wx,wy);
		}
		else{//chesslike
			int x_abs=Mathf.Abs(wx-(int)Base.x);
			int y_abs=Mathf.Abs(wy-(int)Base.y);

			return (x_abs==2&&y_abs==1)||(x_abs==1&&y_abs==2);
		}
	}
	
	public bool IsSick(){
		return Base.State==EntityState.Sick;
	}
	
	public void PlayDeathAnim(){
		Base.graphics.animation.Play("Knight_death");
	}
	
	
	//DEV:TEMP
	
	public bool MoveHAX(int rx,int ry){
		if (!Base.grid.GetPos(Base.x+rx,Base.y+ry)){
			//move
			Base.Move(rx,ry);
			RotateGraphics(rx,ry);
			return true;
		}
		return false;
	}
	public bool MoveToHAX(int wx,int wy){
		return MoveHAX(wx-Base.x,wy-Base.y);
	}

}
