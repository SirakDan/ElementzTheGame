using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	[HideInInspector]
	public bool abierto = false;
	[HideInInspector]
	public float retraso;
	public string passwd;
	private float TiempoAbierto;
	private bool mutex;


	// Use this for initialization
	void Start () {
		mutex = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (abierto) { 
			transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
			if (mutex) { TiempoAbierto = Time.time; mutex = false; } 
		} else {
			transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = true;
			transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
			mutex = true;
		}

		if (abierto && Time.time > TiempoAbierto + retraso) {
			abierto = false;
		}


		ArrayList Combinacion = pass (passwd);
		abierto = true;

		if (transform.GetChild(transform.childCount-1).GetComponent<TorchTrigger> ().switchOn) {
			abierto = true;
		} else {

			for (int i = 0; i < transform.childCount-1; i++) {
				if (transform.GetChild(i).tag == "Trigger") {
					string var = transform.GetChild(i).GetComponent<TorchTrigger> ().switchOn.ToString();
					if (var != Combinacion[i].ToString()) {
						abierto = false;
					}
				}
			}

		}
	}


	private ArrayList pass (string pass) {
		ArrayList Combinacion = new ArrayList ();
		Combinacion.Add (false);
		Combinacion.Add (false);
		Combinacion.Add (false);

		for (int i = 0; i < pass.Length; i++){
			if (pass[i] == '1') { Combinacion.Add (true); } 
			else { Combinacion.Add(false); }
		}

		return Combinacion;
	}
}
