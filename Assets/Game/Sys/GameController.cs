﻿using UnityEngine;
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
	
	public bool GAMEOVER=false;
	
	// Use this for initialization
	void Start (){
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
		
		all_is_locked=true;
		t=new Timer(3000);
	}
	Timer t=new Timer();
	bool all_is_locked=false;
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
		
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("SkeneMainMenu");
		}
		
		
		if (all_is_locked){
			t.Update();
			
			if (t.OVER){
				hud_man.King1Talk(hud_man.database.TheBattleBegins);
				all_is_locked=false;
			}
		}
		
		if (GAMEOVER){
			if (Input.GetKey(KeyCode.Return)){
				//go to main menu
				Application.LoadLevel("SkeneMainMenu");
			}
			return;
		}
		
		if (hud_man.DIALOGUE_ON||all_is_locked) return;
		
		if (Input.GetKey(KeyCode.Space)){
			PlayerActionDone();
		}
		
		//DEV.temp
		/*
		if (Input.GetKeyDown(KeyCode.X)){
			 EnemiesFlee();
			
		}
		
		if (Input.GetKeyDown(KeyCode.Z)){
			EnemiesPlague();
		}
		
		if (Input.GetKeyDown(KeyCode.C)){
			MeetEnemyKing();
			
		}
		
		if (Input.GetKeyDown(KeyCode.V)){
			MeetKing();
		}*/

		//mouse input
		
		if (Input.GetMouseButtonDown(0)){
			
			if (!turn_on&&player!=null){
				Vector2 tile_pos;
				
				
				if (GetMouseTilePos(out tile_pos)){
					if (player.MoveTo((int)tile_pos.x,(int)tile_pos.y)){
						
						if (!playerInGateEnemy()&&!playerInGateEnemy()){
							player_in_tower=false;
						}
						
						PlayerActionDone();
					}
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
					
					
					if (player.Base.DEAD){
						
						GAMEOVER=true;
						hud_man.gameover_hud.GAMEOVER("You are dead!");
					}
				}
			}
		}
		
		
		
		//GAME script
		
		if ( camp_state==0){
			//start dialogue
			if (enemy_units.Count<=100){
				
				EnemiesFlee();
				camp_state++;
			}
		}
		if ( camp_state==1){
			//start dialogue
			if (enemy_units.Count<=50){
				
				EnemiesPlague();
				camp_state++;
			}
		}
		
		if (!player.Base.MOVING&&!player_in_tower){
			
			
			if (playerInGateAlly()){
				//go to king
				MeetKing();
			}
			
			if (playerInGateEnemy()){
				//go to enemy king
				MeetEnemyKing();
			}
		}
	}
	
	void MeetKing(){
		player.transform.position=PlayerTower.player_pos.transform.position;
		player.Base.graphics.transform.rotation=Quaternion.AngleAxis(180,Vector3.up);
		hud_man.King1Talk(hud_man.database.MeetingTheReverendKing);
		player_in_tower=true;
	}
	
	void MeetEnemyKing(){
		player.transform.position=EnemyTower.player_pos.transform.position;
		player.Base.graphics.transform.rotation=Quaternion.AngleAxis(180,Vector3.up);
		hud_man.King2Talk(hud_man.database.MeetingThePlagueKing);
		player_in_tower=true;
	}
	
	bool player_in_tower;
	int camp_state=0;
	
	bool playerInGateEnemy(){
		return player.Base.x==EnemyTower.gate_x&&player.Base.y==EnemyTower.gate_y;
	}
	
	bool playerInGateAlly(){
		return player.Base.x==PlayerTower.gate_x&&player.Base.y==PlayerTower.gate_y;
	}
	
	void EnemiesFlee(){
		hud_man.King1Talk(hud_man.database.TheTideTurns);
		foreach (var u in enemy_units){
				u.Flee();
			}
	}
	
	void EnemiesPlague(){
		hud_man.King1Talk(hud_man.database.ThePlague);
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

	public void GOTO_PLAYERTOVER_BASE ()
	{
		//player.transform.position=new Vector3(PlayerTower.gate_x,0,PlayerTower.gate_y);
		
		player.Base.SetPos(PlayerTower.gate_x,PlayerTower.gate_y);
		player.Base.graphics.transform.rotation=Quaternion.AngleAxis(-90,Vector3.up);
	}
	
}
