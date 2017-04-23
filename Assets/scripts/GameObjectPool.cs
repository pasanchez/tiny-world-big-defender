using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool: MonoBehaviour {

	GameObject prefab;
	GameObject[] pool;
	int size;
	int pointer;


	public void setupPool(GameObject go, int size_) {
		this.prefab = go;
		this.size = size_;
		this.pool = new GameObject[this.size];
		this.pointer = 0;
	}

	public GameObject spawn(Vector3 position, Quaternion rotation) {
		//print (pointer); 
		if (this.pointer < this.size) {
			// Whe should create a  new object
			pool[pointer] = (GameObject) Instantiate(prefab, position, rotation);
			pointer++;
			return pool [pointer-1];
		} else {
			// use the oldest one
			GameObject oldest = pool[0];
			for (int i = 1; i < pool.Length; i++) {
				pool [i - 1] = pool [i];
			}
			oldest.transform.position = position;
			oldest.transform.rotation = rotation;
			oldest.SetActive (true);
			oldest.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			pool [pool.Length-1] = oldest;
			return oldest;
		}
	
	}

}
