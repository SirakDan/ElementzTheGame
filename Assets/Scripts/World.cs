using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	[HideInInspector]
	public bool paused;

	private Puntero mouse;
	private GameObject Nika;
	private Player player;
	private GameObject bodyPlayer;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		paused = false;
		mouse = GameObject.Find("Puntero").GetComponent<Puntero>();
		Nika = GameObject.Find("Nika");
		player = Nika.GetComponent<Player> ();
		bodyPlayer = GameObject.Find("Character");
		mouse.lockCursor();
		Time.timeScale = 1;
		GameObject goAudio = GameObject.Find ("Audio");
		if (goAudio != null) {
			audio = goAudio.GetComponent<AudioSource> ();
		}
	}

	// Update is called once per frame
	void Update () {
		if ((Input.GetKey (KeyCode.Escape) && !paused) || player.Death) {
			paused = true;
			Time.timeScale = 0; 
		}
		DeathByInfiniteFall ();
	}

	void OnGUI() {
		if (paused){ // MENU DE PAUSA
			if (audio != null) { audio.Pause(); }
			string pauseMsg = "Game paused";

			//Tamaño de la Celda
			float TamCX = 200, TamCY = 40, NCeldas = 2;
			if (bodyPlayer == null) { pauseMsg = "Nika has died"; NCeldas = 1; }
			
			//Tamaño del menu
			float TamX = TamCX + 30, TamY = TamCY * NCeldas + 35 + NCeldas * 5, Opcion = -1;

			GUI.Box(new Rect((Screen.width / 2 - TamX / 2), (Screen.height / 2 - TamY / 2), TamX, TamY), pauseMsg);

			if (bodyPlayer != null) {
				Opcion++;
				if(GUI.Button(new Rect((Screen.width / 2 - TamX / 2) + 15, 
				                       (Screen.height / 2 - TamY / 2) + (TamCY * Opcion + 30 + Opcion * 5), 
				                       TamCX, TamCY), "Continue Game")) { 
					paused = false;
					Time.timeScale = 1; 
					mouse.lockCursor();
					if (audio != null) { audio.Play(); }
				}
			}

			Opcion++;
			if(GUI.Button(new Rect((Screen.width / 2 - TamX / 2) + 15, 
			                       (Screen.height / 2 - TamY / 2) + (TamCY * Opcion + 30 + Opcion * 5), 
			                       TamCX, TamCY), "Back To MainTitle")) {
				player.Death = false;
				Time.timeScale = 1;
				if (audio != null) { audio.Stop(); }
				Application.LoadLevel("MainTitle");
			}

		}

	}

	private void DeathByInfiniteFall () {
		if (Nika.transform.position.y < -20) {
			player.Death = true;
		}
	}

}
