using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldColliderDetector : MonoBehaviour {

	GameManager gm;
	float last_time;
	public float recharge_time;

	void Start () {
		last_time = 0;
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}

	void Update () {
		if (Time.time - last_time > recharge_time && gm.shield < 100) {
			last_time = Time.time;
			gm.shield += 1;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		gm.shield -= 10;
	}
}
