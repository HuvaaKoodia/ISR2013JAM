using UnityEngine;
using System.Collections;

public class KnightMain : MonoBehaviour {
	
	public SoldierMain Base;
	
	// Use this for initialization
	void Start () {
		Base.SetMovemenType(MovementType.Relative);
		Base.HP=500;
		Base.attack_power=25;
		
		Base.move_animation="Knight_move";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void RotateGraphics(int rx,int ry){
		Base.graphics_offset.transform.rotation=Quaternion.LookRotation(new Vector3(rx,0,ry),Vector3.up);
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
	
	public bool legitMovePosition(int wx,int wy){
		float dis= Vector2.Distance(new Vector2(Base.x,Base.y),new Vector2(wx,wy));
		dis=Mathf.Floor(dis);
		Debug.Log(dis);
		return dis<=2;
	}
	
	public bool IsSick(){
		return Base.State==SoldierState.Sick;;
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
