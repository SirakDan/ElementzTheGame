using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public float mana = 100f;
	public float regManaAmount = 1f;
	public float coolDownManaReg = 1f;
	public float startRegMana = 3f;
	public float velocidadMaxima;
	public float velocidadSalto;
	public Transform GroundCheck;
	public Transform fairy;
	public LayerMask whatIsGround;
	private bool grounded;
	private float MaxMana;
	[HideInInspector]
	public bool miraDerecha;
	private PlayerProperties properties;
	private int status;
	private float groundRadius;
	private ArrayList elementz;
	private int index;
	private float lastRegManaTime;
	private float lastCostManaTime;
	private Animator anim;
	private Puntero mouse;
	private World w;
	private GameObject body;

	[HideInInspector]
	public bool Death;



	// Use this for initialization
	void Start () {
		Death = false;
		MaxMana = mana;
		miraDerecha = true;
		grounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, whatIsGround);
		properties = this.gameObject.GetComponent<PlayerProperties> ();
		index = status = properties.AIR;
		groundRadius = 0.2f;
		elementz = new ArrayList ();
		elementz.Add (properties.FIRE);
		elementz.Add (properties.STONE);
		elementz.Add (properties.WATER);
		elementz.Add (properties.AIR);
		body = GameObject.Find("Character");
		anim = body.GetComponent<Animator> ();
		mouse = GameObject.Find ("Puntero").GetComponent<Puntero>();
		w = GameObject.Find("World").GetComponent<World>();

	}

	public PlayerProperties Properties {
		get {
			return properties;
		}
	}

	public int Status {
		get {
			return status;
		}
	}

	// Update is called once per frame
	void Update(){

		if (!w.paused && !Death) {

			if (this.status == properties.FIRE) { GetComponent<Light>().range = 10; }
			else { GetComponent<Light>().range = 0; }

			if(this.status == properties.STONE){
				velocidadMaxima = 4;
				velocidadSalto = 18;
				anim.speed = 0.5f;
			} else if (status == properties.FIRE || status == properties.WATER) {
				velocidadMaxima = 7;
				velocidadSalto = 22;
				anim.speed = 1;
			}

			if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) 
			   && grounded){
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, velocidadSalto);
			}

			if (Input.GetAxis("Mouse ScrollWheel") > 0) {

				status = (int) elementz[++index%elementz.Count];

			} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {

				if(--index < 0) { index = elementz.Count-1; }
				status = (int) elementz[index%elementz.Count];

			}

			switch (Input.inputString) {
				case "1": status = properties.FIRE; break;
				case "2": status = properties.STONE; break;
				case "3": status = properties.WATER; break;
				case "4": status = properties.AIR; break;
				default: break;
			}

			if (Time.time > lastCostManaTime + startRegMana && mana < MaxMana) {

				float reg;
				if (MaxMana - mana >= regManaAmount) { reg = regManaAmount; }
				else {reg = MaxMana - mana;}

				if (Time.time > lastRegManaTime + coolDownManaReg) {
					regMana(reg);
					lastRegManaTime = Time.time; 
				}
			}

		}

		if (Death) {
			mouse.unlockCursor();
			Destroy (body);
		}

	}

	// FixedUpdated no necesita deltaTime, se usa para las fisicas.
	void FixedUpdate () {

		if (!Death) {

			grounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, whatIsGround);
			float movimiento = Input.GetAxis ("Horizontal");
			float speed = movimiento * velocidadMaxima;
			rigidbody2D.velocity = new Vector2(movimiento*velocidadMaxima, rigidbody2D.velocity.y);
			//======================CAMBIAR ANIMACION=======================
			anim.SetFloat ("speed", Mathf.Abs(speed));
			//==============================================================


			//===================GIRAR A DONDE ESTA EL CURSOR===============
			Vector2 click = Camera.main.ScreenToWorldPoint (new Vector2 (mouse.mousePosition().x,
			                                                             mouse.mousePosition().y));
			float Orientacion = click.x - transform.position.x;

			if(Orientacion > 0 && !miraDerecha){
				girar();
			}else if(Orientacion < 0 && miraDerecha){
				girar();
			}
			//==============================================================

		}

	}

	public void regMana (float regeneration) {
		mana += regeneration;
		GameObject.Find ("HUD").GetComponent<HUD> ().UpdateManaBar (mana);
	}

	public float reduceMana (float reduction) {
		lastCostManaTime = Time.time;
		mana -= reduction;
		GameObject.Find ("HUD").GetComponent<HUD> ().UpdateManaBar (mana);
		return Time.time;
	}

	void girar(){
		miraDerecha = !miraDerecha;
		Vector3 miEscala = transform.GetChild (2).localScale;
		miEscala.x *= -1;
		transform.GetChild (2).localScale = miEscala;
	}



}
