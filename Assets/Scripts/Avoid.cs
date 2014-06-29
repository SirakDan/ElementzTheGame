using UnityEngine;
using System.Collections;

public class Avoid : MonoBehaviour
{
	public Transform GroundCheck;
	public bool grounded;
	//private Vector3 fwd;	
	private float groundRadius;
	public LayerMask whatToJump;
	public int jumpForce = 1000;
	public int timeToJump = 12;
	private int timerToJump = 0;
	// Use this for initialization
	
	void Start ()
	{
		timerToJump = 0;
		groundRadius = 0.1f;
	}
	
	void FixedUpdate()
	{
		timerToJump++;
		if (timerToJump > timeToJump) {
			timerToJump=0;
			grounded = Physics2D.OverlapCircle (GroundCheck.position, groundRadius, whatToJump);
			//fwd = transform.TransformDirection (Vector3.forward);
			bool hit = Physics2D.Raycast (transform.position, new Vector2 (1, 0), 2, whatToJump);
			bool hit2 = Physics2D.Raycast (transform.position, new Vector2 (-1, 0), 2, whatToJump);
			Color color = hit ? Color.green : Color.red;
			Debug.DrawRay (transform.position, new Vector2 (-1, 0), color);
			Color color2 = hit2 ? Color.green : Color.red;
			Debug.DrawRay (transform.position, new Vector2 (-1, 0), color2);
			if (grounded && (hit || hit2)) {
				rigidbody2D.AddForce (Vector2.up * jumpForce);
			}
		}
	}
	// Update is called once per frame
	void Update ()
	{
		
	}
}

