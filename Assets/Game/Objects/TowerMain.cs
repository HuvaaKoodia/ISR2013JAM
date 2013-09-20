using UnityEngine;
using System.Collections;

public class TowerMain : MonoBehaviour {
	public int x,y;
	public ChessboardGrid grid;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update (){

		
	}
	
	public void SetPos(int x,int y,bool face_up){
		grid.SetPos(x,y,true);
		transform.position=new Vector3(x+0.5f,0,y+0.5f);
		this.x=x;this.y=y;
		
		int y_off=0;
		if (face_up)
			y_off=-2;
		
		for (int i=0;i<3;i++){
			for (int j=0;j<3;j++){
				int xx,yy;
				
				xx=x-1+i;
				yy=y+y_off+j;
				
				grid.SetPos(xx,yy,true);
				grid.SetPos(xx,yy,true);
			}
		}
		
		
	}
}
