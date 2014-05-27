using UnityEngine;
using System.Collections;

public class IABasic : MonoBehaviour
{
		bool miraDerecha;
		public int velocity = 1;
		private float constantVelocity;
		public Transform[] Transforms;
		private Transform[] transforms;
		int objectives;
		int currentObjective;
		private Transform temp;
		private bool enCamino;
		private int secureTime;
		// Use this for initialization
		void Start ()
		{
				miraDerecha = true;
				enCamino = false;
				secureTime = 0;
				constantVelocity = 1;
				currentObjective = 0;
				objectives = Transforms.Length;
				transforms = (Transform[])Transforms.Clone ();
		}
		// Update is called once per frame
		void FixedUpdate ()
		{
				//if (objectives != 0) {
			
				temp = transforms [currentObjective];

		if (temp.localPosition.x < 0.3f && temp.localPosition.x > -0.3f && enCamino) {
						enCamino = false;
						currentObjective = (currentObjective + 1) % objectives;
						if (secureTime > 10) {
								girar ();
								secureTime = 0;
						}

				} else
				if (temp.localPosition.x > 0.3f) {
						rigidbody2D.velocity = new Vector2 (constantVelocity * velocity, rigidbody2D.velocity.y);
						enCamino = true;
						//miraDerecha = false;
				} else if(temp.localPosition.x < -0.3f){	
						rigidbody2D.velocity = new Vector2 (-constantVelocity * velocity, rigidbody2D.velocity.y);
						enCamino = true;
						miraDerecha = true;
				}
				secureTime++;
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
