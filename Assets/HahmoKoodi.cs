using UnityEngine;
using System.Collections;

public class HahmoKoodi : MonoBehaviour {
	
		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey (KeyCode.W) )
			
		{transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1 * Time.deltaTime);}
		
		if (Input.GetKey (KeyCode.S) )
			
		{transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1 * Time.deltaTime);}
		
		if (Input.GetKey (KeyCode.D) )
		
		{transform.position = new Vector3 (transform.position.x + 1 * Time.deltaTime, transform.position.y, transform.position.z);}
			
		if (Input.GetKey (KeyCode.A) )
		
		{transform.position = new Vector3 (transform.position.x - 1 * Time.deltaTime, transform.position.y, transform.position.z);}
		
		
		
	}
}
