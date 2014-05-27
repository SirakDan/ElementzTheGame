using UnityEngine;
using System.Collections;

public class Agua : MonoBehaviour {

	public float CostMana = 25f;
	public GameObject permafrost;
	public GameObject chorroAgua;
	private float timePressed;
	private Puntero mouse;
	private World w;


	// Use this for initialization
	void Start () {
		chorroAgua.GetComponent<ParticleSystem>().emissionRate = 0;
		permafrost.GetComponent<ParticleSystem>().emissionRate = 0;
		chorroAgua.GetComponent<ParticleSystem>().loop = true;
		permafrost.GetComponent<ParticleSystem>().loop = true;
		timePressed = Time.time;
		mouse = GameObject.Find ("Puntero").GetComponent<Puntero>();
		w = GameObject.Find("World").GetComponent<World>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!w.paused) {
			GameObject go = GameObject.FindGameObjectWithTag ("Player");
			Player player = (Player)go.GetComponent (typeof(Player));

			Vector3 point = Camera.main.ScreenToWorldPoint (new Vector3 (mouse.mousePosition().x, mouse.mousePosition().y, chorroAgua.transform.position.z));
			point -= player.transform.position;
			float angle = Mathf.Atan(point.x / point.y);
			if(point.y <= 0){
				chorroAgua.transform.localEulerAngles = new Vector3 (angle * (180f / 3.1416f) + 90, 90, chorroAgua.transform.rotation.z);
				permafrost.transform.localEulerAngles = new Vector3 (angle * (180f / 3.1416f) + 90, 90, chorroAgua.transform.rotation.z);
			}else{
				chorroAgua.transform.localEulerAngles = new Vector3 (angle * (180f / 3.1416f) - 90, 90, chorroAgua.transform.rotation.z);
				permafrost.transform.localEulerAngles = new Vector3 (angle * (180f / 3.1416f) - 90, 90, chorroAgua.transform.rotation.z);
			}

			if(Input.GetMouseButtonDown(0) && player.Status == player.Properties.WATER && player.mana >= CostMana){
				chorroAgua.GetComponent<ParticleSystem>().emissionRate = 50;
			}else if(Input.GetMouseButtonUp(0) || player.mana < CostMana){
				chorroAgua.GetComponent<ParticleSystem>().emissionRate = 0;
			}
			if(chorroAgua.GetComponent<ParticleSystem>().emissionRate > 0 && (Time.time-timePressed) >= 1f){
				timePressed = Time.time;
				player.reduceMana(CostMana);
			}

			if(Input.GetMouseButtonDown(1) && player.Status == player.Properties.WATER && player.mana >= CostMana){
				permafrost.GetComponent<ParticleSystem>().emissionRate = 50;
			}else if(Input.GetMouseButtonUp(1) || player.mana < CostMana){
				permafrost.GetComponent<ParticleSystem>().emissionRate = 0;
			}
			if(permafrost.GetComponent<ParticleSystem>().emissionRate > 0 && (Time.time-timePressed) >= 1f){
				timePressed = Time.time;
				player.reduceMana(CostMana);
			}
		}
	}
}
