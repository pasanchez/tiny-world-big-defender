using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartDropColliderDetector : MonoBehaviour {

	public float alive;
	public AudioClip clip;

	float startTime;
	float lastTime;
	GameManager gm;
	bool visible;

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
		gm.lp += 30;
		if (gm.lp > 100)
			gm.lp = 100;
		GetComponent<AudioSource> ().PlayOneShot (clip);
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<PolygonCollider2D> ().enabled = false;
		Destroy (gameObject,clip.length);
	}


}
