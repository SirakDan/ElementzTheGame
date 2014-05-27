using UnityEngine;
using System.Collections;

public class Puntero : MonoBehaviour {

	public float sensibility = 3;

	private bool mouseOver;
	private bool holdingButton;
	private bool MouseDown;
	private bool click;
	private float timeToHold;
	private World w;


	// Use this for initialization
	void Start () {
		mouseOver = false;
		holdingButton = false;
		MouseDown = false;
		click = false;
		timeToHold = 0;

		w = GameObject.Find("World").GetComponent<World>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) { unlockCursor(); }

		if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) 
		     || Input.GetMouseButtonDown(2)) && !w.paused) { lockCursor(); }

		if (Screen.lockCursor) { transform.position = CaptureMouse (); }
	}

	public void unlockCursor() {
		Screen.showCursor = true;
		Screen.lockCursor = false;
	}

	public void lockCursor() {
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	private Vector3 CaptureMouse() {
		Vector3 pos = new Vector3 (transform.position.x + Input.GetAxis ("Mouse X") * sensibility, 
		                           transform.position.y + Input.GetAxis ("Mouse Y") * sensibility, 
		                           transform.position.z);

		Vector3 cur = Camera.main.WorldToScreenPoint (new Vector3(pos.x, pos.y, pos.z));
		Vector3 min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		
		if (cur.x < 0) { pos.x = min.x; }
		else if (cur.x > Screen.width) { pos.x = max.x; }
		
		if (cur.y < 0) { pos.y = min.y; }
		else if (cur.y > Screen.height) { pos.y = max.y; }

		return pos;
	}

	public Vector3 mousePosition () {
		return Camera.main.WorldToScreenPoint (new Vector3(transform.position.x, 
		                                                   transform.position.y, 
		                                                   transform.position.z));
	}

	///////////////////////////////
	/// 						///
	///		EVENTOS DE RATON	///
	/// 						///
	///////////////////////////////

	private void MouseEvent (GameObject go) {
		if (!w.paused) {
			if (go.renderer.bounds.min.x < transform.position.x && transform.position.x < go.renderer.bounds.max.x &&
			    go.renderer.bounds.min.y < transform.position.y && transform.position.y < go.renderer.bounds.max.y) {
				mouseOver = true;
				if (Input.GetMouseButtonDown(0)) { MouseDown = true; }
			} else { mouseOver = false; MouseDown = false; }
			if (Input.GetMouseButtonDown(0)) { click = true; }
			if (Input.GetMouseButtonUp(0)) { click = false; holdingButton = false; }

			if (click) { 
				timeToHold += 1;
				if (timeToHold > 36f) {
					click = false;
					timeToHold = 0;
					holdingButton = true;
				}
			}
			else { timeToHold = 0; }
		}

	}

	public bool OnMouseUp (GameObject go) {
		MouseEvent (go);
		return (mouseOver && Input.GetMouseButtonUp(0));
	}

	public bool OnMouseDrag (GameObject go) {
		MouseEvent (go);
		return (mouseOver && holdingButton);
	}

	public bool OnMouseOver (GameObject go) {
		MouseEvent (go);
		return (mouseOver && !holdingButton);
	}

	public bool OnMouseDown (GameObject go) {
		MouseEvent (go);
		return MouseDown;
	}

	public bool GetMouseButtonHold (GameObject go) {
		MouseEvent (go);
		return (!mouseOver && holdingButton);
	}

	public bool GetMouseButtonClickIn (GameObject go) {
		MouseEvent (go);
		return (mouseOver && click);
	}

	public bool GetMouseButtonClickOut (GameObject go) {
		MouseEvent (go);
		return (!mouseOver && click);
	}

}
