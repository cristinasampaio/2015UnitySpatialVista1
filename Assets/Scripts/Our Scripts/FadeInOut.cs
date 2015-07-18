using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {

	//Change is size of alpha increase for transperancy
	private float change = 0.1f;
	private float time = 0.0f;
	private Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent<MeshRenderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void FadeIn() {
		time += Time.deltaTime;
		if (time > 0.08f && mat.color.a > 0f) {
			Color newColor = mat.color;
			newColor.a -= change;
			mat.color = newColor;
			time = 0.0f;
		}
	}

	public void FadeOut() {
		time += Time.deltaTime;
		if (time > 0.08f && mat.color.a < 1.0f) {
			Color newColor = mat.color;
			newColor.a += change;
			mat.color = newColor;
			time = 0.0f;
		}
	}
}
