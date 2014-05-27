using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	[HideInInspector]
	public float time;
	private Vector3 Initposition;
	private bool mutexPosition;
	private Player player;
	private Animator anim;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		player = (Player) go.GetComponent (typeof(Player));
		Initposition = transform.position - player.transform.position;
		mutexPosition = true;
		anim = GetComponentInChildren<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if( player.Status == player.Properties.FIRE ) {
			if ((player.transform.GetChild(2).transform.localScale.x > 0 && Initposition.x < 0) || (player.transform.GetChild(2).transform.localScale.x < 0 && Initposition.x > 0)) {
				Initposition.x *= -1;
			}

			Vector3 Actposition = transform.position - player.transform.position;
			if (System.Math.Round(Initposition.x, 2) != System.Math.Round(Actposition.x, 2) && mutexPosition) {
				StartCoroutine(destroyFire(time));
				mutexPosition = false;
			}
		} else {
			StartCoroutine(destroyFire(0));
		}
	}
	
	void OnCollisionEnter2D  (Collision2D col) {
		if(col.gameObject.tag != "Player") {
			StartCoroutine(destroyFire(0));
		}
	}

	// Herramienta: tarda 't' segundos hasta activar la animacion de destruccion y posteriormente destruir el objecto
	public IEnumerator destroyFire(float t) {
		yield return new WaitForSeconds(t);
		transform.collider2D.enabled = false;
		anim.SetBool("destroy", true);
		Destroy (gameObject, 0.467f);
	}

}
