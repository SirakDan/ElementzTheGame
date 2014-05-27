using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

	public string scene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, -1);

		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		Player player = (Player)go.GetComponent (typeof(Player));

		float distance = Mathf.Sqrt ( Mathf.Pow(player.transform.position.x-transform.position.x, 2) + Mathf.Pow(player.transform.position.x-transform.position.x, 2) );
		if(distance <= 3){
			if(transform.localScale.x < 2){
				transform.localScale += new Vector3(0.01f, 0.01f, 0f);
			}
		}else{
			if(transform.localScale.x > 1.5){
				transform.localScale -= new Vector3(0.01f, 0.01f, 0f);
			}
		}
	}

	void OnCollisionEnter2D  (Collision2D col) {
		if(col.gameObject.tag == "Player") {
			Application.LoadLevel(scene);
		}
	}

}
