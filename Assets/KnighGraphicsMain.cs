using UnityEngine;
using System.Collections;

public class KnighGraphicsMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		var y_now=transform.position.y;
		y_speed=y_now-y_pos;
		y_pos=transform.position.y;

	}
	
	float y_speed,y_pos;
	
	void OnTriggerStay(Collider other){
		if (y_speed>=0) return;
		SoldierMain s=other.GetComponent<SoldierMain>();
		
		if (s!=null){
			var dif=transform.position.y-s.transform.position.y;
			s.SquishGraphics(dif);
			
		}
		
	}
}
