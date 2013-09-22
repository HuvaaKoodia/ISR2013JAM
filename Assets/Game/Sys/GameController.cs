using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public ChessboardGrid grid;
	public GameObject soldier_prefab,knight_prefab;
	public TowerMain PlayerTower,EnemyTower;
	public CameraMain player_camera;
	
	public HudManager hud_man;
	
	public KnightMain player;
	List<SoldierMain> units,player_units,enemy_units;
	
	// Use this for initialization
	void Start () {
		units=new List<SoldierMain>();
		player_units=new List<SoldierMain>();
		enemy_units=new List<SoldierMain>();
		
		PlayerTower.grid=grid;
		EnemyTower.grid=grid;
		
		PlayerTower.SetPos(10,2,true);
		EnemyTower.SetPos(10,grid.GridHeight-3,false);
		//player units
		
		var go=Instantiate(knight_prefab,Vector3.zero,Quaternion.identity) as GameObject;
		var km=go.GetComponent<KnightMain>();
		player=km;
		
		units.Add(player.Base);
		
		player.Base.AI=false;
		
		player.Base.grid=grid;
		player.Base.SetState(SoldierState.Ally);
		player.Base.SetPos(10,6);
		
		player_camera.Target=player.transform;
		player_camera.Offset=Vector3.up;
		
		for (int i=0;i<200;i++){
			
			int x,y;
			do{
				x=Random.Range(0,grid.GridWidth);
				y=Random.Range(0,grid.GridHeight/2-2);
			}
			while(grid.GetPos(x,y));
			go=Instantiate(soldier_prefab,Vector3.zero,Quaternion.identity) as GameObject;
			var sm=go.GetComponent<SoldierMain>();
			
			sm.grid=grid;
			sm.Target=EnemyTower;
			sm.EnemyTower=EnemyTower;
			sm.AllyTower=PlayerTower;
			sm.SetPos(x,y);
			
			sm.SetState(SoldierState.Ally);
			
			player_units.Add(sm);
			units.Add(sm);
			sm.AI=true;
		}
		
		//enemy units
		for (int i=0;i<150;i++){
			int x,y;
			do{
				x=Random.Range(0,grid.GridWidth);
				y=Random.Range(grid.GridHeight/2+5,grid.GridHeight);
			}
			while(grid.GetPos(x,y));
			
			go=Instantiate(soldier_prefab,Vector3.zero,Quaternion.identity) as GameObject;
			var sm=go.GetComponent<SoldierMain>();
			
			sm.grid=grid;
			sm.Target=PlayerTower;
			sm.AllyTower=EnemyTower;
			sm.EnemyTower=PlayerTower;
			sm.SetPos(x,y);
			
			sm.SetState(SoldierState.Enemy);
			
			enemy_units.Add(sm);
			units.Add(sm);
			sm.AI=true;
		}
	}
	bool turn_on=false,auto_turn;
	
	private bool GetMouseTilePos(out Vector2 tile){
		var ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		int mask=1<<LayerMask.NameToLayer("ChessBoard");
		RaycastHit info;
		if (Physics.Raycast(ray,out info,500,mask)){
			//calculate correct tile
			int x=(int)Mathf.Floor(info.point.x);
			int y=(int)Mathf.Floor(info.point.z);
			
			tile=new Vector2(x,y);
			return true;
		}
		tile=new Vector2(0,0);
		return false;
	}
	
	private bool GetMouseTower(out TowerMain tower){
		var ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		int mask=1<<LayerMask.NameToLayer("Tower");
		RaycastHit info;
		if (Physics.Raycast(ray,out info,500,mask)){
			tower=info.collider.gameObject.GetComponent<TowerMain>();
			return true;
		}
		tower=null;
		return false;
	}
	bool player_moving=false,units_moving=false;
	
	void PlayerActionDone(){
		if (turn_on) return;
		player_moving=true;
		turn_on=true;
	}
	
	// Update is called once per frame
	void Update (){
		
		if (hud_man.DIALOGUE_ON) return;
		
		if (Input.GetKey(KeyCode.Space)){
			PlayerActionDone();
		}
		
		//DEV.temp
		
		
		if (Input.GetKeyDown(KeyCode.X)){
			
			int a=enemy_units.Count/2;
			for (int i=0;i<a;i++){
				
				SoldierMain unit;
				int c=enemy_units.Count*4;
				
				do{
					unit=enemy_units[Random.Range(0,enemy_units.Count)];
					c--;
					if (c<=0)
						break;
				}
				while (unit.State==SoldierState.Sick);
				
				unit.SetState(SoldierState.Sick);
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Z)){
			foreach (var u in enemy_units){
				u.Flee();
			}
		}

		//mouse input
		
		if (Input.GetMouseButtonDown(0)){
			
			if (!turn_on&&player!=null){
				Vector2 tile_pos;
				
				
				if (GetMouseTilePos(out tile_pos)){
					if (player.MoveTo((int)tile_pos.x,(int)tile_pos.y))
						PlayerActionDone();
				}
			}
		}
		
		if (Input.GetMouseButtonDown(1)){
			
			if (!turn_on&&player!=null){
				
				TowerMain torni;
				Vector2 tile_pos;
				
				if (GetMouseTower(out torni)){
					if (!torni.DEAD&&player.legitMovePosition(torni.gate_x,torni.gate_y)){
						torni.HP-=player.Base.attack_power;
						PlayerActionDone();
					}
				}
				
				if (!turn_on&&GetMouseTilePos(out tile_pos)){
					if (player.AttackTo((int)tile_pos.x,(int)tile_pos.y))
						PlayerActionDone();
				}
			}
		}
		
		if (turn_on){
			if (player_moving){
				if (!player.Base.MOVING){
					player_moving=false;
					
					//update units
					for (int i=0;i<units.Count;i++){
						var s=units[i];
						s.UpdateTurn(units);
					}
					//destroy units
					
					for (int i=0;i<units.Count;i++){
						var s=units[i];
		
						if (s.DEAD){
							if (s==player)
								player=null;
							units.Remove(s);
							player_units.Remove(s);
							enemy_units.Remove(s);
							s.DIE();
							i--;
							
						}
						s.updated_already_this_turn=false;
					}
					units_moving=true;
				}
			}
			
			if(units_moving){
				bool all_good=true;
				for (int i=0;i<units.Count;i++){
					var s=units[i];
					if (s.MOVING){
						all_good=false;
						break;
					}
				}
				if (all_good){
					units_moving=false;
					turn_on=false;
				}
			}
		}
	}
}
