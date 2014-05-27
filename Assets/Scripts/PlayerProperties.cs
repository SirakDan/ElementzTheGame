using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

	private int fire = 0;
	private int stone = 1;
	private int water = 2;
	private int air = 3;
	// Use this for initialization

	public int FIRE
	{
		get
		{
			return fire;
		}
	}

	public int STONE 
	{
		get 
		{
			return stone;
		}
	}

	public int WATER
	{
		get
		{
			return water;
		}
	}

	public int AIR 
	{
		get 
		{
			return air;
		}
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
