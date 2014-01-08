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
	
	public GameObject Parent;
	public UISprite spr;
	float Percent{set{spr.fillAmount=value;}}
	
	void HPchanged(int change){   
 		Percent=(float)target.HP/target.HPMAX;
	}
	
	public void SetVisible(bool visible){
		Parent.SetActive(visible);
	}
}
