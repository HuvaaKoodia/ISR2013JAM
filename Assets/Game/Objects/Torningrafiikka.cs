using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Torningrafiikka : MonoBehaviour
{
	
	public GameObject Portti;
	bool MoveUp = false;
		
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//temppikoodia
		
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			OpenPortti ();
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			SmashPortti ();			
		}
		
		if (MoveUp == true) {
		
		
			if (Portti.transform.position.y < 2) {
				Portti.transform.position = new Vector3 (Portti.transform.position.x, Portti.transform.position.y + 1 * Time.deltaTime, Portti.transform.position.z);
			}
			
		}
		
		if (MoveUp == false) {
			
			if (Portti.transform.position.y > 0) {
				Portti.transform.position = new Vector3 (Portti.transform.position.x, Portti.transform.position.y - 1 * Time.deltaTime, Portti.transform.position.z);
				
			}
		}
	}
	
	public void OpenPortti ()
	{
		MoveUp = true;
	}
	
	public void ClosePortti ()
	{
		MoveUp = false;
	}
	
	public void SmashPortti ()
	{
		
		List<Transform> spikes=new List<Transform>();
		
		for (int i = 0; i < 4; i++)
		{
			Transform c = Portti.transform.GetChild (0);
			c.parent = null;
			c.rigidbody.isKinematic = false;
			spikes.Add(c);
		}
		
		foreach (Transform t in spikes){
			t.rigidbody.AddRelativeTorque (1000, 0, 0);
			t.rigidbody.AddExplosionForce (350, transform.position + Vector3.up*-0.4f, 60);
// does nuthin'			t.rigidbody.AddRelativeTorque (5000000, 5000000, 5000000);
			
		}
	}
}
