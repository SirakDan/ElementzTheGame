using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	private GameObject text;

	// Use this for initialization
	void Start () {
		text = transform.gameObject;
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
			Application.LoadLevel("Working");
		}else if(text.GetComponent<TextMesh> ().text.Equals ("Credits")){
			Application.LoadLevel("Credits");
		}
	}
}
