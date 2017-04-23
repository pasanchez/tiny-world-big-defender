using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour {

	bool playing;

	void Start () {
		playing = true;
	}
	
	void Update () {
		
	}

	public void toggle() {
		GetComponent<AudioSource> ().mute = playing;
		playing = !playing;
	}
}
