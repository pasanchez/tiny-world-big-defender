using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnship : MonoBehaviour {

	float counter;
	public float waitTime;
	public GameObject ship;
	public AudioClip spawn;
	GameManager gm;
	// Use this for initialization
	void Start () {
		counter = Time.time;
		GetComponent<AudioSource> ().PlayOneShot (spawn);
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - counter > waitTime) {
			GameObject go = Instantiate (ship, this.transform.position, this.transform.rotation);
			go.gameObject.GetComponent<EnemyManager> ().bulletPoolObject = gm.evilBulletPoolObject;
			gm.enemies.Add (go);
			gm.enemies.Remove (gameObject);
			Destroy (gameObject);
		}
	}
}
