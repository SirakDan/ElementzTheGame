using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public string resolution = "Develop";

	private SpriteRenderer manaBar;
	private Vector3 manaScale;
	private Color miAzul;
	private float resActX;
	private float resActY;
	private World w;

	// Use this for initialization
	void Start () {
		ResolutionHUD (resolution);
		miAzul =  new Color (0f, 0.2f, 1f, 1f);
		manaBar = GameObject.Find("mana_bar").GetComponent<SpriteRenderer>();
		manaBar.transform.renderer.material.color = miAzul;
		manaScale = manaBar.transform.localScale;
		UpdateTamHUD ();
		w = GameObject.Find("World").GetComponent<World>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!w.paused) {
			UpdateTamHUD ();

			GameObject go = GameObject.FindGameObjectWithTag ("Player");
			Player player = (Player) go.GetComponent (typeof(Player));

			ElementSelector(player);
		}
	}

	void UpdateTamHUD () {
		InScreen(GameObject.Find ("ManaHealthHUD"), "topleft", 0.25f, 0.25f);
		InScreen(GameObject.Find ("Elementz2"), "topright", 0.25f, 0.25f);
		InScreen(GameObject.Find ("ElementzHUD"), "botcenter", 0, 0);
		ResolutionHUD ("Actualizando...");
	}

	private void ElementSelector( Player p ) {
		float posWindy = GameObject.Find("Elementz_0").transform.position.x;
		float posTorchy = GameObject.Find("Elementz_1").transform.position.x;
		float posRocky = GameObject.Find("Elementz_2").transform.position.x;
		float posDropy = GameObject.Find("Elementz_3").transform.position.x;
		
		float posSelector = 0;
		
		switch (p.Status) {
		case 3: posSelector = posWindy; break;
		case 0: posSelector = posTorchy; break;
		case 1: posSelector = posRocky; break;
		case 2: posSelector = posDropy; break;
		default: break;
		}
		
		Vector3 v = GameObject.Find ("Selector").transform.position;
		v.x = posSelector;
		GameObject.Find ("Selector").transform.position = v;
	}

	public void UpdateManaBar (float mana) {
		manaBar.material.color = Color.Lerp(miAzul, Color.red, 1 - mana * 0.01f);
		GameObject.Find ("Efecto_mana_bar").GetComponent<ParticleSystem> ().startColor = manaBar.material.color;
		GameObject.Find ("Efecto_mana_bar").GetComponent<ParticleSystem> ().Play();
		manaBar.transform.localScale = new Vector3(manaScale.x * mana * 0.01f, manaScale.y, manaScale.z);
	}

	private void ResolutionHUD(string r) {
		switch (r) {
		case "BIG": 
			resActX = 1366f;
			resActY = 638f;
			break;
		case "Develop":
			resActX = 740f;
			resActY = 296f;
			break;
		default: //default
			resActX = Screen.width;
			resActY = Screen.height;
			break;
		}
	}

	private void InScreen(GameObject go, string s, float offsetX, float offsetY) {

		if (resActX != Screen.width || resActY != Screen.height) {
			float ftX = Screen.width/resActX;
			//float ftY = Screen.height/resActY;
			
			float ftMax = ftX;
			//if (ftX > ftY) { ftMax = ftY; } else { ftMax = ftX; }
			
			go.transform.localScale = new Vector3(ftMax * go.transform.localScale.x, 
			                                      ftMax * go.transform.localScale.y, 
			                                      go.transform.localScale.z);
		}

		Vector3 temp = go.transform.position;
		float tamX = go.renderer.bounds.max.x - go.renderer.bounds.min.x;
		float tamY = go.renderer.bounds.max.y - go.renderer.bounds.min.y;
		Vector3 temp2;

		switch(s) {
			case "topleft" : 
			temp2 = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, Camera.main.nearClipPlane));
			tamY *= -1;
			offsetY *= -1;
			break;
			case "topright" :
			temp2 = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, Camera.main.nearClipPlane));
			tamX *= -1;
			tamY *= -1;
			offsetX *= -1;
			offsetY *= -1;
			break;
			case "botleft" :
			temp2 = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, Camera.main.nearClipPlane));
			break;
			case "botright" :
			temp2 = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, Camera.main.nearClipPlane));
			tamX *= -0.05f;
			offsetX *= -1;
			break;
			case "botcenter" :
			temp2 = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0, Camera.main.nearClipPlane));
			tamX *= -0.05f;
			break;
			default: /*middlecenter*/
			temp2 = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, Camera.main.nearClipPlane));
			tamX *= -0.05f;
			tamY *= -0.05f;
			break;
		}

		go.transform.position = new Vector3 (temp2.x + tamX/2 + offsetX, temp2.y + tamY/2 + offsetY, temp.z);
	}

}
