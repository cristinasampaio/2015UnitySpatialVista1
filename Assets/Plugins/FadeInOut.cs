using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {
	
	//Change is size of alpha increase for transperancy
	private float change = 0.1f;
	private float time = 0.0f;
	private Material mat;
	private int fade = 1;
	
	// Use this for initialization
	void Start () {
		mat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if (fade == 1) 
		{
			fadeInFunc();
		}

		if (fade == 2) 
		{
			fadeOutFunc();
		}

	}
	
	public void FadeIn() {
		fade = 1;
	}
	
	public void FadeOut() {
		fade = 2;
	}

	private void fadeInFunc()
	{
		time += Time.deltaTime;
		if (time > 0.08f && mat.color.a > 0f) {
			Color newColor = mat.color;
			newColor.a -= change;
			mat.color = newColor;
			time = 0.0f;
		} 
		else 
		{
			//fade = 0;
		}
	}

	private void fadeOutFunc()
	{
		time += Time.deltaTime;
		if (time > 0.08f && mat.color.a < 1.0f) {
			Color newColor = mat.color;
			newColor.a += change;
			mat.color = newColor;
			time = 0.0f;
		}
		else
		{
			//fade = 0;
		}
	}
}
