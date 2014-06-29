using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		Vector3 temp2 = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, Camera.main.nearClipPlane));
		transform.position = new Vector3 (temp2.x, temp2.y, temp.z);;
	}

}
