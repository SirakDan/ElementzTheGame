using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	private GameObject credits;


	// Use this for initialization
	void Start () {
		credits = transform.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		credits.transform.position = new Vector3(credits.transform.position.x, credits.transform.position.y + 0.05f, credits.transform.position.z);
		if(credits.transform.position.y > 20 || Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("MainTitle");
		}
	}
}
