using UnityEngine;
using System.Collections;

public class TorchTrigger : MonoBehaviour {
	
	public float ActTime = 3f;
	private float InitTime;
	[HideInInspector]
	public bool switchOn;
	private bool mutex;
	
	// Use this for initialization
	void Start () {
		switchOn = false;
		mutex = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (switchOn) {
			if (!mutex) { InitTime = Time.time; mutex = true; }
			if (Time.time > InitTime + ActTime) {
				Switch("off");
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "FireBall") {;
			Switch("on");
		}
	}
	
	void Switch(string s) {
		switch(s) {
		case "on": 
			GetComponent<Light>().range = 10;
			transform.GetChild(0).GetComponent<ParticleSystem>().Play();
			switchOn = true;
			mutex = false;
			break;
		case "off": 
			GetComponent<Light>().range = 0;
			transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
			switchOn = mutex = false;
			break;
		default: break;
		}
	}

}
