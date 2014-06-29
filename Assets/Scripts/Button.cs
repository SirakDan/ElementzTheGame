using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	private GameObject text;

	private GameObject goAudio;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		text = transform.gameObject;
		goAudio = GameObject.Find ("Audio");
		audio = goAudio.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		text.GetComponent<TextMesh>().renderer.material.color = Color.gray;
	}

	void OnMouseExit(){
		text.GetComponent<TextMesh>().renderer.material.color = Color.white;
	}

	void OnMouseDown(){
		if(text.GetComponent<TextMesh> ().text.Equals ("Play")) {
			audio.Play();
			DontDestroyOnLoad(goAudio);
			Application.LoadLevel("Darkness_2");
		}else if(text.GetComponent<TextMesh> ().text.Equals ("Credits")){
			Application.LoadLevel("Credits");
		}
	}
}
