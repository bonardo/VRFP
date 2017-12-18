using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	private float rotateSpeed = 85;
	private controller conts;
	// Use this for initialization

	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
	}

	void OnTriggerEnter(){
		controller.instance.Collecta (gameObject);
		AudioSource source = GetComponent<AudioSource>();
		source.Play();
		Debug.Log("Play Music");
	}
}
