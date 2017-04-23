using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldColliderDectector : MonoBehaviour {

	GameManager gm;
	public AudioClip hit;

	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
//		print ("Earth hit!");
		gm.population -= 50;
		GetComponent<AudioSource> ().PlayOneShot (hit);

	}
}
