using UnityEngine;
using System.Collections;

public class IAAdvanced : MonoBehaviour
{
	bool miraDerecha;
	public int velocity = 1;
	private float constantVelocity;
	private Transform[] transforms;
	int objectives;
	//int currentObjective;
	private Transform temp;
	//private bool enCamino;
	//private int secureTime;

	private int distance = 6;
	public LayerMask player;
	private GameObject go1;
	private float time;
	private float timeEspera;
	private bool espera;

	// Use this for initialization
	void Start ()
	{
		miraDerecha = true;
		//enCamino = false;
		//secureTime = 0;
		constantVelocity = 1;
		//currentObjective = 0;

		time = Time.time;
		timeEspera = time;
		espera = false;
		go1 = gameObject;
	}
	// Update is called once per frame
	void FixedUpdate ()
	{

		if (go1.transform.position.x - this.transform.position.x > 0.3f) {
			rigidbody2D.velocity = new Vector2 (constantVelocity * velocity, rigidbody2D.velocity.y);
			//enCamino = true;
			if(!miraDerecha){
				girar();
			}
		} else if(go1.transform.position.x - this.transform.position.x < -0.3f){	
			rigidbody2D.velocity = new Vector2 (-constantVelocity * velocity, rigidbody2D.velocity.y);
			//enCamino = true;
			if(miraDerecha){
				girar();
			}
		}


		int i = -1;
		if(miraDerecha){
			i = 1;
		}
		bool hitMiddle = Physics2D.Raycast (transform.position, new Vector2(i,0), distance, player);
		//Debug.DrawRay(transform.position, new Vector2(4*i,0), Color.red);
		bool hitUp = Physics2D.Raycast (transform.position, new Vector2(i,1f), distance, player);
		//Debug.DrawRay(transform.position, new Vector2(4*i,1f+1), Color.red);
		bool hitDown = Physics2D.Raycast (transform.position, new Vector2(i,-1f), distance, player);
		//Debug.DrawRay(transform.position, new Vector2(4*i,0), Color.red);
		if (hitMiddle || hitUp || hitDown) {
			GameObject p = GameObject.FindGameObjectWithTag ("Player");
			go1 = p;
			time = Time.time;
			espera = false;
		} else {

			if(time - Time.time < -2){
				go1 = gameObject;
				if(!espera){
					timeEspera = Time.time;
				}
				espera = true;

			}
		}
		if(espera){
			if(timeEspera - Time.time < -1){
				girar();
				timeEspera = Time.time;
			}
		}


		//}
	}
	void girar ()
	{
		miraDerecha = !miraDerecha;
		Vector3 miEscala;
		for (int i = 0; i < transform.childCount; i++) {
			miEscala = transform.GetChild (i).localScale;
			miEscala.x *= -1;
			transform.GetChild (i).localScale = miEscala;
		}
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<Player>().Death = true;
		}				
	}
}
