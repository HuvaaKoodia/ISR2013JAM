using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public ChessboardGrid grid;
	public GameObject soldier_prefab;
	public TowerMain PlayerTower,EnemyTower;
	
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
		
		for (int i=0;i<20;i++){
			
			int x,y;
			do{
				x=Random.Range(0,grid.GridWidth);
				y=Random.Range(0,grid.GridHeight/2-2);
			}
			while(grid.GetPos(x,y));
			var go=Instantiate(soldier_prefab,Vector3.zero,Quaternion.identity) as GameObject;
			var sm=go.GetComponent<SoldierMain>();
			
			sm.grid=grid;
			sm.Target=EnemyTower;
			sm.SetPos(x,y);
			
			sm.setColor(new Color(0.65f,0.65f,0.65f));
			sm.AllyUnit=true;
			
			player_units.Add(sm);
			units.Add(sm);
		}
		
		//enemy units
		
		for (int i=0;i<10;i++){
			int x=Random.Range(0,grid.GridWidth);
			int y=Random.Range(grid.GridHeight/2+2,grid.GridHeight);
		
			var go=Instantiate(soldier_prefab,Vector3.zero,Quaternion.identity) as GameObject;
			var sm=go.GetComponent<SoldierMain>();
			
			sm.grid=grid;
			sm.Target=PlayerTower;
			sm.SetPos(x,y);
			sm.setColor(new Color(0.1f,0.1f,0.1f));
			sm.AllyUnit=false;
			
			enemy_units.Add(sm);
			units.Add(sm);
		}
	}
	bool turn_on=false,auto_turn;
	// Update is called once per frame
	void Update (){
		
		if (Input.GetKeyDown(KeyCode.Space)){
			auto_turn=!auto_turn;
		}
		
		if(auto_turn&&!turn_on){
			
			//update units
			for (int i=0;i<units.Count;i++){
				var s=units[i];
				s.UpdateTurn();
			}
			//destroy units
			
			for (int i=0;i<units.Count;i++){
				var s=units[i];

				if (s.DEAD){
					units.Remove(s);
					player_units.Remove(s);
					enemy_units.Remove(s);
					s.DIE();
					i--;
				}
			}
			turn_on=true;
			//auto_turn=false;
		}
		if (turn_on){
			bool all_good=true;
			for (int i=0;i<units.Count;i++){
				var s=units[i];
				if (s.MOVING){
					all_good=false;
					break;
				}
			}
			if (all_good)
				turn_on=false;
			
		}
	}
}
