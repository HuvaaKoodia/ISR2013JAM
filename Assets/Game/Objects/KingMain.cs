using UnityEngine;
using System.Collections;

public class KingMain : MonoBehaviour {
	
	public GameObject graphics;
	private Vector3 base_pos;
	
	// Use this for initialization
	void Start () {
		base_pos=graphics.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartTalking(){
		graphics.animation.Play("King_talk");
	}
	
	public void StopTalking(){
		if (is_dead) return;
		graphics.animation.Stop();
		graphics.transform.localPosition=base_pos;
		graphics.transform.localRotation=Quaternion.identity;
	}

	public void ZOMBIFY ()
	{
		graphics.renderer.material.color=GameController.ZombieColor;
	}
	
	bool is_dead=false;
	
	public void DIE()
	{
		is_dead=true;
		graphics.animation.Play ("King_death");
	}
}
