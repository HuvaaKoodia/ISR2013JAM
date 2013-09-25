using UnityEngine;
using System.Collections;

public class StartScaler : MonoBehaviour {

	public float speed_multi=0.1f;
	bool starting=true,ending=false;
	
	public float min=0;
	public bool destroy_when_shrunk=true,linear_add=true;
	// Use this for initialization
	void Start () {
		setToMin();
	}
	
	// Update is called once per frame
	void Update () {
		if (starting){
			if (transform.localScale.x<1){
				if (linear_add)
					transform.localScale+=Vector3.one*Time.deltaTime*speed_multi;
				else
					transform.localScale+=Vector3.one*(1-transform.localScale.x)*Time.deltaTime*speed_multi;
			}
			else{
				starting=false;
				transform.localScale=Vector3.one;
			}
		}
		else if (ending){
			if (transform.localScale.x>min){
				if (linear_add)
					transform.localScale-=Vector3.one*Time.deltaTime*speed_multi;
				else
					transform.localScale-=Vector3.one*(transform.localScale.x)*Time.deltaTime*speed_multi;
					
			}
			else{
				ending=false;
				transform.localScale=Vector3.one*min;
				if (destroy_when_shrunk)
					Destroy(gameObject);
			}
		}
	}
	
	public void Enlarge ()
	{
		setToMin();
		starting=true;
	}
	
	public void Shrink ()
	{
		setToMax();
		ending=true;
	}

	public void stopAll ()
	{
		starting=ending=false;
	}

	public void setToMin()
	{
		transform.localScale=Vector3.one*min;
	}
	public void setToMax()
	{
		transform.localScale=Vector3.one*1f;
	}
}
