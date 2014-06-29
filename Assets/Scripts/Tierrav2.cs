/// <summary>
/// Tierrav2. NO TOCAR BAJO NINGUN CONCEPTO
/// PARA CUALQUIER TIPO DE PROBLEMA AVISAR A ALEX
/// </summary>

using UnityEngine;
using System.Collections;

public class Tierrav2 : MonoBehaviour {

	public float distanciaRomper = 2.5f;
	public float CostMana = 10f;
	public float coolDownManaCost = 1f;
	public float coolDownEffects = 2f;

	private GameObject tierra;
	private PhysicsMaterial2D Ground;
	private float lastCostManaTime;
	private float Initfriction;
	private Puntero mouse;
	private Animator anim;
	private bool regUP;
	private bool regDRAG;
	private bool levitando;
	private World w;

	void Start () {;
		mouse = GameObject.Find ("Puntero").GetComponent<Puntero>();
		w = GameObject.Find("World").GetComponent<World>();
		tierra = transform.gameObject;
		if (tierra.tag != "Ground") {
			Ground = tierra.GetComponent<BoxCollider2D> ().sharedMaterial;
			anim = GetComponent<Animator>();
		}
		if (tierra.tag == "MobileStone") { Initfriction = Ground.friction; }

		regUP = false;
		regDRAG = false;
		levitando = false;

	}

	void Update () {

		if (!w.paused) {

		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		Player player = (Player) go.GetComponent (typeof(Player));

			switch (tierra.tag) {
			case "MobileStone": 

				if (player.Status == player.Properties.STONE) {

					if (mouse.OnMouseDrag(gameObject)) {regDRAG = true; regUP = false;}
					if (mouse.OnMouseUp(gameObject)) { regDRAG = false; }
					if ( regDRAG && player.mana >= CostMana/2 ) {

						Vector3 click;
						click.x = mouse.transform.position.x;
						click.y = mouse.transform.position.y;
						click.z = tierra.transform.position.z;
						tierra.transform.position = click;
						
						anim.SetBool("moving", true);
						tierra.collider2D.enabled = !anim.GetBool ("moving");
						tierra.layer = 13;

						if (Time.time > lastCostManaTime + coolDownManaCost) {
							lastCostManaTime = player.reduceMana(CostMana/2); }

						levitando = true;

					} else if ( regDRAG && player.mana < CostMana/2 ) {
						
						anim.SetBool("moving", false);
						tierra.collider2D.enabled = !anim.GetBool ("moving");
						tierra.layer = 0;
						tierra.rigidbody2D.isKinematic = false;
						regDRAG = regUP = false;
						levitando = false;

					}

					if(Input.GetMouseButtonDown(1) && levitando && mouse.OnMouseOver(gameObject)){
						tierra.rigidbody2D.isKinematic = false;
/*-------->*/			ChangeFriction(Initfriction); 
						levitando = false;
						regUP = false;
						
					}
					
					if (!regDRAG && !regUP && player.mana < CostMana/2) {
						if (Time.time > lastCostManaTime + coolDownEffects) {
							tierra.rigidbody2D.isKinematic = false;
/*-------->*/				ChangeFriction(Initfriction); 
						}
						
						levitando = false;
						regUP = false;
					}

					if (mouse.OnMouseUp(gameObject)) {regUP = true;}
					if ( regUP && player.mana >= CostMana/2) {
						anim.SetBool("moving", false);
						tierra.collider2D.enabled = !anim.GetBool ("moving");
						tierra.layer = 0;

						if (Time.time > lastCostManaTime + coolDownManaCost) {
							lastCostManaTime = player.reduceMana(CostMana/2); }

						levitando = true;

					} else if (levitando) { 

						tierra.rigidbody2D.isKinematic = true;
/*-------->*/			ChangeFriction(0);
						regUP = false;
						levitando = false;

					}

				} else {
					if (Time.time > lastCostManaTime + coolDownEffects) {
						tierra.rigidbody2D.isKinematic = false;
/*-------->*/			ChangeFriction(Initfriction); 
					}
					regUP = false;
				}
				break;
			case "BrokenStone": 

				if (player.Status == player.Properties.STONE && player.mana < CostMana/2) {
					if(Input.GetMouseButtonDown(1) && mouse.OnMouseOver(gameObject)){
						float distance = Mathf.Sqrt( Mathf.Pow(transform.position.x - go.transform.position.x, 2) + Mathf.Pow(transform.position.y - go.transform.position.y, 2));
						if(distance <= distanciaRomper){
							anim.SetBool("crash", true);
							Destroy (gameObject, 0.683f);
							lastCostManaTime = player.reduceMana(CostMana); 
						}
					}
				}
				break;
			case "Ground": 
				break;
			default: break;
			}
		}
	}

	// Herramienta: Cambia la friccion del material de la piedra
	// CUIDADO: Se tiene que desactivar y volver a activar el Collider2D
	//			Debido a un bug de Unity
	private void ChangeFriction(float f) {
		Ground.friction = f;
		if (tierra.collider2D.enabled) {
			tierra.collider2D.enabled = false;
			tierra.collider2D.enabled = true;
		}
	}


}