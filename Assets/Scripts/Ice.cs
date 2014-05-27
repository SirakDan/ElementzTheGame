using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {
	
	//public float distanceToMelt = 2f;
	public float timeToMelt = 3f;
	private float coolDown;
	private Animator anim;
	private Player player;
	private float InitTime;
	private float InitTime2;
	private bool mutex2;
	private bool mutex;

	// Use this for initialization
	void Start () {
		coolDown = 3f;
		mutex2 = false;
		mutex = false;
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		player = (Player) go.GetComponent (typeof(Player));
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Derretir()) {
			anim.SetBool ("melt", true);
			Destroy (gameObject, 0.683f);
		}*/
		if (!mutex) { InitTime2 = Time.time; mutex = true; }
		if (player.Status != player.Properties.WATER && Time.time >= InitTime2 + coolDown) {

			if (!mutex2) { InitTime = Time.time; mutex2 = true; }
			if (Time.time > InitTime + timeToMelt) {
				anim.SetBool ("melt", true);
				Destroy (gameObject, 0.683f);
			}
		}

	}

	/*private bool Derretir() {
		bool der = false;

		float x = transform.position.x - player.transform.position.x;
		float y = transform.position.y - player.transform.position.y;
		float h = Mathf.Sqrt(Mathf.Pow(x,2) + Mathf.Pow(y,2));

		if (h <= distanceToMelt && player.GetComponent<ParticleSystem>().emissionRate > 0) { der = true; }

		return der;
	}*/
}
