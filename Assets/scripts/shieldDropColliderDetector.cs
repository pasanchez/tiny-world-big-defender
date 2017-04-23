using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldDropColliderDetector : MonoBehaviour {


	public float alive;
	public AudioClip clip;

	float startTime;
	GameManager gm;
	bool visible;
	float lastTime;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		startTime = Time.time;
		visible = true;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > alive) {
			Destroy (gameObject);
		}
		if (Time.time - startTime > alive*0.7) {
			if (Time.time - lastTime > 0.3) {
				lastTime = Time.time;
				visible = !visible;
			} 
		}
		GetComponent<SpriteRenderer> ().enabled = visible;
	}

	void OnTriggerEnter2D(Collider2D other) {
		gm.shield += 30;
		if (gm.shield > 100)
			gm.shield = 100;
		GetComponent<AudioSource> ().PlayOneShot (clip);
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<PolygonCollider2D> ().enabled = false;
		Destroy (gameObject,clip.length);

	}

}
