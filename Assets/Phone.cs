using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {
	public AudioClip phone;
	AudioSource source;
	// Use this for initialization
	void Start () {
		source = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<AudioSource>().isPlaying == true) {
			GetComponent<AudioSource>().Stop();
		}
	}
	public void ring(){
		GetComponent<AudioSource>().clip = phone;
		GetComponent<AudioSource>().Play ();
	}
}
