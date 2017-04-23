using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	float last_time;

	public GameObject heart;
	public GameObject shield;
	public float shotDelay;
	public float shotSpeed;
	bool canShoot;
	GameManager gm;

	GameObjectPool bulletPool;
	public GameObject bulletPoolObject;
	public AudioClip destroyed;
	public AudioClip shot;

	// Use this for initialization
	void Start () {
		canShoot = true;
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		last_time = 0;
		bulletPool = bulletPoolObject.GetComponent<GameObjectPool> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - last_time > shotDelay && gm.gameMode == 0 && canShoot) {
			//shoot!
			Vector3 f = (Vector3.zero - this.transform.position).normalized;
			float angle = Mathf.Atan2 (f.y, f.x);
			GameObject bullet = bulletPool.spawn(this.transform.position, Quaternion.Euler(0,0,90+angle*Mathf.Rad2Deg));
			bullet.GetComponent<Rigidbody2D> ().AddForce (shotSpeed * f);
			GetComponent<AudioSource> ().PlayOneShot(shot);
			last_time = Time.time;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		GetComponent<AudioSource> ().PlayOneShot(destroyed);
		gm.points += 1;

		if (Random.value < 0.1f) {
			Instantiate (heart, this.transform.position, this.transform.rotation);
		} else	if (Random.value < 0.1f){
			Instantiate (shield, this.transform.position, this.transform.rotation);
		}
		gm.enemies.Remove (gameObject);
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<PolygonCollider2D> ().enabled = false;
		Destroy (gameObject,destroyed.length);
		canShoot = false;

	}
}
