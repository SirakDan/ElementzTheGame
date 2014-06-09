using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	public GameObject cloud1;
	public GameObject cloud2;
	public GameObject cloud3;

	private float cloud1Speed;
	private float cloud2Speed;
	private float cloud3Speed;


	// Use this for initialization
	void Start () {
		startInRandomPosition(cloud1);
		startInRandomPosition(cloud2);
		startInRandomPosition(cloud3);
		cloud1Speed = Random.Range(0.01f, 0.03f);
		cloud2Speed = Random.Range(0.01f, 0.03f);
		cloud3Speed = Random.Range(0.01f, 0.03f);
	}
	
	// Update is called once per frame
	void Update () {

		if(cloud1.transform.position.x <= -15.8f){
			startInRandomPosition(cloud1);
			cloud1Speed = Random.Range(0.01f, 0.03f);
		}else{
			cloud1.transform.position = new Vector3(cloud1.transform.position.x - cloud1Speed, cloud1.transform.position.y, cloud1.transform.position.z);
		}

		if(cloud2.transform.position.x <= -15.8f){
			startInRandomPosition(cloud2);
			cloud2Speed = Random.Range(0.01f, 0.03f);
		}else{
			cloud2.transform.position = new Vector3(cloud2.transform.position.x - cloud2Speed, cloud2.transform.position.y, cloud2.transform.position.z);
		}

		if(cloud3.transform.position.x <= -15.8f){
			startInRandomPosition(cloud3);
			cloud3Speed = Random.Range(0.01f, 0.03f);
		}else{
			cloud3.transform.position = new Vector3(cloud3.transform.position.x - cloud3Speed, cloud1.transform.position.y, cloud3.transform.position.z);
		}

	}

	private void startInRandomPosition(GameObject cloud){
		float random = Random.Range (1.1f, 5.5f);
		cloud.transform.position = new Vector3(16, random, -1);
	}

}
