using UnityEngine;
using System.Collections;

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
			ClosePortti ();			
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
}
