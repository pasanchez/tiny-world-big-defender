using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	float angle;
	GameManager gm;
	public AudioClip hit;


	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		angle = this.transform.rotation.eulerAngles.z;
		//print (angle);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void setPosition(float angle) {
		this.transform.position =  new Vector3(-0.6f * Mathf.Sin (angle),
									 0.6f * Mathf.Cos (angle),
									 0f);

		this.transform.rotation = Quaternion.Euler(new Vector3 (0, 0, Mathf.Rad2Deg * angle));
	}

	public void move(float speed) {
		angle += speed;
		setPosition (angle);
	}

	void OnTriggerEnter2D(Collider2D other) {
		gm.lp -= 10;
		GetComponent<AudioSource> ().PlayOneShot (hit);
	}
}
