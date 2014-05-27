using UnityEngine;
using System.Collections;

public class Fuego : MonoBehaviour {

	public Rigidbody2D fireBall;
	public float speed;
	public float coolDownManaCost = 1f;
	public float CostMana = 10f;
	private Rigidbody2D bulletInstance;
	private bool buttonPressed0;
	private bool buttonPressed1;
	private float lastCostManaTime;
	private bool mutexDestro;
	private Puntero mouse;
	private World w;
	
	// Use this for initialization
	void Start () {
		mutexDestro = true;
		buttonPressed0 = false;
		buttonPressed1 = false;
		mouse = GameObject.Find ("Puntero").GetComponent<Puntero>();
		w = GameObject.Find("World").GetComponent<World>();
	}

	// Update is called once per frame
	void Update () {

		if (!w.paused) {

			GameObject go = GameObject.FindGameObjectWithTag ("Player");
			Player player = (Player) go.GetComponent (typeof(Player));

			// Boton Derecho del raton es disparo cargado (pt1): INICIO
			if (Input.GetMouseButtonDown(0) && player.Status == player.Properties.FIRE && player.mana >= CostMana) {

				buttonPressed0 = true;
				((FireBall) fireBall.GetComponent("FireBall")).time = 1f;
				bulletInstance = Instantiate(fireBall, transform.position, Quaternion.Euler(new Vector2(0,0))) as Rigidbody2D;
				bulletInstance.GetComponent<CircleCollider2D>().enabled = false;
				mutexDestro = true;
				lastCostManaTime = player.reduceMana(CostMana); 
			}

			// Boton Derecho del raton es disparo cargado (pt2): CARGANDO
			if(bulletInstance != null && buttonPressed0 && player.mana >= CostMana/2) {
				bulletInstance.transform.position = transform.position;
				if (Time.time > lastCostManaTime + coolDownManaCost) {
					lastCostManaTime = player.reduceMana(CostMana/2); 
				}
			} else if (bulletInstance != null && buttonPressed0 && player.mana < CostMana/2) { 
				if (mutexDestro) {
					FireBall f = bulletInstance.gameObject.GetComponent<FireBall>();
					StartCoroutine(f.destroyFire(0)); 
					mutexDestro = false;
				}
			}

			// Boton Derecho del raton es disparo cargado (pt3): DISPARADO
			if (Input.GetMouseButtonUp(0) && player.Status == player.Properties.FIRE && bulletInstance != null) {

				buttonPressed0 = false;
				Vector2 click = Camera.main.ScreenToWorldPoint (new Vector2 (mouse.mousePosition().x,
				                                                             mouse.mousePosition().y));

				bulletInstance.transform.position = transform.position;
				bulletInstance.GetComponent<CircleCollider2D>().enabled = true;
				bulletInstance.velocity = Speed(click.x, click.y, GameObject.Find("Character").transform.localScale);
			}

			// Boton Izquierdo del raton es aura de fuego (pt1): INICIO DE AURA
			if (Input.GetMouseButtonDown(1) && player.Status == player.Properties.FIRE && player.mana >= CostMana/2) {
				buttonPressed1 = true;
				player.GetComponent<ParticleSystem>().emissionRate = 50;
			}

			// Boton Izquierdo del raton es aura de fuego (pt2): GASTO DE MANA
			if(buttonPressed1 && player.Status == player.Properties.FIRE && player.mana >= CostMana/2) {
				if (Time.time > lastCostManaTime + coolDownManaCost) {
					lastCostManaTime = player.reduceMana(CostMana/2); 
				}
			}

			// Boton Izquierdo del raton es aura de fuego (pt3): CANCELAR AURA
			if (Input.GetMouseButtonUp(1) || player.Status != player.Properties.FIRE || player.mana < CostMana/2) {
				buttonPressed1 = false;
				player.GetComponent<ParticleSystem>().emissionRate = 0;
			}

		}

	}

	// Herramienta: Establece el vector de velocidad, en la direccion de x0, y0 y la orientacion en X
	private Vector2 Speed(float x0, float y0, Vector3 Orientacion) {
		float x = x0-transform.position.x;
		float y = y0-transform.position.y;
		float h = Mathf.Sqrt(Mathf.Pow(x,2) + Mathf.Pow(y,2));
		float yprima = speed*y/h;
		float xprima = Mathf.Sqrt(Mathf.Pow(speed,2) - Mathf.Pow(yprima,2));

		if (Orientacion.x < 0) { xprima *= -1; }

		return new Vector2(xprima, yprima);
	}

}
