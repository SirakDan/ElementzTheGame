using UnityEngine;
using System.Collections;

public class Tierra : MonoBehaviour {
	
	private GameObject tierra;
	private Sprite sprite1;
	private Sprite sprite2;
	private Sprite sprite3;
	public float distanciaRomper;
	private PhysicsMaterial2D Ground;
	private Animator anim;
	private bool on;
	public float CostMana = 10f;
	private float lastCostManaTime;
	public float coolDownManaCost = 1f;
	private float Initfriction;
	private Puntero mouse;
	public float coolDownEffects = 2f;

	// Use this for initialization
	public void Start () {
		mouse = GameObject.Find ("Puntero").GetComponent<Puntero>();
		tierra = transform.gameObject;
		if (tierra.tag != "Ground") {
			Ground = tierra.GetComponent<BoxCollider2D> ().sharedMaterial;
			anim = GetComponent<Animator>();
		}
		if (tierra.tag == "MobileStone") { Initfriction = Ground.friction; }
	}
	
	// Update is called once per frame
	public void Update () {
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		Player player = (Player) go.GetComponent (typeof(Player));
		
		if (player.Status != player.Properties.STONE) {
			on = false;
		}
		
		if (tierra.tag == "MobileStone") {

			if (player.Status == player.Properties.STONE && on && player.mana >= CostMana/2) {
				tierra.rigidbody2D.isKinematic = true;
				ChangeFriction(0);
			} else {
				if (Time.time > lastCostManaTime + coolDownEffects) {
					tierra.rigidbody2D.isKinematic = false;
					ChangeFriction(Initfriction); 
				}
				on = false;
			}
			
			if (player.Status == player.Properties.STONE && on && tierra.rigidbody2D.isKinematic && player.mana >= CostMana/2) {
				if (Time.time > lastCostManaTime + coolDownManaCost) {
					lastCostManaTime = player.reduceMana(CostMana/2); 
				}
			}
		}

		//EVENTOS
		if (mouse.OnMouseUp(gameObject)) { OnMouseUp();}

		// ERROR: SOLO SE PUEDE MANTENER UNA EN SINTONIA
		// POSIBLE SOLUCION MODIFICAR EL ON, CON SUMO CUIDADO
		if (mouse.OnMouseDrag(gameObject)) { OnMouseDrag(); }

		if (mouse.OnMouseOver(gameObject)) { OnMouseOver(); }

		if (mouse.OnMouseDown(gameObject)) { OnMouseDown(); }
	}

	// Quitar animacion de piedra en movimiento
	public void OnMouseUp(){
		if (tierra.tag == "MobileStone") {
			anim.SetBool("moving", false);
			tierra.collider2D.enabled = !anim.GetBool ("moving");
		}
	}
	
	// Movimiento de la piedra
	public void OnMouseDrag(){
		if (tierra.tag == "MobileStone") {
			
			GameObject go = GameObject.FindGameObjectWithTag ("Player");
			Player player = (Player) go.GetComponent (typeof(Player));
			
			if (player.Status == player.Properties.STONE && on) {
				
				Vector3 click;
				click.x = mouse.transform.position.x;
				click.y = mouse.transform.position.y;
				click.z = tierra.transform.position.z;
				tierra.transform.position = click;

				anim.SetBool("moving", true);
				tierra.collider2D.enabled = !anim.GetBool ("moving");

			} else {
				OnMouseUp();
				on = false;
			}
		}
	}
	
	// Romper Piedras y Desintonizar piedras
	public void OnMouseOver() {

		if (Input.GetMouseButtonDown (1) && tierra.tag == "MobileStone") {

			GameObject go = GameObject.FindGameObjectWithTag ("Player");
			Player player = (Player) go.GetComponent (typeof(Player));

			if (player.Status == player.Properties.STONE && on && tierra.rigidbody2D.isKinematic) {
				on = false;
			}

		}
		
		if(Input.GetMouseButtonDown(1) && tierra.tag == "BrokenStone"){
			
			GameObject go = GameObject.FindGameObjectWithTag ("Player");
			Player player = (Player) go.GetComponent (typeof(Player));
			
			if (player.Status == player.Properties.STONE) {
				float distance = Mathf.Sqrt( Mathf.Pow(transform.position.x - go.transform.position.x, 2) + Mathf.Pow(transform.position.y - go.transform.position.y, 2));
				if(distance <= distanciaRomper){
					anim.SetBool("crash", true);
					Destroy (gameObject, 0.683f);
					lastCostManaTime = player.reduceMana(CostMana); 
				}
			}
		}
		
	}
	
	// Herramienta: Saber si el cursor esta encima
	public void OnMouseDown(){
		 on = true;
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
