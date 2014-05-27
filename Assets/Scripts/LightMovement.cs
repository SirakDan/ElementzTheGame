using UnityEngine;
using System.Collections;

public class LightMovement : MonoBehaviour {

	private bool directionRight; 


	// Use this for initialization
	void Start () {
		directionRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		Light l = transform.GetComponent<Light>();
		if(directionRight){
			l.transform.Rotate(0, 1, 0);
			if(l.transform.rotation.y >= 0.5){directionRight = false;}
		}else{
			l.transform.Rotate(0, -1, 0);
			if(l.transform.rotation.y <= -0.5){directionRight = true;}
		}
	}
}
