using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	Text points_text;
	Text population_text;
	Text lost_message;
	help_enabler help_panel;
	Slider lp_slider;
	Slider shield_slider;
	public int shield;
	public int population;
	public int lp;
	public int points;
	public float numEnemies_Default;
	float numEnemies;
	public int maxClusterSize_default;
	int maxClusterSize;
	public GameObject enemy;
	public GameObject evil_ammo;
	public int evilbulletPoolSize;
	float lastTime;
	public float spawnTime_default;
	float spawnTime;
	public GameObject evilBulletPoolObject;
	public ArrayList enemies;
	public int gameMode;
	public AudioClip lost;


	void Start () {
		enemies = new ArrayList ();
		setup ();
		gameMode = 2;
	}


	 public void setup() {
		foreach (GameObject go in enemies) {
			Destroy (go);
		}
		shield = 100;
		lp = 100;
		population = 10000;
		points = 0;
		help_panel = GameObject.Find ("Canvas").transform.GetChild(5).GetComponent<help_enabler> ();
		points_text = GameObject.Find ("Canvas").transform.GetChild(0).GetComponent<Text> ();
		lost_message = GameObject.Find ("Canvas").transform.GetChild(4).GetComponent<Text> ();
		lp_slider = GameObject.Find ("Canvas").transform.GetChild(2).GetComponent<Slider> ();
		this.shield_slider = GameObject.Find ("Canvas").transform.GetChild(3).GetComponent<Slider> ();
		population_text = GameObject.Find ("Canvas").transform.GetChild(1).GetComponent<Text> ();
		points_text.text = "POINTS:  " + points;
		//lastTime = Time.time;
		evilBulletPoolObject.GetComponent<GameObjectPool> ().setupPool (evil_ammo, evilbulletPoolSize);
	}

	
	// Update is called once per frame
	void Update () {
		checkIfLost ();
		changeDificulty ();
		if (gameMode == 0) {
			help_panel.enable(false);
			lost_message.enabled = false;
			lp_slider.value = lp;
			this.shield_slider.value = shield;
			points_text.text = "POINTS:  " + points;
			population_text.text = "POPULATION:  " + population + "M";
			//GameObject[] ships = GameObject.FindGameObjectsWithTag ("SPACESHIP");
			if (Time.time - lastTime > spawnTime && enemies.Count < numEnemies) {
				int clusterSize = (int)Mathf.Ceil (Random.value * maxClusterSize);
				for (int i = 0; i < clusterSize; i++) {
					float x = Random.value;
					float y = Random.value;
					if (x > 0.35f && x < 0.65f && y > 0.35f && y < 0.65f)
						x += 0.35f;
					Vector3 position = new Vector3 (x, y, 10f);
					GameObject ship = Instantiate (enemy, Camera.main.ViewportToWorldPoint (position), Quaternion.identity);
					enemies.Add (ship);
				}
				lastTime = Time.time; 
			} 
		} else if (gameMode == 1) {
			help_panel.enable(false);
			lost_message.enabled = true;
		} else if (gameMode == 2) {
			help_panel.enable(true);
		}
	}

	void checkIfLost() {
		if ((lp <= 0 || population <= 0) && gameMode!=1) {
			gameMode = 1;
			GetComponent<AudioSource> ().PlayOneShot (lost);
		}
	}

	void changeDificulty() {
		numEnemies = numEnemies_Default + (float) Mathf.Floor(points / 10);
		maxClusterSize = maxClusterSize_default +  (int) Mathf.Floor(points / 20);
		spawnTime = spawnTime_default - (float) Mathf.Floor(points / 30) * 0.5f;
		if (spawnTime < 1)
			spawnTime = 1;
	}
}
