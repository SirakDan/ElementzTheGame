using UnityEngine;
using System.Collections;

public class Incendio : MonoBehaviour {

	public float distanceToExtin = 5f;
	public float ExtinTime = 0.5f;

	private ParticleSystem chorroAgua;

	private Light FireLight;
	private ParticleSystem llamas1;
	private ParticleSystem llamas2;
	private ParticleSystem llamas3;
	private float lastExtinTime;

	// Use this for initialization
	void Start () {
		chorroAgua = GameObject.Find ("ChorroAgua").GetComponent<ParticleSystem>();
		//FireLight = GameObject.Find ("FireLight").GetComponent<Light>();
		//llamas1 = GameObject.Find ("llamas1").GetComponent<ParticleSystem>();
		//llamas2 = GameObject.Find ("llamas2").GetComponent<ParticleSystem>();
		//llamas3 = GameObject.Find ("llamas3").GetComponent<ParticleSystem>();
		FireLight = transform.GetChild (0).GetComponent<Light> ();
		llamas1 = transform.GetChild (1).GetComponent<ParticleSystem>();
		llamas2 = transform.GetChild (2).GetComponent<ParticleSystem>();
		llamas3 = transform.GetChild (3).GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () { //bajar la emision de las llamas hasta apagarse
		if (Apagar()) {
			if (Time.time > lastExtinTime + ExtinTime) {
				llamas1.emissionRate -= 10;
				llamas2.emissionRate -= 10;
				llamas3.emissionRate -= 10;
				FireLight.intensity -= 1;
				lastExtinTime = Time.time;
			}
		}
		if (llamas1.emissionRate == 0) { 
			Destroy (gameObject); 
		}
	}

	private bool Apagar() {
		bool apa = false;
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		Player player = (Player) go.GetComponent (typeof(Player));
		
		float x = transform.position.x - player.transform.position.x;
		float y = transform.position.y - player.transform.position.y;
		float h = Mathf.Sqrt(Mathf.Pow(x,2) + Mathf.Pow(y,2));

		bool estaEnLaDerecha = (transform.position.x > player.transform.position.x);

		if (h <= distanceToExtin && chorroAgua.emissionRate > 0 &&
		    estaEnLaDerecha == player.miraDerecha) { apa = true; }
		
		return apa;
	}


}
