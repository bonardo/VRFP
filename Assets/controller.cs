using UnityEngine.UI;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class controller : MonoBehaviour {
	public Text Score;
	public Text highScore;
	public GameObject howToPlay;
	public GameObject howToPley;
	public GameObject PopUp;
	private float startTime=30;
	private bool walking = false;
	private bool brain = true;
	private Vector3 spawnPoint;
	public static controller instance = null;
	// Use this for initialization

	void Start () {
		spawnPoint = transform.localPosition;
		//startTime = Time.time;
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
				startTime -= Time.deltaTime;
//				float t = Time.time - startTime;
				Score.text = startTime.ToString ("f2") + " detik";
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

				if (startTime < PlayerPrefs.GetFloat ("HighScore")) {
					PlayerPrefs.SetFloat ("HighScore", startTime);
				}
				if(startTime <= 0)
				{
					Gameovers();
				}
			} else {
				Time.timeScale = 0f;
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
		//Tidakterpakai
		else if(col.gameObject.name == "Diamond")
		{
			Debug.Log ("KENA");
			col.gameObject.GetComponent<Renderer> ().enabled = false;
			col.gameObject.GetComponent<Collider> ().enabled = false;
			Destroy (col.gameObject);
			startTime += 5f;
		}
	}

	void Awake(){
		if (instance == null) {
			instance = this;
		}
		else if (instance != null){
			Destroy(gameObject);
		}
	}

	void Gameovers(){
		Time.timeScale = 0f;
		PopUp.SetActive(true);
		brain = false;
	}

	public void Reseto(){
		Debug.Log("s");
	}

	public void MoveScenes(string sceneName){
		Application.LoadLevel (sceneName);
	}


	public void Collecta(GameObject passedObject){
		passedObject.GetComponent<Renderer> ().enabled = false;
		passedObject.GetComponent<Collider> ().enabled = false;
		startTime += 5f;
		Destroy(passedObject,1.0f);
	}

}
