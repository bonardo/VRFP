using UnityEngine.UI;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class controller : MonoBehaviour {
	public Text Score;
	public Text highScore;
	public GameObject howToPlay;
	private float startTime;
	private float a;
	private bool walking = false;
	private bool brain = true;
	private Vector3 spawnPoint;
	// Use this for initialization

	void Start () {
		spawnPoint = transform.localPosition;
		startTime = Time.time;
		highScore.text = PlayerPrefs.GetFloat ("HighScore", 0).ToString ();
		howToPlay.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
	//count++;
		if (brain == false)
			return;
		else {
			if (howToPlay.activeInHierarchy == false) {
				Time.timeScale = 1f;
				float t = Time.time - startTime; 
				Score.text = t.ToString ("f2") + " detik";
				if (walking == true && brain == true) {
					transform.position = transform.position + Camera.main.transform.forward * .5f * Time.deltaTime;
					//transform.position = transform.position + Player.transform.forward * .5f * Time.deltaTime; 
				}

				if (transform.position.y < -10f) {
					transform.position = spawnPoint;
				}

				Ray ray = Camera.main.ViewportPointToRay (new Vector3 (.5f, .5f, 0));
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.name.Contains ("Plane")) {
						walking = false;
						//	Debug.Log ("Walking False");
					} else {
						walking = true;
						//	Debug.Log ("Walking True");
					}
				}

				if (t < PlayerPrefs.GetFloat ("HighScore")) {
					PlayerPrefs.SetFloat ("HighScore", t);
				}
			} else {
				a = 0f;
				Time.timeScale = a;
			}
				
		}
	}
	void OnCollisionEnter(Collision col){
		if(col.gameObject.name == "BLOCK")
		{
			Debug.Log ("YOU WIN");
			brain = false;
			Score.color = Color.yellow;
			Destroy (col.gameObject);
		}
	}

	public void Reset() {
		transform.localPosition = spawnPoint;
		Debug.Log ("PRESSED");
	}
}
