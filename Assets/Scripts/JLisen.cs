using UnityEngine;
using System.Collections;

public class JLisen : MonoBehaviour {


	GameObject go;
	Player player;
	Animator anim;
	// Use this for initialization
	void Start () {
		go = GameObject.FindGameObjectWithTag ("Player");
		player = (Player) go.GetComponent (typeof(Player));
		anim = GetComponent<Animator> ();
		//Debug.Log ("Player status: " + player.Status);
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetInteger ("sintony", player.Status);
		//Debug.Log ("Player status: " + player.Status);
	}
}
