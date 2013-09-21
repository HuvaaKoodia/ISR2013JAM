using UnityEngine;
using System.Collections;

public class ChessboardGrid : MonoBehaviour {
	
	public int GridWidth,GridHeight;
	
	bool[,] grid;
	SoldierMain[,] unit_grid;
	
	// Use this for initialization
	void Awake (){
		grid=new bool[GridWidth,GridHeight];
		unit_grid=new SoldierMain[GridWidth,GridHeight];
	}
	
	//Update is called once per frame
	void Update() {
	
	}
	
	public bool GetPos(int x,int y){
		if (outsideGrid(x,y)) return true;
		return grid[x,y];
	}
	
	public void SetPos(int x,int y,bool v){
		if (outsideGrid(x,y)) return;
		grid[x,y]=v;
	}
	
	public void ClearPos(int x,int y){
		if (outsideGrid(x,y)) return;
		grid[x,y]=false;
		unit_grid[x,y]=null;
	}
	
	public SoldierMain GetUnit(int x,int y){
		if (outsideGrid(x,y)) return null;
		return unit_grid[x,y];
	}
	
	public void SetUnit(int x,int y,SoldierMain soldier){
		if (outsideGrid(x,y)) return;
		unit_grid[x,y]=soldier;
	}
	
	private bool outsideGrid(int x,int y){
		return (!Subs.insideArea(new Vector2(x,y),new Rect(0,0,GridWidth,GridHeight)));
	}
}
