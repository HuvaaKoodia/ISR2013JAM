using UnityEngine;
using System.Collections;

public class HealthBarMain : MonoBehaviour {
	
	SoldierMain target;
	public SoldierMain Target{
		set{
			target=value;
			target.HPchanged+=HPchanged;
		}
	}
	public UISprite spr;
	float Percent{set{spr.fillAmount=value;}}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void HPchanged(int change){   
 		Percent=(float)target.HP/target.HPMAX;
	}
}
