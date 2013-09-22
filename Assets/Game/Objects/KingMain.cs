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
		graphics.animation.Play();
	}
	
	public void StopTalking(){
		
		graphics.animation.Stop();
		graphics.transform.localPosition=base_pos;
		graphics.transform.localRotation=Quaternion.identity;
	}
}
