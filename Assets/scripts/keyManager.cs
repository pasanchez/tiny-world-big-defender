using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyManager : MonoBehaviour {

	GameManager gm;

	public GameObject player;
	public GameObject bullet;
	GameObjectPool bulletPool;
	public int bulletPoolSize;
	public float speed_slow;
	public float speed_fast;
	float speed;
	public float shotDelay;
	public float shotSpeed;
	float last_time;
	public AudioClip shot;
	public GameObject audioController;

	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		last_time = Time.time;
		//bulletPool = new GameObjectPool (bullet, bulletPoolSize);
		bulletPool = gameObject.AddComponent<GameObjectPool>();
		bulletPool.setupPool (bullet, bulletPoolSize);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("m")) {
			audioController.GetComponent<musicController> ().toggle ();
		}
		player.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;
		player.transform.GetChild (0).GetComponent<Collider2D> ().enabled = false;
		if (gm.gameMode == 0) {
			if (Input.GetKey ("space")) {
				speed = speed_fast;
			} else {
				speed = speed_slow;
			}

			if (Input.GetKey ("right"))
				player.GetComponent<PlayerManager> ().move (-speed);
			if (Input.GetKey ("left"))
				player.GetComponent<PlayerManager> ().move (speed);
			if (Input.GetKey ("space") && gm.shield > 0) {
				player.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = true;
				player.transform.GetChild (0).GetComponent<Collider2D> ().enabled = true;

			} else if (Input.GetKey ("up") && Time.time - last_time > shotDelay) {
				spawnBullet ();
				GetComponent<AudioSource> ().PlayOneShot (shot);
				last_time = Time.time;
			}
		} else if (gm.gameMode == 1) {
			if (Input.GetKey ("r")) {
				gm.setup ();
				gm.gameMode = 0;
			}
		} else if (gm.gameMode == 2) {
			if (Input.GetKey ("space")) {
				gm.setup ();
				gm.gameMode = 0;
			}
		}
			
	}

	void spawnBullet() {
		float angle = player.transform.rotation.eulerAngles.z;
		Vector3 offset = new Vector3 (-0.3f*Mathf.Sin(Mathf.Deg2Rad*angle),0.3f*Mathf.Cos(Mathf.Deg2Rad*angle),0);
		Vector3 position = new Vector3 (player.transform.position.x + offset.x, player.transform.position.y 
			+ offset.y, player.transform.position.z + offset.z);
		GameObject bulletGO = bulletPool.spawn(position, player.transform.rotation);
		bulletGO.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-shotSpeed*Mathf.Sin(Mathf.Deg2Rad*angle),
			shotSpeed*Mathf.Cos(Mathf.Deg2Rad*angle)));
	}
}
