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
		if (audio.isPlaying == true) {
			audio.Stop();
		}
	}
	public void ring(){
		audio.clip = phone;
		audio.Play ();
	}
}
