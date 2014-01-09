using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public static Color ZombieColor=new Color(0.8f,0f,1f);
	public static Color EnemyColor=new Color(0.1f,0.1f,0.1f);
	public static Color AllyColor=new Color(0.8f,0.8f,0.8f);
	
	public ChessboardGrid grid;
	public GameObject soldier_prefab,knight_prefab;
	public TowerMain PlayerTower,EnemyTower;
	public CameraMain Player_camera;
	
	public HudManager hud_man;
	public BackToMenuMain back_to_menu;
	
	public HealthBarMain HealthBar;
	
	public KnightMain Player{get; private set;}
	List<SoldierMain> units,Player_units,enemy_units;
	
	bool gameover=false;
	
	public bool GAMEOVER{
		get{
			return gameover;
		}
		set{
			gameover=value;
			if (gameover){
				Player_camera.LOCK_INPUT=true;
			}
		}
	}
	
	int ally_max;
	int enemy_max;
	
	void OnMenuChange(){
		Player_camera.LOCK_INPUT=back_to_menu.INMENU;
	}
	
	void OnDialToggle(bool on){
		if (!gameover)
			HealthBar.SetVisible(!on);
	}
	
	// Use this for initialization
	void Start (){
		back_to_menu.OnChange+=OnMenuChange;
		hud_man.OnDialogueToggleEvent+=OnDialToggle;
		hud_man.gameover_hud.OnGameoverToggleEvent+=OnDialToggle;
		
		units=new List<SoldierMain>();
		Player_units=new List<SoldierMain>();
		enemy_units=new List<SoldierMain>();
		
		PlayerTower.grid=grid;
		EnemyTower.grid=grid;
		
		PlayerTower.SetPos(10,2,true);
		EnemyTower.SetPos(10,grid.GridHeight-3,false);
		//Player units
		
		var go=Instantiate(knight_prefab,Vector3.zero,Quaternion.identity) as GameObject;
		var km=go.GetComponent<KnightMain>();
		Player=km;
		HealthBar.Target=Player.Base;
		
		units.Add(Player.Base);
		
		Player.Base.AI=false;
		Player.Base.AllyTower=PlayerTower;
		Player.Base.EnemyTower=EnemyTower;
		
		Player.Base.grid=grid;
		Player.Base.SetState(EntityState.Ally);
		Player.Base.SetPos(10,6);
		
		Player_camera.Target=Player.transform;
		Player_camera.Offset=Vector3.up;
		
		ally_max=Random.Range(100,200);
		enemy_max=Random.Range(100,200);
		
		for (int i=0;i<ally_max;i++){
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
			
			sm.SetState(EntityState.Ally);
			
			Player_units.Add(sm);
			units.Add(sm);
			sm.AI=true;
		}
		
		//enemy units
		for (int i=0;i<enemy_max;i++){
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
			
			sm.SetState(EntityState.Enemy);
			
			enemy_units.Add(sm);
			units.Add(sm);
			sm.AI=true;
		}
		
		all_is_locked=true;
		t=new Timer(2000);
		
//		if (Subs.RandomPercent()>50)
//			camp_state=-1;
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
	
	private bool GetMouseSoldier(out SoldierMain soldier){
		var ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		int mask=1<<LayerMask.NameToLayer("Soldier");
		RaycastHit info;
		if (Physics.Raycast(ray,out info,500,mask)){
			soldier=info.collider.gameObject.GetComponent<SoldierMain>();
			return true;
		}
		soldier=null;
		return false;
	}
	
	bool Player_moving=false,units_moving=false;
	
	void PlayerActionDone(){
		if (turn_on) return;
		Player_moving=true;
		turn_on=true;
	}
	
	bool gameover_camera_sweep=false;
	bool player_won=true;
	
	// Update is called once per frame
	void Update (){
		
		if (gameover_camera_sweep){
			
			t.Update();
			
			if (t.OVER){
				GAMEOVER=true;
				if (!player_won)
					hud_man.gameover_hud.GAMEOVER("The King Is dead!");
				else
					hud_man.gameover_hud.GAMEOVER("The Plague King is captured!");
				
			}
			
			return;
		}
		
		//game start timer
		if (all_is_locked){
			t.Update();
			
			if (t.OVER){
				hud_man.King1Talk(hud_man.database.GetDialogueData("TheBattleBegins"));
				all_is_locked=false;
				t.Reset();
			}
		}
		
		if (GAMEOVER){
			if (Input.GetKey(KeyCode.Return)){
				//go to main menu
				Application.LoadLevel("MainMenuScene");
			}
			return;
		}
		
		if (hud_man.DIALOGUE_ON||all_is_locked) return;
				
		if (Input.GetButtonDown("Back To Menu")){
			back_to_menu.ToggleMenu();
		}
		
		if (back_to_menu.IsOn()) return;
		
		if (Input.GetButton("Wait")){
			PlayerActionDone();
		}
		
		//DEV.temp
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.M)){
			
			if (!turn_on&&Player!=null){
				Vector2 tile_pos;
				
				if (GetMouseTilePos(out tile_pos)){
					if (Player.MoveToHAX((int)tile_pos.x,(int)tile_pos.y)){
						
						if (!PlayerInGateEnemy()&&!PlayerInGateEnemy()){
							Player_in_tower=false;
						}
						
						PlayerActionDone();
					}
				}
			}
		}
	
		if (Input.GetKeyDown(KeyCode.X)){
			 EnemiesFlee();
		}
		
		if (Input.GetKeyDown(KeyCode.Z)){
			EnemiesPlague();
		}
		
		if (Input.GetKeyDown(KeyCode.B)){
			Player.Base.SetState(EntityState.Sick);
		}
		
		if (Input.GetKeyDown(KeyCode.C)){
			MeetEnemyKing();
			
		}
		if (Input.GetKeyDown(KeyCode.V)){
			MeetKing();
		}
#endif
		//mouse input
		
		if (Input.GetButtonDown("Interact & Move")){
			
			if (!turn_on&&Player!=null){
				TowerMain torni;
				SoldierMain soldier;
				Vector2 tile_pos;
				bool moving=true; 
				
				if (GetMouseSoldier(out soldier)){
					if (Player.Base!=soldier){
						if (Player.legitInteractPosition(soldier.x,soldier.y)){
							if (soldier.State==EntityState.Ally){
								if (Player.IsSick()){
									hud_man.SoldierTalk(soldier,hud_man.database.SoldierAllyRandomStartZombie);
								}
								else{
									hud_man.SoldierTalk(soldier,hud_man.database.SoldierAllyRandomStart);
								}
								
							}
							moving=false;
						}
					}
				}
				if (GetMouseTower(out torni)){
					if (!torni.DEAD&&!torni.GATEOPEN&&Player.legitInteractPosition(torni.gate_x,torni.gate_y)){
						if (torni.IsAlly()){
							TalkToKing();
						}
						else{
							TalkToEnemyKing();
						}
						moving=false;
					}
				}
				
				if (moving&&GetMouseTilePos(out tile_pos)){
					if (Player.MoveTo((int)tile_pos.x,(int)tile_pos.y)){
						
						if (!PlayerInGateEnemy()&&!PlayerInGateEnemy()){
							Player_in_tower=false;
						}
						
						PlayerActionDone();
					}
				}
			}
		}
		
		if (Input.GetButtonDown("Attack")){
			
			if (!turn_on&&Player!=null){
				TowerMain torni;
				Vector2 tile_pos;
				
				if (GetMouseTower(out torni)){
					if (!torni.DEAD&&Player.legitInteractPosition(torni.gate_x,torni.gate_y)){
						torni.HP-=Player.Base.attack_power;
						PlayerActionDone();
					}
				}
				
				if (!turn_on&&GetMouseTilePos(out tile_pos)){
					if (Player.AttackTo((int)tile_pos.x,(int)tile_pos.y))
						PlayerActionDone();
				}
			}
		}
		
		if (turn_on){
			if (Player_moving){
				if (!Player.Base.MOVING){
					Player_moving=false;
					
					//goto towers
					if (!Player_in_tower){
						if (PlayerInGateAlly()){
							MeetKing();
						}
						if (PlayerInGateEnemy()){
							MeetEnemyKing();
						}
					}
					
					//Close enemy gate
					if (EnemyTower.GATEOPEN){
						foreach (var u in units){
							if (u.x==EnemyTower.gate_x&&u.y==EnemyTower.gate_y-1){
								EnemyTower.CloseGate();
							}
						}
					}
					
					//update units
					for (int i=0;i<units.Count;i++){
						var s=units[i];
						s.UpdateTurn(units);
						
						if (s.GATEGONNER==true){
							var pos=s.transform.position+new Vector3(2,3f,-3);
							player_won=true;
							if (s.EnemyTower==PlayerTower){
								player_won=false;
								pos=s.transform.position+new Vector3(2,3f,3);
							}
							Player_camera.MoveToCameraPos(pos,Quaternion.LookRotation(s.transform.position-pos,Vector3.up));
							gameover_camera_sweep=true;
							t.Delay=3000;
							t.Reset();
						}
					}
					//destroy units
					for (int i=0;i<units.Count;i++){
						var s=units[i];
		
						if (s.DEAD){
							if (s==Player.Base){
								
							}
							else{
								s.DIE();
							}
							units.Remove(s);
							Player_units.Remove(s);
							enemy_units.Remove(s);
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
					//turn over
					units_moving=false;
					turn_on=false;
					
					if (Player.Base.DEAD){
						GAMEOVER=true;
						Player.PlayDeathAnim();
						hud_man.gameover_hud.GAMEOVER("You are dead!");
					}
					else{
						Player.TurnEndUpdate();
					}
				}
			}
		}
		
		//GAME script
		if (!turn_on){
			if ( camp_state==0){
				//flee!
				if (enemy_units.Count<Player_units.Count&&enemy_units.Count<=enemy_max*0.5f){
					EnemiesFlee();
					camp_state++;
				}
			}
			if ( camp_state==1){
				//plague!
				if (enemy_units.Count<Player_units.Count&&enemy_units.Count<=enemy_max*0.25f){
					
					EnemiesPlague();
					camp_state++;
				}
			}
		}
	}
	
	void MeetKing(){
		Player.transform.position=PlayerTower.player_pos.transform.position;
		Player.RotateGraphics(-90);
		
		if (Player.IsSick()){
			hud_man.King1Talk(hud_man.database.ZOMBIE_MeetingTheReverendKing);
		}
		else
			hud_man.King1Talk(hud_man.database.MeetingTheReverendKing);
		Player_in_tower=true;
	}
	
	void MeetEnemyKing(){
		Player.transform.position=EnemyTower.player_pos.transform.position;
		Player.RotateGraphics(-90);
		
		if (Player.IsSick()){
			hud_man.King2Talk(hud_man.database.ZOMBIE_MeetingThePlagueKing);
		}
		else
			hud_man.King2Talk(hud_man.database.MeetingThePlagueKing);
		Player_in_tower=true;
	}
	
	int k1_i=0,k2_i=0;
	void TalkToKing(){
		if (!Player.IsSick()){
			if (k1_i==0)
				hud_man.King1Talk(hud_man.database.GetDialogueData("AtTheGateOfTheReverendKing"));
			else
				hud_man.King1Talk(hud_man.database.GetDialogueData("AtTheGateOfTheReverendKingNoComment"));
			 k1_i++;
		}
	}
	void TalkToEnemyKing(){
		if (!Player.IsSick()){
			if (k2_i==0)
				hud_man.King2Talk(hud_man.database.GetDialogueData("AtTheGateOfThePlagueKing"));
			else
				hud_man.King2Talk(hud_man.database.GetDialogueData("AtTheGateOfThePlagueKingNoComment"));
			 k2_i++;
		}
	}
	
	bool Player_in_tower;
	int camp_state=0;
	
	bool PlayerInGateEnemy(){
		return Player.Base.x==EnemyTower.gate_x&&Player.Base.y==EnemyTower.gate_y;
	}
	
	bool PlayerInGateAlly(){
		return Player.Base.x==PlayerTower.gate_x&&Player.Base.y==PlayerTower.gate_y;
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
				while (unit.State==EntityState.Sick);
				
				unit.SetState(EntityState.Sick);
			}
		
	}

	public void GOTO_PLAYERTOWER_BASE ()
	{

		Player.Base.SetPos(PlayerTower.gate_x,PlayerTower.gate_y);
		Player.RotateGraphics(0);
	}
	public void GOTO_ENEMYTOWER_BASE ()
	{
		Player.Base.SetPos(EnemyTower.gate_x,EnemyTower.gate_y);
		Player.RotateGraphics(180);
	}
	
}
