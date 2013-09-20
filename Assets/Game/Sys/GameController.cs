using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public ChessboardGrid grid;
	public GameObject soldier_prefab;
	public TowerMain PlayerTower,EnemyTower;
	
	List<SoldierMain> player_units,enemy_units;
	
	// Use this for initialization
	void Start () {
		player_units=new List<SoldierMain>();
		enemy_units=new List<SoldierMain>();
		
		PlayerTower.grid=grid;
		EnemyTower.grid=grid;
		
		PlayerTower.SetPos(10,3,true);
		EnemyTower.SetPos(10,grid.GridHeight-3,false);
		//player units
		
		for (int i=0;i<10;i++){
			int x=Random.Range(0,grid.GridWidth);
			int y=Random.Range(0,grid.GridHeight/2-2);
		
			var go=Instantiate(soldier_prefab,Vector3.zero,Quaternion.identity) as GameObject;
			var sm=go.GetComponent<SoldierMain>();
			
			sm.grid=grid;
			sm.Target=EnemyTower;
			sm.SetPos(x,y);
			player_units.Add(sm);
			sm.setColor(new Color(0.65f,0.65f,0.65f));
			sm.AllyUnit=true;
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
			enemy_units.Add(sm);
			sm.setColor(new Color(0.1f,0.1f,0.1f));
			sm.AllyUnit=false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space)){
			for (int i=0;i<player_units.Count;i++){
				player_units[i].UpdateTurn();	
			}
			
			for (int i=0;i<enemy_units.Count;i++){
				enemy_units[i].UpdateTurn();	
			}
		}
	}
}
