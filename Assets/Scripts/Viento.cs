using UnityEngine;
using System.Collections;

public class Viento : MonoBehaviour {

	public float CostMana = 10f;
	public float DashSpeed = 50f;
	public float DashTime = 0.125f;
	public Rigidbody2D blow;
	public float BlowSpeed = 25f;
	public float CoolDown = 1f;
	
	private Player player;
	private World w;
	private bool ActivateDash;
	private float InitDashTime;
	private Puntero mouse;
	private Rigidbody2D bulletInstance;

	// Use this for initialization
	void Start () {
		ActivateDash = false;
		player = GameObject.Find("Nika").GetComponent<Player>();
		w = GameObject.Find("World").GetComponent<World>();
		mouse = GameObject.Find ("Puntero").GetComponent<Puntero>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!w.paused) {

			if (player.Status == player.Properties.AIR) {

				//PASIVA
				if (!ActivateDash) { 
					player.velocidadMaxima = 10; 
					player.velocidadSalto = 26;
					GameObject.Find("Character").GetComponent<Animator> ().speed = 1.5f;
				}


				//HABILIDAD "DASH"
				float AgainDash = InitDashTime + DashTime + CoolDown;
				if (Input.GetMouseButtonDown(1) && player.mana > CostMana && Time.time > AgainDash) {
					ActivateDash = true;
					InitDashTime = player.reduceMana(CostMana);
				}
				if (ActivateDash) {
					if (Time.time < InitDashTime + DashTime) {
						player.velocidadMaxima = DashSpeed;
						player.velocidadSalto = 26;
						GameObject.Find("Character").GetComponent<Animator> ().speed = 2f;
					} else { ActivateDash = false; }
				}


				//ATAQUE "BLOW"
				if (Input.GetMouseButtonDown(0) && player.mana >= CostMana) {
					Vector2 click = Camera.main.ScreenToWorldPoint (new Vector2 (mouse.mousePosition().x,
					                                                             mouse.mousePosition().y));

					blow.GetComponent<Blow>().lifetime = 1f;
					blow.GetComponent<Blow>().Orientacion = click.x - transform.position.x;
					float posinit = 1f;
					if (blow.GetComponent<Blow>().Orientacion < 0) { posinit = -0.6f; }

					bulletInstance = Instantiate(blow, 
					                             new Vector3(transform.position.x + posinit, 
					                                         transform.position.y, 
					                                         transform.position.z), 
					                             Quaternion.Euler(new Vector2(0,0))) as Rigidbody2D;

					bulletInstance.velocity = Speed(click.x, click.y, 
					                                GameObject.Find("Character").transform.localScale);

					player.reduceMana(CostMana); 
				}

			}

		}
	}

	// Herramienta: Establece el vector de velocidad, en la direccion de x0, y0 y la orientacion en X
	private Vector2 Speed(float x0, float y0, Vector3 Orientacion) {
		float x = x0-transform.position.x;
		float y = y0-transform.position.y;
		float h = Mathf.Sqrt(Mathf.Pow(x,2) + Mathf.Pow(y,2));
		float yprima = BlowSpeed*y/h;
		float xprima = Mathf.Sqrt(Mathf.Pow(BlowSpeed,2) - Mathf.Pow(yprima,2));
		
		if (Orientacion.x < 0) { xprima *= -1; }
		
		return new Vector2(xprima, yprima);
	}
}
